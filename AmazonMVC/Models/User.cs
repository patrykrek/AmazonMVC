using Microsoft.AspNetCore.Identity;

namespace AmazonMVC.Models
{
    public class User : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
