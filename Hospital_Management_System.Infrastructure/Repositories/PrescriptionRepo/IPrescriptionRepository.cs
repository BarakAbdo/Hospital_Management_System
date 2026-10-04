
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.PrescriptionRepo
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
