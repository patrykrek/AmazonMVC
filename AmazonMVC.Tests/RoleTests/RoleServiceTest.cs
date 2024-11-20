using AmazonMVC.Interfaces;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using AmazonMVC.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonMVC.Tests.RoleTests
{
    public class RoleServiceTest
    {
        private readonly Mock<IRoleRepository> _mockReposistory;
        private readonly RoleService _roleService;

        public RoleServiceTest()
        {
            _mockReposistory = new Mock<IRoleRepository>();
            _roleService = new RoleService(_mockReposistory.Object);
        }

        [Fact]
        public async Task AssignRole_WhenCalled_ShouldAssignRoleToUser()
        {
            //arrange
            var roleId = Guid.NewGuid();

            var Email = "test@test.com";

            _mockReposistory.Setup(repo => repo.AssignRole(roleId,Email))
                .Returns(Task.CompletedTask);

            //act

            await _roleService.AssignRole(roleId, Email);

            //assert

            _mockReposistory.Verify(repo => repo.AssignRole(roleId, Email), Times.Once());
        }

        [Fact]
        public async Task DisplayRoles_WhenCalled_ShouldDisplayAllRoles()
        {
            //arrange

            var rolesList = new List<Role>
            {
                new Role { Name = "Role1"},

                new Role { Name = "Role2" }
            };

            _mockReposistory.Setup(repo => repo.DisplayRoles())
                .ReturnsAsync(rolesList);
            
            //act

            var result = await _roleService.DisplayRoles();

            //assert

            Assert.NotNull(result);
            Assert.Equal(rolesList.Count, result.Count);
            Assert.Equal("Role1", result[0].Name);
            Assert.Equal("Role2", result[1].Name);
        }
    }
}
