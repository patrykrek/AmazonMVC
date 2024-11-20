using AmazonMVC.Data;
using AmazonMVC.DTO;
using AmazonMVC.Interfaces;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace AmazonMVC.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<GetProductDTO>> DisplayProductsAsync()
        {
           return await _productRepository.DisplayProductsAsync();
        }
        public async Task<Result> AddProductAsync(AddProductDTO productDTO)
        {
            return await _productRepository.AddProductAsync(productDTO);

        }

        public async Task DeleteProductAsync(DeleteProductDTO product)
        {
            await _productRepository.DeleteProductAsync(product);
           
        }
        public async Task<UpdateProductDTO> GetProductForEdit(int id)
        {
            return await _productRepository.GetProductForEdit(id);
        }
        public async Task EditProductAsync(UpdateProductDTO updateProductDTO)
        {
            await _productRepository.EditProductAsync(updateProductDTO);
        }

        
    }
}
