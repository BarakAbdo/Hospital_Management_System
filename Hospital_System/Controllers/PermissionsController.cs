using Hospital_Management_System.Application.Dtos.PermissionsDtos;
using Hospital_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var permissions = _permissionService.GetAllPermissions();
            return View(permissions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatePermissionDto permission)
        {
            if (ModelState.IsValid)
            {
                _permissionService.AddPermission(permission);
                return RedirectToAction("Index");
            }
            return View(permission);
        }

        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var permission = _permissionService.GetPermissionByUId(uid);
            if (permission == null)
            {
                return NotFound();
            }

            var update = new UpdatePermissionDto
            {
                Id = permission.Id,
                UID = permission.UID,
                Name = permission.Name
            };
            return View(update);
        }

        [HttpPost]
        public IActionResult Edit(UpdatePermissionDto permission)
        {
            if (ModelState.IsValid)
            {
                _permissionService.UpdatePermission(permission);
                return RedirectToAction(nameof(Index));
            }
            return View(permission);
        }

        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var permission = _permissionService.GetPermissionByUId(uid);
            if (permission == null)
            {
                return NotFound();
            }
            return View(permission);
        }

        [HttpPost]
        public IActionResult Delete(PermissionDto permission)
        {
            _permissionService.DeletePermission(permission);
            return RedirectToAction(nameof(Index));
        }
    }
}