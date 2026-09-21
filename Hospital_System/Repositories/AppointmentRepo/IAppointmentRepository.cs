
using Hospital_System.Models;

namespace Hospital_System.Repositories.AppointmentRepo
{
    public interface IAppointmentRepository
    {
        IEnumerable<Doctor> Doctors { get; }
        IEnumerable<Patient> Patients { get; }
        IEnumerable<AppointmentFile> AppointmentFiles { get; }
        IEnumerable<Appointment> GetAll();

        Appointment GetById(int id);
        Appointment GetByUId(string uid);

        void Add(Appointment appointment);


        void Update(Appointment appointment);


        void Delete(Appointment appointment);

        void AddFile(AppointmentFile appointmentFile);
        void DeleteAppointmentFile(AppointmentFile appointmentFile);
        void Save();


    }
}
