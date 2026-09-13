using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class DoctorsController : Controller
    {
        //Dependency Injection 

        private readonly AppDbContext _db;
        public DoctorsController(AppDbContext db)
        {
            _db = db;

        }
        [HttpGet]
        public IActionResult Index()
        {
            //Entity Framework Approach      
            IEnumerable<Doctor> doctors = _db.Doctors.Include(e=>e.Department).ToList();
            return View(doctors);

        }

        public void GetDepartments() 
        {
            IEnumerable<Department> departments = _db.Departments.ToList();
            SelectList departmentSelectList = new SelectList(departments,"Id","Name");
            ViewBag.departmentSelectList = departmentSelectList;
        }
        [HttpGet]
        public IActionResult Create() 
        {
            GetDepartments();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor) 
        {
            if (ModelState.IsValid) 
            {
                _db.Doctors.Add(doctor);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetDepartments();
            return View(doctor);
        }
        [HttpGet]
        public IActionResult Edit(int Id) 
        {
            GetDepartments();
            var doctor = _db.Doctors.Find(Id);
            if (doctor == null) 
            {
                return NotFound();
            }
            return View(doctor);
            
        }
        [HttpPost]
        public IActionResult Edit(Doctor doctor) 
        {
            if (ModelState.IsValid) 
            {
                _db.Doctors.Update(doctor);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetDepartments(); 
            return View(doctor);
        }
        [HttpGet]
        public IActionResult Delete(int Id) 
        {
            GetDepartments();
            var doctor = _db.Doctors.Find(Id);
            if (doctor == null) 
            {
                
                return NotFound();
            }
            return View(doctor);
        }
        [HttpPost]
        public IActionResult Delete(Doctor doctor) 
        {
            if (ModelState.IsValid) 
            {
                _db.Doctors.Remove(doctor);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetDepartments();
            return View(doctor);
        }
    }
}
