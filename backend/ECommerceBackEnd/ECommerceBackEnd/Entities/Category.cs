using MongoDB.Bson;

namespace ECommerceBackEnd.Entities
{
    public class Category
    {
       public ObjectId Id { get; init; }
       public int CategoryId { get; init; }
       public string CategoryName { get; init; }
    }
}
