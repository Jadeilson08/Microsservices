using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool hasProducts = productCollection.Find(x => true).Any();

            if (!hasProducts)
            {
                productCollection.InsertManyAsync(GetPreConfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreConfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Notebook",
                    Description = "foo",
                    Image = "product-1.png",
                    Category = "tecnologia",
                    Price = 3100.00M
                },
                new Product()
                {
                    Id = "602d2149e483f2a3990b47f5",
                    Name = "Notebook",
                    Description = "foo",
                    Image = "product-1.png",
                    Category = "tecnologia",
                    Price = 3100.00M
                },
                new Product()
                {
                    Id = "970d2149e773f2a3990b47f5",
                    Name = "Notebook",
                    Description = "foo",
                    Image = "product-1.png",
                    Category = "tecnologia",
                    Price = 3100.00M
                },
            };
        }
    }
}
