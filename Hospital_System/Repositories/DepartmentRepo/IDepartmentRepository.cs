using Hospital_System.Models;

namespace Hospital_System.Repositories.DepartmentRepo
{
    public interface IDepartmentRepository
    {

        IEnumerable<Department> Departments { get; }
        IEnumerable<Department> GetAll();

            Department GetById(int id);
            Department GetByUId(string uid);

            void Add(Department department);


            void Update(Department department);


            void Delete(Department department);

            void Save();


        
    }
}
