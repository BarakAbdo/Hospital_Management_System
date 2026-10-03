using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Dtos.PatientsDtos;
using Hospital_System.Dtos.PermissionsDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;
using Hospital_System.Repositories.MedicalRecordRepo;
using Hospital_System.Repositories.PermissionRepo;
using Hospital_System.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
