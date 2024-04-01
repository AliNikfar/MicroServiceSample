using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public interface IBasketService
    {
        Task<CatalogModel> GetBasket(string userName);
    }
}
