using Hospital_System.Data;
using Hospital_System.Dtos.AppointmentsDtos;
using Hospital_System.Dtos.DepartmentDtos;
using Hospital_System.Dtos.DoctorsDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.AppointmentRepo;
using Hospital_System.Repositories.DoctorRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class DoctorsController : Controller
    {

        private readonly IDoctorRepository _repo;
        public DoctorsController(IDoctorRepository repo)
        {
            _repo = repo;

        }


        //Dependency Injection 

        //private readonly AppDbContext _db;
        //public DoctorsController(AppDbContext db)
        //{
        //    _db = db;

        //}
        [HttpGet]
       
          public IActionResult Index()
        {
            //IEnumerable<Doctor> doctor = _repo.GetAll();
            var doctors = _repo.GetAll().Select(d => new DoctorDto
            {
                Id = d.Id,
                UID = d.UID,
                Name = d.Name,
                Specialization = d.Specialization,
                Phone = d.Phone,
                DepartmentId = d.DepartmentId,
                DepartmentName = d.Department.Name,

                
            }).ToList();

            return View(doctors);
        }

        

        public void GetDepartments() 
        {
            IEnumerable<Department> departments = _repo.Departments.ToList();
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
                //Mapping
                var doc = new Doctor
                {
                    UID = Guid.NewGuid().ToString(),
                    Name = doctor.Name,
                    Specialization = doctor.Specialization,
                    Phone = doctor.Phone,
                    DepartmentId = doctor.DepartmentId,
                    
                };

                _repo.Add(doc);
                _repo.Save();

                //_db.Doctors.Add(doc);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetDepartments();
            return View(doctor);
        }
        [HttpGet]
        public IActionResult Edit(string uid) 
        {
            GetDepartments();

            var doctor = _repo.GetByUId(uid);

            //var doctor = _repo.Doctors.FirstOrDefault(d=>d.UID == uid);
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
                if (doctor.UID == null)
                    doctor.UID = Guid.NewGuid().ToString();
                var doc = new Doctor
                {
                    UID = doctor.UID,
                    Name = doctor.Name,
                    Specialization = doctor.Specialization,
                    Phone = doctor.Phone,
                    DepartmentId = doctor.DepartmentId,
                    Id = doctor.Id
                };

                _repo.Update(doc);
                _repo.Save();


                //_db.Doctors.Update(doc);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetDepartments(); 
            return View(doctor);
        }
        [HttpGet]
        public IActionResult Delete(string uid) 
        {
            GetDepartments();
            var doctor = _repo.GetByUId(uid);
            //var doctor = _repo.Doctors.FirstOrDefault(d=>d.UID == uid);
            if (doctor == null) 
            {
                
                return NotFound();
            }
            return View(doctor);
        }
        [HttpPost]
        public IActionResult Delete(Doctor doctor) 
        {

            //var oldDepartment= _repo.Doctors.FirstOrDefault(a => a.UID == doctor.UID);
            var oldDoctor = _repo.GetByUId(doctor.UID);
            //if (ModelState.IsValid)
            //{
                
            //}
            if (oldDoctor != null) 
            {

                _repo.Delete(oldDoctor);
                _repo.Save();

                //_db.Doctors.Remove(doctor);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetDepartments();
            return View(doctor);
        }

        private string UploadFiles(IFormFile file, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString()
                              + Path.GetExtension(file.FileName);


            string folderPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      "Files",
      "Doctors"
  );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Doctors/" + fileName;
        }




        public IActionResult ManageFiles(string uid)
        {

            var doctor = _repo.GetByUId(uid);
            //var doctor = _repo.Doctors.FirstOrDefault(e => e.Id == doctorId);
           
            if (doctor == null)
                return NotFound();

            var files = _repo.DoctorFiles.Where(e => e.DoctorId == doctor.Id).ToList();
            
            ViewBag.DoctorName = doctor.Name;
            ViewBag.Files = files;

            DoctorFile doctorFile = new DoctorFile();
            doctorFile.DoctorId = doctor.Id;

            return View(doctorFile);
        }


        [HttpPost]
        public IActionResult ManageFiles(DoctorFile doctorFile, IFormFile fileDoctor)
        {
            if (fileDoctor != null)
            {
                doctorFile.FileURL = UploadFiles(fileDoctor, doctorFile.Name);
            }

            _repo.AddFile(doctorFile);
            _repo.Save();

            //_db.DoctorFiles.Add(doctorFile);
            //_db.SaveChanges();


            //return RedirectToAction(nameof(ManageFiles), new { doctorId = doctorFile.DoctorId });
            var doctor = _repo.GetAll().FirstOrDefault(d => d.Id == doctorFile.DoctorId);
            return RedirectToAction(nameof(ManageFiles), new { uid = doctor?.UID });
        }

        public IActionResult DeleteFile(int id, string uid)
        {
            var file = _repo.DoctorFiles.FirstOrDefault(f => f.Id == id);
            if (file != null)
            {
                _repo.DeleteDoctorFile(file);
                _repo.Save();
            }

            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
