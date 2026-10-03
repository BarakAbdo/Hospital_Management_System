
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.DoctorRepo
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Doctor> _dbSet;
        public DoctorRepository(AppDbContext db) : base(db)
        {
            _db = db;
            _dbSet = _db.Set<Doctor>();
        }

        public IEnumerable<Doctor> Doctors => _db.Doctors.ToList();
        public IEnumerable<Department> Departments => _db.Departments.ToList();
        public IEnumerable<DoctorFile> DoctorFiles => _db.DoctorFiles.ToList();

        public IEnumerable<Doctor> GetAllDoc()
        {
            return _dbSet.Include(d => d.Department).ToList();
        }
      

        public Doctor? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void AddFile(DoctorFile doctorFile)
        {
            _db.DoctorFiles.Add(doctorFile);
        }

        public void DeleteDoctorFile(DoctorFile doctorFile)
        {
            _db.DoctorFiles.Remove(doctorFile);
        }

    }
}
