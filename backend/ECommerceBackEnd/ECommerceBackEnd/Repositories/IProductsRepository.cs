using ECommerceBackEnd.Entities;

namespace ECommerceBackEnd.Repositories
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int PID);
        void CreateProduct(Product newProduct);
        public void UpdateProduct(Product product);
        public void DeleteProduct(int PID);
        public IEnumerable<Product> GetProductByCategory(int id);
    }
}
