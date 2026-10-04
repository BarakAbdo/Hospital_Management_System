using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.MedicalRecordRepo
{
    public interface IMedicalRecordRepository : IRepository<MedicalRecord>
    {
        IEnumerable<MedicalRecord> MedicalRecords { get; }
        IEnumerable<Patient> Patients { get; }
        IEnumerable<Doctor> Doctors { get; }
        IEnumerable<MedicalRecordFile> MedicalRecordFiles { get; }

        IEnumerable<MedicalRecord> GetAllMedr();
        MedicalRecord GetByUId(string uid);

        void AddFile(MedicalRecordFile medicalRecordFile);

        void DeleteMedicalRecordFile(MedicalRecordFile medicalRecordFile);
    }
}
