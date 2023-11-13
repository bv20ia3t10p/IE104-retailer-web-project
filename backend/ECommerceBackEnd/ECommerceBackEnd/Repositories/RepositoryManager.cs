using ECommerceBackEnd.Contracts;
using MongoDB.Driver;

namespace ECommerceBackEnd.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private const string databaseName = "ie104";
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly IMongoDatabase _database;
        public RepositoryManager(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase(databaseName);
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_database));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_database,"categories"));
            _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(_database));
        }
        public ICustomerRepository Customer => _customerRepository.Value;
        public ICategoryRepository Category => _categoryRepository.Value;
        public IProductRepository Product => _productRepository.Value;

    }
}
