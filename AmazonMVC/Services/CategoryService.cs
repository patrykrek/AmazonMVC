using AmazonMVC.Data;
using AmazonMVC.DTO;
using AmazonMVC.Interfaces;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmazonMVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepostitory _categoryRepostitory;
        

        public CategoryService(ICategoryRepostitory categoryRepostitory)
        {
            _categoryRepostitory = categoryRepostitory;
        }

        public async Task<List<GetCategoryDTO>> GetAllCategoriesAsync()
        {
            return await _categoryRepostitory.GetAllCategoriesAsync(); 
        }
        public async Task AddCategoryAsync(AddCategoryDTO categoryDTO)
        {
             await _categoryRepostitory.AddCategoryAsync(categoryDTO);
        }

        public async Task DeleteCategoryAsync(DeleteCategoryDTO category)
        {
            await _categoryRepostitory.DeleteCategoryAsync(category);
        }

        public async Task<UpdateCategoryDTO> GetCategoryForEdit(int id)
        {
            return await _categoryRepostitory.GetCategoryForEdit(id);
           
        }

        public async Task EditCategoryAsync(UpdateCategoryDTO category)
        {
            await _categoryRepostitory.EditCategoryAsync(category);
        }
    }
}
