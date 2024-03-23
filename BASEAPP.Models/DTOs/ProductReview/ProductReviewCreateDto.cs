using System.ComponentModel.DataAnnotations;

namespace BASEAPP.Models.DTOs.ProductReview
{
    public class ProductReviewCreateDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; }

        [Required]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}
