using Hospital_System.Models;

namespace Hospital_System.Repositories.MedicalRecordRepo
{
    public interface IMedicalRecordRepository
    {
        IEnumerable<MedicalRecord> MedicalRecords { get; }
        IEnumerable<Patient> Patients { get; }
        IEnumerable<Doctor> Doctors { get; }
        IEnumerable<MedicalRecordFile> MedicalRecordFiles { get; }


        IEnumerable<MedicalRecord> GetAll();

        MedicalRecord GetById(int id);
        MedicalRecord GetByUId(string uid);

        void Add(MedicalRecord medicalRecord);


        void Update(MedicalRecord medicalRecord);


        void Delete(MedicalRecord medicalRecord);

        void AddFile(MedicalRecordFile medicalRecordFile);

        void DeleteMedicalRecordFile(MedicalRecordFile medicalRecordFile);
        void Save();
    }
}
