using Microsoft.AspNetCore.Identity;

namespace BASEAPP.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = null!;
    }
}   
