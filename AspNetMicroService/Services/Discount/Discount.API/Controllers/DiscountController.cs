using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        private readonly IDiscountRepository _DiscountRepository;
        private readonly ILogger _Logger;

        public DiscountController(ILogger<DiscountController> logger, IDiscountRepository discountRepository)
        {
            _Logger = logger;
            _DiscountRepository = discountRepository;
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var discount = await _DiscountRepository.GetDiscount(productName);
            return Ok(discount);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCoupon([FromBody] Coupon coupon)
        {
            var isScuccess = await _DiscountRepository.CreateCoupon(coupon);

            if(isScuccess) return Ok();

            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCoupon([FromBody] Coupon coupon)
        {
            var isScuccess = await _DiscountRepository.UpdateCoupon(coupon);

            if (isScuccess) return Ok();

            return NotFound();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCoupon([FromBody] string productName)
        {
            var isScuccess = await _DiscountRepository.DeleteCoupon(productName);

            if (isScuccess) return Ok();

            return NotFound();
        }

    }
}
