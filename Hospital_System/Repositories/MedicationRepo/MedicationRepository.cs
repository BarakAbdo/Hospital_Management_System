using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.MedicationRepo
{
    public class MedicationRepository : Repository<Medication>, IMedicationRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Medication> _dbSet;
        public MedicationRepository(AppDbContext db) : base (db)
        {
            _db = db;
            _dbSet = _db.Set<Medication>();
        }

        public IEnumerable<Medication> Medications => _db.Medications.ToList();
        public IEnumerable<MedicationFile> MedicationFiles => _db.MedicationFiles.ToList();

      

        public Medication? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
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
