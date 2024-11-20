using AmazonMVC.DTO;
using AmazonMVC.Models;
using AmazonMVC.Validator;
using FluentValidation.TestHelper;

namespace AmazonMVC.Tests.ValidatorTests;

public class CategoryValidatorTest
{

    private readonly CategoryValidator _validator;

    public CategoryValidatorTest()
    {
        _validator = new CategoryValidator();
    }

    [Fact]
    public void CategoryValidator_NameFieldIsEmpty_ShouldValidateIt()
    {
        //arrange

        var category = new AddCategoryDTO { Name = " " };

        //act

        var result = _validator.TestValidate(category);


        //assert

        result.ShouldHaveValidationErrorFor(c => c.Name);

    }



}