using AmazonMVC.DTO;
using AmazonMVC.Models;
using AmazonMVC.Repositories;
using AmazonMVC.Repositories.Interfaces;
using AmazonMVC.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonMVC.Tests.CategoryTests
{
    public class CategoryServiceTests // napisalem testy w przypadku ze dane wejsciowe sa poprawne i program dziala prawidlowo skibidi sigma pozdro
    {
        private readonly Mock<ICategoryRepostitory> _mockRepository;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _mockRepository = new Mock<ICategoryRepostitory>();
            _categoryService = new CategoryService(_mockRepository.Object);
        }
        [Fact]
        public async Task GetAllCategoriesAsync_DisplayCategories_ShouldReturnCategories()
        {
            //arrange

            var categoryList = new List<GetCategoryDTO>
            {
                new GetCategoryDTO { Id = 1, Name = "Category1" },
                new GetCategoryDTO { Id = 2, Name = "Category2" }
            };

            _mockRepository.Setup(repo => repo.GetAllCategoriesAsync())
                .ReturnsAsync(categoryList);

            //act

            var result = await _categoryService.GetAllCategoriesAsync();


            //assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Category1", result[0].Name);
            Assert.Equal("Category2", result[1].Name);

        }

        [Fact]

        public async Task DeleteCategoryAsync_WhenCategoryExists_ShouldRemoveCategoryFromDatabase()
        {
            //arrange

            var categoryToRemove = new DeleteCategoryDTO { Id = 1 };

            _mockRepository.Setup(repo => repo.DeleteCategoryAsync(It.Is<DeleteCategoryDTO>(c => c.Id == categoryToRemove.Id)))
                .Returns(Task.CompletedTask); //sprawdza czy metoda ta zostanie wywolana z obiektem klasy deletecategoryDTO ktorego Id sie rowna categorytoRemove.Id 


            //act

            await _categoryService.DeleteCategoryAsync(categoryToRemove);

            //assert

            _mockRepository.Verify(repo => repo.DeleteCategoryAsync(It.Is<DeleteCategoryDTO>(c => c.Id == categoryToRemove.Id)),
                Times.Once, "Metoda DeleteCategoryAsync powinna być wywołana raz.");

        }

        [Fact]
        public async Task AddCategoryAsync_WhenCategoryIsValid_ShouldAddCategoryToDatabase()
        {
            //arrange

            var categoryToAdd = new AddCategoryDTO { Name = "Category" };

            _mockRepository.Setup(repo => repo.AddCategoryAsync(It.Is<AddCategoryDTO>(c => c.Name == categoryToAdd.Name)))
                .Returns(Task.CompletedTask);

            //act
            await _categoryService.AddCategoryAsync(categoryToAdd);

            //assert
            _mockRepository.Verify(repo => repo.AddCategoryAsync(It.Is<AddCategoryDTO>(c => c.Name == categoryToAdd.Name)),
                 Times.Once());//times once sprawdza ile razy metoda mocku zostala wywolana. wtym przypadku akurat sprawdza czy zostala wykonana raz

        }

        [Fact]

        public async Task EditCategoryAsync_WhenCategoryExists_ShouldEditCategory()
        {
            //arrange

            var editedCategory = new UpdateCategoryDTO { Id = 1, Name = "EditedCategory" };

            _mockRepository.Setup(repo => repo.EditCategoryAsync(It.Is<UpdateCategoryDTO>(c => c.Id == editedCategory.Id && c.Name == editedCategory.Name)))
                .Returns(Task.CompletedTask);

            //act

            await _categoryService.EditCategoryAsync(editedCategory);


            //assert

            _mockRepository.Verify(repo => repo.EditCategoryAsync(It.Is<UpdateCategoryDTO>(c => c.Id == editedCategory.Id && c.Name == editedCategory.Name)),
                Times.Once());
        }

        [Fact]
        public async Task GetCategoryForEdit_WhenCategoryExists_ShouldReturnCategoryForEdit()
        {
            //arrange

            var categoryForUpdate = new UpdateCategoryDTO { Name = "Category" };

            int categoryId = 1;

            _mockRepository.Setup(repo => repo.GetCategoryForEdit(categoryId))
                .ReturnsAsync(categoryForUpdate);


            //act

            var result = await _categoryService.GetCategoryForEdit(categoryId);

            //assert

            Assert.NotNull(result);
            Assert.Equal(categoryForUpdate.Name, result.Name);
            _mockRepository.Verify(repo => repo.GetCategoryForEdit(categoryId), Times.Once());
        }

        [Fact]
        public async Task DeleteCategoryAsync_WhenCategoryExists_ShouldRemoveCategory()
        {
            //arrange

            var categoryForDelete = new DeleteCategoryDTO { Id = 1 };

            _mockRepository.Setup(repo => repo.DeleteCategoryAsync(It.Is<DeleteCategoryDTO>(c => c.Id == categoryForDelete.Id)))
                .Returns(Task.CompletedTask);

            //act

            await _categoryService.DeleteCategoryAsync(categoryForDelete);

            //assert

            _mockRepository.Verify(repo => repo.DeleteCategoryAsync(It.Is<DeleteCategoryDTO>(c => c.Id == categoryForDelete.Id)),
                Times.Once());
        }
    }
}
