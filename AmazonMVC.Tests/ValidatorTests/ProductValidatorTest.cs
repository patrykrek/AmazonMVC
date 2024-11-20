using AmazonMVC.DTO;
using AmazonMVC.Validator;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonMVC.Tests.ValidatorTests
{
    public class ProductValidatorTest
    {
        private readonly ProductValidator _validator = new ProductValidator();

        [Fact]

        public void ProductValidator_NameFieldIsEmpty_ShouldReturnErrorMessage()
        {
            //arrange
            var productName = new AddProductDTO { Name = " " };

            //act

            var result = _validator.TestValidate(productName);
            //assert

            result.ShouldHaveValidationErrorFor(p => p.Name)
                .WithErrorMessage("Name is required");


        }

        [Fact]

        public void ProductValidator_PriceFieldIsLessThan0_ShouldReturnErrorMessage()
        {
            //arrange

            var productPrice = new AddProductDTO { Price = -100 };

            //act

            var result = _validator.TestValidate(productPrice);
            //assert

            result.ShouldHaveValidationErrorFor(p => p.Price)
                .WithErrorMessage("Price can't be less than 0");
        }

        [Fact]
        public void ProductValidator_StockQuantityFieldIsLessThan0_ShouldReturnErrorMessage()
        {
            //arrange
            var productStockQuantity = new AddProductDTO { StockQuantity = -111 };

            //act

            var result = _validator.TestValidate(productStockQuantity);
            //assert

            result.ShouldHaveValidationErrorFor(p => p.StockQuantity)
                .WithErrorMessage("Stock quantity can't be less than 0");
        }

        [Fact]
        public void ProductValidator_CategoryIdFieldIsLessThan0_ShouldReturnErrorMessage()
        {
            //arrange
            var productCategoryId = new AddProductDTO { CategoryId = -1 };

            //act
            var result = _validator.TestValidate(productCategoryId);
            //assert
            result.ShouldHaveValidationErrorFor(p => p.CategoryId)
                .WithErrorMessage("Vaule can't be less than 0");
        }
    }
}
