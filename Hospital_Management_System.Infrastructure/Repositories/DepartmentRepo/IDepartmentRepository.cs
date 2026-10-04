using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.DepartmentRepo
{
    public interface IDepartmentRepository : IRepository<Department>
    {

        IEnumerable<Department> Departments { get; }
        IEnumerable<DepartmentFile> DepartmentFiles { get; }

        Department GetByUId(string uid);

           
        void AddDepartmentFile(DepartmentFile departmentFile);
        void DeleteDepartmentFile(DepartmentFile departmentFile);

        
    }
}
