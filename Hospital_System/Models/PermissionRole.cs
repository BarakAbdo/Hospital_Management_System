using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management_System.Models
{
    public class PermissionRole
    {
        [ForeignKey("Permissions")]
        public int PermissionId { get; set; }
        public Permission? Permissions { get; set; } // Navigation property


        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public Role? Roles { get; set; } // Navigation property
    }
}
