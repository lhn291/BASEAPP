using System.ComponentModel.DataAnnotations;

namespace BASEAPP.Models.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        [Required]
        public double Percentage { get; set; }

    }
}
