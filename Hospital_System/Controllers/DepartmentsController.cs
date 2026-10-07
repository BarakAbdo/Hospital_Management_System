using Hospital_Management_System.Application.Dtos.DepartmentDtos;
using Hospital_Management_System.Application.Services.Base;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
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
                _departmentService.AddDepartment(department, image);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var department = _departmentService.GetByUId(uid);
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
                _departmentService.UpdateDepartment(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var department = _departmentService.GetByUId(uid);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(DepartmentDto department)
        {
            
            _departmentService.DeleteDepartment(department);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageFiles(string uid)
        {
            var department = _departmentService.GetByUId(uid);
            if (department == null)
                return NotFound();

            ViewBag.DepartmentName = department.Name;
            ViewBag.Files = _departmentService.GetDepartmentFiles(department.Id);

            DepartmentFile departmentFile = new DepartmentFile();
            departmentFile.DepartmentId = department.Id;

            return View(departmentFile);
        }

        [HttpPost]
        public IActionResult ManageFiles(DepartmentFile departmentFile, IFormFile fileDepartment)
        {
            _departmentService.AddDepartmentFile(departmentFile, fileDepartment);
            var department = _departmentService.GetById(departmentFile.DepartmentId);
            return RedirectToAction(nameof(ManageFiles), new { uid = department?.UID });
        }

        public IActionResult DeleteFile(int id, string uid)
        {
            _departmentService.DeleteDepartmentFile(id);
            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}