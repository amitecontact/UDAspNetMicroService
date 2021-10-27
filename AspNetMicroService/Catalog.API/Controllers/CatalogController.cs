using Catalog.API.Entities;
using Catalog.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private ILogger _Logger;
        private IProductRepository _ProductRepository;

        public CatalogController(ILogger<CatalogController> logger, IProductRepository productRepository)
        {
            _Logger = logger;
            _ProductRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _ProductRepository.GetProducts());
        }

        [HttpGet("{Id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string Id)
        {
            var product = await _ProductRepository.GetProductById(Id);

            if (product == null)
            {
                _Logger.LogError($"Product with id : {Id}, not found ");
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/{category:minlength(5)}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {   
            return Ok(await _ProductRepository.GetProductByCategroy(category));
        }

        [Route("[action]/{name:minlength(3)}", Name = "GetProductByName")]
        [HttpGet]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            var product = await _ProductRepository.GetProductByName(name);

            if (product == null)
            {
                _Logger.LogError($"Product with name : {name}, not found");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _ProductRepository.AddProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id}, product);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
           return Ok(await _ProductRepository.UpdateProduct(product));

        }

        [HttpDelete("{id}", Name ="DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct(string Id)
        {
            return Ok(await _ProductRepository.DeleteProduct(Id));

        }

    }
}
