using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public void GetPatient() 
        {
            IEnumerable<Patient> patients = _db.Patients.ToList();
            SelectList patientSelectList = new SelectList(patients,"Id","Name");
            ViewBag.patientSelectList = patientSelectList;
        }

        public void GetDoctor() 
        {
            IEnumerable<Doctor> doctors = _db.Doctors.ToList();
            SelectList doctorSelectList = new SelectList(doctors,"Id","Name");
            ViewBag.doctorSelectList = doctorSelectList;
        }
        [HttpGet]
        public IActionResult Create()
        {
            GetPatient();
            GetDoctor();
            
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
            GetPatient();
            GetDoctor();

            return View(medicalRecord);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            GetPatient();
            GetDoctor();
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
            GetPatient();
            GetDoctor();
            return View(medicalRecord);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            GetPatient();
            GetDoctor();
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
            var medicalRecords = _db.MedicalRecords.Find(medicalRecord.Id);   
            if (medicalRecords == null)
            {
                return NotFound();
            }
            _db.MedicalRecords.Remove(medicalRecords);
            _db.SaveChanges();
            return RedirectToAction("Index");
            
           
        }
    }
}
