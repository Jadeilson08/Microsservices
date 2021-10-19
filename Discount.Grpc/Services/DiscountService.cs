using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository repository;
        private readonly IMapper mapper;

        public DiscountService(IDiscountRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await repository.GetDiscount(request.ProductName);
            
            if(coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount Product {request.ProductName} Not Found"));
            
            var couponModel = mapper.Map<CouponModel>(coupon);
            
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = mapper.Map<Coupon>(request.Coupon);

            var isSaved = await repository.CreateDiscount(coupon);

            if (!isSaved)
                throw new RpcException(new Status(StatusCode.Internal, $"Discount Product {coupon.ProductName} Not Found"));

            return mapper.Map<CouponModel>(coupon);
        }
    }
}
