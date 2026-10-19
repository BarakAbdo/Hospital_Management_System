
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.AppointmentRepo
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        IEnumerable<Doctor> Doctors { get; }
        IEnumerable<Patient> Patients { get; }
        IEnumerable<AppointmentFile> AppointmentFiles { get; }

        IEnumerable<Appointment> GetAllApp();
        Appointment GetByUId(string uid);
        void AddFile(AppointmentFile appointmentFile);
        void DeleteAppointmentFile(AppointmentFile appointmentFile);
        


    }
}
