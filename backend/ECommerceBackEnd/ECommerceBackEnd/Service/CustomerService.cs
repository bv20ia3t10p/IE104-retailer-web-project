﻿using AutoMapper;
using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Entities;
using ECommerceBackEnd.Service.Contracts;

namespace ECommerceBackEnd.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public CustomerService(IMapper mapper, IRepositoryManager repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IEnumerable<CustomerDTO> GetCustomers() => _mapper.Map<IEnumerable<CustomerDTO>>(_repository.Customer.GetCustomers());
        public CustomerDTO GetCustomerById(int id) => _mapper.Map<CustomerDTO>(_repository.Customer.GetCustomerById(id));
        public CustomerDTO CreateCustomer(CustomerDTO customer)
        {
            var customerEntity = _mapper.Map<Customer>(customer);
            customerEntity.CustomerId = _repository.Customer.GetLatestId();
            _repository.Customer.CreateCustomer(customerEntity);
            return _mapper.Map<CustomerDTO>(customerEntity);
        }
        public void DeleteCustomerById(int id)
        {
            var customerInDb = _repository.Customer.GetCustomerById(id);
            if (customerInDb == null)
            {
                throw new Exception("Customer not found");
            }
            _repository.Customer.DeleteCustomer(customerInDb);
        }
        public CustomerDTO UpdateCustomer(CustomerDTO newCustomer)
        {
            var customerInDb = _repository.Customer.GetCustomerById(newCustomer.CustomerId);
            if (customerInDb == null) throw new Exception("Customer not found");
            _mapper.Map(newCustomer, customerInDb);
            _repository.Customer.UpdateCustomer(customerInDb);
            return _mapper.Map<CustomerDTO>(customerInDb);
        }
        public void UpdateMultipleCustomerPassword(string newPw)
        {
            _repository.Customer.UpdateMultipleCustomerPassword(newPw);
        }
    }
}
