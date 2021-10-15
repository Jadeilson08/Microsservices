using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
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

        public async Task<Coupon> GetDiscount(string name)
        {
            var connection = GetConnection();

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                    ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = name });

            if (coupon is null)
                return new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var connection = GetConnection();

            var affected = await connection.ExecuteAsync(
                "INSERT INTO Coupon (ProductName, Description, Amount)" +
                "VALUES (@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            return Convert.ToBoolean(affected);
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var connection = GetConnection();

            var affected = await connection.ExecuteAsync(
                "UPDATE Coupon " +
                "SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount,
                    Id = coupon.Id
                });

            return Convert.ToBoolean(affected);
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var connection = GetConnection();

            var affected = await connection.ExecuteAsync(
                "DELETE FROM Coupon " +
                "WHERE ProductName = @ProductName",
                new { productName });

            return Convert.ToBoolean(affected);
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionStrings"));
        }
    }
}
