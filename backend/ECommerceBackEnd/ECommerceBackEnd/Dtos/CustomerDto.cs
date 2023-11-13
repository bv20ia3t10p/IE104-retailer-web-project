using MongoDB.Bson;

namespace ECommerceBackEnd.Dtos
{
    public class CustomerDTO
    {
        public ObjectId Id { get; init; }
        public string? City { get; init; }
        public string? Country { get; init; }
        public string? Street { get; init; }
        public string? State { get; init; }
        public string? Zip { get; init; }
        public string? Segment { get; init; }
        public int CusID { get; init; }
        public string? Email { get; init; }
        public string PW { get; init; }
        public string? Fname { get; init; }
        public string? Lname { get; init; }


    }
}
