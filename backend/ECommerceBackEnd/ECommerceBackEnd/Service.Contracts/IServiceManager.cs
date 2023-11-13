namespace ECommerceBackEnd.Service.Contracts
{
    public interface IServiceManager
    {
        ICategoryService Category { get; }
        IProductService Product { get; }
    }
}
