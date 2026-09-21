namespace Hospital_System.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Location { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();

        public string? ImageUrl { get; set; }
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();//Navigation property
    }
}

