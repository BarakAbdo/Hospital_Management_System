using Hospital_Management_System.Application.Dtos.PrescriptionsDtos;
using Hospital_Management_System.Application.Services.Base;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital_Management_System.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var prescriptions = _prescriptionService.GetAllPrescriptions();
            return View(prescriptions);
        }

        private void GetPatient()
        {
            IEnumerable<Patient> patients = _prescriptionService.GetAllPatients();
            SelectList patientSelectList = new SelectList(patients, "Id", "Name");
            ViewBag.patientSelectList = patientSelectList;
        }

        private void GetDoctor()
        {
            IEnumerable<Doctor> doctors = _prescriptionService.GetAllDoctors();
            SelectList doctorSelectList = new SelectList(doctors, "Id", "Name");
            ViewBag.doctorSelectList = doctorSelectList;
        }

        private void GetMedication()
        {
            IEnumerable<Medication> medications = _prescriptionService.GetAllMedications();
            SelectList medicationSelectList = new SelectList(medications, "Id", "Name");
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
        public IActionResult Create(CreatePrescriptionDto prescription)
        {
            if (ModelState.IsValid)
            {
                _prescriptionService.AddPrescription(prescription);
                return RedirectToAction("Index");
            }
            GetPatient();
            GetDoctor();
            GetMedication();

            return View(prescription);
        }

        [HttpGet]
        public IActionResult Edit(string uid)
        {
            GetPatient();
            GetDoctor();
            GetMedication();

            var prescription = _prescriptionService.GetPrescriptionByUId(uid);
            if (prescription == null)
            {
                return NotFound();
            }

            var update = new UpdatePrescriptionDto
            {
                Id = prescription.Id,
                UID = prescription.UID,
                Dosage = prescription.Dosage,
                Duration = prescription.Duration,
                PatientId = prescription.PatientId,
                DoctorId = prescription.DoctorId,
                MedicationId = prescription.MedicationId
            };
            return View(update);
        }

        [HttpPost]
        public IActionResult Edit(UpdatePrescriptionDto prescription)
        {
            if (ModelState.IsValid)
            {
                _prescriptionService.UpdatePrescription(prescription);
                return RedirectToAction("Index");
            }
            GetPatient();
            GetDoctor();
            GetMedication();
            return View(prescription);
        }

        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var prescription = _prescriptionService.GetPrescriptionByUId(uid);
            if (prescription == null)
            {
                return NotFound();
            }
            return View(prescription);
        }

        [HttpPost]
        public IActionResult Delete(PrescriptionDto prescription)
        {
            var oldPrescription = _prescriptionService.GetPrescriptionByUId(prescription.UID);
            if (oldPrescription == null)
            {
                return NotFound();
            }

            _prescriptionService.DeletePrescription(prescription);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageFiles(string uid)
        {
            var prescription = _prescriptionService.GetPrescriptionByUId(uid);
            if (prescription == null)
                return NotFound();

            var files = _prescriptionService.GetPrescriptionFiles(prescription.Id);

            ViewBag.PrescriptionDosage = prescription.Dosage;
            ViewBag.PrescriptionUid = prescription.UID;
            ViewBag.Files = files;

            PrescriptionFile prescriptionFile = new PrescriptionFile
            {
                PrescriptionId = prescription.Id
            };

            return View(prescriptionFile);
        }

        [HttpPost]
        public IActionResult ManageFiles(PrescriptionFile prescriptionFile, IFormFile filePrescription)
        {
            _prescriptionService.AddPrescriptionFile(prescriptionFile, filePrescription);

            var prescription = _prescriptionService.GetPrescriptionById(prescriptionFile.PrescriptionId);
            return RedirectToAction(nameof(ManageFiles), new { uid = prescription?.UID });
        }

        public IActionResult DeleteFile(int id, string uid)
        {
            _prescriptionService.DeletePrescriptionFile(id);
            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}