using AmazonMVC.Models;

namespace AmazonMVC.DTO
{
    public class GetOrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
