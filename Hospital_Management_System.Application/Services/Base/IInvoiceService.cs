using Hospital_Management_System.Application.Dtos.InvoicesDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
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
