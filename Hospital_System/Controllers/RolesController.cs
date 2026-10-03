using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Dtos.PermissionsDtos;
using Hospital_System.Dtos.RolesDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;
using Hospital_System.Repositories.MedicalRecordRepo;
using Hospital_System.Repositories.RoleRepo;
using Hospital_System.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class RolesController : Controller
    {

        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            var roles = _roleService.GetAllRoles();
            return View(roles);
        }

        [HttpPost]
        public IActionResult Create(CreateRoleDto role)
        {
            if (ModelState.IsValid)
            {
                _roleService.AddRole(role);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(UpdateRoleDto role)
        {
            if (ModelState.IsValid)
            {
                _roleService.UpdateRole(role);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Role role)
        {
            var oldRole = _roleService.GetRoleByUId(role.UID);
            if (oldRole == null)
            {
                return NotFound();
            }

            _roleService.DeleteRole(role.UID);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AssignPermissions(string uid)
        {
            var role = _roleService.GetRoleByUId(uid);
            if (role == null)
            {
                return NotFound();
            }

            var allPermissions = _roleService.GetAllPermissions();
            var assignedPermissions = _roleService.GetAssignedPermissionIds(role.Id);

            ViewBag.AllPermissions = allPermissions;
            ViewBag.AssignedPermissions = assignedPermissions;

            return View(role);
        }

        // POST - Update Permissions
        [HttpPost]
        public IActionResult AssignPermissions(string uid, List<int> permissionIds)
        {
            var role = _roleService.GetRoleByUId(uid);
            if (role == null)
            {
                return NotFound();
            }

            _roleService.UpdateRolePermissions(role.Id, permissionIds);

            return RedirectToAction("AssignPermissions", new { uid = uid });
        }

    }
}
