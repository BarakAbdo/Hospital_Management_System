using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class DepartmentFile
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }

        public string FileURL { get; set; } = "";


        [ForeignKey(nameof(Departments))]
        public int DepartmentId { get; set; }

        public Department? Departments { get; set; }
    }
}
