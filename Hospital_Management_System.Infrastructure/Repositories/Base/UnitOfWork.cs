using Hospital_Management_System.Infrastructure.Data;
using Hospital_Management_System.Infrastructure.Repositories.AccountRepo;
using Hospital_Management_System.Infrastructure.Repositories.AppointmentRepo;
using Hospital_Management_System.Infrastructure.Repositories.DepartmentRepo;
using Hospital_Management_System.Infrastructure.Repositories.DoctorRepo;
using Hospital_Management_System.Infrastructure.Repositories.InvoiceRepo;
using Hospital_Management_System.Infrastructure.Repositories.MedicalRecordRepo;
using Hospital_Management_System.Infrastructure.Repositories.MedicationRepo;
using Hospital_Management_System.Infrastructure.Repositories.PatientRepo;
using Hospital_Management_System.Infrastructure.Repositories.PermissionRepo;
using Hospital_Management_System.Infrastructure.Repositories.PrescriptionRepo;
using Hospital_Management_System.Infrastructure.Repositories.RoleRepo;
using Hospital_Management_System.Infrastructure.Repositories.UserRepo;
using System.Runtime.CompilerServices;

namespace Hospital_Management_System.Infrastructure.Repositories.Base
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
