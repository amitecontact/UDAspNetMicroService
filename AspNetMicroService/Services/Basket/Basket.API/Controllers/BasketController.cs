using Basket.API.Entities;
using Basket.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        private readonly IBasketRepository _BasketRepository;
        private readonly ILogger<BasketController> _Logger;

        public BasketController(ILogger<BasketController> logger, IBasketRepository basketRepository)
        {
            _BasketRepository = basketRepository;
            _Logger = logger;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        //[HttpGet]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var shoppingCart = await _BasketRepository.GetBasket(userName);

            return Ok(shoppingCart ?? new ShoppingCart(userName));
        }

        [HttpPost (Name = "AddBasket")]
        [HttpPut (Name = "UpdateBasket")]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCart>> AddBasket([FromBody] ShoppingCart shoppingCart)
        {
            var _shoppingCart = await _BasketRepository.UpdateBasket(shoppingCart);

            return Ok(_shoppingCart);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> RemoveBasket(string userName)
        {
            await _BasketRepository.RemoveBasket(userName);

            return Ok();
        }
    }
}
