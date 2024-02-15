using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistance;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInfrastructureServicees(this IServiceCollection services , IConfiguration configuration)
        {
            //Add Configurations if Needs
            services.AddDbContext<OrderContext>( options=>
            {
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
            });
            services.AddScoped ( typeof(IAsyncRepository<>),typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEmailService, EmailService>(); 

            return services;
        }
    }
}
