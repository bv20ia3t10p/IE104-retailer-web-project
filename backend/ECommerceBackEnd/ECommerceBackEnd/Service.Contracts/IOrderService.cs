using ECommerceBackEnd.Dtos;

namespace ECommerceBackEnd.Service.Contracts
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetOrders();
        IEnumerable<OrderDto> GetOrdersByCustomer(int id);
        OrderDto GetOrder(int id);
        OrderDto UpdateOrderStatus(UpdateOrderStatusDto updateOrderStatus);
        void DeleteOrder(int id);
        OrderDto CreateOrder(CreateOrderDto createOrderDto);

    }
}
