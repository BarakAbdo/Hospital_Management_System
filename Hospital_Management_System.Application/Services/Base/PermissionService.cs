using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Application.Dtos.PermissionsDtos;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Application.Services.Base
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PermissionDto> GetAllPermissions()
        {
            var permissions = _unitOfWork.PermissionRepo.GetAll();

            var permissionDtos = permissions.Select(p => new PermissionDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name
            }).ToList();

            return permissionDtos;
        }

        public PermissionDto GetPermissionById(int id)
        {
            var p = _unitOfWork.PermissionRepo.GetById(id);

            var permissionDto = new PermissionDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name
            };
            return permissionDto;
        }

        public PermissionDto GetPermissionByUId(string uid)
        {
            var p = _unitOfWork.PermissionRepo.GetByUId(uid);

            var permissionDto = new PermissionDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name
            };
            return permissionDto;
        }

        public void AddPermission(CreatePermissionDto permissionDto)
        {
            var permission = new Permission
            {
                UID = Guid.NewGuid().ToString(),
                Name = permissionDto.Name
            };

            _unitOfWork.PermissionRepo.Add(permission);
            _unitOfWork.Save();
        }

        public void UpdatePermission(UpdatePermissionDto permissionDto)
        {
            var oldPermission = _unitOfWork.PermissionRepo.GetById(permissionDto.Id);
            if (oldPermission != null)
            {
                if (string.IsNullOrEmpty(permissionDto.UID))
                {
                    permissionDto.UID = Guid.NewGuid().ToString();
                }

                oldPermission.Name = permissionDto.Name;
                oldPermission.UID = permissionDto.UID;

                _unitOfWork.PermissionRepo.Update(oldPermission);
                _unitOfWork.Save();
            }
        }

        public void DeletePermission(PermissionDto permissionDto)
        {
            var oldPermission = _unitOfWork.PermissionRepo.GetByUId(permissionDto.UID);
            if (oldPermission != null)
            {
                _unitOfWork.PermissionRepo.Delete(oldPermission);
                _unitOfWork.Save();
            }
        }
    }
}