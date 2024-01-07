using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Discount.Api.Extentions
{
    public static class HostExtensions
    {
        public static void MigrateDatabase(this IServiceProvider sp,int? retry= 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = sp.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = sp.GetRequiredService<ILoggerFactory>();
                try
                {
                    logger.CreateLogger("migrating postgreSql database ");

                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
                    connection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };
                    command.CommandText = "Drop Table If Exists CouponE";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE  coupone
                                            (
                                                id serial Primary Key,
                                                productname varchar(200) NOT NULL,
                                                description text,
                                                amount integer
                                            )";
                    command.ExecuteNonQuery();

                    command.CommandText = @" Insert into Coupone(productName,Description,Amount) values ('IPhone x','',150000) ,('samsung G1','bhfgfgjb dfgdf g',1800000) ";
                    command.ExecuteNonQuery();
                    logger.CreateLogger("Migration Has Been Completed!!!");
                }
                catch(NpgsqlException ex)
                {
                    logger.CreateLogger("an error as been occured");
                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase(sp, retryForAvailability);
                    }
                }


            }
        }
    }
}
