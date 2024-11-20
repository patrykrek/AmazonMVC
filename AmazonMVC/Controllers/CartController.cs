using AmazonMVC.DTO;
using AmazonMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AmazonMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> DisplayCart()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (user == null)
            {
                return Unauthorized();
            }

            var cart = await _cartService.DisplayCart(user);

            return View(cart);

        }

        public IActionResult AddItemsToCart(int ProductId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = new AddCartDTO
            {
                ProductId = ProductId,
                UserId = user
            };

            return View(cart);
        }

        public async Task<IActionResult> AddItemsToCartForm(AddCartDTO cartitem)
        {
            if (!ModelState.IsValid)
            {
                return View("AddItemsToCart", cartitem);
            }
            await _cartService.AddItemsToCart(cartitem);

            return RedirectToAction("DisplayCart");
        }

        public async Task<IActionResult> DeleteCartItem(DeleteCartDTO cartItem)
        {
            await _cartService.DeleteCartItem(cartItem);

            return RedirectToAction("DisplayCart");
        }

        public async Task<IActionResult> ClearCart()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _cartService.ClearCart(user);

            return RedirectToAction("DisplayCart");
        }
    }
}
