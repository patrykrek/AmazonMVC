using AmazonMVC.Data;
using AmazonMVC.DTO;
using AmazonMVC.Interfaces;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmazonMVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
       

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> CreateOrderAsync(string userId)
        {
            return await _orderRepository.CreateOrderAsync(userId);
            
        }

        public async Task<List<GetOrderDTO>> DisplayUserOrdersAsync(string userId)
        {
            return await _orderRepository.DisplayUserOrdersAsync(userId);
        }
                
        public async Task<List<GetOrderDTO>> DisplayAllOrdersAsync() // for admin
        {
            return await _orderRepository.DisplayAllOrdersAsync();
        }
    }
}
