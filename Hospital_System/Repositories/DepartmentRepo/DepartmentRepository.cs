using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.DepartmentRepo
{
    public class DepartmentRepository :Repository<Department>, IDepartmentRepository
    {

        private readonly AppDbContext _db;
        private readonly DbSet<Department> _dbSet;
        public DepartmentRepository(AppDbContext db) : base (db) 
        {
            _db = db;
            _dbSet = _db.Set<Department>();
        }

        public IEnumerable<Department> Departments => _db.Departments.ToList();

        public IEnumerable<DepartmentFile> DepartmentFiles => _db.DepartmentFiles.ToList();

        public Department? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
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

