using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public string? Dosage { get; set; }
        public string? Duration { get; set; }

        [ForeignKey("patient")]
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        //------------------------------------

        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        //------------------------------------
        [ForeignKey("Medication")]
        public int? MedicationId { get; set; }
        public Medication? Medication { get; set; }

    }
}
