using AmazonMVC.Data;
using AmazonMVC.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AmazonMVC.Validator
{
    public class ProductValidator : AbstractValidator<AddProductDTO>
    {
        
        public ProductValidator()
        {
          
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price can't be less than 0");

            RuleFor(p => p.StockQuantity)
                .GreaterThan(0).WithMessage("Stock quantity can't be less than 0");

            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("Vaule can't be less than 0");
                
        }
        
    }
}
