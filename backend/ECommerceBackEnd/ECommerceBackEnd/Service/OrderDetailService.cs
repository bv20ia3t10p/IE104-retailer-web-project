﻿using AutoMapper;
using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Entities;
using ECommerceBackEnd.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackEnd.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        public OrderDetailService(IMapper mapper, IRepositoryManager repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public IEnumerable<OrderDetailDto> GetByOrder(int orderId) => _mapper.Map<IEnumerable<OrderDetailDto>>(_repository.OrderDetail.GetDetailsForOrder(orderId));
        public IEnumerable<OrderDetailDto> GetByProduct(int productId) => _mapper.Map<IEnumerable<OrderDetailDto>>(_repository.OrderDetail.GetOrderDetailsForProduct(productId));
        public OrderDetailDto GetByOrderAndProduct(int oid, int pid) => _mapper.Map<OrderDetailDto>(_repository.OrderDetail.GetDetailByProductAndOrder(oid, pid));
        public OrderDetailDto CreateOrderDetail(CreateOrderDetailDto orderDetailDto)
        {
            var odEntity = _mapper.Map<OrderDetail>(orderDetailDto);
            var productInDb = _repository.Product.GetProduct(orderDetailDto.ProductCardId) ?? throw new Exception("Product not found");
            var categoryInDb = _repository.Category.GetCategoryById(productInDb.ProductCategoryId) ?? throw new Exception("Category not found");
            var orderInDb = _repository.Order.GetOrderById(orderDetailDto.OrderId) ?? throw new Exception("Order not found");
            _mapper.Map(productInDb, odEntity);
            _mapper.Map(orderInDb, odEntity);
            _mapper.Map(categoryInDb, odEntity);
            odEntity.Id = new MongoDB.Bson.ObjectId();
            odEntity.OrderItemId = _repository.OrderDetail.GetLatestId();
            _repository.OrderDetail.CreateOrderDetail(odEntity);
            return _mapper.Map<OrderDetailDto>(odEntity);
        }
        public IEnumerable<OrderDetailDto> CreateMultipleOrderDetails(IEnumerable<CreateOrderDetailDto> orderDetails)
        {
            var CreatedOrderList = new List<OrderDetailDto>();
            foreach (var cod in orderDetails)
            {
                CreatedOrderList.Add(CreateOrderDetail(cod));
            }
            return CreatedOrderList;
        }
        public OrderDetailDto UpdateOrderDetail(UpdateOrderDetailDto orderDetail)
        {
            var odEntity = _repository.OrderDetail.GetOrderDetailById(orderDetail.OrderItemId) ?? throw new Exception("Order detail not found");
            var odEntityId = odEntity.Id;
            _mapper.Map(orderDetail, odEntity);
            var productInDb = _repository.Product.GetProduct(orderDetail.ProductCardId) ?? throw new Exception("Product not found");
            _mapper.Map(productInDb, odEntity);
            odEntity.Id = odEntityId;
            _repository.OrderDetail.UpdateOrderDetail(odEntity);
            return _mapper.Map<OrderDetailDto>(odEntity);
        }
        public void DeleteOrderDetail(int id)
        {
            var odEntity = _repository.OrderDetail.GetOrderDetailById(id) ?? throw new Exception("Order detail not found");
            _repository.OrderDetail.DeleteOrderDetail(odEntity);
        }
    }
}
