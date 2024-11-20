using AmazonMVC.Models;
using FluentValidation;

namespace AmazonMVC.Validator
{
    public class RoleValidator : AbstractValidator<AssignRoleViewModel>
    {
        public RoleValidator()
        {
            RuleFor(r => r.Email).NotEmpty();
        }
    }
}
