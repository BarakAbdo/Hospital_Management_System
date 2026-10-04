namespace Hospital_Management_System.Application.Dtos.MedicationsDtos
{
    public class CreateMedicationDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }
    public class UpdateMedicationDto : CreateMedicationDto
    { 
    public int Id { get; set; }
    }
    public class MedicationDto : UpdateMedicationDto 
    {
    
    }
}
