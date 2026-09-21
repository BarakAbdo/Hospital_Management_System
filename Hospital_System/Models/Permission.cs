namespace Hospital_Management_System.Models
{
    public class Permission
    {

        public int Id { get; set; }
        public string Name { get; set; } = "";

        public string UID { get; set; } = Guid.NewGuid().ToString();
        public ICollection<Role>? Roles { get; set; } = new List<Role>(); // Navigation property

    }
}
