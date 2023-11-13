using AutoMapper;
using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Service.Contracts;

namespace ECommerceBackEnd.Service
{
    public class ProductService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        public ProductService(IMapper mapper, IRepositoryManager repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public IEnumerable<ProductDto> GetProducts() => _mapper.Map<IEnumerable<ProductDto>>(_repository.Product.GetProducts());
    }
}
