using System.ComponentModel.DataAnnotations;

namespace BASEAPP.Models.DTOs.ProductReview
{
    public class ProductReviewDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; }

        public string? UserName { get; set; }
    }
}
