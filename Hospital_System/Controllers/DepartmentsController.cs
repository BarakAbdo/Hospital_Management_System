using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System.Controllers
{
    public class DepartmentsController : Controller
    {
        //Dependency Injection 

        private readonly AppDbContext _db;
        public DepartmentsController(AppDbContext db)
        {
            _db = db;

        }

        [HttpGet]
        public IActionResult Index()
        {
            //Entity Framework Approach      
            IEnumerable<Department> departments = _db.Departments.ToList();
            return View(departments);

        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department) 
        {
            if (ModelState.IsValid) 
            {
                _db.Departments.Add(department);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Edit(int Id) 
        {
            var department = _db.Departments.Find(Id);
            if (department == null) 
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost]
        public IActionResult Edit(Department department) 
        {
            if (ModelState.IsValid) 
            {
                _db.Departments.Update(department);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Delete(int Id) 
        {
            var department = _db.Departments.Find(Id);
            if (department == null) 
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(Department department) 
        {
            if (ModelState.IsValid) 
            {
                _db.Departments.Remove(department);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }
    }
}
