using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.PatientRepo
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Patient> _dbSet;
        public PatientRepository(AppDbContext db) : base(db) 
        {
            _db = db;
            _dbSet = _db.Set<Patient>();
        }

        public IEnumerable<Patient> Patients => _db.Patients.ToList();
        public IEnumerable<PatientFile> PatientFiles => _db.PatientFiles.ToList();


        public Patient? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
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
