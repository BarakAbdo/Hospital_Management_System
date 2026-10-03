using Hospital_Management_System.Models;
using Hospital_System.Dtos.PermissionsDtos;

namespace Hospital_System.Services.Base
{
    public interface IPermissionService
    {
        IEnumerable<PermissionDto> GetAllPermissions();
        Permission GetPermissionById(int id);
        Permission GetPermissionByUId(string uid);
        void AddPermission(CreatePermissionDto permissionDto);
        void UpdatePermission(UpdatePermissionDto permissionDto);
        void DeletePermission(string uid);
    }
}
