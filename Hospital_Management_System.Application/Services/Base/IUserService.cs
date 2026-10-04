using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Application.Dtos.UsersDtos;
using Microsoft.AspNetCore.Http;


namespace Hospital_Management_System.Application.Services.Base
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsers();
        User GetUserById(int id);
        User GetUserByUId(string uid);
        void AddUser(CreateUserDto userDto);
        void UpdateUser(UpdateUserDto userDto);
        void DeleteUser(string uid);

        IEnumerable<Role> GetAllRoles();
        IEnumerable<int> GetUserRoleIds(int userId);
        void UpdateUserRoles(int userId, List<int> roleIds);

        IEnumerable<UserFile> GetUserFiles(int userId);
        void AddUserFile(UserFile userFile, IFormFile fileUser);
        void DeleteUserFile(int fileId);
    }
}
