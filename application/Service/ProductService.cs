using application.Entities;
using application.Infra;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace application.Service
{
    /// <summary>
    /// Example used File .env
    /// </summary>
    public class ProductService
    {
        private readonly string _host = EnvConfig.MongoDBHost();
        private readonly IMongoCollection<Product> _prodCollection;

        public ProductService()
        {
            string[] paramConfig = _host.Split(";");
            var ConnectionString = paramConfig[0];
            var DatabaseName = paramConfig[1];
            var ProdsCollectionName = paramConfig[2];

            var mongoClient = new MongoClient(ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(DatabaseName);
            _prodCollection = mongoDatabase.GetCollection<Product>(ProdsCollectionName);
        }

        public async Task<List<Product>> GetAsync() =>
        await _prodCollection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetAsync(string id) =>
            await _prodCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product newProd) =>
            await _prodCollection.InsertOneAsync(newProd);

        public async Task UpdateAsync(string id, Product updatedProd) =>
            await _prodCollection.ReplaceOneAsync(x => x.Id == id, updatedProd);

        public async Task RemoveAsync(string id) =>
            await _prodCollection.DeleteOneAsync(x => x.Id == id);


    }
}
