using MongoDB.Bson;

namespace ECommerceBackEnd.Entities
{

        public record Product
        {
            public ObjectId Id { get; init; }
            public int? DepartmentId { get; init; }
            public string? DepartmentName {  get; init; } 
            public int ProductCardId { get; init; }
            public int? ProductCategoryId { get; init; }
            public string? ProductName {  get; init; }
            public double? ProductPrice { get; init; }
            public int? OrderItemCardprodId { get; init; }
            public int? OrderItemId { get; init; }
            public double? OrderItemProfitRatio { get; init; }
            public double? Sales {  get; init; }
            public bool? ProductStatus { get; init; }
        }
    }
