using Hospital_Management_System.Application.Dtos;
using Hospital_Management_System.Application.Dtos.LoginsDtos;
using Hospital_Management_System.Application.Dtos.RegistersDtos;
using Hospital_Management_System.Application.Dtos.UsersDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Win32;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IUserService
    {
       
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserById(int id);
        UserDto GetUserByUId(string uid);

        void AddUser(CreateUserDto userDto);
        void UpdateUser(UpdateUserDto userDto);
        void DeleteUser(UserDto userDto);

        IEnumerable<Role> GetAllRoles();
        IEnumerable<int> GetUserRoleIds(int userId);
        void UpdateUserRoles(int userId, List<int> roleIds);

        IEnumerable<UserFile> GetUserFiles(int userId);
        void AddUserFile(UserFile userFile, IFormFile fileUser);
        void DeleteUserFile(int fileId);

        User? Login(LoginDto loginDto);

        User? Register(RegisterDto registerDto);
    }
}