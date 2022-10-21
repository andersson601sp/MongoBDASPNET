using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using application.Entities;
using application.Infra;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace application.Service
{
    /// <summary>
    /// Example used appsettings
    /// </summary>
    public class ProductStoreApi
    {
        private readonly IMongoCollection<Product> _prodsCollection;

        public ProductStoreApi(IOptions<ProductStoreDatabaseSettings> prodtoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            prodtoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                prodtoreDatabaseSettings.Value.DatabaseName);

            _prodsCollection = mongoDatabase.GetCollection<Product>(
                prodtoreDatabaseSettings.Value.ProdsCollectionName);
        }

        public async Task<List<Product>> GetAsync() =>
    await _prodsCollection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetAsync(string id) =>
            await _prodsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product newProd) =>
            await _prodsCollection.InsertOneAsync(newProd);

        public async Task UpdateAsync(string id, Product updatedProdk) =>
            await _prodsCollection.ReplaceOneAsync(x => x.Id == id, updatedProdk);

        public async Task RemoveAsync(string id) =>
            await _prodsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
