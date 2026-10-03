using Hospital_Management_System.Models;
using Hospital_System.Dtos.UsersDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Services.Base
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _unitOfWork.UserRepo.GetAll();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                UID = u.UID,
                Name = u.Name,
                Email = u.Email,
                Username = u.Username
            }).ToList();
        }

        public User GetUserById(int id)
        {
            return _unitOfWork.UserRepo.GetById(id);
        }

        public User GetUserByUId(string uid)
        {
            return _unitOfWork.UserRepo.GetByUId(uid);
        }

        public void AddUser(CreateUserDto userDto)
        {
            var user = new User
            {
                UID = Guid.NewGuid().ToString(),
                Name = userDto.Name,
                Email = userDto.Email,
                Username = userDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
            };

            _unitOfWork.UserRepo.Add(user);
            _unitOfWork.Save();
        }

        public void UpdateUser(UpdateUserDto userDto)
        {
            var oldUser = _unitOfWork.UserRepo.GetById(userDto.Id);
            if (oldUser == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(userDto.UID))
            {
                userDto.UID = Guid.NewGuid().ToString();
            }

            oldUser.UID = userDto.UID;
            oldUser.Name = userDto.Name;
            oldUser.Email = userDto.Email;
            oldUser.Username = userDto.Username;

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                oldUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }

            _unitOfWork.UserRepo.Update(oldUser);
            _unitOfWork.Save();
        }

        public void DeleteUser(string uid)
        {
            var oldUser = _unitOfWork.UserRepo.GetByUId(uid);
            if (oldUser != null)
            {
                _unitOfWork.UserRepo.Delete(oldUser);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _unitOfWork.UserRepo.Roles.ToList();
        }

        public IEnumerable<int> GetUserRoleIds(int userId)
        {
            return _unitOfWork.UserRepo.RoleUsers
                .Where(x => x.UserId == userId)
                .Select(x => (int)x.RoleId) 
                .ToList();
        }

        public void UpdateUserRoles(int userId, List<int> roleIds)
        {
            _unitOfWork.UserRepo.UpdateUserRoles(userId, roleIds);
            _unitOfWork.Save();
        }

        public IEnumerable<UserFile> GetUserFiles(int userId)
        {
            return _unitOfWork.UserRepo.UserFiles.Where(e => e.UserId == userId).ToList();
        }

        public void AddUserFile(UserFile userFile, IFormFile fileUser)
        {
            if (fileUser != null)
            {
                userFile.FileURL = UploadFiles(fileUser, userFile.Name);
                _unitOfWork.UserRepo.AddUserFile(userFile);
                _unitOfWork.Save();
            }
        }

        public void DeleteUserFile(int fileId)
        {
            var file = _unitOfWork.UserRepo.UserFiles.FirstOrDefault(f => f.Id == fileId);
            if (file != null)
            {
                _unitOfWork.UserRepo.DeleteUserFile(file);
                _unitOfWork.Save();
            }
        }

        private string UploadFiles(IFormFile file, string name)
        {
            if (file == null)
            {
                return string.Empty;
            }

            string fileName = name + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Files",
                "Users"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Users/" + fileName;
        }
    }
}
