using AutoMapper;
using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Entities;
using ECommerceBackEnd.Service.Contracts;

namespace ECommerceBackEnd.Service
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        public OrderService(IMapper mapper, IRepositoryManager repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public IEnumerable<OrderDto> GetOrders() => _mapper.Map<IEnumerable<OrderDto>>(_repository.Order.GetOrders());
        public IEnumerable<OrderDto> GetOrdersByCustomer(int id) => _mapper.Map<IEnumerable<OrderDto>>(_repository.Order.GetOrdersByCustomer(id));
        public OrderDto GetOrder(int id) => _mapper.Map<OrderDto>(_repository.Order.GetOrderById(id));
        public OrderDto UpdateOrderStatus(UpdateOrderStatusDto updateOrderStatus)
        {
            var orderInDb = _repository.Order.GetOrderById(updateOrderStatus.OrderId);
            if (orderInDb == null)
            {
                throw new Exception("Order not found");
            }
            _mapper.Map(updateOrderStatus, orderInDb);
            _repository.Order.UpdateOrder(orderInDb);
            return _mapper.Map<OrderDto>(orderInDb);
        }
        public OrderDto UpdateOrderLocation(UpdateOrderLocationDto updateOrderLocationDto)
        {
            var orderInDb = _repository.Order.GetOrderById(updateOrderLocationDto.OrderId);
            if (orderInDb == null)
            {
                throw new Exception("Order not found");
            }
            // Call Google API and extract from UpdateDTO Lat and Long
            //public string Market { get; set; }
            //public string OrderCity { get; set; }
            //public string OrderCountry { get; set; }
            //public string OrderRegion { get; set; }
            _mapper.Map(updateOrderLocationDto, orderInDb);
            _repository.Order.UpdateOrder(orderInDb);
            return _mapper.Map<OrderDto>(orderInDb);
        }
        public void DeleteOrder(int id)
        {
            var orderInDb = _repository.Order.GetOrderById(id);
            if (orderInDb == null)
            {
                throw new Exception("Order not found");
            }
            _repository.Order.DeleteOrder(orderInDb);
        }
        public OrderDto CreateOrder(CreateOrderDto newOrder)
        {
            int latestId = _repository.Order.GetLatestId();
            var orderEntity = _mapper.Map<Order>(newOrder);
            var customerInDb = _repository.Customer.GetCustomerById(newOrder.CustomerId);
            if (customerInDb != null )
            {
                // This also maps Customer entity object ID to Order Id so we gotta override that by generating new object ID manually, usually this is done by automapper
                _mapper.Map(customerInDb, orderEntity);
            }
            orderEntity.OrderId = latestId;
            orderEntity.Id = new MongoDB.Bson.ObjectId();
            _repository.Order.CreateOrder(orderEntity);
            return _mapper.Map<OrderDto>(orderEntity);
        }
    }
}
