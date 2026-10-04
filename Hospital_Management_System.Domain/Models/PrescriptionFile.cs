using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management_System.Domain.Models
{
    public class PrescriptionFile
    {
        public int Id { get; set; }

        public string ?Dosage { get; set; }

        public string FileURL { get; set; } = "";

        [ForeignKey(nameof(prescriptions))]
        public int PrescriptionId { get; set; }

        public Prescription? prescriptions { get; set; }
    }
}
