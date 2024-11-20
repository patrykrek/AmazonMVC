using AmazonMVC.DTO;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using AmazonMVC.Services;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonMVC.Tests.CartTests
{
    public class CartServiceTest
    {
        private readonly Mock<ICartRepository> _mockRepositpory;
        private readonly CartService _cartService;
        private readonly Mock<IMapper> _mockMapper;

        public CartServiceTest()
        {
            _mockRepositpory = new Mock<ICartRepository>();
            _mockMapper = new Mock<IMapper>();
            _cartService = new CartService(_mockRepositpory.Object);
            
        }

        [Fact]
        public async Task DisplayCartAsync_WhenCalled_ShouldReturnUserItemsInCart()
        {
            //arrange
            var userId = "UserId";

            var cartList = new List<GetCartDTO>
            {
                new GetCartDTO { Id = 1, Price = 100, ProductId = 1, Quantity = 1 },
                new GetCartDTO {Id = 2, Price = 10, ProductId = 1, Quantity = 2 },
            };

            _mockRepositpory.Setup(repo => repo.DisplayCart(userId)).ReturnsAsync(cartList);

            //act

            var result = await _cartService.DisplayCart(userId);

            //assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(100, result[0].Price);
            Assert.Equal(10, result[1].Price);
        }

        [Fact]
        public async Task AddItemToCartAsync_WhenProductExists_ShouldAddProductToCart()
        {
            //arrange
            var cartItems = new AddCartDTO
            {
                ProductId = 1,
                Quantity = 2,
            };

            var product = new Product
            {
                Id = 1,
                StockQuantity = 10,
                Price = 100
            };
            var cartItem = new CartItem
            {
                ProductId = 1,
                Quantity = 2,
                Price = 200,
            };
            
            var getCartDTO = new GetCartDTO
            {
                ProductId = 1,
                Quantity = 2,
                Price = 200

            };

            _mockRepositpory.Setup(repo => repo.AddItemsToCart(It.IsAny<AddCartDTO>()))
                .ReturnsAsync(getCartDTO);

            _mockMapper.Setup(mapper => mapper.Map<CartItem>(cartItem))
                .Returns(cartItem);

            _mockMapper.Setup(mapper => mapper.Map<GetCartDTO>(cartItem))
                .Returns(getCartDTO);

            //act

            var result = await _cartService.AddItemsToCart(cartItems);

            //assert

            Assert.NotNull(result);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(2, result.Quantity);
            Assert.Equal(200, result.Price);

            _mockRepositpory.Verify(repo => repo.AddItemsToCart(It.IsAny<AddCartDTO>()),
                Times.Once());         
           
        }

        [Fact]
        public async Task ClearCart_WhenCalled_ShouldRemoveItemsFromCart()
        {
            //arrange
            var userId = "UserId";

            _mockRepositpory.Setup(repo => repo.ClearCart(userId))
                .Returns(Task.CompletedTask);

            //act

            await _cartService.ClearCart(userId);

            //assert

            _mockRepositpory.Verify(repo => repo.ClearCart(userId), Times.Once());
        }

        [Fact]
        public async Task DeleteCartItem_WhenCalled_ShouldDeleteItemFromCart()
        {
            //arrange
            var cartItemToRemove = new DeleteCartDTO { Id = 1 };

            _mockRepositpory.Setup(repo => repo.DeleteCartItem(It.Is<DeleteCartDTO>(c => c.Id == cartItemToRemove.Id)))
                .Returns(Task.CompletedTask);

            //act

            await _cartService.DeleteCartItem(cartItemToRemove);

            //assert

            _mockRepositpory.Verify(repo => repo.DeleteCartItem(cartItemToRemove), Times.Once());
        }
    }
}
