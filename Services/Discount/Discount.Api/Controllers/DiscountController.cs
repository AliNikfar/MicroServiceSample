using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        [HttpGet("{productName}",Name ="GetDiscount")]
        [ProducesResponseType(typeof(Coupone),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupone>> GetDiscount(string productName)
        {
            var coupon =await  _discountRepository.GetDiscount(productName);
            return Ok(coupon);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupone), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupone>> CreateDiscount([FromBody]Coupone coupone)
        {
            var result = await _discountRepository.CreateDiscount(coupone);
            return CreatedAtRoute("GetDiscount", new { ProductName = coupone.ProductName }, coupone);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupone>> DeleteDiscount( string productName)
        {
            return Ok(await _discountRepository.DeleteeDiscount(productName));
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupone>> UpdateDiscount([FromBody] Coupone coupone)
        {
            return Ok(await _discountRepository.UpdateDiscount(coupone));
        }
    }
}
