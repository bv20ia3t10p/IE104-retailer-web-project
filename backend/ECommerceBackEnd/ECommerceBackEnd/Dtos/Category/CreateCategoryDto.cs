using System.ComponentModel.DataAnnotations;

namespace ECommerceBackEnd.Dtos.Category
{
    public class CreateCategoryDto
    {

        [Required]
        public int? CName {  get; init; }
    }
}
