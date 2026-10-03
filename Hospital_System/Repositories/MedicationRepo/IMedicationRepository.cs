using Hospital_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.MedicationRepo
{
    public interface IMedicationRepository : IRepository<Medication>
    {
        IEnumerable<Medication> Medications { get; }
        IEnumerable<MedicationFile> MedicationFiles { get; }

        
        Medication GetByUId(string uid);

        void AddFile(MedicationFile medicationFile);
        void DeleteMedicationFile(MedicationFile medicationFile);
    }
}
