using Hospital_Management_System.Application.Dtos.DoctorsDtos;
using Hospital_Management_System.Application.Dtos.MedicalRecordsDtos;
using Hospital_Management_System.Application.Dtos.PatientsDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IMedicalRecordService
    {
        IEnumerable<MedicalRecordDto> GetAllMedicalRecords();
        IEnumerable<PatientDto> GetAllPatients();
        IEnumerable<DoctorDto> GetAllDoctors();
        MedicalRecordDto GetMedicalRecordById(int id);
        MedicalRecordDto GetMedicalRecordByUId(string uid);

        void AddMedicalRecord(CreateMedicalRecordDto medicalRecordDto);
        void UpdateMedicalRecord(UpdateMedicalRecordDto medicalRecordDto);
        void DeleteMedicalRecord(MedicalRecordDto medicalRecordDto);

        IEnumerable<MedicalRecordFile> GetMedicalRecordFiles(int medicalRecordId);
        void AddMedicalRecordFile(MedicalRecordFile medicalRecordFile, IFormFile fileMedicalRecord);
        void DeleteMedicalRecordFile(int fileId);
    }
}
