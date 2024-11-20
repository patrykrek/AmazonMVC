using AmazonMVC.Data;
using AmazonMVC.DTO;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmazonMVC.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetProductDTO>> DisplayProductsAsync()
        {
            var getDb = await _context.Products.ToListAsync();

            var getProducts = getDb.Select(p => _mapper.Map<GetProductDTO>(p)).ToList();

            return getProducts;
        }

        public async Task<Result> AddProductAsync(AddProductDTO productDTO)
        {
            var categoryExists = await _context.Category.FirstOrDefaultAsync(c => c.Id == productDTO.CategoryId);

            if (categoryExists == null)
            {
                return new Result { Success = false, ErrorMessage = $"Category with ID: {productDTO.CategoryId} not found. Try again" };
            }

            var newProduct = new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                StockQuantity = productDTO.StockQuantity,
                CategoryId = productDTO.CategoryId,
            };
         
            await _context.Products.AddAsync(newProduct);

            await _context.SaveChangesAsync();

            return new Result { Success = true };
        }

        public async Task DeleteProductAsync(DeleteProductDTO product)
        {
            var findProduct = await _context.Products.FindAsync(product.Id);

            if (findProduct != null)
            {
                _context.Products.Remove(findProduct);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<UpdateProductDTO> GetProductForEdit(int id)
        {
            var getProduct = await _context.Products.FindAsync(id);

            return new UpdateProductDTO
            {
                Name = getProduct.Name,
                Price = getProduct.Price,
                StockQuantity = getProduct.StockQuantity,
                CategoryId = getProduct.CategoryId,
            };
        }

        public async Task EditProductAsync(UpdateProductDTO updateProductDTO)
        {
            var findProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == updateProductDTO.Id);

            if (findProduct != null)
            {
                findProduct.Name = updateProductDTO.Name;
                findProduct.Price = updateProductDTO.Price;
                findProduct.StockQuantity = updateProductDTO.StockQuantity;
                findProduct.CategoryId = updateProductDTO.CategoryId;

                await _context.SaveChangesAsync();
            }
        }
    }
}
