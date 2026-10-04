
namespace Hospital_Management_System.Application.Dtos
{
    public class UserRolesVM
    {

        public int UserId { get; set; }
        public string UserUid { get; set; } 
        public string? UserName { get; set; }

        public List<RoleCheckVM>? Roles { get; set; }
    }

    public class RoleCheckVM
    {
        public int RoleId { get; set; }
        public string RoleUid { get; set; } 
        public string? RoleName { get; set; }

        public bool IsSelected { get; set; }
    }
}

