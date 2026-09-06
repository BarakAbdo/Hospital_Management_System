using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Hospital_System.Controllers
{
    public class MedicationsController : Controller
    {
        //Dependency Injection
        private readonly AppDbContext _db;
        public MedicationsController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index() 
        {
            //Entity Framework Approach
            IEnumerable<Medication> medications = _db.Medications.ToList();
            return View(medications);
        }
        [HttpGet]
        public IActionResult Create() 
        {
        return View();  
        }
        [HttpPost]
        public IActionResult Create(Medication medication) 
        {
            if (ModelState.IsValid) 
            {
                _db.Medications.Add(medication);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medication);
        }
        [HttpGet]
        public IActionResult Edit(int Id) 
        {
            var medication = _db.Medications.Find(Id);
            if (medication == null) 
            {
                return NotFound();
            }
            return View(medication); 
        }
        [HttpPost]
        public IActionResult Edit(Medication medication) 
        {
            if (ModelState.IsValid) 
            {
                _db.Medications.Update(medication);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medication);

        }
        [HttpGet]
        public IActionResult Delete(int Id) 
        {
            var medication = _db.Medications.Find(Id);
            if (medication == null) 
            {
                return NotFound();
            }
            return View(medication);
        }
        [HttpPost]
        public IActionResult Delete(Medication medication) 
        {
            if (ModelState.IsValid)
            {
                _db.Medications.Remove(medication);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medication);
        }
    }
}
