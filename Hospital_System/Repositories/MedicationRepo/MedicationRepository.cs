using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.MedicationRepo
{
    public class MedicationRepository :IMedicationRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Medication> _dbSet;
        public MedicationRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Medication>();
        }

        public IEnumerable<Medication> Medications => _db.Medications.ToList();
        public IEnumerable<MedicationFile> MedicationFiles => _db.MedicationFiles.ToList();

       

        public void Add(Medication medication)
        {
            _dbSet.Add(medication);
        }

        public void Delete(Medication medication)
        {
            _dbSet.Remove(medication);
        }

        public IEnumerable<Medication> GetAll()
        {
            return _dbSet.ToList();
        }

        public Medication? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public Medication? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Medication medication)
        {
            _dbSet.Update(medication);
        }
        public void AddFile(MedicationFile medicationFile)
        {
            _db.MedicationFiles.Add(medicationFile);
        }

        public void DeleteMedicationFile(MedicationFile medicationFile)
        {
            _db.MedicationFiles.Remove(medicationFile);
        }
    }
}
