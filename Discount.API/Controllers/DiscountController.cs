using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository repository;

        public DiscountController(IDiscountRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{product}")]
        public async Task<IActionResult> Get(string product)
        {
            return Ok(await repository.GetDiscount(product));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Coupon coupon)
        {
            return Ok(await repository.CreateDiscount(coupon));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Coupon coupon)
        {
            return Ok(await repository.UpdateDiscount(coupon));
        }
        
        [HttpDelete("{product}")]
        public async Task<IActionResult> Delete(string product)
        {
            return Ok(await repository.DeleteDiscount(product));
        }
    }
}
