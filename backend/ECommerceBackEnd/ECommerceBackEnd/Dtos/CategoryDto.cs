using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ECommerceBackEnd.Dtos
{
    public record CategoryDto
    {
        public ObjectId Id { get; init; }
        public int CID { get; init; }
        public string? CName { get; init; }
    }
    public record CreateCategoryDto
    {

        [Required]
        public string? CName { get; init; }
    }
    public record UpdateCategoryDto
    {
        [Required]
        public int? CID { get; init; }
        [Required]
        public string? CName { get; init; }
    }
}
