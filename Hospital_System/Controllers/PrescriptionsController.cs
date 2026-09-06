using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class PrescriptionsController : Controller
    {
        //Dependancy Injaction
        private readonly AppDbContext _db;
        public PrescriptionsController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index() 
        {
            //Entity Framework Approach
            IEnumerable<Prescription>prescriptions = _db.Prescriptions.Include(e=>e.Patient)
                .Include(e=>e.Doctor)
                .Include(e=>e.Medication).ToList();
            return View(prescriptions);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _db.Prescriptions.Add(prescription);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prescription);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var prescription = _db.Prescriptions.Find(Id);
            if (prescription == null)
            {
                return NotFound();
            }
            return View(prescription);
        }
        [HttpPost]
        public IActionResult Edit(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _db.Prescriptions.Update(prescription);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prescription);

        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var prescription = _db.Prescriptions.Find(Id);
            if (prescription == null)
            {
                return NotFound();
            }
            return View(prescription);
        }
        [HttpPost]
        public IActionResult Delete(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _db.Prescriptions.Remove(prescription);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prescription);
        }
    }
}
