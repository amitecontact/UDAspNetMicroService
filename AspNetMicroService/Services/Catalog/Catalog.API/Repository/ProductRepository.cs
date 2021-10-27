using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repository
{
    public class ProductRepository : IProductRepository
    {

        private ICatalogContext _CatalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _CatalogContext = catalogContext;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _CatalogContext
                        .Products
                        .Find(p => true)
                        .ToListAsync();
        }
        public async Task<Product> GetProductById(string Id)
        {
            return await _CatalogContext
                        .Products
                        .Find(p => p.Id == Id)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.ProductName, name); 

            return await _CatalogContext
                        .Products
                        .Find(filter)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategroy(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);

            return await _CatalogContext
                        .Products
                        .Find(filter)
                        .ToListAsync();
        }

        public async Task AddProduct(Product product)
        {
            await _CatalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateStatus = await _CatalogContext.Products.ReplaceOneAsync(p => p.Id == product.Id, product);

            return updateStatus.IsAcknowledged && updateStatus.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            var updateStatus = await _CatalogContext.Products.DeleteOneAsync(filter);

            return updateStatus.IsAcknowledged && updateStatus.DeletedCount > 0;
        }
    }
}
