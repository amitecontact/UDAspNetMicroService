using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration appConfiguration)
        {
            var client = new MongoClient(appConfiguration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(appConfiguration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(appConfiguration.GetValue<string>("DatabaseSettings:CollectionName"));

            CatalogContextSeed.SeedContext(Products);
        }

        public IMongoCollection<Product> Products { get;  }
    }
}
