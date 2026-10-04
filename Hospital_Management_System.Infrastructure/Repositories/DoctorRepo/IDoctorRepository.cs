using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.DoctorRepo
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
