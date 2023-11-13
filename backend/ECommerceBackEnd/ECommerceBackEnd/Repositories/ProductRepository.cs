using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerceBackEnd.Repositories
{
    public class ProductRepository:IProductRepository
    {
        public int latestId = 0;
        private readonly IMongoCollection<Product> productsCollection;
        private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;
        private const string collectionName = "products";
        public ProductRepository(IMongoDatabase database) {
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


        void IProductRepository.CreateProduct(Product newProduct)
        {
            productsCollection.InsertOne(newProduct);
            this.latestId++;
        }
        void IProductRepository.DeleteProduct(int PID)
        {
            var filter = filterBuilder.Eq(product => product.ProductCardId,PID );
            productsCollection.DeleteMany(filter);
        }

        Product IProductRepository.GetProduct(int PID)
        {
            var filter = filterBuilder.Eq(product => product.ProductCardId, PID);
            Product p = new Product();
            p = productsCollection.Find(filter).SingleOrDefault();
            return p;
        }

        IEnumerable<Product> IProductRepository.GetProducts()
        {
            return productsCollection.Find(new BsonDocument()).ToList();
        }

        void IProductRepository.UpdateProduct(Product product)
        {
            var filter = filterBuilder.Eq(existingProduct => existingProduct.ProductCardId, product.ProductCardId);
            productsCollection.ReplaceOne(filter, product);
        }
        public IEnumerable<Product> GetProductByCategory(int id)
        {
            var filter = filterBuilder.Eq("ProductCategoryId", id);
            return productsCollection.Find(filter).ToList();
        }
    }
}
