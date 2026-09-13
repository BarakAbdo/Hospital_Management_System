using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public void GetPatient() 
        {
            IEnumerable<Patient> patient = _db.Patients.ToList();
            SelectList patientSelectList = new SelectList(patient,"Id","Name");
            ViewBag.patientSelectList = patientSelectList;
        }

        public void GetDoctor() 
        { 
            IEnumerable<Doctor> doctors = _db.Doctors.ToList();
            SelectList doctorSelectList = new SelectList(doctors, "Id", "Name");
            ViewBag.doctorSelectList = doctorSelectList;
        }

        public void GetMedication() 
        {
            IEnumerable<Medication> medications = _db.Medications.ToList();
            SelectList medicationSelectList = new SelectList(medications,"Id", "Name"); 
            ViewBag.medicationSelectList = medicationSelectList;
        }
        [HttpGet]
        public IActionResult Create()
        {
            GetPatient();
            GetDoctor();
            GetMedication();

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
            GetPatient();
            GetDoctor();
            GetMedication();

            return View(prescription);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            GetPatient();
            GetDoctor();
            GetMedication();
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
            GetPatient();
            GetDoctor();
            GetMedication();
            return View(prescription);

        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            GetPatient();
            GetDoctor();
            GetMedication();
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

            GetDoctor();
            GetPatient();
            var prescriptions = _db.Prescriptions.Find(prescription.Id);
            if (prescriptions == null)
            {
                return NotFound();
            }

            _db.Prescriptions.Remove(prescriptions);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
