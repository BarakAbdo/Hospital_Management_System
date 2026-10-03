using Hospital_Management_System.Models;
using Hospital_System.Dtos.UsersDtos;
using Hospital_System.Models;

namespace Hospital_System.Services.Base
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
