using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System.Net;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("Api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingController(IOrderService orderService, IBasketService basketService, ICatalogService catalogService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _catalogService = catalogService;
        }
        [HttpGet("{userName}" , Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
             var basket = await _basketService.GetBasket(userName);
            if (basket is not null)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _catalogService.GetCatalog(item.ProductId);
                    item.ProductName = product.Name;
                    item.Category = product.Category;
                    item.Description = product.Description;
                    item.Summry = product.Summry;
                    item.ImageFile = product.ImageFile;

                }

                var orders = await _orderService.GetOrderByUserName(userName);
                var shoppingModel = new ShoppingModel
                {
                    UserName = userName,
                    BasketWithProduct = basket,
                    Orders = orders
                };


                return Ok(shoppingModel);
            }
            else
                return NotFound();
        }
    }
}
