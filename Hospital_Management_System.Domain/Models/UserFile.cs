using Hospital_Management_System.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management_System.Domain.Models
{
    public class UserFile
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        

        public string FileURL { get; set; } = "";

        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }

        public User? Users { get; set; }
    }
}
