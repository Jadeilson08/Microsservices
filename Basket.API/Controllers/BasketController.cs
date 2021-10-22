using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository repository;
        private readonly DiscountGrpcService service;
        public BasketController(IBasketRepository repository, DiscountGrpcService service)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.service = service;
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(ShoppingCar), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string username)
        {
            var basket = await repository.GetBasket(username);
            
            return Ok(basket ?? new ShoppingCar(username));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCar), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] ShoppingCar basket)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await service.GetDiscount(item.ProductName);

                item.UpdatePrice(coupon.Amount);
            }

            return Ok(await repository.UpdateBasket(basket));
        }

        [HttpDelete("{username}")]
        [ProducesResponseType(typeof(ShoppingCar), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string username)
        {
            await repository.DeleteBasket(username);
            return Ok();
        }
    }
}
