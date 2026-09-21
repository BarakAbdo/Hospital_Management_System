using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Dtos.PatientsDtos;
using Hospital_System.Dtos.PermissionsDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.MedicalRecordRepo;
using Hospital_System.Repositories.PermissionRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Controllers
{
    public class PermissionsController : Controller
    {

        private readonly IPermissionRepository _repo;

        public PermissionsController(IPermissionRepository repo)
        {
            _repo = repo;
            
        }

        //private readonly AppDbContext _db;
        //public PermissionsController(AppDbContext db)
        //{
        //    _db = db;
        //}
        public IActionResult Index()
        {
            //IEnumerable<Permission> permissions = _repo.GetAll();
            var permission = _repo.GetAll().Select(p => new PermissionDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name
               
            });
            return View(permission);
        }

        // =========================
        // Create
        // =========================
        [HttpPost]
        public IActionResult Create(CreatePermissionDto permission)
        {
            if (ModelState.IsValid)
            {
                //Mapping
                var per = new Permission
                {
                    UID = Guid.NewGuid().ToString(),
                    Name = permission.Name
                    
                };

                _repo.Add(per);
                _repo.Save();

                //_db.Permissions.Add(per);
                //_db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // =========================
        // Edit
        // =========================
        [HttpPost]
        public IActionResult Edit(UpdatePermissionDto permission)
        {

            var oldPermission = _repo.GetById(permission.Id);
            if (oldPermission == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(permission.UID))
            {
                permission.UID = Guid.NewGuid().ToString();
            }

            oldPermission.Name = permission.Name;
            oldPermission.UID = permission.UID;

            _repo.Update(oldPermission);
            _repo.Save();

            //_db.Permissions.Update(per);
            //_db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // Delete
        // =========================
        [HttpPost]
        public IActionResult Delete(string UID)
        {
           
            var oldPermission = _repo.GetByUId(UID);

            if (oldPermission == null)
            {
                return NotFound();
            }

            _repo.Delete(oldPermission);
            _repo.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
