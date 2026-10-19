using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management_System.Domain.Models
{
    public class PatientFile
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string FileURL { get; set; } = "";

        [ForeignKey(nameof(patients))]
        public int PatientId { get; set; }

        public Patient? patients { get; set; }
    }
}
