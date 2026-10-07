using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Application.Dtos.RolesDtos;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Application.Services.Base
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<RoleDto> GetAllRoles()
        {
            var roles = _unitOfWork.RoleRepo.GetAll();

            var roleDto = roles.Select(r => new RoleDto
            {
                Id = r.Id,
                UID = r.UID,
                Name = r.Name
            }).ToList();
            return roleDto;
        }

        public RoleDto GetRoleById(int id)
        {
            var r = _unitOfWork.RoleRepo.GetById(id);

            var roleDto = new RoleDto
            {
                Id = r.Id,
                UID = r.UID,
                Name = r.Name
            };
            return roleDto;
        }

        public RoleDto GetRoleByUId(string uid)
        {
            var r = _unitOfWork.RoleRepo.GetByUId(uid);

            var roleDto = new RoleDto
            {
                Id = r.Id,
                UID = r.UID,
                Name = r.Name
            };
            return roleDto;
        }

        public void AddRole(CreateRoleDto roleDto)
        {
            var role = new Role
            {
                UID = Guid.NewGuid().ToString(),
                Name = roleDto.Name
            };

            _unitOfWork.RoleRepo.Add(role);
            _unitOfWork.Save();
        }

        public void UpdateRole(UpdateRoleDto roleDto)
        {
            if (string.IsNullOrEmpty(roleDto.UID))
            {
                roleDto.UID = Guid.NewGuid().ToString();
            }

            var role = new Role
            {
                Id = roleDto.Id,
                UID = roleDto.UID,
                Name = roleDto.Name
            };

            _unitOfWork.RoleRepo.Update(role);
            _unitOfWork.Save();
        }

        public void DeleteRole(RoleDto roleDto)
        {
            var oldRole = _unitOfWork.RoleRepo.GetByUId(roleDto.UID);
            if (oldRole != null)
            {
                _unitOfWork.RoleRepo.Delete(oldRole);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<Permission> GetAllPermissions()
        {
            return _unitOfWork.RoleRepo.Permissions.ToList();
        }

        public IEnumerable<int> GetAssignedPermissionIds(int roleId)
        {
            return _unitOfWork.RoleRepo.PermissionRoles
                .Where(pr => pr.RoleId == roleId)
                .Select(pr => pr.PermissionId)
                .ToList();
        }

        public void UpdateRolePermissions(int roleId, List<int> permissionIds)
        {
            _unitOfWork.RoleRepo.UpdatePermissions(roleId, permissionIds);
            _unitOfWork.Save();
        }
    }
}