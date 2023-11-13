using ECommerceBackEnd.Contracts;
using MongoDB.Driver;

namespace ECommerceBackEnd.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private const string databaseName = "ie104";
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IProductsRepository> _productsRepository;
        private readonly Lazy<ICategoriesRepository> _categoriesRepository;
        private readonly IMongoDatabase _database;
        public RepositoryManager(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase(databaseName);
            _productsRepository = new Lazy<IProductsRepository>(() => new ProductsRepository(_database));
            _categoriesRepository = new Lazy<ICategoriesRepository>(() => new CategoriesRepository(_database));
            _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(_database));
        }
        public ICustomerRepository Customer => _customerRepository.Value;
        public ICategoriesRepository Categories => _categoriesRepository.Value;
        public IProductsRepository Product => _productsRepository.Value;

    }
}
