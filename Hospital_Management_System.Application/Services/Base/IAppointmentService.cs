using Hospital_Management_System.Application.Dtos.AppointmentsDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
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
