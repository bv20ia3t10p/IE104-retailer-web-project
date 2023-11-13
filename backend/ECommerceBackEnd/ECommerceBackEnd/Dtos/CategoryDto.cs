using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ECommerceBackEnd.Dtos
{
    public record CategoryDto
    {
        public ObjectId Id { get; init; }
        public int CategoryId { get; init; }
        public string? CategoryName { get; init; }
    }
    public record CreateCategoryDto
    {

        [Required]
        public string? CategoryName { get; init; }
    }
    public record UpdateCategoryDto
    {
        [Required]
        public int? CategoryId { get; init; }
        [Required]
        public string? CategoryName { get; init; }
    }
}
