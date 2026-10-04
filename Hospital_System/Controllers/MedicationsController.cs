

using Hospital_Management_System.Application.Dtos.MedicationsDtos;
using Hospital_Management_System.Application.Services.Base;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class MedicationsController : Controller
    {
        private readonly IMedicationService _medicationService;

        public MedicationsController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }


        [HttpGet]
        public IActionResult Index() 
        {
            var medications = _medicationService.GetAllMedications();
            return View(medications);
            
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
                _medicationService.AddMedication(medication);
                return RedirectToAction("Index");
            }
            return View(medication);
        }
        [HttpGet]
        public IActionResult Edit(string uid) 
        {
            var medication = _medicationService.GetMedicationByUId(uid);
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
                _medicationService.UpdateMedication(medication);
                return RedirectToAction("Index");
            }
            return View(medication);

        }


        [HttpGet]
        public IActionResult Delete(string uid) 
        {
            var medication = _medicationService.GetMedicationByUId(uid);
            if (medication == null)
            {
                return NotFound();
            }
            return View(medication);
        }


        [HttpPost]
        public IActionResult Delete(Medication medication) 
        {
            if (ModelState.IsValid)
            {
                var oldMedication = _medicationService.GetMedicationByUId(medication.UID);
                if (oldMedication == null)
                {
                    return NotFound();
                }
                _medicationService.DeleteMedication(oldMedication);
                return RedirectToAction("Index");
            }
            return View(medication);
        }


        [HttpGet]
        public IActionResult ManageFiles(string uid)
        {
            var medication = _medicationService.GetMedicationByUId(uid);
            if (medication == null)
                return NotFound();

            var files = _medicationService.GetMedicationFiles(medication.Id);

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
            _medicationService.AddMedicationFile(medicationFile, fileMedication);

            var medication = _medicationService.GetMedicationById(medicationFile.MedicationId);
            return RedirectToAction(nameof(ManageFiles), new { uid = medication?.UID });
        }

        public IActionResult DeleteFile(int id, string uid)
        {
            _medicationService.DeleteMedicationFile(id);
            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
