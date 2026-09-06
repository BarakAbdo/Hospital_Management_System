using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public string? Diagnosis { get; set; }
        public string? Notes { get; set; }

        [ForeignKey("Paient")]
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }//Foreign Key Property
        public Doctor? Doctor { get; set; }//Navigation Property
    }
}
