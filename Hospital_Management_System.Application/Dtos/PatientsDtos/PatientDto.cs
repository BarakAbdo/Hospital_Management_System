namespace Hospital_Management_System.Application.Dtos.PatientsDtos
{
    public class CreatePatientDto
    {
        public string? Name { get; set; }

        public string? Gender { get; set; }

        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string UID { get; set; } = Guid.NewGuid().ToString();
    }
    public class UpdatePatientDto : CreatePatientDto
    { 
    public int Id { get; set; }
    }
    public class PatientDto : UpdatePatientDto
    { 
    
    }
}
