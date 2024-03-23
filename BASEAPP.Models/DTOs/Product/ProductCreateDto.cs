using System.ComponentModel.DataAnnotations;

namespace BASEAPP.Models.DTOs.Product
{
    public class ProductCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [Required]
        public int? BrandId { get; set; }
    }
}
