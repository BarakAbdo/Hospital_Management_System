using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Hospital_System.Models
{
    public class Invoice
    {
        public int Id { get; set;}
      
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Status { get; set; }

        public string UID { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Patient")]
        public int? PatientId { get; set; }//Foregin key property
        public Patient? Patients { get; set; }//Navigation Property
        

    }
}
