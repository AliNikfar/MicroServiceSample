using Discount.Api.Entities;
using Npgsql;
using Dapper;
namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public  DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  async Task<bool> CreateDiscount(Coupone coupone)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("insert into coupone(ProductName,Description,Amount) values" +
                " (@ProductName,@Description,@Amount)", new { ProductName = coupone.ProductName, Description = coupone.Description, Amount = coupone.Amount });
            if(affected == 0)
            {
                return false;
            }
            return true;

        }

        public async Task<bool> DeleteeDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("delete from coupone  where ProductName = @ProductName ",new { ProductName=productName });
            if (affected == 0)
            {
                return false;
            }
            return true;

        }

        public async Task<Coupone> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
            var coupone = await connection.QueryFirstOrDefaultAsync<Coupone>("select  * from coupone where productName = @ProductName", new { ProductName = productName});
            if(coupone == null)
            {
                return new Coupone{ ProductName = "No Discount" , Amount = 0 , Description = "No Discount"  };
            }
            return coupone;
        }

        public async Task<bool> UpdateDiscount(Coupone coupone)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("update coupone set ProductName=@ProductName , Amount=@Amount , Description=@Description where id = @Id ", coupone);
            if (affected == 0)
            {
                return false;
            }
            return true;
        }
    }
}
