using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class Doctor
    {

        [Key]
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Specialization { get; set; }
        public string? Phone { get; set; }
        
        [ForeignKey("Department")]
        public int DepartmentId { get; set; } //Foregin key property
        public Department? Department { get; set; }//Navigation Property

        public ICollection<Appointment> appointments { get; set; } = new List<Appointment>();//Navigation property
        public ICollection<MedicalRecord> medicalRecords { get; set; } = new List<MedicalRecord>();//Navigation Property
        public ICollection<Prescription> prescriptions { get; set; } = new List<Prescription>();//Navigation Property

    }
}
