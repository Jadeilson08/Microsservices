using System.Threading.Tasks;
using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient client;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient client)
        {
            this.client = client;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var dicountRequest = new GetDiscountRequest { ProductName = productName };

            return await client.GetDiscountAsync(dicountRequest);
        }
    }
}
