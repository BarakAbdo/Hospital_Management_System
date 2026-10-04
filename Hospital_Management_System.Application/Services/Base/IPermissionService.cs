using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Application.Dtos.PermissionsDtos;

namespace Hospital_Management_System.Application.Services.Base
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
