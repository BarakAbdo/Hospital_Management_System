using Hospital_Management_System.Models;
using Hospital_System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class RolesController : Controller
    {
        private readonly AppDbContext _db;

        public RolesController(AppDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Role> roles = _db.Roles.ToList();
            return View(roles);
        }

        // =========================
        // Create
        // =========================
        [HttpPost]
        public IActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _db.Roles.Add(role);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // =========================
        // Edit
        // =========================
        [HttpPost]
        public IActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                _db.Roles.Update(role);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // =========================
        // Delete
        // =========================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var role = _db.Roles.Find(id);

            if (role != null)
            {
                _db.Roles.Remove(role);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public IActionResult AssignPermissions(int roleId)
        {
            var role = _db.Roles.Find(roleId);

            if (role == null)
            {
                return NotFound();
            }

            var allPermissions = _db.Permissions.ToList();

            var assignedPermissions = _db.PermissionRoles
                .Where(pr => pr.RoleId == roleId)
                .Select(pr => pr.PermissionId)
                .ToList();

            ViewBag.AllPermissions = allPermissions;
            ViewBag.AssignedPermissions = assignedPermissions;

            return View(role);
        }

        // POST - Update Permissions
        [HttpPost]
        public IActionResult AssignPermissions(int roleId, List<int> permissionIds)
        {
            var role = _db.Roles.Find(roleId);

            if (role == null)
            {
                return NotFound();
            }


            // 1. Get old permissions
            var oldPermissions = _db.PermissionRoles
                .Where(pr => pr.RoleId == roleId)
                .ToList();


            // 2. Remove old permissions
            _db.PermissionRoles.RemoveRange(oldPermissions);


            // 3. Add selected permissions
            foreach (var permissionId in permissionIds )
            {
                var permissionRole = new PermissionRole
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                };

                _db.PermissionRoles.Add(permissionRole);
            }


            // 4. Save
            _db.SaveChanges();


            return RedirectToAction("AssignPermissions", new { roleId = roleId });
        }

    }
}
