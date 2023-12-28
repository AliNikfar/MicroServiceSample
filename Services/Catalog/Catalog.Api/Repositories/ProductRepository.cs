using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region constractor
        private readonly  ICatalogContext _catalogContext;
        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        #endregion

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.Find(p=>true).ToListAsync();
        }
        public async Task<Product> GetProduct(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
                DeleteResult DeleteResult =await _catalogContext.Products.DeleteOneAsync(filter);
            return DeleteResult.IsAcknowledged && DeleteResult.DeletedCount > 0;
        }



        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            //using filter for find 
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name); 
            return await _catalogContext.Products.Find(filter).ToListAsync();
        }



        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            //using filter for find 
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _catalogContext.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
