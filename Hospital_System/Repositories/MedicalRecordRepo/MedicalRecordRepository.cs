using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.MedicalRecordRepo
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<MedicalRecord> _dbSet;
        public MedicalRecordRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<MedicalRecord>();
        }

        public IEnumerable<MedicalRecord> MedicalRecords => _db.MedicalRecords.Include(m => m.Patient).Include(m => m.Doctor).ToList();
        public IEnumerable<Patient> Patients => _db.Patients.ToList();
        public IEnumerable<Doctor> Doctors => _db.Doctors.ToList();
        public IEnumerable<MedicalRecordFile> MedicalRecordFiles => _db.MedicalRecordFiles.ToList();

        public IEnumerable<MedicalRecord> GetAll()
        {
            return _dbSet.Include(m => m.Patient).Include(m => m.Doctor).ToList();
        }

        //public IEnumerable<MedicalRecord> GetAll()
        //{
        //    return _dbSet.ToList();
        //}

        public void Add(MedicalRecord medicalRecord)
        {
            _dbSet.Add(medicalRecord);
        }

        public void Delete(MedicalRecord medicalRecord)
        {
            _dbSet.Remove(medicalRecord);
        }

        

        public MedicalRecord? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public MedicalRecord? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(MedicalRecord medicalRecord)
        {
            _dbSet.Update(medicalRecord);
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
