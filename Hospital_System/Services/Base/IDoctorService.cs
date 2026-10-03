using Hospital_System.Dtos.DoctorsDtos;
using Hospital_System.Models;

namespace Hospital_System.Services.Base
{
    public interface IDoctorService
    {
        IEnumerable<DoctorDto> GetAllDoctors();
        IEnumerable<Department> GetAllDepartments();
        Doctor GetDoctorById(int id);
        Doctor GetDoctorByUId(string uid);
        void AddDoctor(CreateDoctorDto doctorDto);
        void UpdateDoctor(UpdateDoctorDto doctorDto);
        void DeleteDoctor(Doctor doctor);

       
        IEnumerable<DoctorFile> GetDoctorFiles(int doctorId);
        void AddDoctorFile(DoctorFile doctorFile, IFormFile fileDoctor);
        void DeleteDoctorFile(int fileId);
    }
}
