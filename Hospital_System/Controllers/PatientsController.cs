using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System.Controllers
{
    public class PatientsController : Controller
    {

        //Dependency Injection 
        
        private readonly AppDbContext _db;
        public PatientsController(AppDbContext db)
        {
            _db = db;

        }
        [HttpGet]
        public IActionResult Index()
        {
            //Entity Framework Approach      
            IEnumerable<Patient> patients = _db.Patients.ToList();
            return View(patients);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _db.Patients.Add(patient);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var patient = _db.Patients.Find(Id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _db.Patients.Update(patient);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);

        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var patient = _db.Patients.Find(Id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        [HttpPost]
        public IActionResult Delete(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _db.Patients.Remove(patient);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }
    }
}
