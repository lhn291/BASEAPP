using System.ComponentModel.DataAnnotations;

namespace BASEAPP.Models.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

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

        [Required]
        public int? CategoryId { get; set; }
        public Category Category { get; set; } = new Category();

        public double AverageRating { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        public int? BrandId { get; set; }
        public Brand Brand { get; set; } = new Brand();

        [MaxLength(255)]
        public string? Location { get; set; }

        [MaxLength(255)]
        public string? ShopName { get; set; }

        public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

        public string? Color { get; set; }

        public string? Size { get; set; }
    }
}
