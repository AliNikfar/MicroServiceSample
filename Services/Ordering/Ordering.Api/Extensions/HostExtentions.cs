using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Api.Extensions
{
    public static class HostExtentions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host 
            , Action<TContext,IServiceProvider> Seeder,int? retry=0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var Services = scope.ServiceProvider;
                var logger = Services.GetRequiredService<ILogger<TContext>>();
                var context = Services.GetService<TContext>();
                try
                    {
                    logger.LogInformation("Migrating started for SqlServer");
                    context.Database.Migrate();
                    // Pretty much easy to seed Database then.
                    Seeder(context, Services);
                    logger.LogInformation("Migrating Has been Done SqlServer");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            }
            return host;
        }
    }
}
