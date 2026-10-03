using Hospital_System.Dtos.AppointmentsDtos;
using Hospital_System.Models;

namespace Hospital_System.Services.Base
{
    public interface IAppointmentService
    {
        IEnumerable<AppointmentDto> GetAllAppointments();

        IEnumerable<Doctor> GetAllDoctors();
        IEnumerable<Patient> GetAllPatients();

        void AddAppointment(CreateAppointmentDto appointmentDto);
        void UpdateAppointment(UpdateAppointmentDto appointmentDto);
        void DeleteAppointment(Appointment appointment);

        Appointment GetByUId(string uid);
        Appointment GetById(int id);

        IEnumerable<AppointmentFile> GetAppointmentFiles(int appointmentId);
        void AddAppointmentFile(AppointmentFile appointmentFile, IFormFile file);
        void DeleteAppointmentFile(int fileId);
    }
}
