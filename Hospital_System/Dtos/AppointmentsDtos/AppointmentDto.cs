using Hospital_System.Models;

namespace Hospital_System.Dtos.AppointmentsDtos
{
    public class CreateAppointmentDto
    {
        public DateTime Date { get; set; }
        public string? Time { get; set; }
        public string? Status { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }

        public string UID { get; set; } = Guid.NewGuid().ToString();
    }

    public class UpdateAppointmentDto : CreateAppointmentDto
    {
        public int Id { get; set; }
    }

    public class AppointmentDto : UpdateAppointmentDto
    {

        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
    }
}
