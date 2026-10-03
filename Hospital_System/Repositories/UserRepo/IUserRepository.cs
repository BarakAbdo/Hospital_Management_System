using Hospital_Management_System.Models;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Repositories.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Role> Roles { get; }
        IEnumerable<UserRole> RoleUsers { get; }
        IEnumerable<UserFile> UserFiles { get; }
        User GetByUId(string uid);
        
        void UpdateUserRoles(int userId, List<int> selectedRoleIds);
        void AddUserFile(UserFile userFile);

        void DeleteUserFile(UserFile userFile);
    }
}
