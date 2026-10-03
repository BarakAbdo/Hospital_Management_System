using Hospital_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.PatientRepo
{
    public interface IPatientRepository : IRepository<Patient>
    {
        IEnumerable<Patient> Patients { get; }
        IEnumerable<PatientFile> PatientFiles { get; }




        Patient GetByUId(string uid);


        void AddFile(PatientFile patientFile);

        void DeletePatientFile(PatientFile patientFile);
    }
}
