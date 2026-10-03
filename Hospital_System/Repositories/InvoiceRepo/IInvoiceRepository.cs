using Hospital_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.InvoiceRepo
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
