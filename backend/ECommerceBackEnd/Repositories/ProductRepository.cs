using ECommerceBackEnd.Entities;
using Microsoft.AspNetCore.Http.Features;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerceBackEnd.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> productsCollection;
        private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;
        private const string databaseName = "ie104";
        private const string collectionName = "products";
        public ProductRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            productsCollection = database.GetCollection<Product>(collectionName);
        }

        public void CreateProduct(Product product)
        {
            productsCollection.InsertOne(product);
        }
        public IEnumerable<Product> GetProducts()
        {
            return productsCollection.Find(new BsonDocument()).ToList();
        }
        public Product GetProduct(string id)
        {
            var filter = filterBuilder.Eq(product => product.ProductId, id);
            return productsCollection.Find(filter).SingleOrDefault();

        }
        
        public void DeleteProduct(string id)
        {
            var filter = filterBuilder.Eq(product => product.ProductId, id);
            productsCollection.DeleteOne(filter);
        }
        public void UpdateProduct(Product product)
        {
            var filter = filterBuilder.Eq(existingProduct => existingProduct.ProductId, product.ProductId);
            productsCollection.ReplaceOne(filter, product);
        }
    }
}
