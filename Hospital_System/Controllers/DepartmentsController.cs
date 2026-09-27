using Hospital_System.Data;
using Hospital_System.Dtos.AppointmentsDtos;
using Hospital_System.Dtos.DepartmentDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.AppointmentRepo;
using Hospital_System.Repositories.DepartmentRepo;
using Microsoft.AspNetCore.Mvc;
using static Hospital_System.Dtos.DepartmentDtos.DepartmentDto;


namespace Hospital_System.Controllers
{
    public class DepartmentsController : Controller
    {
        //Dependency Injection 

        private readonly IDepartmentRepository _repo;
        public DepartmentsController(IDepartmentRepository repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public IActionResult Index()
        {
            //IEnumerable<Department> departments = _repo.GetAll();
            var departments = _repo.GetAll().Select(d => new DepartmentDto
            {
                Id = d.Id,
                UID = d.UID,
                Name = d.Name,
                Location = d.Location
            }).ToList();

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto department, IFormFile image)
        {

            if (ModelState.IsValid)
            {
                //Mapping
                var dept = new Department
                {
                    UID = Guid.NewGuid().ToString(),
                    Name = department.Name,
                    Location = department.Location,
                };

                if (image != null)
                {
                    dept.ImageUrl = UploadImage(image);
                }

                _repo.Add(dept);
                _repo.Save();

                //_db.Departments.Add(dept);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var department = _repo.GetByUId(uid);

            //var department = _db.Departments.Find(Id);
            if (department == null)
            {
                return NotFound();
            }

            var update = new UpdateDepartmentDto
            {
                Id = department.Id,
                UID = department.UID,
                Name = department.Name,
                Location = department.Location


            };
            return View(update);
        }
        [HttpPost]
        public IActionResult Edit(UpdateDepartmentDto department)
        {
            if (ModelState.IsValid)
            {
                if (department.UID == null)
                    department.UID = Guid.NewGuid().ToString();
                var dept = new Department
                {
                    UID = department.UID,
                    Name = department.Name,
                    Location = department.Location,
                    Id = department.Id
                };

                _repo.Update(dept);
                _repo.Save();

                //_db.Departments.Update(dept);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }


        private string UploadImage(IFormFile image)
        {
            string fileName = Guid.NewGuid().ToString()
                              + Path.GetExtension(image.FileName);


            string folderPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      "images",
      "Departments"
  );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return "/images/Departments/" + fileName;
        }


        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var department = _repo.GetByUId(uid);
            //var department = _repo.Departments.FirstOrDefault(d=>d.UID == uid);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            var oldDepartment = _repo.GetByUId(department.UID);
            //var oldDepartment= _repo.Departments.FirstOrDefault(a => a.UID == department.UID);
            if (oldDepartment == null)
            {
                return NotFound();
            }

            _repo.Delete(oldDepartment);
                _repo.Save();

                //_db.Departments.Remove(department);
                //_db.SaveChanges();
                return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageFiles(string uid)
        {
            var department = _repo.GetByUId(uid);

            if (department == null)
                return NotFound();

            var files = _repo.DepartmentFiles.Where(e => e.DepartmentId == department.Id).ToList();
            ViewBag.DepartmentName = department.Name;

            ViewBag.Files = files;

            DepartmentFile departmentFile = new DepartmentFile();

            departmentFile.DepartmentId = department.Id;

            return View(departmentFile);
        }

        [HttpPost]
        public IActionResult ManageFiles(DepartmentFile departmentFile, IFormFile fileDepartment)
        {
            if (fileDepartment != null)
            {
                departmentFile.FileURL = UploadImage(fileDepartment);
            }

            _repo.AddDepartmentFile(departmentFile);
            _repo.Save();

            var department = _repo.GetAll().FirstOrDefault(a => a.Id == departmentFile.DepartmentId);
            return RedirectToAction(nameof(ManageFiles), new { uid = department?.UID });
        }

        public IActionResult DeleteFile(int id, string uid)
        {
            var file = _repo.DepartmentFiles.FirstOrDefault(f => f.Id == id);
            if (file != null)
            {
                _repo.DeleteDepartmentFile(file);
                _repo.Save();
            }

            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
