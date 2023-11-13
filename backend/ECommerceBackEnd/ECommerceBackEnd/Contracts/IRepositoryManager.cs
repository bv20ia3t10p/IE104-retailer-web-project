namespace ECommerceBackEnd.Contracts
{
    public interface IRepositoryManager
    {
        IProductsRepository Product { get; }
        ICustomerRepository Customer { get; }
        ICategoriesRepository Categories { get; }
    }
}
