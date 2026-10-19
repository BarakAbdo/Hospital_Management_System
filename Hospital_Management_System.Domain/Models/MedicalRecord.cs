using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management_System.Domain.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public string? Diagnosis { get; set; }
        public string? Notes { get; set; }

        public string UID { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }//Foreign Key Property
        public Doctor? Doctor { get; set; }//Navigation Property
    }
}
