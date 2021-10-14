using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public async Task<Coupon> GetDiscount(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            throw new NotImplementedException();
        }
    }
}
