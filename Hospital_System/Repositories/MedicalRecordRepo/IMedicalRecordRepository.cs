using Hospital_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.MedicalRecordRepo
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
