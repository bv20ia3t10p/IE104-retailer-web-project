using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace ECommerceBackEnd.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public int latestId = 0;
        private readonly IMongoCollection<Customer> customerCollection;
        private readonly FilterDefinitionBuilder<Customer> filterBuilder = Builders<Customer>.Filter;
        private const string collectionName = "customers";

        public CustomerRepository(IMongoDatabase database)
        {
            customerCollection = database.GetCollection<Customer>(collectionName);
            latestId = GetLatestId() + 1;
        }
        private int GetLatestId()
        {
            var sort = Builders<Customer>.Sort.Descending("CustomerId");
            return customerCollection.Find(bson => true)
                .SortBy(bson => bson.CustomerId)
                .ThenByDescending(bson => bson.CustomerId)
                .FirstOrDefault().CustomerId;
        }
        public void Create(Customer customer)
        {
            customerCollection.InsertOne(customer);
        }

        public void Delete(int id)
        {
            var filter = filterBuilder.Eq(category => category.CustomerId, id);
            customerCollection.DeleteMany(filter);
        }

        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
