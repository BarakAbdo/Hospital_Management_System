using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Application.Dtos.RolesDtos;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IRoleService
    {
        IEnumerable<RoleDto> GetAllRoles();
        Role GetRoleById(int id);
        Role GetRoleByUId(string uid);

        void AddRole(CreateRoleDto roleDto);
        void UpdateRole(UpdateRoleDto roleDto);
        void DeleteRole(string uid);

        
        IEnumerable<Permission> GetAllPermissions();
        IEnumerable<int> GetAssignedPermissionIds(int roleId);
        void UpdateRolePermissions(int roleId, List<int> permissionIds);
    }
}
