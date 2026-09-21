using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Dtos.PermissionsDtos;
using Hospital_System.Dtos.RolesDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.MedicalRecordRepo;
using Hospital_System.Repositories.RoleRepo;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class RolesController : Controller
    {

        private readonly IRoleRepository _repo;

        public RolesController(IRoleRepository repo)
        {
            _repo = repo;
        }

        //private readonly AppDbContext _db;

        //public RolesController(AppDbContext db)
        //{
        //    _db = db;
        //}


        public IActionResult Index()
        {
            IEnumerable<Role> roles = _repo.GetAll();
            var role = _repo.GetAll().Select(r => new RoleDto
            {
                Id = r.Id,
                UID = r.UID,
                Name = r.Name

            }).ToList();
            return View(role);
        }

        // =========================
        // Create
        // =========================
        [HttpPost]
        public IActionResult Create(CreateRoleDto role)
        {
            if (ModelState.IsValid)
            {
                //Mapping 
                var rol = new Role
                {
                    UID = Guid.NewGuid().ToString(),
                    Name = role.Name,

                };

                _repo.Add(rol);
                _repo.Save();

                //_db.Roles.Add(rol);
                //_db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // =========================
        // Edit
        // =========================
        [HttpPost]
        public IActionResult Edit(UpdateRoleDto role)
        {
            if (ModelState.IsValid)
            {

                if (role.UID == null)
                    role.UID = Guid.NewGuid().ToString();

                //Mapping 
                var rol = new Role
                {
                    UID = role.UID,
                    Name = role.Name,
                    Id = role.Id
                };

                _repo.Update(rol);
                _repo.Save();

                //_db.Roles.Update(rol);
                //_db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // =========================
        // Delete
        // =========================
        [HttpPost]
        public IActionResult Delete(Role role)
        {
            var oldRole = _repo.GetByUId(role.UID);
            //var oldRole = _repo.Roles.FirstOrDefault(r => r.UID == role.UID);
            if (oldRole == null)
            {

                return NotFound();
                //_db.Roles.Remove(role);
                //_db.SaveChanges();
            }
            _repo.Delete(oldRole);
            _repo.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AssignPermissions(string uid)
        {
            var role = _repo.GetByUId(uid);

            if (role == null)
            {
                return NotFound();
            }

            var allPermissions = _repo.Permissions.ToList();

            var assignedPermissions = _repo.PermissionRoles
                .Where(pr => pr.RoleId == role.Id)
                .Select(pr => pr.PermissionId)
                .ToList();

            ViewBag.AllPermissions = allPermissions;
            ViewBag.AssignedPermissions = assignedPermissions;

            return View(role);
        }

        // POST - Update Permissions
        [HttpPost]
        public IActionResult AssignPermissions(string uid, List<int> permissionIds)
        {
            var role = _repo.GetByUId(uid);

            if (role == null)
            {
                return NotFound();
            }

            _repo.UpdatePermissions(role.Id, permissionIds);
            _repo.Save();

            //return RedirectToAction("Index");
            return RedirectToAction("AssignPermissions", new { uid = uid });
        }

    }
}
