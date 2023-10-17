using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ECommerceBackEnd.Dtos.Product
{
    public class UpdateItemDto
    {
        [Required]
        public int? DepId { get; init; }
        public string? DName { get; init; }
        [Required]
        public int? PID { get; init; }
        public int? CID { get; init; }
        [Required]
        public string? PName { get; init; }
        [Required]
        public double? Price { get; init; }
        public int? OID { get; init; }
        public int? OPID { get; init; }
        public double? Ratio { get; init; }
        public double? Gain { get; init; }
        public bool? Available { get; init; }
    }
}
