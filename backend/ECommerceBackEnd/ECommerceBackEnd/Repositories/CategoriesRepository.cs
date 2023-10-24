using ECommerceBackEnd.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerceBackEnd.Repositories
{
    public class CategoriesRepository:ICategoriesRepository
    {
        public int latestId = 0;
        private readonly IMongoCollection<Category> categoryCollection;
        private readonly FilterDefinitionBuilder<Category> filterBuilder = Builders<Category>.Filter;
        private const string databaseName = "ie104";
        private const string collectionName = "categories";
        public CategoriesRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            categoryCollection = database.GetCollection<Category>(collectionName);
            latestId = GetLatestId() + 1;
        }

        private int GetLatestId()
        {
            var sort = Builders<Product>.Sort.Descending("CategoryId");
            return categoryCollection.Find(bson => true)
                .SortBy(bson => bson.CategoryId)
                .ThenByDescending(bson => bson.CategoryId)
                .FirstOrDefault().CategoryId;
        }
        void ICategoriesRepository.CreateCategory(Category newCategory)
        {
            categoryCollection.InsertOne(newCategory);
            latestId++;
        }

        void ICategoriesRepository.DeleteCategory(int CID)
        {
            var filter = filterBuilder.Eq(category => category.CategoryId, CID);
            categoryCollection.DeleteMany(filter);
        }

        Category ICategoriesRepository.GetCategory(int CID)
        {
            var filter = filterBuilder.Eq(category => category.CategoryId, CID);
            return categoryCollection.Find(filter).SingleOrDefault();
        }

        IEnumerable<Category> ICategoriesRepository.GetCategories()
        {
            return categoryCollection.Find(new BsonDocument()).ToList();
        }

        void ICategoriesRepository.UpdateCategory(Category category)
        {
            var filter = filterBuilder.Eq(existingCategory => category.CategoryId, category.CategoryId );
            categoryCollection.ReplaceOne(filter, category);
        }
    }
}
