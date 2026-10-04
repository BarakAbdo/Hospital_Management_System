using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management_System.Domain.Models
{
    public class DoctorFile
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string FileURL { get; set; } = "";

        [ForeignKey(nameof(Doctors))]
        public int DoctorId { get; set; }

        public Doctor? Doctors { get; set; }
    }
}
