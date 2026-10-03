using Hospital_System.Dtos.DepartmentDtos;
using Hospital_System.Dtos.DoctorsDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.AppointmentRepo;
using Hospital_System.Repositories.Base;
using Hospital_System.Repositories.DoctorRepo;
using Hospital_System.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }


        [HttpGet]
       
          public IActionResult Index()
          {
            var doctors = _doctorService.GetAllDoctors();
            return View(doctors);
        }

        

        public void GetDepartments() 
        {
            IEnumerable<Department> departments = _doctorService.GetAllDepartments();
            SelectList departmentSelectList = new SelectList(departments,"Id","Name");
            ViewBag.departmentSelectList = departmentSelectList;
        }


        [HttpGet]
        public IActionResult Create() 
        {
            GetDepartments();
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateDoctorDto doctor) 
        {
            if (ModelState.IsValid)
            {
                _doctorService.AddDoctor(doctor);
                return RedirectToAction("Index");
            }
            GetDepartments();
            return View(doctor);
        }

        
        [HttpGet]
        public IActionResult Edit(string uid) 
        {
            GetDepartments();
            var doctor = _doctorService.GetDoctorByUId(uid);
            if (doctor == null) 
            {
                return NotFound();
            }

            var update = new UpdateDoctorDto
            {
                Id = doctor.Id,
                UID = doctor.UID,
                Name = doctor.Name,
                Specialization = doctor.Specialization,
                Phone = doctor.Phone,
                DepartmentId = doctor.DepartmentId

            };
            return View(update);
            
        }
        [HttpPost]
        public IActionResult Edit(UpdateDoctorDto doctor) 
        {
            if (ModelState.IsValid)
            {
                _doctorService.UpdateDoctor(doctor);
                return RedirectToAction("Index");
            }

            GetDepartments();
            return View(doctor);
        }
           
        
        [HttpGet]
        public IActionResult Delete(string uid) 
        {
            GetDepartments();
            var doctor = _doctorService.GetDoctorByUId(uid);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }


        [HttpPost]
        public IActionResult Delete(Doctor doctor) 
        {
            var oldDoctor = _doctorService.GetDoctorByUId(doctor.UID);
            if (oldDoctor != null)
            {
                _doctorService.DeleteDoctor(oldDoctor);
                return RedirectToAction("Index");
            }
            GetDepartments();
            return View(doctor);
        }

        public IActionResult ManageFiles(string uid)
        {
            var doctor = _doctorService.GetDoctorByUId(uid);
            if (doctor == null)
            {
                return NotFound();
            }

            var files = _doctorService.GetDoctorFiles(doctor.Id);
            ViewBag.DoctorName = doctor.Name;
            ViewBag.Files = files;

            DoctorFile doctorFile = new DoctorFile();
            doctorFile.DoctorId = doctor.Id;

            return View(doctorFile);
        }


        [HttpPost]
        public IActionResult ManageFiles(DoctorFile doctorFile, IFormFile fileDoctor)
        {
            _doctorService.AddDoctorFile(doctorFile, fileDoctor);

            var doctor = _doctorService.GetDoctorById(doctorFile.DoctorId);
            return RedirectToAction(nameof(ManageFiles), new { uid = doctor?.UID });
        }



        public IActionResult DeleteFile(int id, string uid)
        {
            _doctorService.DeleteDoctorFile(id);
            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
