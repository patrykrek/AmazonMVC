using AmazonMVC.DTO;
using AmazonMVC.Models;

namespace AmazonMVC.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<GetOrderDTO>> DisplayAllOrdersAsync();
        Task<List<GetOrderDTO>> DisplayUserOrdersAsync(string userId);
        Task<Result> CreateOrderAsync(string userId);
    }
}
