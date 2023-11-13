using AutoMapper;
using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Repositories;
using ECommerceBackEnd.Service.Contracts;

namespace ECommerceBackEnd.Service
{
    public class ServiceManager: IServiceManager
    {
        private readonly Lazy<ICategoryService> _categoryService;
        public ServiceManager( IMapper mapper, IRepositoryManager repositoryManager ) {
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(mapper,repositoryManager));
        }
        public ICategoryService Category => _categoryService.Value;
    }
}
