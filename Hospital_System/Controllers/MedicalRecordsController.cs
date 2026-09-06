using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class MedicalRecordsController : Controller
    {
        //Dependancy Injection
        private readonly AppDbContext _db;
        public MedicalRecordsController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index() 
        {
            //Entity Framework Approach
            IEnumerable<MedicalRecord> medicalRecords = _db.MedicalRecords.Include(e=>e.Patient).Include(e=>e.Doctor).ToList();
            return View(medicalRecords);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _db.MedicalRecords.Add(medicalRecord);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicalRecord);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var medicalRecord = _db.MedicalRecords.Find(Id);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            return View(medicalRecord);

        }
        [HttpPost]
        public IActionResult Edit(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _db.MedicalRecords.Update(medicalRecord);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicalRecord);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var medicalRecord = _db.MedicalRecords.Find(Id);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            return View(medicalRecord);
        }
        [HttpPost]
        public IActionResult Delete(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _db.MedicalRecords.Remove(medicalRecord);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicalRecord);
        }
    }
}
