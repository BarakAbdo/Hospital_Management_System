using Hospital_Management_System.Models;
using Hospital_System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly AppDbContext _db;
        public PermissionsController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Permission> permissions = _db.Permissions.ToList();
            return View(permissions);
        }

        // =========================
        // Create
        // =========================
        [HttpPost]
        public IActionResult Create(Permission permission)
        {
            if (ModelState.IsValid)
            {
                _db.Permissions.Add(permission);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // =========================
        // Edit
        // =========================
        [HttpPost]
        public IActionResult Edit(Permission permission)
        {
            if (ModelState.IsValid)
            {
                _db.Permissions.Update(permission);
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
            var permission = _db.Permissions.Find(id);
            if (permission != null)
            {
                _db.Permissions.Remove(permission);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
