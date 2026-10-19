using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.InvoiceRepo
{
    public interface IInvoiceRepository : IRepository<Invoice> 
    {
        
        IEnumerable<Invoice> Invoices { get; }
        IEnumerable<Patient> Patients { get; }
        IEnumerable<InvoiceFile> InvoiceFiles { get; }

        IEnumerable<Invoice> GetAllInvo();
        Invoice GetByUId(string uid);

    

        void AddFile(InvoiceFile invoiceFile);
        void DeleteInvoiceFile(InvoiceFile invoiceFile);

    }
}
