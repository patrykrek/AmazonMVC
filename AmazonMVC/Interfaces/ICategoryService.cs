using AmazonMVC.DTO;

namespace AmazonMVC.Interfaces
{
    public interface ICategoryService
    {
        Task<List<GetCategoryDTO>> GetAllCategoriesAsync();
        Task AddCategoryAsync(AddCategoryDTO categoryDTO);
        Task DeleteCategoryAsync(DeleteCategoryDTO deleteCategory);
        Task<UpdateCategoryDTO> GetCategoryForEdit(int id);
        Task EditCategoryAsync(UpdateCategoryDTO category);
    }
}
