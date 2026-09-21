using Hospital_System.Models;

namespace Hospital_System.Dtos.DoctorsDtos
{
    public class CreateDoctorDto
    {
        public string Name { get; set; }
        public string Specialization { get; set; }

        public string Phone { get; set; }

        public int DepartmentId { get; set; }

        public string UID { get; set; } = Guid.NewGuid().ToString();

    }

    public class UpdateDoctorDto : CreateDoctorDto
    {
    public int Id { get; set; }
    }
    public class DoctorDto : UpdateDoctorDto
    {
        public string? DepartmentName { get; set; }
    }
}
