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


namespace Hospital_System.Repositories.Base
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
