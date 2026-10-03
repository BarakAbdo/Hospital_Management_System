using Hospital_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.DepartmentRepo
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
