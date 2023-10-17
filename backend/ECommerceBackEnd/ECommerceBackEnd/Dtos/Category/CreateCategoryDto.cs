using System.ComponentModel.DataAnnotations;

namespace ECommerceBackEnd.Dtos.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public int? CID { get; init; }
        [Required]
        public int? CName {  get; init; }
    }
}
