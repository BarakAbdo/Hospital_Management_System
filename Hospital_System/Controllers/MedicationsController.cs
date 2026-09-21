using Hospital_System.Data;
using Hospital_System.Dtos.DepartmentDtos;
using Hospital_System.Dtos.MedicalRecordsDtos;
using Hospital_System.Dtos.MedicationsDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.MedicalRecordRepo;
using Hospital_System.Repositories.MedicationRepo;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Hospital_System.Controllers
{
    public class MedicationsController : Controller
    {


        private readonly IMedicationRepository _repo;

        public MedicationsController(IMedicationRepository repo)
        {
            _repo = repo;
        }

        //Dependency Injection
        //private readonly AppDbContext _db;
        //public MedicationsController(AppDbContext db)
        //{
        //    _db = db;
        //}
        [HttpGet]
        public IActionResult Index() 
        {

            //IEnumerable<Medication> medications = _repo.GetAll();
            var medication = _repo.GetAll().Select(m => new MedicationDto
            {
                Id = m.Id,
                UID = m.UID,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                Stock = m.Stock,
            }).ToList();
            return View(medication);
        }
        [HttpGet]
        public IActionResult Create() 
        {
        return View();  
        }
        [HttpPost]
        public IActionResult Create(CreateMedicationDto medication) 
        {
            if (ModelState.IsValid) 
            {
                //Mapping
                var med = new Medication
                {
                    UID = Guid.NewGuid().ToString(),
                    Name = medication.Name,
                    Description = medication.Description,
                    Price = medication.Price,
                    Stock = medication.Stock,
                };

                _repo.Add(med);
                _repo.Save();

                //_db.Medications.Add(med);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medication);
        }
        [HttpGet]
        public IActionResult Edit(string uid) 
        {
            var medication = _repo.GetByUId(uid);
            if (medication == null) 
            {
                return NotFound();
            }

            var update = new UpdateMedicationDto
            {
            Id = medication.Id,
            UID = medication.UID,
            Name = medication.Name,
            Description = medication.Description,
            Price = medication.Price,
            Stock = medication.Stock
            };
            return View(update); 
        }
        [HttpPost]
        public IActionResult Edit(UpdateMedicationDto medication) 
        {
            if (ModelState.IsValid) 
            {
                if (medication.UID == null)
                    medication.UID = Guid.NewGuid().ToString();

                //Mapping
                var med = new Medication
                {
                    UID = medication.UID,
                    Name = medication.Name,
                    Description = medication.Description,
                    Price = medication.Price,
                    Stock = medication.Stock,
                    Id = medication.Id
                };

                _repo.Update(med);
                _repo.Save();

                //_db.Medications.Update(med);
                //_db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(medication);

        }
        [HttpGet]
        public IActionResult Delete(string uid) 
        {
            var medication = _repo.GetByUId(uid);
            //var medication = _repo.Medications.FirstOrDefault(m => m.UID == uid);
            if (medication == null) 
            {
                return NotFound();
            }
            return View(medication);
        }
        [HttpPost]
        public IActionResult Delete(Medication medication) 
        {
            var oldMedication = _repo.GetByUId(medication.UID);
            //var oldMedication= _repo.Medications.FirstOrDefault(m => m.UID == medicalRecord.UID);
            if (ModelState.IsValid)
            {
                if (oldMedication == null)
                {
                    return NotFound();
                }

                _repo.Delete(oldMedication);
                _repo.Save();
                //_db.Medications.Remove(medication);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medication);
        }



        private string UploadFiles(IFormFile file, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString()
                              + Path.GetExtension(file.FileName);


            string folderPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      "Files",
      "Medications"
  );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Medications/" + fileName;
        }



        [HttpGet]
        public IActionResult ManageFiles(string uid)
        {
            var medication = _repo.Medications.FirstOrDefault(e => e.UID == uid);

            if (medication == null)
                return NotFound();

            var files = _repo.MedicationFiles.Where(e => e.MedicationId == medication.Id).ToList();
           
            ViewBag.MedicationName = medication.Name;
            ViewBag.Files = files;

            ViewBag.MedicationUid = medication.UID;

            MedicationFile medicationFile = new MedicationFile();

            medicationFile.MedicationId = medication.Id;

            return View(medicationFile);
        }


        [HttpPost]
        public IActionResult ManageFiles(MedicationFile medicationFile, IFormFile fileMedication)
        {
            if (fileMedication != null)
            {
                medicationFile.FileURL = UploadFiles(fileMedication, medicationFile.Name);
            }

            _repo.AddFile(medicationFile);
            _repo.Save();

            //_repo.MedicationFiles.Add(medicationFile);
            //_repo.SaveChanges();

            var medication = _repo.Medications.FirstOrDefault(m => m.Id == medicationFile.MedicationId);
            return RedirectToAction(nameof(ManageFiles), new { uid = medication?.UID });

            //return RedirectToAction(nameof(ManageFiles), new { medicationId = medicationFile.MedicationId });

        }

        public IActionResult DeleteFile(int id, string uid)
        {
            var file = _repo.MedicationFiles.FirstOrDefault(f => f.Id == id);
            if (file != null)
            {
                _repo.DeleteMedicationFile(file);
                _repo.Save();
            }

            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
