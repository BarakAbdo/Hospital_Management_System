using Hospital_System.Data;

using Hospital_System.Dtos.PrescriptionsDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.MedicalRecordRepo;
using Hospital_System.Repositories.PrescriptionRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class PrescriptionsController : Controller
    {

        private readonly IPrescriptionRepository _repo;

        public PrescriptionsController(IPrescriptionRepository repo)
        {
            _repo = repo;
        }

        //Dependancy Injaction
        //private readonly AppDbContext _db;
        //public PrescriptionsController(AppDbContext db)
        //{
        //    _db = db;
        //}
        public IActionResult Index() 
        {

            //IEnumerable<Prescription> prescriptions = _repo.GetAll();
            var prescription = _repo.GetAll().Select(p => new PrescriptionDto
            {
                Id = p.Id,
                UID = p.UID,
                Dosage = p.Dosage,
                Duration = p.Duration,
                PatientId = p.PatientId,
                DoctorId = p.DoctorId,
                MedicationId = p.MedicationId,
                PatientName = p.Patient.Name,
                DoctorName = p.Doctor.Name,
                MedicationName = p.Medication.Name
            }).ToList();
        return View(prescription);
        }
        public void GetPatient() 
        {
            IEnumerable<Patient> patient = _repo.Patients.ToList();
            SelectList patientSelectList = new SelectList(patient,"Id","Name");
            ViewBag.patientSelectList = patientSelectList;
        }

        public void GetDoctor() 
        { 
            IEnumerable<Doctor> doctors = _repo.Doctors.ToList();
            SelectList doctorSelectList = new SelectList(doctors, "Id", "Name");
            ViewBag.doctorSelectList = doctorSelectList;
        }

        public void GetMedication() 
        {
            IEnumerable<Medication> medications = _repo.Medications.ToList();
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
        public IActionResult Create(CreatePrescriptionDto prescription)
        {
            if (ModelState.IsValid)
            {

                var pre = new Prescription
                {
                    UID = Guid.NewGuid().ToString(),
                    Dosage = prescription.Dosage,
                    Duration = prescription.Duration,
                    PatientId = prescription.PatientId,
                    DoctorId = prescription.DoctorId,
                    MedicationId = prescription.MedicationId,

                };

                _repo.Add(pre);
                _repo.Save();
                //_db.Prescriptions.Add(pre);
                //_db.SaveChanges();
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
            var prescription = _repo.GetByUId(uid);
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
                DoctorId= prescription.DoctorId,
                MedicationId = prescription.MedicationId
            };
            return View(update);
        }
        [HttpPost]
        public IActionResult Edit(UpdatePrescriptionDto prescription)
        {
            if (ModelState.IsValid)
            {
                if (prescription.UID == null)
                    prescription.UID = Guid.NewGuid().ToString();

                //Mapping
                var pre = new Prescription
                {
                    UID = prescription.UID,
                    Dosage = prescription.Dosage,
                    Duration = prescription.Duration,
                    PatientId = prescription.PatientId,
                    DoctorId = prescription.DoctorId,
                    MedicationId = prescription.MedicationId,
                    Id = prescription.Id

                };

                _repo.Update(pre);
                _repo.Save();

                //_db.Prescriptions.Update(pre);
                //_db.SaveChanges();
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
            GetPatient();
            GetDoctor();
            GetMedication();
            var prescription = _repo.GetByUId(uid);
            //var prescription = _repo.Prescriptions.FirstOrDefault(p => p.UID == uid);
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
            var oldPrescription = _repo.GetByUId(prescription.UID);
            if (oldPrescription == null)
            {
                return NotFound();
            }

            _repo.Delete(oldPrescription);
            _repo.Save();


            //_db.Prescriptions.Remove(prescriptions);
            //_db.SaveChanges();
            return RedirectToAction("Index");
        }

        private string UploadFiles(IFormFile file, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString()
                              + Path.GetExtension(file.FileName);


            string folderPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      "Files",
      "Prescriptions"
  );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Prescriptions/" + fileName;
        }



        [HttpGet]
        public IActionResult ManageFiles(string uid)
        {
            var prescription = _repo.Prescriptions.FirstOrDefault(e => e.UID == uid);

            if (prescription == null)
                return NotFound();

            var files = _repo.PrescriptionFiles.Where(e => e.PrescriptionId == prescription.Id).ToList();
            
            ViewBag.PrescriptionDosage = prescription.Dosage;
            ViewBag.PrescriptionUid = prescription.UID;
            ViewBag.Files = files;

            PrescriptionFile prescriptionFile = new PrescriptionFile();

            prescriptionFile.PrescriptionId = prescription.Id;

            return View(prescriptionFile);
        }


        [HttpPost]
        public IActionResult ManageFiles(PrescriptionFile prescriptionFile, IFormFile filePrescription)
        {
            if (filePrescription != null)
            {
                prescriptionFile.FileURL = UploadFiles(filePrescription, prescriptionFile.Dosage);
            }

            _repo.AddFile(prescriptionFile);
            _repo.Save();

            //_db.PrescriptionFiles.Add(prescriptionFile);
            //_db.SaveChanges();

            var prescription = _repo.Prescriptions.FirstOrDefault(p => p.Id == prescriptionFile.PrescriptionId);
            return RedirectToAction(nameof(ManageFiles), new { uid = prescription?.UID });

            //return RedirectToAction(nameof(ManageFiles), new { prescriptionId = prescriptionFile.PrescriptionId });

        }

        public IActionResult DeleteFile(int id, string uid)
        {
            var file = _repo.PrescriptionFiles.FirstOrDefault(f => f.Id == id);
            if (file != null)
            {
                _repo.DeletePrescriptionFile(file);
                _repo.Save();
            }

            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
