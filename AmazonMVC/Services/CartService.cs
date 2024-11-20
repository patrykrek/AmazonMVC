using AmazonMVC.Data;
using AmazonMVC.DTO;
using AmazonMVC.Interfaces;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmazonMVC.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<List<GetCartDTO>> DisplayCart(string UserId)
        {
            return await _cartRepository.DisplayCart(UserId);         
        }
        public async Task<GetCartDTO> AddItemsToCart(AddCartDTO cartItems)
        {
            return await _cartRepository.AddItemsToCart(cartItems);
        }

        public async Task DeleteCartItem(DeleteCartDTO cartItem) 
        {
            await _cartRepository.DeleteCartItem(cartItem);
        }

        public async Task ClearCart(string userId)
        {
            await _cartRepository.ClearCart(userId);
        }

        
    }
}
