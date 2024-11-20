using AmazonMVC.Models;

namespace AmazonMVC.DTO
{
    public class GetCartDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }

    }
}
