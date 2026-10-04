namespace Hospital_Management_System.Domain.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
        public ICollection<Prescription> prescriptions { get; set; } = new List<Prescription>();

    }
}
