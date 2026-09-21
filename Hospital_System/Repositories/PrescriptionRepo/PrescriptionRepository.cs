using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.PrescriptionRepo
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Prescription> _dbSet;

        public PrescriptionRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Prescription>();
        }

        public IEnumerable<Prescription> Prescriptions => _db.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Include(p => p.Medication)
            .ToList();

        public IEnumerable<PrescriptionFile> PrescriptionFiles => _db.PrescriptionFiles.ToList();
        public IEnumerable<Patient> Patients => _db.Patients.ToList();
        public IEnumerable<Doctor> Doctors => _db.Doctors.ToList();
        public IEnumerable<Medication> Medications => _db.Medications.ToList();
        public void Add(Prescription prescription)
        {
            _dbSet.Add(prescription);
        }

        public void Delete(Prescription prescription)
        {
            _db.Remove(prescription);
        }

        public IEnumerable<Prescription> GetAll()
        {
            return _dbSet
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Medication)
                .ToList();
        }

        public Prescription? GetById(int id)
        {
            return _dbSet
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Medication)
                .FirstOrDefault(e => e.Id == id);
        }

        public Prescription? GetByUId(string uid)
        {
            return _dbSet
                 .Include(p => p.Patient)
                 .Include(p => p.Doctor)
                 .Include(p => p.Medication)
                 .FirstOrDefault(e => e.UID == uid);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Prescription prescription)
        {
            _dbSet.Update(prescription);
        }
        public void AddFile(PrescriptionFile prescriptionFile)
        {
            _db.PrescriptionFiles.Add(prescriptionFile);
        }


        public void DeletePrescriptionFile(PrescriptionFile prescriptionFile)
        {
            _db.PrescriptionFiles.Remove(prescriptionFile);
        }
    }
}
