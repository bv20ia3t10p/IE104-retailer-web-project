using MongoDB.Bson;

namespace ECommerceBackEnd.Dtos.Category
{
    public class CategoryDto
    {
        public ObjectId Id { get; init; }
        public int CID { get; init; }
        public string CName { get; init; }
    }
}
