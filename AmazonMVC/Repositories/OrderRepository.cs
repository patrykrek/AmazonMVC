using AmazonMVC.Data;
using AmazonMVC.DTO;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmazonMVC.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> CreateOrderAsync(string userId)
        {
            var cartItems = await _context.CartItems.Where(ci => ci.UserId == userId)
                .Include(p => p.Product).ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return new Result { Success = false, ErrorMessage = $"Cart is empty. Buy products" };
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                UserId = userId,
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList() // Creating list OrderItems with CartItems
            };

            await _context.Orders.AddAsync(order);

            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            return new Result { Success = true };

        }

        public async Task<List<GetOrderDTO>> DisplayUserOrdersAsync(string userId) // user displaying HIS orders
        {
            var getDb = await _context.Orders.Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(p => p.Product)
                .ToListAsync();

            var getUserOrders = getDb.Select(o => _mapper.Map<GetOrderDTO>(o)).ToList();

            return getUserOrders;
        }




        public async Task<List<GetOrderDTO>> DisplayAllOrdersAsync() // for admin, he can display every user's orders
        {
            var getDb = await _context.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();

            var getOrders = getDb.Select(o => _mapper.Map<GetOrderDTO>(o)).ToList();

            return getOrders;
        }
    }
}
