using Hospital_Management_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.PermissionRepo
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        IEnumerable<Permission> Permissions { get; }

        Permission GetByUId(string uid);

    }
}
