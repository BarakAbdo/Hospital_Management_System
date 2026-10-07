using Hospital_Management_System.Application.Dtos.MedicalRecordsDtos;
using Hospital_Management_System.Application.Services.Base;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital_Management_System.Controllers
{
    public class MedicalRecordsController : Controller
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordsController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        public IActionResult Index()
        {
            var medicalRecords = _medicalRecordService.GetAllMedicalRecords();
            return View(medicalRecords);
        }

        public void GetPatient()
        {
            var patients = _medicalRecordService.GetAllPatients();
            SelectList patientSelectList = new SelectList(patients, "Id", "Name");
            ViewBag.patientSelectList = patientSelectList;
        }

        public void GetDoctor()
        {
            var doctors = _medicalRecordService.GetAllDoctors();
            SelectList doctorSelectList = new SelectList(doctors, "Id", "Name");
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
        public IActionResult Create(CreateMedicalRecordDto medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _medicalRecordService.AddMedicalRecord(medicalRecord);
                return RedirectToAction("Index");
            }
            GetPatient();
            GetDoctor();
            return View(medicalRecord);
        }

        [HttpGet]
        public IActionResult Edit(string uid)
        {
            GetPatient();
            GetDoctor();
            var medicalRecord = _medicalRecordService.GetMedicalRecordByUId(uid);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            var update = new UpdateMedicalRecordDto
            {
                Id = medicalRecord.Id,
                UID = medicalRecord.UID,
                Diagnosis = medicalRecord.Diagnosis,
                Notes = medicalRecord.Notes,
                PatientId = medicalRecord.PatientId,
                DoctorId = medicalRecord.DoctorId
            };
            return View(update);
        }

        [HttpPost]
        public IActionResult Edit(UpdateMedicalRecordDto medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _medicalRecordService.UpdateMedicalRecord(medicalRecord);
                return RedirectToAction("Index");
            }

            GetPatient();
            GetDoctor();
            return View(medicalRecord);
        }

        [HttpGet]
        public IActionResult Delete(string uid)
        {
            GetPatient();
            GetDoctor();
            var medicalRecord = _medicalRecordService.GetMedicalRecordByUId(uid);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            return View(medicalRecord);
        }

        [HttpPost]
        public IActionResult Delete(MedicalRecordDto medicalRecord)
        {
            
            _medicalRecordService.DeleteMedicalRecord(medicalRecord);
            return RedirectToAction("Index");
        }

        public IActionResult ManageFiles(string uid)
        {
            var medicalRecord = _medicalRecordService.GetMedicalRecordByUId(uid);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            var files = _medicalRecordService.GetMedicalRecordFiles(medicalRecord.Id);
            ViewBag.MedicalRecordNotes = medicalRecord.Notes;
            ViewBag.Files = files;
            ViewBag.MedicalRecordUid = medicalRecord.UID;

            MedicalRecordFile medicalRecordFile = new MedicalRecordFile();
            medicalRecordFile.MedicalRecordId = medicalRecord.Id;

            return View(medicalRecordFile);
        }

        [HttpPost]
        public IActionResult ManageFiles(MedicalRecordFile medicalRecordFile, IFormFile fileMedicalRecord)
        {
            _medicalRecordService.AddMedicalRecordFile(medicalRecordFile, fileMedicalRecord);

            var medicalRecord = _medicalRecordService.GetMedicalRecordById(medicalRecordFile.MedicalRecordId);
            return RedirectToAction(nameof(ManageFiles), new { uid = medicalRecord?.UID });
        }

        public IActionResult DeleteFile(int id, string uid)
        {
            _medicalRecordService.DeleteMedicalRecordFile(id);
            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}