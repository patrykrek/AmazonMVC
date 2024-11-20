using AmazonMVC.DTO;
using AmazonMVC.Models;

namespace AmazonMVC.Interfaces
{
    public interface IOrderService
    {
        Task<List<GetOrderDTO>> DisplayAllOrdersAsync();
        Task<List<GetOrderDTO>> DisplayUserOrdersAsync(string userId);
        Task<Result> CreateOrderAsync(string userId);
    }
}
