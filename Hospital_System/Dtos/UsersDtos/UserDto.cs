namespace Hospital_System.Dtos.UsersDtos
{
    public class CreateUserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();

    }

    public class UpdateUserDto : CreateUserDto
    {
        public int Id { get; set; }
    }

    public class UserDto : UpdateUserDto
    {
    }
}