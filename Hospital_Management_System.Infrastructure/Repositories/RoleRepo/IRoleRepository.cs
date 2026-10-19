using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.RoleRepo
{
    public interface IRoleRepository : IRepository<Role>
    {

        IEnumerable<Role> Roles { get; }
        IEnumerable<Permission> Permissions { get; }
        IEnumerable<PermissionRole> PermissionRoles { get; }
        Role GetByUId(string uid);
        void UpdatePermissions(int roleId, List<int> permissionIds);
        
        
    }
}
