using AmazonMVC.Models;
using AmazonMVC.Validator;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonMVC.Tests.ValidatorTests
{
    public class RoleValidatorTest
    {
        private readonly RoleValidator _validator = new RoleValidator();

        [Fact]

        public void RoleValidator_EmailFieldIsEmpty_ShouldValidateThisField()
        {
            //arrange

            var roleEmail = new AssignRoleViewModel { Email = " " };
            //act

            var result = _validator.TestValidate(roleEmail);
            //assert

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }
    }
}
