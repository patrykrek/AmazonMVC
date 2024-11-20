using AmazonMVC.DTO;
using AmazonMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmazonMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Category()
        {
            return View();
        }

        public async Task<IActionResult> DisplayCategory()
        {
            var getCategories = await _categoryService.GetAllCategoriesAsync();

            return View(getCategories); 
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        public async Task<IActionResult> AddCategoryForm(AddCategoryDTO newCategory)
        {
            if (!ModelState.IsValid)
            {
                return View("AddCategory", newCategory);
            }
            await _categoryService.AddCategoryAsync(newCategory);

            return RedirectToAction("DisplayCategory");
        }

        public async Task<IActionResult> DeleteCategory(DeleteCategoryDTO category)
        {
            await _categoryService.DeleteCategoryAsync(category);

            return RedirectToAction("DisplayCategory");
        }

        public async Task<IActionResult> EditCategory(int id)
        {
            await _categoryService.GetCategoryForEdit(id);

            return View();
        }

        public async Task<IActionResult> EditCategoryForm(UpdateCategoryDTO category)
        {
            if (!ModelState.IsValid)
            {
                return View("EditCategory", category);
            }

            await _categoryService.EditCategoryAsync(category);

            return RedirectToAction("DisplayCategory");
        }
    }
}
