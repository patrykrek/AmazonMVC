using AmazonMVC.Models;

namespace AmazonMVC.DTO
{
    public class AddCartDTO
    {
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
