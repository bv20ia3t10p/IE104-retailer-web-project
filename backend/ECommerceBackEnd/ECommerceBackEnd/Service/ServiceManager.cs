using AutoMapper;
using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Repositories;
using ECommerceBackEnd.Service.Contracts;

namespace ECommerceBackEnd.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IProductService> _productService;
        public ServiceManager(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(mapper, repositoryManager));
            _productService = new Lazy<IProductService>(() => new ProductService(mapper, repositoryManager));
            _customerService = new Lazy<ICustomerService>(() => new CustomerService(mapper, repositoryManager));
        }
        public ICategoryService Category => _categoryService.Value;
        public IProductService Product => _productService.Value;
        public ICustomerService Customer => _customerService.Value;
    }
}
