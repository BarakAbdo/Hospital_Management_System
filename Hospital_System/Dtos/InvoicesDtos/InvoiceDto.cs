using Hospital_System.Models;

namespace Hospital_System.Dtos.InvoicesDtos
{
    public class CreateInvoiceDto
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Status { get; set; }
        public int? PatientId { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }

    public class UpdateInvoiceDto : CreateInvoiceDto
    {
        public int Id { get; set; }
    }

    public class InvoiceDto : UpdateInvoiceDto
    {
        public string ? PatientName { get; set; }
    }
}
