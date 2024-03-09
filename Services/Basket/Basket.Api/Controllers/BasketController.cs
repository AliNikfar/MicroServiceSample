using AutoMapper;
using Basket.Api.Entities;
using Basket.Api.GrpsServices;
using Basket.Api.Repositories;
using EventBus.Messages.Events;
using MassTransit;
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
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketRepository basketRepositor,DiscountGrpcService discountService , IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _basketRepositor = basketRepositor;
           _discountService = discountService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _basketRepositor.GetUserBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            foreach (ShoppingCartItem crnt in basket.Items)
            {
                var coupone = await _discountService.GetDiscount(crnt.ProductName);
                crnt.Price -= coupone.Amount;
            }

            return Ok(await _basketRepositor.UpdateBasket(basket));
        }
        [HttpDelete("{UserName}",Name = "DeleteBasket")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketRepositor.DeleteBasket(userName);
            return Ok();
        }

        #region Checkout
        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get existed basket with total price
            var basket = await _basketRepositor.GetUserBasket(basketCheckout.UserName);
            if (basket is null)
                return BadRequest();

            // create basketcheckoutEvent -- set TotalPrice on BacketCheckout Event Message
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            // send checkout event to rabbitMQ
            await _publishEndpoint.Publish(eventMessage);

            //Remove Basket
            _basketRepositor.DeleteBasket(basketCheckout.UserName);

            return Accepted();
        }

        #endregion

    }
}
