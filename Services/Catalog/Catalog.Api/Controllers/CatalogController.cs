using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        #region DI
        private readonly IProductRepository _productRepo;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepo, ILogger<CatalogController> logger)
        {
            this._productRepo = productRepo;
            this._logger = logger;
        }
        #endregion


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var result = await _productRepo.GetProducts();
            return Ok(result);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var response = await _productRepo.GetProductById(id);
            if (response is not null)
            {
                return Ok(response);
            }
            else
            {
                _logger.LogError($"Product With Id:{id} not Found");
                return NotFound();
            }

        }
        [HttpGet("[action]/{category}")]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var response = await _productRepo.GetProductsByCategory(category);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepo.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id,product });
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {

            return Ok(await _productRepo.UpdateProduct(product));
        }


        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var response = await _productRepo.DeleteProduct(id);

            if (response)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
