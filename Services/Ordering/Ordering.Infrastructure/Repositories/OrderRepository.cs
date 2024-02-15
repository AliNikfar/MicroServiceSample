

using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string UserName)
        {
            return await 
                _dbContext.Orders.Where(p => p.UserName == UserName).ToListAsync();
        }
    }
}
