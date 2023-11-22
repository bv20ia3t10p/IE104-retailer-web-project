using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceBackEnd.Entities
{

    public class Order
    {
        public ObjectId Id { get; set; }
        public int OrderId { get; set; }
        public string Type { get; set; }
        [Column("Daysforshipping(real)")]
        public int DayForShippingReal { get; set; }
        [Column("Daysforshipment(scheduled)")]
        public int DayForShipmentScheduled { get; set; }
        public string DeliveryStatus { get; set; }
        [Column("Late_delivery_risk")]
        public int LateDeliveryRisk { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerFname { get; set; }
        public int CustomerId { get; set; }
        public string Lname { get; set; }
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
        [Column("orderdate(DateOrders)")]
        public DateTime OrderDate { get; set; }
        [Column("shippingDate(DateOrders")]
        public DateTime ShippingDate { get; set; }
        public string ShippingMode { get; set; }
        public decimal Total { get; set; }


    }
}
