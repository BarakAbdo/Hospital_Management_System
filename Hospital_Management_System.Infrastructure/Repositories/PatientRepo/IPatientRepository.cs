using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.PatientRepo
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
