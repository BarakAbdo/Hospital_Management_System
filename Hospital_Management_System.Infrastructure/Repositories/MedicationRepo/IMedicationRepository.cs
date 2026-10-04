using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.MedicationRepo
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
