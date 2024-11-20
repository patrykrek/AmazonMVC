using AmazonMVC.DTO;
using AmazonMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmazonMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;   
        }

        public IActionResult Product()
        {
            return View();
        }

        
        public async Task<IActionResult> DisplayProducts()
        {
            var products = await _productService.DisplayProductsAsync();

            return View(products);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public async Task<IActionResult> AddProductsForm(AddProductDTO newProduct)
        {
            if (!ModelState.IsValid)
            {
                return View("AddProduct", newProduct);
            }


            var result = await _productService.AddProductAsync(newProduct);

            if (!result.Success)
            {                          
                    return View("CategoryNotAvailable", new { message = result.ErrorMessage });             
            }

            return RedirectToAction("DisplayProducts");
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var productForEdit = await _productService.GetProductForEdit(id);

            return View(productForEdit);
        }

        public async Task<IActionResult> EditProductForm(UpdateProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return View("EditProduct");
            }
            await _productService.EditProductAsync(product);

            return RedirectToAction("DisplayProducts");
        }

        public async Task<IActionResult> DeleteProduct(DeleteProductDTO deleteProduct)
        {
            await _productService.DeleteProductAsync(deleteProduct);

            return RedirectToAction("DisplayProducts");
        }

        public IActionResult CategoryNotAvailable(string message)
        {
            ViewData["ErrorMessage"] = message;
            
            return View();
        }

    }
}
