using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.AppointmentRepo
{
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly AppDbContext _db;
        private readonly DbSet<Appointment> _dbSet;
        public AppointmentRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Appointment>();
        }

        public IEnumerable<Doctor> Doctors => _db.Doctors.ToList();
        public IEnumerable<Patient> Patients => _db.Patients.ToList();
        public IEnumerable<AppointmentFile> AppointmentFiles => _db.AppointmentFiles.ToList();

        public void Add(Appointment appointment)
        {
            _dbSet.Add(appointment);
        }

        public void Delete(Appointment appointment)
        {
            var relatedFiles = _db.AppointmentFiles
                                  .Where(f => f.AppointmentId == appointment.Id)
                                  .ToList();

            if (relatedFiles.Any())
            {
                _db.AppointmentFiles.RemoveRange(relatedFiles);
            }

            _dbSet.Remove(appointment);
        }
        

        public void AddFile(AppointmentFile appointmentFile)
        {
            _db.AppointmentFiles.Add(appointmentFile);
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _dbSet.Include(a => a.Patient).Include(a => a.Doctor).ToList();
        }

        //public IEnumerable<Appointment> GetAll()
        //{
        //    return _dbSet.ToList();
        //}

        public Appointment? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public Appointment? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        
        public void Update(Appointment appointment)
        {
            _dbSet.Update(appointment);
        }

        public void DeleteAppointmentFile(AppointmentFile appointmentFile)
        {
            _db.AppointmentFiles.Remove(appointmentFile);
        }
    }
}
