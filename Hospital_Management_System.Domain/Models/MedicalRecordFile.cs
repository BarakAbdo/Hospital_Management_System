using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management_System.Domain.Models
{
    public class MedicalRecordFile
    {
        public int Id { get; set; }

        public string? Notes { get; set; }

        public string FileURL { get; set; } = "";

        

        [ForeignKey(nameof(MedicalRecords))]
        public int MedicalRecordId { get; set; }

        public MedicalRecord? MedicalRecords { get; set; }
    }
}
