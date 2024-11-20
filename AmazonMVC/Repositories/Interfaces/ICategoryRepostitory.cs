using AmazonMVC.DTO;

namespace AmazonMVC.Repositories.Interfaces
{
    public interface ICategoryRepostitory
    {
        Task<List<GetCategoryDTO>> GetAllCategoriesAsync();
        Task AddCategoryAsync(AddCategoryDTO categoryDTO);
        Task DeleteCategoryAsync(DeleteCategoryDTO deleteCategory);
        Task<UpdateCategoryDTO> GetCategoryForEdit(int id);
        Task EditCategoryAsync(UpdateCategoryDTO category);
    }
}
