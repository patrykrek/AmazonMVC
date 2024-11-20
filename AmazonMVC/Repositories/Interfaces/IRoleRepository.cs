using AmazonMVC.Models;

namespace AmazonMVC.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> DisplayRoles();
        Task AssignRole(Guid RoleId, string Email);
    }
}
