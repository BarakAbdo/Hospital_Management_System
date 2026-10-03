
using Hospital_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.PrescriptionRepo
{
    public interface IPrescriptionRepository : IRepository<Prescription>
    {
        IEnumerable<Prescription> Prescriptions { get; }
        IEnumerable<PrescriptionFile> PrescriptionFiles { get; }
        IEnumerable<Patient> Patients { get; }
        IEnumerable<Doctor> Doctors { get; }
        IEnumerable<Medication> Medications { get; }

        IEnumerable<Prescription> GetAllPre();
        Prescription GetByUId(string uid);

        void AddFile(PrescriptionFile prescriptionFile);

        void DeletePrescriptionFile(PrescriptionFile prescriptionFile);
    }
}
