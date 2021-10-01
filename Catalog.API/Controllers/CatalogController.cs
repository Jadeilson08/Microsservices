using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository repository;
        public CatalogController(IProductRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        { 
            var products = await repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(string id)
        {
            var product = await repository.GetProductById(id);
            
            if (product is null)
                return NoContent();
            
            return Ok(product);
        }

        [HttpGet("{category}/catagory")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByCategory(string category)
        {
            if (category is null)
                return BadRequest("Category invalid");

            var product = await repository.GetProductByCategory(category);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (product is null)
                return BadRequest("Ivalid product");

            await repository.CreateProduct(product);
            
            //return CreatedAtRoute("", new { id = product.Id }, product);

            return Created(nameof(Get), product.Id);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] Product product)
        {
            if (product is null)
                return BadRequest("Ivalid product");

            return Ok(await repository.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
                return BadRequest("Invalid Id");

            return Ok(await repository.DeleteProduct(id));
        }

    }
}
