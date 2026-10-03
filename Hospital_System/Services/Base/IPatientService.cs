using Hospital_System.Dtos.PatientsDtos;
using Hospital_System.Models;

namespace Hospital_System.Services.Base
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
