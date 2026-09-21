using Hospital_System.Models;

namespace Hospital_System.Repositories.MedicationRepo
{
    public interface IMedicationRepository
    {
        IEnumerable<Medication> Medications { get; }
        IEnumerable<MedicationFile> MedicationFiles { get; }
        IEnumerable<Medication> GetAll();

        Medication GetById(int id);
        Medication GetByUId(string uid);

        void Add(Medication medication);


        void Update(Medication medication);


        void Delete(Medication medication);

        void AddFile(MedicationFile medicationFile);
        void DeleteMedicationFile(MedicationFile medicationFile);
        void Save();
    }
}
