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


namespace Hospital_Management_System.Infrastructure.Repositories.Base
{
    public interface IUnitOfWork
    {
        IAccountRepository  AccountRepo { get; }

        IAppointmentRepository AppointmentRepo { get; }

        IDepartmentRepository DepartmentRepo { get; }

        IDoctorRepository DoctorRepo { get; }

        IInvoiceRepository InvoiceRepo { get; }

        IMedicalRecordRepository MedicalRecordRepo { get; }

        IMedicationRepository MedicationRepo { get; }

        IPatientRepository PatientRepo { get; }

        IPrescriptionRepository PrescriptionRepo { get; }

        IPermissionRepository PermissionRepo { get; }

        IRoleRepository RoleRepo { get; }

        
        IUserRepository UserRepo { get; }

        void Save();
    }
}
