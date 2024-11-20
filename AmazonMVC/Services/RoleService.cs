using AmazonMVC.Interfaces;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AmazonMVC.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        
        public async Task AssignRole(Guid RoleId, string Email)
        {
            await _roleRepository.AssignRole(RoleId, Email);                  
        }

        public async Task<List<Role>> DisplayRoles()
        {
            return await _roleRepository.DisplayRoles();
        }
    }
}
