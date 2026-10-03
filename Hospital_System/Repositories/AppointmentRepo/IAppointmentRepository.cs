
using Hospital_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.AppointmentRepo
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
