using System.ComponentModel.DataAnnotations;

namespace BASEAPP.Models.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Name { get; set; } = null!;
    }
}
