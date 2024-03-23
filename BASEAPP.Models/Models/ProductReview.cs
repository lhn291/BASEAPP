using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BASEAPP.Models.Models
{
    public class ProductReview
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public int ProductId { get; set; } = 0;

        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; }

        [Required]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public Product? Product { get; set; }

        public string? ImageUrl { get; set; }
    }
}
