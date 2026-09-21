using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class AppointmentFile
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Time { get; set; }
        public string? Status { get; set; }

        public string FileURL { get; set; } = "";

        [ForeignKey(nameof(Appointments))]
        public int? AppointmentId { get; set; }

        public Appointment? Appointments { get; set; }
    }
}
