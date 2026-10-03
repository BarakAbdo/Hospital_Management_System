using Hospital_System.Dtos.InvoicesDtos;
using Hospital_System.Models;

namespace Hospital_System.Services.Base
{
    public interface IInvoiceService
    {
        IEnumerable<InvoiceDto> GetAllInvoices();
        IEnumerable<Patient> GetAllPatients();
        Invoice GetInvoiceById(int id);
        Invoice GetInvoiceByUId(string uid);
        void AddInvoice(CreateInvoiceDto invoiceDto);
        void UpdateInvoice(UpdateInvoiceDto invoiceDto);
        void DeleteInvoice(Invoice invoice);

        
        IEnumerable<InvoiceFile> GetInvoiceFiles(int invoiceId);
        void AddInvoiceFile(InvoiceFile invoiceFile, IFormFile fileInvoice);
        void DeleteInvoiceFile(int fileId);
    }
}
