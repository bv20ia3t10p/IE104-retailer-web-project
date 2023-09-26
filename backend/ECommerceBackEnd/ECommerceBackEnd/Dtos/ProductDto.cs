using MongoDB.Bson;

namespace ECommerceBackEnd.Dtos
{
    public class ProductDto
    {
        public ObjectId Id { get; init; }
        public int? DepId { get; init; }
        public string? DName { get; init; }
        public int? PID { get; init; }
        public int? CID { get; init; }
        public string? PName { get; init; }
        public double? Price { get; init; }
        public int? OID { get; init; }
        public int? OPID { get; init; }
        public double? Ratio { get; init; }
        public double? Gain { get; init; }
        public bool? Available {  get; init; }
    }
}
