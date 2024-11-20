using AmazonMVC.DTO;

namespace AmazonMVC.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<List<GetCartDTO>> DisplayCart(string UserId);
        Task<GetCartDTO> AddItemsToCart(AddCartDTO cartItems);
        Task DeleteCartItem(DeleteCartDTO cartItem);
        Task ClearCart(string userId);
    }
}
