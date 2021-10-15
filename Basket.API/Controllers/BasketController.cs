using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository repository;

        public BasketController(IBasketRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
