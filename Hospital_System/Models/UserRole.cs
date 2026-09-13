using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management_System.Models
{
    public class UserRole
    {
        [ForeignKey("Roles")]
        public int? RoleId { get; set; }
        public Role? Roles { get; set; }

        [ForeignKey("Users")]
        public int? UserId { get; set; }
        public User? Users { get; set; }
    }
}
