using Basket.Api.Entities;
using Basket.Api.GrpsServices;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepositor;
        private readonly DiscountGrpcService _discountService;

        public BasketController(IBasketRepository basketRepositor,DiscountGrpcService discountService)
        {
            _basketRepositor = basketRepositor;
           _discountService = discountService;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _basketRepositor.GetUserBasket(userName);
            foreach(ShoppingCartItem crnt in basket.Items )
            {
                var coupone = await _discountService.GetDiscount(crnt.ProductName);
                crnt.Price -= coupone.Amount;
            }
            return Ok(basket ?? new ShoppingCart(userName));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            return Ok(await _basketRepositor.UpdateBasket(basket));
        }
        [HttpDelete("{UserName}",Name = "DeleteBasket")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketRepositor.DeleteBasket(userName);
            return Ok();
        }

    }
}
