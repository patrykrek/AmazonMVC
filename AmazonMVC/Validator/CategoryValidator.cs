using AmazonMVC.DTO;
using FluentValidation;

namespace AmazonMVC.Validator
{
    public class CategoryValidator : AbstractValidator<AddCategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(cat => cat.Name).NotEmpty();
        }
    }
}
