using AmazonMVC.DTO;

namespace AmazonMVC.Interfaces
{
    public interface ICartService
    {
        Task<List<GetCartDTO>> DisplayCart(string UserId);
        Task<GetCartDTO> AddItemsToCart(AddCartDTO cartItems);
        Task DeleteCartItem(DeleteCartDTO cartItem);
        Task ClearCart(string userId);
    }
}
