using Hospital_Management_System.Application.Dtos.RolesDtos;
using Hospital_Management_System.Application.Services.Base;
using Hospital_Management_System.Domain.Models;
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

        [HttpGet]
        public IActionResult Index()
        {
            var roles = _roleService.GetAllRoles();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRoleDto role)
        {
            if (ModelState.IsValid)
            {
                _roleService.AddRole(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var role = _roleService.GetRoleByUId(uid);
            if (role == null)
            {
                return NotFound();
            }

            var update = new UpdateRoleDto
            {
                Id = role.Id,
                UID = role.UID,
                Name = role.Name
            };
            return View(update);
        }

        [HttpPost]
        public IActionResult Edit(UpdateRoleDto role)
        {
            if (ModelState.IsValid)
            {
                _roleService.UpdateRole(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var role = _roleService.GetRoleByUId(uid);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost]
        public IActionResult Delete(RoleDto role)
        {
            var oldRole = _roleService.GetRoleByUId(role.UID);
            if (oldRole == null)
            {
                return NotFound();
            }

            _roleService.DeleteRole(role);
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