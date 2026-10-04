namespace Hospital_Management_System.Application.Dtos.PermissionsDtos
{
    public class CreatePermissionDto
    {
        public string Name { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }
    public class UpdatePermissionDto : CreatePermissionDto
    { 
    public int Id { get; set; }

    }
    public class PermissionDto : UpdatePermissionDto
    { 
    
    }



}
