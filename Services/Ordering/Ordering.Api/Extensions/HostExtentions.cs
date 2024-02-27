using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Api.Extensions
{
    public static class HostExtentions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host 
            , Action<TContext,IServiceProvider> seeder,int? retry=0) where TContext : DbContext
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
                    InvokeSeeder(seeder, context, Services);
                    logger.LogInformation("Migrating Has been Done SqlServer");
                }
                catch (SqlException ex)
                {
                    logger?.LogError(ex, "an error occured while database migration");
                    if (retryForAvailability < 50)
                    {
                        retryForAvailability += 1;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailability);
                    }
                    throw;
                }
            }
            return host;
        }
        private static void InvokeSeeder<TContext>(Action<TContext,IServiceProvider> seeder
            , TContext context
            , IServiceProvider services)
            where TContext:DbContext
        {
            context.Database.Migrate();
            // Pretty much easy to seed Database then.
            seeder(context, services);

        }
    }
}
