using Hospital_System.Dtos.DepartmentDtos;
using Hospital_System.Models;

namespace Hospital_System.Services.Base
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDto> GetAllDepartments();
        Department GetByUId(string uid);
        Department GetById(int id);
        void AddDepartment(CreateDepartmentDto dto, IFormFile image);
        void UpdateDepartment(UpdateDepartmentDto dto);
        void DeleteDepartment(Department department);

        
        IEnumerable<DepartmentFile> GetDepartmentFiles(int departmentId);
        void AddDepartmentFile(DepartmentFile fileModel, IFormFile file);
        void DeleteDepartmentFile(int fileId);
    }
}
