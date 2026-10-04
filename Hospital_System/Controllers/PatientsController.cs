

using Hospital_Management_System.Application.Dtos.PatientsDtos;
using Hospital_Management_System.Application.Services.Base;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var patients = _patientService.GetAllPatients();
            return View(patients);

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreatePatientDto patient)
        {
            if (ModelState.IsValid)
            {
                _patientService.AddPatient(patient);
                return RedirectToAction("Index");
            }
            return View(patient);
        }


        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var patient = _patientService.GetPatientByUId(uid);
            if (patient == null)
            {
                return NotFound();
            }

            var update = new UpdatePatientDto
            {
                Id = patient.Id,
                UID = patient.UID,
                Name = patient.Name,
                Gender = patient.Gender,
                Phone = patient.Phone,
                DateOfBirth = patient.DateOfBirth
            };
            return View(update);
        }



        [HttpPost]
        public IActionResult Edit(UpdatePatientDto patient)
        {
            if (ModelState.IsValid)
            {
                _patientService.UpdatePatient(patient);
                return RedirectToAction("Index");
            }
            return View(patient);
        }


        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var patient = _patientService.GetPatientByUId(uid);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        public IActionResult Delete(Patient patient)
        {
            var oldPatient = _patientService.GetPatientByUId(patient.UID);
            if (oldPatient == null)
            {
                return NotFound();
            }

            _patientService.DeletePatient(oldPatient);
            return RedirectToAction("Index");

        }

        public IActionResult ManageFiles(string uid)
        {
            var patient = _patientService.GetPatientByUId(uid);
            if (patient == null)
                return NotFound();

            var files = _patientService.GetPatientFiles(patient.Id);

            ViewBag.PatientName = patient.Name;
            ViewBag.Files = files;

            PatientFile patientFile = new PatientFile();
            patientFile.PatientId = patient.Id;

            return View(patientFile);
        }


        [HttpPost]
        public IActionResult ManageFiles(PatientFile patientFile, IFormFile filePatient)
        {
            _patientService.AddPatientFile(patientFile, filePatient);

            var patient = _patientService.GetPatientById(patientFile.PatientId);
            return RedirectToAction(nameof(ManageFiles), new { uid = patient?.UID });
        }


        public IActionResult DeleteFile(int id, string uid)
        {
            _patientService.DeletePatientFile(id);
            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
