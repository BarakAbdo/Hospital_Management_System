using Hospital_Management_System.Domain.Models;

namespace Hospital_Management_System.Application.Dtos.PrescriptionsDtos
{
    public class CreatePrescriptionDto
    {
        public string? Dosage { get; set; }
        public string? Duration { get; set; }

        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? MedicationId { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }
    public class UpdatePrescriptionDto : CreatePrescriptionDto
    { 
    public int Id { get; set; }
    }
    public class PrescriptionDto : UpdatePrescriptionDto
    {
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public string? MedicationName { get; set; }
        
    }
}
