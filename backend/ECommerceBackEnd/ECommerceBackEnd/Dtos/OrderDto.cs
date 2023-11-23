using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceBackEnd.Dtos
{
    public record OrderDto
    {
        public int OrderId { get; set; }
        public string Type { get; set; }
        public int DayForShippingReal { get; set; }
        public int DayForShipmentScheduled { get; set; }
        public string DeliveryStatus { get; set; }
        public int LateDeliveryRisk { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerFname { get; set; }
        public int CustomerId { get; set; }
        public string CustomerLname { get; set; }
        public string CustomerSegment { get; set; }
        public string CustomerState { get; set; }
        public int CustomerZipcode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Market { get; set; }
        public string OrderCity { get; set; }
        public string OrderCountry { get; set; }
        public string OrderRegion { get; set; }
        public string OrderState { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public string ShippingMode { get; set; }
        public decimal Total { get; set; }
    }
    public record CreateOrderDto
    {
        public string Type { get; set; }
        public string DeliveryStatus { get; set; }
        public int LateDeliveryRisk { get; set; }
        public int CustomerId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Market { get; set; }
        public string OrderCity { get; set; }
        public string OrderCountry { get; set; }
        public string OrderRegion { get; set; }
        public string OrderState { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public string ShippingMode { get; set; }
        public decimal Total { get; set; }
    }
    public record UpdateOrderStatusDto {
        public int OrderId { get; set; }
        public string Type { get; set; }
        public int DayForShippingReal { get; set; }
        public int DayForShipmentScheduled { get; set; }
        public string DeliveryStatus { get; set; }
        public int LateDeliveryRisk { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public string ShippingMode { get; set; }
        public decimal Total { get; set; }
    }
    public record UpdateOrderLocationDto
    {
        public int OrderId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
