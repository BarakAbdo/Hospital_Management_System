using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.DepartmentRepo
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly AppDbContext _db;
        private readonly DbSet<Department> _dbSet;
        public DepartmentRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Department>();
        }

        public IEnumerable<Department> Departments => _db.Departments.ToList();

        public IEnumerable<DepartmentFile> DepartmentFiles => _db.DepartmentFiles.ToList();

        public void Add(Department department)
        {

            if (department == null) return;
            _dbSet.Add(department);
        }

        public void Delete(Department department)
        {
            

            _dbSet.Remove(department);
          
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbSet.ToList();
        }

        public Department? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public Department? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Department department)
        {
            _dbSet.Update(department);
        }

        public void AddDepartmentFile(DepartmentFile departmentFile)
        {
            _db.DepartmentFiles.Add(departmentFile);
        }

        public void DeleteDepartmentFile(DepartmentFile departmentFile)
        {
            _db.DepartmentFiles.Remove(departmentFile);
        }
    }
}

