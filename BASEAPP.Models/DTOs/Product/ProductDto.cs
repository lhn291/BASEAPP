using System.ComponentModel.DataAnnotations;

namespace BASEAPP.Models.DTOs.User
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string? Description { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string? CategoryName { get; set; }

        public string? BrandName { get; set; }

        public double AverageRating { get; set; }

        public string? ImagePath { get; set; }
    }
}
