using Hospital_System.Dtos.MedicalRecordsDtos;
using Hospital_System.Models;

namespace Hospital_System.Services.Base
{
    public interface IMedicalRecordService
    {
        IEnumerable<MedicalRecordDto> GetAllMedicalRecords();
        IEnumerable<Patient> GetAllPatients();
        IEnumerable<Doctor> GetAllDoctors();
        MedicalRecord GetMedicalRecordById(int id);
        MedicalRecord GetMedicalRecordByUId(string uid);
        void AddMedicalRecord(CreateMedicalRecordDto medicalRecordDto);
        void UpdateMedicalRecord(UpdateMedicalRecordDto medicalRecordDto);
        void DeleteMedicalRecord(MedicalRecord medicalRecord);

        // دوال الملفات
        IEnumerable<MedicalRecordFile> GetMedicalRecordFiles(int medicalRecordId);
        void AddMedicalRecordFile(MedicalRecordFile medicalRecordFile, IFormFile fileMedicalRecord);
        void DeleteMedicalRecordFile(int fileId);
    }
}
