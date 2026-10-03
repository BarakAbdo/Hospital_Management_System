using Hospital_System.Data;
using Hospital_System.Repositories.AccountRepo;
using Hospital_System.Repositories.AppointmentRepo;
using Hospital_System.Repositories.DepartmentRepo;
using Hospital_System.Repositories.DoctorRepo;
using Hospital_System.Repositories.InvoiceRepo;
using Hospital_System.Repositories.MedicalRecordRepo;
using Hospital_System.Repositories.MedicationRepo;
using Hospital_System.Repositories.PatientRepo;
using Hospital_System.Repositories.PermissionRepo;
using Hospital_System.Repositories.PrescriptionRepo;
using Hospital_System.Repositories.RoleRepo;
using Hospital_System.Repositories.UserRepo;
using System.Runtime.CompilerServices;

namespace Hospital_System.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db) 
        {
            _db = db;
            AccountRepo = new AccountRepository(db);

            AppointmentRepo = new AppointmentRepository(db);

            DepartmentRepo = new DepartmentRepository(db);

            DoctorRepo = new DoctorRepository(db);

            InvoiceRepo = new InvoiceRepository(db);

            MedicalRecordRepo = new MedicalRecordRepository(db);

            MedicationRepo = new MedicationRepository(db);

            PatientRepo = new PatientRepository(db);

            PrescriptionRepo = new PrescriptionRepository(db);

            PermissionRepo = new PermissionRepository(db);

            RoleRepo = new RoleRepository(db);

            UserRepo = new UserRepository(db);
        }
        public IAccountRepository AccountRepo { get; }

        public IAppointmentRepository AppointmentRepo { get; }

        public IDepartmentRepository DepartmentRepo { get; }

        public IDoctorRepository DoctorRepo { get; }

        public IInvoiceRepository InvoiceRepo { get; }

        public IMedicalRecordRepository MedicalRecordRepo { get; }

        public IMedicationRepository MedicationRepo { get; }

        public IPatientRepository PatientRepo { get; }

        public IPrescriptionRepository PrescriptionRepo { get; }

        public IPermissionRepository PermissionRepo { get; }

        public IRoleRepository RoleRepo { get; }

        public IUserRepository UserRepo { get; }

        public void Save()
        { 
            _db.SaveChanges();
        }
    }
}
