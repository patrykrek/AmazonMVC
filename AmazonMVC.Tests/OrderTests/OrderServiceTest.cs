using AmazonMVC.DTO;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using AmazonMVC.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonMVC.Tests.OrderTests
{
    public class OrderServiceTest
    {
        private readonly Mock<IOrderRepository> _mockRepository;
        private readonly OrderService _orderService;

        public OrderServiceTest()
        {
            _mockRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_mockRepository.Object);
        }

        [Fact]
        public async Task DisplayAllOrders_AdminCanDisplayEveryOrder_ShouldReturnListOfOrders()
        {
            //arrange
            var listOfOrders = new List<GetOrderDTO>
            {
                new GetOrderDTO { Id = 1, OrderDate = DateTime.Now, UserId = "UserId" },

                new GetOrderDTO { Id = 2, OrderDate = DateTime.Now, UserId = "UserId2" }
            };

            _mockRepository.Setup(repo => repo.DisplayAllOrdersAsync())
                .ReturnsAsync(listOfOrders); 

            //act

            var result = await _orderService.DisplayAllOrdersAsync();

            //assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("UserId", result[0].UserId);
            Assert.Equal("UserId2", result[1].UserId);
        }

        [Fact]
        public async Task DisplayUserOrders_WhenCalled_ShouldDisplayUserOrders()
        {
            //arrange
            var userId = "UserId";

            var listOfOrders = new List<GetOrderDTO>
            {
                new GetOrderDTO { Id = 1, OrderDate = DateTime.Now, UserId = "UserId", OrderItems = new List<OrderItem>() },

                new GetOrderDTO { Id = 2, OrderDate = DateTime.Now, UserId = "UserId2", OrderItems = new List<OrderItem>() }
            };

            _mockRepository.Setup(repo => repo.DisplayUserOrdersAsync(userId))
                .ReturnsAsync(listOfOrders);

            //act

            var result = await _orderService.DisplayUserOrdersAsync(userId);

            //assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(userId, result[0].UserId);

            _mockRepository.Verify(repo => repo.DisplayUserOrdersAsync(userId),Times.Once());
        }

        [Fact]
        public async Task CreateOrderAsync_IfCartIsNotEmpty_ShouldCreateOrderAndAddItToDatabase()
        {
            //arrange
            var expectedResult = new Result { Success = true };

            var userId = "UserId";

            _mockRepository.Setup(repo => repo.CreateOrderAsync(userId))
                .ReturnsAsync(expectedResult);


            //act

            var result = _orderService.CreateOrderAsync(userId);

            //assert

            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);

            _mockRepository.Verify(repo => repo.CreateOrderAsync(userId), Times.Once());
        }
    }
}
