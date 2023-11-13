namespace ECommerceBackEnd.Service.Contracts
{
    public interface IServiceManager
    {
        ICategoryService Category { get; }
        ICustomerService Customer { get; }
        IProductService Product { get; }
        IAuthService Auth { get; }
    }
}
