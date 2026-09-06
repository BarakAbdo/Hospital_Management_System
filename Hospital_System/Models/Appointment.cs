using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Time { get; set; }
        public string? Status { get; set; }

        
        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }//Navigation Property

        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }//Navigation Property

    }
}
