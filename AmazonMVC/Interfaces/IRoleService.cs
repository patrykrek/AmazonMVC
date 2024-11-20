using AmazonMVC.Models;

namespace AmazonMVC.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> DisplayRoles();
        Task AssignRole(Guid RoleId,string Email);
    }
}
