using Hospital_Management_System.Application.Dtos.PatientsDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IPatientService
    {
        IEnumerable<PatientDto> GetAllPatients();
        Patient GetPatientById(int id);
        Patient GetPatientByUId(string uid);
        void AddPatient(CreatePatientDto patientDto);
        void UpdatePatient(UpdatePatientDto patientDto);
        void DeletePatient(Patient patient);

        // دوال الملفات
        IEnumerable<PatientFile> GetPatientFiles(int patientId);
        void AddPatientFile(PatientFile patientFile, IFormFile filePatient);
        void DeletePatientFile(int fileId);
    }
}
