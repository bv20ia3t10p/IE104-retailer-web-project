using System.ComponentModel.DataAnnotations;

namespace ECommerceBackEnd.Dtos.Category

{
    public class UpdateCategoryDto
    {
        [Required]
        public int? CID { get; init; }
        [Required]
        public string CName { get; init;}
    }
}
