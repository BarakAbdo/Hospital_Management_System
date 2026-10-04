using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.PermissionRepo
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        IEnumerable<Permission> Permissions { get; }

        Permission GetByUId(string uid);

    }
}
