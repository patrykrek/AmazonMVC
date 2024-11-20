using AmazonMVC.DTO;
using AmazonMVC.Models;

namespace AmazonMVC.Interfaces
{
    public interface IProductService
    {
        Task<List<GetProductDTO>> DisplayProductsAsync();
        Task<Result> AddProductAsync(AddProductDTO productDTO);
        Task DeleteProductAsync(DeleteProductDTO id);
        Task EditProductAsync(UpdateProductDTO updateProductDTO);
        Task<UpdateProductDTO> GetProductForEdit(int id);
        


    }
}
