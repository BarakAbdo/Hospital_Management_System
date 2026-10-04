using Hospital_Management_System.Application.Dtos.MedicalRecordsDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
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
