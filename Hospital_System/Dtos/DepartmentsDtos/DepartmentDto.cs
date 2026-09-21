namespace Hospital_System.Dtos.DepartmentDtos
{
    public class CreateDepartmentDto
    {
        public string Name { get; set; }
        public string Location { get; set; }

        public string UID { get; set; } = Guid.NewGuid().ToString();
    }

    public class UpdateDepartmentDto : CreateDepartmentDto
    {
        public int Id { get; set; }
    }

    public class DepartmentDto : UpdateDepartmentDto
    {
       
    }
}
