using Hospital_Management_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.RoleRepo
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
