using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.AppointmentRepo
{
    public class AppointmentRepository : Repository<Appointment> , IAppointmentRepository
    {

        private readonly AppDbContext _db;
        private readonly DbSet<Appointment> _dbSet;
        public AppointmentRepository(AppDbContext db) : base(db) 
        {
            _db = db;
            _dbSet = _db.Set<Appointment>();
        }

        public IEnumerable<Doctor> Doctors => _db.Doctors.ToList();
        public IEnumerable<Patient> Patients => _db.Patients.ToList();
        public IEnumerable<AppointmentFile> AppointmentFiles => _db.AppointmentFiles.ToList();

        public IEnumerable<Appointment> GetAllApp()
        {
            return _db.Appointments
                       .Include(a => a.Patient)
                       .Include(a => a.Doctor)
                       .ToList();
        }

        public void AddFile(AppointmentFile appointmentFile)
        {
            _db.AppointmentFiles.Add(appointmentFile);
        }


        //public IEnumerable<Appointment> GetAll()
        //{
        //    return _dbSet.ToList();
        //}

        public Appointment? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void DeleteAppointmentFile(AppointmentFile appointmentFile)
        {
            _db.AppointmentFiles.Remove(appointmentFile);
        }
    }
}
