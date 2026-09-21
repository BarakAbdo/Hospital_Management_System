using Hospital_Management_System.Models;

namespace Hospital_System.Repositories.PermissionRepo
{
    public interface IPermissionRepository
    {
        IEnumerable<Permission> Permissions { get; }

        IEnumerable<Permission> GetAll();
        Permission GetById(int id);
        Permission GetByUId(string uid);

        void Add(Permission permission);

        void Update(Permission permission);

        void Delete(Permission permission);

        void Save();
    }
}
