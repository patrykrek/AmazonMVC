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

namespace AmazonMVC.Tests.ProductTests
{
    public class ProductServiceTests 
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockRepository.Object);
        }

        [Fact]
        public async Task DisplayProductsAsync_GetAllProductsFromDataBase_ShouldDisplayListOfProducts()
        {
            //arrange

            var productList = new List<GetProductDTO>
            {
                new GetProductDTO { Id = 1, Name = "Product", StockQuantity = 1, Price = 100, CategoryId = 1 },
                new GetProductDTO { Id = 2, Name = "Product2", StockQuantity = 12, Price = 10, CategoryId = 1 }
            };

            _mockRepository.Setup(repo => repo.DisplayProductsAsync())
                .ReturnsAsync(productList);

            //act

            var result = await _productService.DisplayProductsAsync();

            //assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Product", result[0].Name);
            Assert.Equal("Product2", result[1].Name);
            
        }

        [Fact]
        public async Task AddProductsAsync_WhenCategoryIdExists_ShouldAddProductToDatabase()
        {
            //arrange
            var productToAdd = new AddProductDTO { Name = "Product", StockQuantity= 1, Price = 100, CategoryId = 1 };

            var Result = new Result { Success = true };

            _mockRepository.Setup(repo => repo.AddProductAsync(It.Is<AddProductDTO>(p => p.Name == productToAdd.Name && p.CategoryId == productToAdd.CategoryId)))
                .ReturnsAsync(Result);


            //act

            var result = await _productService.AddProductAsync(productToAdd);

            //assert

            _mockRepository.Verify(repo => repo.AddProductAsync(It.Is<AddProductDTO>(p => p.Name == productToAdd.Name && p.CategoryId == productToAdd.CategoryId))
            ,Times.Once);
            
            Assert.NotNull(result);
            Assert.True(result.Success);               
        }

        [Fact]
        public async Task DeleteProductAsync_WhenProductExists_ShouldRemoveItFromDatabase()
        {
            //arrange
            var productToRemove = new DeleteProductDTO { Id = 1 };

            _mockRepository.Setup(repo => repo.DeleteProductAsync(It.Is<DeleteProductDTO>(p => p.Id == productToRemove.Id)))
                .Returns(Task.CompletedTask);

            //act

            await _productService.DeleteProductAsync(productToRemove);

            //assert

            _mockRepository.Verify(repo => repo.DeleteProductAsync(It.Is<DeleteProductDTO>(p => p.Id == productToRemove.Id)),
                Times.Once);
        }

        [Fact]
        public async Task GetProductForEdit_WhenProductExists_ShouldReturnProductForEdit()
        {
            //arrange
            var productForEdit = new UpdateProductDTO { Name = "Product" };

            int productId = 1;

            _mockRepository.Setup(repo => repo.GetProductForEdit(productId)).ReturnsAsync(productForEdit);

            //act

            var result = await _productService.GetProductForEdit(productId);

            //assert

            _mockRepository.Verify(repo => repo.GetProductForEdit(productId),Times.Once);
            Assert.NotNull(result);
            Assert.Equal(productForEdit.Name, result.Name);
            
        }

        [Fact]
        public async Task EditProductAsync_WhenProductExists_ShouldEditProduct()
        {
            //arrange
            var editedProduct = new UpdateProductDTO { Id = 1, Name = "Test" };

            _mockRepository.Setup(repo => repo.EditProductAsync(It.Is<UpdateProductDTO>(p => p.Id == editedProduct.Id && p.Name == editedProduct.Name)))
                .Returns(Task.CompletedTask);

            //act

            await _productService.EditProductAsync(editedProduct);

            //assert

            _mockRepository.Verify(repo => repo.EditProductAsync(It.Is<UpdateProductDTO>(p => p.Id == editedProduct.Id && p.Name == editedProduct.Name)),
                Times.Once);

        }
       
    }
}
