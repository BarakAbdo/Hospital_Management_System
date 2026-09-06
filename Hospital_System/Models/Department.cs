namespace Hospital_System.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Location { get; set; }
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();//Navigation property
    }
}

