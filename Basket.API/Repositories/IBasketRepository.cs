using Basket.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCar> GetBasket(string username);
        Task<ShoppingCar> UpdateBasket(ShoppingCar basket);
        Task DeleteBasket(string username);
    }
}
