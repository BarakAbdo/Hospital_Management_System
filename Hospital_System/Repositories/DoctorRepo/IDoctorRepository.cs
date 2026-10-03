using Hospital_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.DoctorRepo
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        IEnumerable<Doctor> Doctors { get; }
        IEnumerable<Department> Departments { get; }
        IEnumerable<DoctorFile> DoctorFiles { get; }

        IEnumerable<Doctor> GetAllDoc();
        Doctor? GetByUId(string uid);

        void AddFile(DoctorFile doctorFile);

        void DeleteDoctorFile(DoctorFile doctorFile);

       

    }
}
