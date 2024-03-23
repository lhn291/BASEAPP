using System.ComponentModel.DataAnnotations;

namespace BASEAPP.Models.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; } = null!;
    }

}
