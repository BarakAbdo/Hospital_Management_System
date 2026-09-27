using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class MedicationFile
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string FileURL { get; set; } = "";

        [ForeignKey(nameof(Medications))]
        public int MedicationId { get; set; }

        public Medication? Medications { get; set; }
    }
}
