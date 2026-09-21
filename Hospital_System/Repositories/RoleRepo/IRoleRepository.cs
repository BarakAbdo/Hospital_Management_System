using Hospital_Management_System.Models;
using Hospital_System.Models;

namespace Hospital_System.Repositories.RoleRepo
{
    public interface IRoleRepository
    {

        IEnumerable<Role> Roles { get; }
        IEnumerable<Permission> Permissions { get; }
        IEnumerable<PermissionRole> PermissionRoles { get; }
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        Role GetByUId(string uid);

        void Add(Role role);

        void Update(Role role);

        void Delete(Role role);

        void UpdatePermissions(int roleId, List<int> permissionIds);
        
        void Save();
    }
}
