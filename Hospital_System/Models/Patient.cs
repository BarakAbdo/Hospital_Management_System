namespace Hospital_System.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Gender { get; set; }

        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Appointment> appointments { get; set; } = new List<Appointment>();//Navigation property
        public ICollection<Invoice> invoices { get; set; } = new List<Invoice>();//Navigation Property
        public ICollection<MedicalRecord> medicalRecords { get; set; } = new List<MedicalRecord>();
        public ICollection<Prescription> prescriptions { get; set; } = new List<Prescription>();
    }
}
