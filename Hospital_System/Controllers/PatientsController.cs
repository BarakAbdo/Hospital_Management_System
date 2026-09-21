using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Dtos.MedicationsDtos;
using Hospital_System.Dtos.PatientsDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.MedicalRecordRepo;
using Hospital_System.Repositories.PatientRepo;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientRepository _repo;

        public PatientsController(IPatientRepository repo)
        {
            _repo = repo;
        }



        //Dependency Injection 

        //private readonly AppDbContext _db;
        //public PatientsController(AppDbContext db)
        //{
        //    _db = db;

        //}
        [HttpGet]
        public IActionResult Index()
        {
            //IEnumerable<Patient> patients = _repo.GetAll();
            var patient = _repo.GetAll().Select(p => new PatientDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name,
                Gender = p.Gender,
                Phone = p.Phone,
                DateOfBirth = p.DateOfBirth
            }).ToList();
            return View(patient);

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
                //Mapping
                var pat = new Patient
                {
                    UID = Guid.NewGuid().ToString(),
                    Name = patient.Name,
                    Gender = patient.Gender,
                    Phone = patient.Phone,
                    DateOfBirth = patient.DateOfBirth
                };

                _repo.Add(pat);
                _repo.Save();

                //_db.Patients.Add(pat);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var patient = _repo.GetByUId(uid);
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
                Phone= patient.Phone,
                DateOfBirth = patient.DateOfBirth
            };
            return View(update);
        }
        [HttpPost]
        public IActionResult Edit(UpdatePatientDto patient)
        {
            if (ModelState.IsValid)
            {

                if (patient.UID == null)
                    patient.UID = Guid.NewGuid().ToString();
                //Mapping
                var pat = new Patient
                {
                    UID = patient.UID,
                    Name = patient.Name,
                    Gender = patient.Gender,
                    Phone = patient.Phone,
                    DateOfBirth = patient.DateOfBirth,
                    Id = patient.Id

                };

                _repo.Update(pat);
                _repo.Save();

                //_db.Patients.Update(pat);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);

        }


        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var patient = _repo.GetByUId(uid);
            //var patient = _repo.Patients.FirstOrDefault(p => p.UID == uid);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        public IActionResult Delete(Patient patient)
        {
            var oldPatient = _repo.GetByUId(patient.UID);
            //if (ModelState.IsValid)
            //{}
                if (oldPatient == null)
                {
                    return NotFound();
                }

                _repo.Delete(oldPatient);
                _repo.Save();  
                
                //_db.Patients.Remove(patient);
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
      "Patients"
  );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Patients/" + fileName;
        }




        public IActionResult ManageFiles(string uid)
        {
            var patient = _repo.Patients.FirstOrDefault(e => e.UID == uid);

            if (patient == null)
                return NotFound();

            var files = _repo.PatientFiles.Where(e => e.PatientId == patient.Id).ToList();
           
            ViewBag.PatientName = patient.Name;
            ViewBag.Files = files;

            PatientFile patientFile = new PatientFile();
            patientFile.PatientId = patient.Id;

            return View(patientFile);
        }


        [HttpPost]
        public IActionResult ManageFiles(PatientFile patientFile, IFormFile filePatient)
        {
            if (filePatient != null)
            {
                patientFile.FileURL = UploadFiles(filePatient, patientFile.Name);
            }

            _repo.AddFile(patientFile);
            _repo.Save();

            //_repo.PatientFiles.Add(patientFile);
            //_repo.SaveChanges();

            var patient = _repo.Patients.FirstOrDefault(p => p.Id == patientFile.PatientId);

            //return RedirectToAction(nameof(ManageFiles), new { patientId = patientFile.PatientId });

            return RedirectToAction(nameof(ManageFiles), new { uid = patient?.UID });
        }

        public IActionResult DeleteFile(int id, string uid)
        {
            var file = _repo.PatientFiles.FirstOrDefault(f => f.Id == id);
            if (file != null)
            {
                _repo.DeletePatientFile(file); // أو _repo.PatientFiles.Remove(file) حسب الدالة في الـ Repo لديك
                _repo.Save();
            }

            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
