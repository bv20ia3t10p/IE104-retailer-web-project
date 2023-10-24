using ECommerceBackEnd.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerceBackEnd.Repositories
{
    public class ProductsRepository:IProductsRepository
    {
        public int latestId = 0;
        private readonly IMongoCollection<Product> productsCollection;
        private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;
        private const string databaseName = "ie104";
        private const string collectionName = "products";
        public ProductsRepository(IMongoClient mongoClient) {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            productsCollection = database.GetCollection<Product>(collectionName);
            latestId = GetLatestId() + 1;
        }

        private int GetLatestId()
        {
            var sort = Builders<Product>.Sort.Descending("ProductId");
            return productsCollection.Find(bson => true)
                .SortBy(bson => bson.ProductCardId)
                .ThenByDescending(bson=>bson.ProductCardId)
                .FirstOrDefault().ProductCardId;
        }


        void IProductsRepository.CreateProduct(Product newProduct)
        {
            productsCollection.InsertOne(newProduct);
            this.latestId++;
        }
        void IProductsRepository.DeleteProduct(int PID)
        {
            var filter = filterBuilder.Eq(product => product.ProductCardId,PID );
            productsCollection.DeleteMany(filter);
        }

        Product IProductsRepository.GetProduct(int PID)
        {
            var filter = filterBuilder.Eq(product => product.ProductCardId, PID);
            return productsCollection.Find(filter).SingleOrDefault();
        }

        IEnumerable<Product> IProductsRepository.GetProducts()
        {
            return productsCollection.Find(new BsonDocument()).ToList();
        }

        void IProductsRepository.UpdateProduct(Product product)
        {
            var filter = filterBuilder.Eq(existingProduct => existingProduct.ProductCardId, product.ProductCardId);
            productsCollection.ReplaceOne(filter, product);
        }
        public IEnumerable<Product> GetProductByCategory(int id)
        {
            // var filter = filterBuilder.Eq("ProductCategoryId",id)
            // return;
            throw new NotImplementedException();
        }
    }
}
