using Hospital_System.Models;

namespace Hospital_System.Dtos.MedicalRecordsDtos
{
    public class CreateMedicalRecordDto
    {
        public string? Diagnosis { get; set; }
        public string? Notes { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }
    public class UpdateMedicalRecordDto : CreateMedicalRecordDto
    {
        public int Id { get; set; }
    }
    public class MedicalRecordDto : UpdateMedicalRecordDto
    {
        public string? PatientName { get; set; }

        public string? DoctorName { get; set; }
    }
}
