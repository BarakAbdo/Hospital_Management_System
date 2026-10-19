using Hospital_Management_System.Application.Dtos.AppointmentsDtos;
using Hospital_Management_System.Application.Dtos.DoctorsDtos;
using Hospital_Management_System.Application.Dtos.PatientsDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IAppointmentService
    {
        IEnumerable<AppointmentDto> GetAllAppointments();
        IEnumerable<DoctorDto> GetAllDoctors();
        IEnumerable<PatientDto> GetAllPatients();

        void AddAppointment(CreateAppointmentDto appointmentDto);
        void UpdateAppointment(UpdateAppointmentDto appointmentDto);
        void DeleteAppointment(AppointmentDto appointmentDto);

        AppointmentDto GetByUId(string uid);
        AppointmentDto GetById(int id);

        IEnumerable<AppointmentFile> GetAppointmentFiles(int appointmentId);
        void DeleteAppointmentFile(int fileId);
        void AddAppointmentFile(AppointmentFile appointmentFile, IFormFile file);
    }
}