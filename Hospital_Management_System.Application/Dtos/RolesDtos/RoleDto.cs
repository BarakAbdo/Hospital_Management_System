namespace Hospital_Management_System.Application.Dtos.RolesDtos
{
    public class CreateRoleDto
    {
        public string Name { get; set; } = "";
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }
    public class UpdateRoleDto : CreateRoleDto
    {
    public int Id { get; set; } 
    }
    public class RoleDto : UpdateRoleDto
    { 
    
    }
}
