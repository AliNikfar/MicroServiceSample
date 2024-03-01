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
                new Order{
                    FirstName="ali",
                    LastName = "Nikfar" ,
                    UserName = "aliNikfar" ,
                    PaymentMethod = 1,
                    EmailAddress="ali.nikfar2000@gmail.com" ,
                    City="tehran" ,
                    BankName = "saderat",
                    Country = "Iran",
                    ZipCode = "1234567890",
                    TotalPrice = 10000}
            };
        }
    }
}
