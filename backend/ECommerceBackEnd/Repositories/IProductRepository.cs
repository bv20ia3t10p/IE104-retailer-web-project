using ECommerceBackEnd.Entities;

namespace ECommerceBackEnd.Repositories
{
    public interface IProductRepository
    {
        Product GetProduct(string id);
        IEnumerable<Product> GetProducts();
        void CreateProduct (Product product);
        void UpdateProduct (Product product);
        void DeleteProduct (string id);

    }
}
