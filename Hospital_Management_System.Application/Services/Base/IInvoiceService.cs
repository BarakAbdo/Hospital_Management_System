using Hospital_Management_System.Application.Dtos.InvoicesDtos;
using Hospital_Management_System.Application.Dtos.PatientsDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IInvoiceService
    {
        IEnumerable<InvoiceDto> GetAllInvoices();
        IEnumerable<PatientDto> GetAllPatients();
        InvoiceDto GetInvoiceById(int id);
        InvoiceDto GetInvoiceByUId(string uid);

        void AddInvoice(CreateInvoiceDto invoiceDto);
        void UpdateInvoice(UpdateInvoiceDto invoiceDto);
        void DeleteInvoice(InvoiceDto invoiceDto);

        IEnumerable<InvoiceFile> GetInvoiceFiles(int invoiceId);
        void AddInvoiceFile(InvoiceFile invoiceFile, IFormFile fileInvoice);
        void DeleteInvoiceFile(int fileId);
    }
}