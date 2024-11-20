using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AmazonMVC.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task AssignRole(Guid RoleId, string Email)
        {
            var findUser = await _userManager.FindByEmailAsync(Email);

            if (findUser == null)
            {
                throw new ArgumentException($"User not found");
            }

            var findRole = await _roleManager.FindByIdAsync(RoleId.ToString());

            if (findRole == null)
            {
                throw new ArgumentException($"Role not found");
            }

            if (!await _userManager.IsInRoleAsync(findUser, findRole.Name))
            {
                await _userManager.AddToRoleAsync(findUser, findRole.Name);
            }



        }

        public async Task<List<Role>> DisplayRoles()
        {
            var getRoles = await _roleManager.Roles.Select(r => new Role { Id = Guid.Parse(r.Id), Name = r.Name }).ToListAsync();

            return getRoles;
        }
    }
}
