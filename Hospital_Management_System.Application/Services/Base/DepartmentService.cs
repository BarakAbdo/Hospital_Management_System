using Hospital_Management_System.Application.Dtos.DepartmentDtos;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Hospital_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departmentDto = _unitOfWork.DepartmentRepo.GetAll().Select(d => new DepartmentDto
            {
                Id = d.Id,
                UID = d.UID,
                Name = d.Name,
                Location = d.Location
            }).ToList();
            return departmentDto;
        }

        public DepartmentDto GetByUId(string uid)
        {
            var d = _unitOfWork.DepartmentRepo.GetByUId(uid);

            var departmentDto = new DepartmentDto
            {
                Id = d.Id,
                UID = d.UID,
                Name = d.Name,
                Location = d.Location
            };
            return departmentDto;
        }


        public DepartmentDto GetById(int id)
        {
            var d = _unitOfWork.DepartmentRepo.GetAll().FirstOrDefault(a => a.Id == id);

            var departmentDto= new DepartmentDto
            {
                Id = d.Id,
                UID = d.UID,
                Name = d.Name,
                Location = d.Location
            };
            return departmentDto;
        }

        public void AddDepartment(CreateDepartmentDto departmentDto, IFormFile image)
        {
            var dept = new Department
            {
                UID = Guid.NewGuid().ToString(),
                Name = departmentDto.Name,
                Location = departmentDto.Location
            };

            if (image != null)
            {
                dept.ImageUrl = UploadImage(image, "Departments");
            }

            _unitOfWork.DepartmentRepo.Add(dept);
            _unitOfWork.Save();
        }

        public void UpdateDepartment(UpdateDepartmentDto dto)
        {
            if (string.IsNullOrEmpty(dto.UID))
                dto.UID = Guid.NewGuid().ToString();

            var dept = new Department
            {
                Id = dto.Id,
                UID = dto.UID,
                Name = dto.Name,
                Location = dto.Location
            };

            _unitOfWork.DepartmentRepo.Update(dept);
            _unitOfWork.Save();
        }

        public void DeleteDepartment(DepartmentDto departmentDto)
        {
            var oldDept = _unitOfWork.DepartmentRepo.GetByUId(departmentDto.UID);
            if (oldDept != null)
            {
                _unitOfWork.DepartmentRepo.Delete(oldDept);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<DepartmentFile> GetDepartmentFiles(int departmentId)
        {
            return _unitOfWork.DepartmentRepo.DepartmentFiles
                .Where(e => e.DepartmentId == departmentId)
                .ToList();
        }

        public void AddDepartmentFile(DepartmentFile fileModel, IFormFile file)
        {
            if (file != null)
            {
                fileModel.FileURL = UploadImage(file, "Departments");
            }
            _unitOfWork.DepartmentRepo.AddDepartmentFile(fileModel);
            _unitOfWork.Save();
        }

        public void DeleteDepartmentFile(int fileId)
        {
            var file = _unitOfWork.DepartmentRepo.DepartmentFiles.FirstOrDefault(f => f.Id == fileId);
            if (file != null)
            {
                _unitOfWork.DepartmentRepo.DeleteDepartmentFile(file);
                _unitOfWork.Save();
            }
        }

        private string UploadImage(IFormFile image, string folderName)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);

            Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return $"/images/{folderName}/" + fileName;
        }
    }
}