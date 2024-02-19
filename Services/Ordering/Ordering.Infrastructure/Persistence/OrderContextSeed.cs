using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if(!await orderContext.Orders.AnyAsync())
            {
                await orderContext.Orders.AddRangeAsync(GetPreConfigureOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Data Seed Section Configured");
            }
        }

        public static IEnumerable<Order> GetPreConfigureOrders()
        {
            return new List<Order>
            {
                new Order{ Id = 1,
                    FirstName="ali",
                    LastName = "Nikfar" ,
                    EmailAddress="ali.nikfar2000@gmail.com" ,
                    City="tehran" ,
                    Country = "Iran",
                    TotalPrice = 10000}
            };
        }
    }
}
