namespace Hospital_System.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ICollection<Prescription> prescriptions { get; set; } = new List<Prescription>();

    }
}
