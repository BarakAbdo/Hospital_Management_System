using Hospital_Management_System.Application.Dtos.PatientsDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IPatientService
    {
        IEnumerable<PatientDto> GetAllPatients();
        PatientDto GetPatientById(int id);
        PatientDto GetPatientByUId(string uid);

        void AddPatient(CreatePatientDto patientDto);
        void UpdatePatient(UpdatePatientDto patientDto);
        void DeletePatient(PatientDto patientDto);

        IEnumerable<PatientFile> GetPatientFiles(int patientId);
        void AddPatientFile(PatientFile patientFile, IFormFile filePatient);
        void DeletePatientFile(int fileId);
    }
}