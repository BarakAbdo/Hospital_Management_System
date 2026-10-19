using Hospital_Management_System.Application.Dtos.DepartmentDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDto GetByUId(string uid);
        DepartmentDto GetById(int id);

        void AddDepartment(CreateDepartmentDto departmentDto, IFormFile image);
        void UpdateDepartment(UpdateDepartmentDto dto);
        void DeleteDepartment(DepartmentDto departmentDto);

        IEnumerable<DepartmentFile> GetDepartmentFiles(int departmentId);
        void AddDepartmentFile(DepartmentFile fileModel, IFormFile file);
        void DeleteDepartmentFile(int fileId);
    }
}