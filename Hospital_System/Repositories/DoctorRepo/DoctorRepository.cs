
using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.DoctorRepo
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Doctor> _dbSet;
        public DoctorRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Doctor>();
        }

        public IEnumerable<Doctor> Doctors => _db.Doctors.ToList();
        public IEnumerable<Department> Departments => _db.Departments.ToList();
        public IEnumerable<DoctorFile> DoctorFiles => _db.DoctorFiles.ToList();

        public IEnumerable<Doctor> GetAll()
        {
            return _dbSet.Include(d => d.Department).ToList();
        }

        public void Add(Doctor doctor)
        {
            _dbSet.Add(doctor);
        }

        public void Delete(Doctor doctor)
        {
            _dbSet.Remove(doctor);
        }

       

        public Doctor? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public Doctor? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Doctor doctor)
        {
            _dbSet.Update(doctor);
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
