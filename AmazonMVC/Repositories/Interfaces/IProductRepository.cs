using AmazonMVC.DTO;
using AmazonMVC.Models;

namespace AmazonMVC.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<GetProductDTO>> DisplayProductsAsync();
        Task<Result> AddProductAsync(AddProductDTO productDTO);
        Task DeleteProductAsync(DeleteProductDTO product);
        Task<UpdateProductDTO> GetProductForEdit(int id);
        Task EditProductAsync(UpdateProductDTO updateProductDTO);
    }
}
