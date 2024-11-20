using AmazonMVC.Interfaces;
using AmazonMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmazonMVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> DisplayRoles()
        {
            var getRoles = await _roleService.DisplayRoles();

            return View(getRoles);
        }

        public IActionResult AssignRole(Guid RoleId, string RoleName)
        {          
            var model = new AssignRoleViewModel
            {
                RoleId = RoleId,
               
            };

            ViewBag.RoleName = RoleName;    

            return View(model);
        }

        public async Task<IActionResult> AssignRoleToUserForm(AssignRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AssignRole",model);
            }
            await _roleService.AssignRole(model.RoleId, model.Email);

            return RedirectToAction("DisplayRoles");
        }


    }
}
