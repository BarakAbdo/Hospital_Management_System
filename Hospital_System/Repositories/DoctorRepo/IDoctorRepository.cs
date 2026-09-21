using Hospital_System.Models;

namespace Hospital_System.Repositories.DoctorRepo
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> Doctors { get; }
        IEnumerable<Department> Departments { get; }
        IEnumerable<DoctorFile> DoctorFiles { get; }
        IEnumerable<Doctor> GetAll();

        Doctor GetById(int id);
        Doctor GetByUId(string uid);

        void Add(Doctor doctor);


        void Update(Doctor doctor);


        void Delete(Doctor doctor);

        void AddFile(DoctorFile doctorFile);

        void DeleteDoctorFile(DoctorFile doctorFile);

        void Save();

    }
}
