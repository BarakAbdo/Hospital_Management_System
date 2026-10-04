using Hospital_Management_System.Application.Dtos.InvoicesDtos;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<InvoiceDto> GetAllInvoices()
        {
            var invoices = _unitOfWork.InvoiceRepo.GetAllInvo();

            var invoiceDtos = invoices.Select(i => new InvoiceDto
            {
                Id = i.Id,
                UID = i.UID,
                Amount = i.Amount,
                Date = i.Date,
                Status = i.Status,
                PatientId = i.PatientId,
                PatientName = i.Patients?.Name
            }).ToList();

            return invoiceDtos;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _unitOfWork.InvoiceRepo.Patients.ToList();
        }

        public Invoice GetInvoiceById(int id)
        {
            return _unitOfWork.InvoiceRepo.GetById(id);
        }

        public Invoice GetInvoiceByUId(string uid)
        {
            return _unitOfWork.InvoiceRepo.GetByUId(uid);
        }

        public void AddInvoice(CreateInvoiceDto invoiceDto)
        {
            var invoice = new Invoice
            {
                UID = Guid.NewGuid().ToString(),
                Amount = invoiceDto.Amount,
                Date = invoiceDto.Date,
                Status = invoiceDto.Status,
                PatientId = invoiceDto.PatientId
            };

            _unitOfWork.InvoiceRepo.Add(invoice);
            _unitOfWork.Save();
        }

        public void UpdateInvoice(UpdateInvoiceDto invoiceDto)
        {
            if (invoiceDto.UID == null)
            {
                invoiceDto.UID = Guid.NewGuid().ToString();
            }

            var invoice = new Invoice
            {
                Id = invoiceDto.Id,
                UID = invoiceDto.UID,
                Amount = invoiceDto.Amount,
                Date = invoiceDto.Date,
                Status = invoiceDto.Status,
                PatientId = invoiceDto.PatientId
            };

            _unitOfWork.InvoiceRepo.Update(invoice);
            _unitOfWork.Save();
        }

        public void DeleteInvoice(Invoice invoice)
        {
            var oldInvoice = _unitOfWork.InvoiceRepo.GetByUId(invoice.UID);
            if (oldInvoice != null)
            {
                _unitOfWork.InvoiceRepo.Delete(oldInvoice);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<InvoiceFile> GetInvoiceFiles(int invoiceId)
        {
            return _unitOfWork.InvoiceRepo.InvoiceFiles.Where(e => e.InvoiceId == invoiceId).ToList();
        }

        public void AddInvoiceFile(InvoiceFile invoiceFile, IFormFile fileInvoice)
        {
            if (fileInvoice != null)
            {
                invoiceFile.FileURL = UploadFiles(fileInvoice, invoiceFile.Status);
            }

            _unitOfWork.InvoiceRepo.AddFile(invoiceFile);
            _unitOfWork.Save();
        }

        public void DeleteInvoiceFile(int fileId)
        {
            var file = _unitOfWork.InvoiceRepo.InvoiceFiles.FirstOrDefault(f => f.Id == fileId);
            if (file != null)
            {
                _unitOfWork.InvoiceRepo.DeleteInvoiceFile(file);
                _unitOfWork.Save();
            }
        }

        private string UploadFiles(IFormFile file, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Files",
                "Invoices"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Invoices/" + fileName;
        }
    }
}
