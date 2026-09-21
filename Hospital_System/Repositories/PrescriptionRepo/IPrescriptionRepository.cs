using Hospital_Management_System.Models;
using Hospital_System.Models;

namespace Hospital_System.Repositories.PrescriptionRepo
{
    public interface IPrescriptionRepository
    {
        IEnumerable<Prescription> Prescriptions { get; }
        IEnumerable<PrescriptionFile> PrescriptionFiles { get; }
        IEnumerable<Patient> Patients { get; }
        IEnumerable<Doctor> Doctors { get; }
        IEnumerable<Medication> Medications { get; }

        IEnumerable<Prescription> GetAll();
        Prescription GetById(int id);
        Prescription GetByUId(string uid);

        void Add(Prescription prescription);

        void Update(Prescription prescription);

        void Delete(Prescription prescription);

        void AddFile(PrescriptionFile prescriptionFile);

        void DeletePrescriptionFile(PrescriptionFile prescriptionFile);
        void Save();
    }
}
