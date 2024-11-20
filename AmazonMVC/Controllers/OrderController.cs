using AmazonMVC.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AmazonMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Orders()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var getorders = await _orderService.DisplayUserOrdersAsync(user);

            return View(getorders);
        }

        public async Task<IActionResult> CreateOrder()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _orderService.CreateOrderAsync(user);

            if (!result.Success)
            {
                return View("CartIsEmpty", new { message = result.ErrorMessage });
            }

            return RedirectToAction("Orders");

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisplayAllOrders() // admin
        {
            var getOrders = await _orderService.DisplayAllOrdersAsync();

            return View(getOrders);
        }

        public IActionResult CartIsEmpty(string message)
        {
            ViewData["ErrorMessage"] = message;

            return View();  
        }
    }
}
