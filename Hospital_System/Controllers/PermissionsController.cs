

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

        public IActionResult Index()
        {
            var permissions = _permissionService.GetAllPermissions();
            return View(permissions);
        }

   
        [HttpPost]
        public IActionResult Create(CreatePermissionDto permission)
        {
            if (ModelState.IsValid)
            {
                _permissionService.AddPermission(permission);
            }
            return RedirectToAction("Index");
        }


    
        [HttpPost]
        public IActionResult Edit(UpdatePermissionDto permission)
        {
            if (ModelState.IsValid)
            {
                _permissionService.UpdatePermission(permission);
            }
            return RedirectToAction(nameof(Index));
        }


      
        [HttpPost]
        public IActionResult Delete(string UID)
        {

            _permissionService.DeletePermission(UID);
            return RedirectToAction(nameof(Index));
        }
    }
}
