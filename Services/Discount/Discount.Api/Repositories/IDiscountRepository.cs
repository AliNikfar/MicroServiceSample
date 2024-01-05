using Discount.Api.Entities;

namespace Discount.Api.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupone> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupone coupone);
        Task<bool> UpdateDiscount(Coupone coupone);
        Task<bool> DeleteeDiscount(string productName);
    }
}
