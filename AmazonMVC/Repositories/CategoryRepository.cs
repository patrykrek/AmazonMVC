using AmazonMVC.Data;
using AmazonMVC.DTO;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmazonMVC.Repositories
{
    public class CategoryRepository : ICategoryRepostitory
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetCategoryDTO>> GetAllCategoriesAsync()
        {
            var getDb = await _context.Category.ToListAsync();

            var getCategory = getDb.Select(c => _mapper.Map<GetCategoryDTO>(c)).ToList();

            return getCategory;
        }

        public async Task AddCategoryAsync(AddCategoryDTO categoryDTO)
        {
            var newcat = new Category
            {
                Name = categoryDTO.Name
            };

            await _context.Category.AddAsync(newcat);

            await _context.SaveChangesAsync();

        }

        public async Task DeleteCategoryAsync(DeleteCategoryDTO category)
        {
            var findCategory = await _context.Category.FindAsync(category.Id);

            if (findCategory != null)
            {
                _context.Category.Remove(findCategory);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<UpdateCategoryDTO> GetCategoryForEdit(int id)
        {
            var getCategory = await _context.Category.FindAsync(id);

            return new UpdateCategoryDTO
            {
                Name = getCategory.Name,
            };

        }

        public async Task EditCategoryAsync(UpdateCategoryDTO category)
        {
            var findCategory = await _context.Category.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (findCategory != null)
            {
                findCategory.Name = category.Name;

                await _context.SaveChangesAsync();
            }
        }

    }
}
