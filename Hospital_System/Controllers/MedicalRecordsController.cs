using Hospital_System.Data;
using Hospital_System.Dtos.AppointmentsDtos;
using Hospital_System.Dtos.InvoicesDtos;
using Hospital_System.Dtos.MedicalRecordsDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.MedicalRecordRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class MedicalRecordsController : Controller
    {

        private readonly IMedicalRecordRepository _repo;

        public MedicalRecordsController(IMedicalRecordRepository repo)
        {
            _repo = repo;
        }



        //Dependancy Injection
        //private readonly AppDbContext _db;
        //public MedicalRecordsController(AppDbContext db)
        //{
        //    _db = db;
        //}
        public IActionResult Index() 
        {
            //IEnumerable<MedicalRecord> medicalRecords = _repo.GetAll();
            var medicalRecord = _repo.GetAll().Select(m => new MedicalRecordDto
            {
                Id = m.Id,
                UID = m.UID,
                Diagnosis = m.Diagnosis,
                Notes = m.Notes,
                PatientId = m.PatientId,
                DoctorId = m.DoctorId,
                PatientName = m.Patient.Name,
                DoctorName = m.Doctor.Name,
            }).ToList();
            return View(medicalRecord);
        }

        public void GetPatient() 
        {
            IEnumerable<Patient> patients = _repo.Patients.ToList();
            SelectList patientSelectList = new SelectList(patients,"Id","Name");
            ViewBag.patientSelectList = patientSelectList;
        }

        public void GetDoctor() 
        {
            IEnumerable<Doctor> doctors = _repo.Doctors.ToList();
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
        public IActionResult Create(CreateMedicalRecordDto medicalRecord)
        {
            if (ModelState.IsValid)
            {
                //Mapping
                var med = new MedicalRecord
                {

                    UID = Guid.NewGuid().ToString(),
                    Diagnosis = medicalRecord.Diagnosis,
                    Notes = medicalRecord.Notes,
                    PatientId = medicalRecord.PatientId,
                    DoctorId = medicalRecord.DoctorId,

                };

                _repo.Add(med);
                _repo.Save();

                //_db.MedicalRecords.Add(med);
                //_db.SaveChanges();
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
            var medicalRecord = _repo.GetByUId(uid);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            var update = new UpdateMedicalRecordDto
            {
               Id = medicalRecord.Id,
               UID = medicalRecord.UID,
               Diagnosis = medicalRecord.Diagnosis,
               Notes= medicalRecord.Notes,
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

                if (medicalRecord.UID == null)
                    medicalRecord.UID = Guid.NewGuid().ToString();
                //Mapping
                var med = new MedicalRecord
                {
                    UID = medicalRecord.UID,
                    Diagnosis = medicalRecord.Diagnosis,
                    Notes = medicalRecord.Notes,
                    PatientId = medicalRecord.PatientId,
                    DoctorId = medicalRecord.DoctorId,
                    Id = medicalRecord.Id
                };

                _repo.Update(med);
                _repo.Save();

                //_db.MedicalRecords.Update(med);
                //_db.SaveChanges();
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
            var medicalRecord = _repo.GetByUId(uid);
            //var medicalRecord = _repo.MedicalRecords.FirstOrDefault(m => m.UID == uid);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            return View(medicalRecord);
        }
        [HttpPost]
        public IActionResult Delete(MedicalRecord medicalRecord)
        {
            var oldMedicalRecord = _repo.GetByUId(medicalRecord.UID);
            //var oldMedicalRecord= _repo.MedicalRecord.FirstOrDefault(a => a.UID == medicalRecord.UID);
            if (oldMedicalRecord == null)
            {
                return NotFound();
            }
            _repo.Delete(oldMedicalRecord);
            _repo.Save();

            //_db.MedicalRecords.Remove(medicalRecords);
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
      "MedicalRecords"
  );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/MedicalRecords/" + fileName;
        }




        public IActionResult ManageFiles(string uid)
        {
            var medicalRecord = _repo.MedicalRecords.FirstOrDefault(e => e.UID == uid);

            if (medicalRecord == null)
                return NotFound();

            var files = _repo.MedicalRecordFiles.Where(e => e.MedicalRecordId == medicalRecord.Id).ToList();
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
            if (fileMedicalRecord != null)
            {
                medicalRecordFile.FileURL = UploadFiles(fileMedicalRecord, medicalRecordFile.Notes);
            }

            _repo.AddFile(medicalRecordFile);
            _repo.Save();

            //_re.MedicalRecordFiles.Add(medicalRecordFile);
            //_db.SaveChanges();

            var medicalRecord = _repo.MedicalRecords.FirstOrDefault(e => e.Id == medicalRecordFile.MedicalRecordId);
            return RedirectToAction(nameof(ManageFiles), new { uid = medicalRecord?.UID });

            //return RedirectToAction(nameof(ManageFiles), new { medicalRecordId = medicalRecordFile.MedicalRecordId });

        }


        public IActionResult DeleteFile(int id, string uid)
        {
            var file = _repo.MedicalRecordFiles.FirstOrDefault(f => f.Id == id);
            if (file != null)
            {
                _repo.DeleteMedicalRecordFile(file);
                _repo.Save();
            }

            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }


    }
}
