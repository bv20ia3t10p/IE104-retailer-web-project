namespace ECommerceBackEnd.Entities
{
    public class Products
    {
        public record Product
        {
            public string ProductId { get; init; }
            public string? Name { get; init; }
            public string? Category {  get; init; }
            public string? SubCategory { get; init; }
            public float? Rating {  get; init; }
            public string? Currency { get; init; }
            public int? NumberOfRates {  get; init; }
            public float? Price { get; init; }
            
        }
    }
}
