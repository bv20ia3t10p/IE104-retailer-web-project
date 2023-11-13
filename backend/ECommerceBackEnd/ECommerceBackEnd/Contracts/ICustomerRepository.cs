using ECommerceBackEnd.Entities;

namespace ECommerceBackEnd.Contracts
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        void Create(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}
