using MongoDB.Bson;

namespace ECommerceBackEnd.Entities
{
    public class Customer
    {
        public ObjectId Id { get; init; }
        public string? CustomerCity { get; init; }
        public string? CustomerCountry { get; init; }
        public string? CustomerSegment { get; init; }
        public string? CustomerStreet { get; init; }
        public string? CustomerState { get; init; }
        public string? CustomerZipcode { get; init; }
        public int CustomerId { get; init; }
        public string? CustomerEmail { get; init; }
        public string CustomerPassword { get; init; }
        public string? CustomerFname { get; init; }
        public string? CustomerLname { get; init; }

    }
}
