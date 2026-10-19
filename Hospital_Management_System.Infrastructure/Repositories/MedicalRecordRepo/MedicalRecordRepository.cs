using Hospital_Management_System.Infrastructure.Data;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Infrastructure.Repositories.MedicalRecordRepo
{
    public class MedicalRecordRepository : Repository<MedicalRecord>, IMedicalRecordRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<MedicalRecord> _dbSet;
        public MedicalRecordRepository(AppDbContext db) : base(db) 
        {
            _db = db;
            _dbSet = _db.Set<MedicalRecord>();
        }

        public IEnumerable<MedicalRecord> MedicalRecords => _db.MedicalRecords.Include(m => m.Patient).Include(m => m.Doctor).ToList();
        public IEnumerable<Patient> Patients => _db.Patients.ToList();
        public IEnumerable<Doctor> Doctors => _db.Doctors.ToList();
        public IEnumerable<MedicalRecordFile> MedicalRecordFiles => _db.MedicalRecordFiles.ToList();

        public IEnumerable<MedicalRecord> GetAllMedr()
        {
            return _dbSet.Include(m => m.Patient).Include(m => m.Doctor).ToList();
        }

        //public IEnumerable<MedicalRecord> GetAll()
        //{
        //    return _dbSet.ToList();
        //}

     

      

        public MedicalRecord? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

     

        public void AddFile(MedicalRecordFile medicalRecordFile)
        {
            _db.MedicalRecordFiles.Add(medicalRecordFile);
        }

        public void DeleteMedicalRecordFile(MedicalRecordFile medicalRecordFile)
        {
            _db.MedicalRecordFiles.Remove(medicalRecordFile);
        }
    }
}
