using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.PatientRepo
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Patient> _dbSet;
        public PatientRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Patient>();
        }

        public IEnumerable<Patient> Patients => _db.Patients.ToList();
        public IEnumerable<PatientFile> PatientFiles => _db.PatientFiles.ToList();

        public void Add(Patient patient)
        {
            _dbSet.Add(patient);
        }

        public void Delete(Patient patient)
        {
            _dbSet.Remove(patient);
        }

        public IEnumerable<Patient> GetAll()
        {
            return _dbSet.ToList();
        }

        public Patient? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public Patient? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Patient patient)
        {
            _dbSet.Update(patient);
        }

        public void AddFile(PatientFile patientFile)
        {
            _db.PatientFiles.Add(patientFile);
        }

        public void DeletePatientFile(PatientFile patientFile)
        {
            _db.PatientFiles.Remove(patientFile);
        }
    }
}
