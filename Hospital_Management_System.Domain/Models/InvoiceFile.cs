using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management_System.Domain.Models
{
    public class InvoiceFile
    {
        public int Id { get; set; }

        public string? Status { get; set; }

        public string FileURL { get; set; } = "";

        [ForeignKey(nameof(Invoices))]
        public int InvoiceId { get; set; }

        public Invoice? Invoices { get; set; }
    }
}
