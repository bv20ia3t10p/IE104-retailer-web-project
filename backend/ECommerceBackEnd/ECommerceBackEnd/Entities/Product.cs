﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceBackEnd.Entities
{
    [BsonIgnoreExtraElements]
    public record Product
    {
        public ObjectId Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int ProductCardId { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int OrderItemCardprodId { get; set; }
        public bool ProductStatus { get; set; }
        public string ProductDescription { get; set; }
        public int ProductSoldQuantity { get; set; }
    }
}
