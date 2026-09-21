using Hospital_System.Models;

namespace Hospital_System.Repositories.InvoiceRepo
{
    public interface IInvoiceRepository
    {
        
        IEnumerable<Invoice> Invoices { get; }
        IEnumerable<Patient> Patients { get; }
        IEnumerable<InvoiceFile> InvoiceFiles { get; }

        IEnumerable<Invoice> GetAll();
        Invoice GetById(int id);
        Invoice GetByUId(string uid);

        void Add(Invoice invoice);


        void Update(Invoice invoice);


        void Delete(Invoice invoice);

        void AddFile(InvoiceFile invoiceFile);
        void DeleteInvoiceFile(InvoiceFile invoiceFile);
        void Save();

    }
}
