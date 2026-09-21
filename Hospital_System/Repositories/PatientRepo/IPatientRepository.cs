using Hospital_System.Models;

namespace Hospital_System.Repositories.PatientRepo
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> Patients { get; }
        IEnumerable<PatientFile> PatientFiles { get; }

        IEnumerable<Patient> GetAll();

        Patient GetById(int id);
        Patient GetByUId(string uid);

        void Add(Patient patient);

        void Update(Patient patient);

        void Delete(Patient patient);

        void AddFile(PatientFile patientFile);

        void DeletePatientFile(PatientFile patientFile);
        void Save();
    }
}
