using System.ComponentModel.DataAnnotations;

namespace BASEAPP.Models.DTOs.Product
{
    public class ProductUpdateDto
    {
        [MaxLength(255)]
        [Required]
        public string Name { get; set; } = string.Empty; 

        [MaxLength(500)]
        public string? Description { get; set; } 

        [Required]
        public double Price { get; set; } 

        [Required]
        public int StockQuantity { get; set; } 

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } 

        public string? ImagePath { get; set; }
    }
}
