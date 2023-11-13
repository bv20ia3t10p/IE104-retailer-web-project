using ECommerceBackEnd.Dtos;

namespace ECommerceBackEnd.Service.Contracts
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        ProductDto GetProduct(int id);
        IEnumerable<ProductDto> GetProductByCategory(int categoryId);
        ProductDto CreateProduct(CreateProductDto newProduct);
        ProductDto UpdateProduct(UpdateProductDto updateProduct);
        void DeleteProduct(int id);
    }
}
