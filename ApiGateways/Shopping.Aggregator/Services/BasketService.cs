using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class BasketService : IBasketService
    {
        public Task<CatalogModel> GetBasket(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
