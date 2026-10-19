using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Infrastructure.Repositories.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Role> Roles { get; }
        IEnumerable<UserRole> RoleUsers { get; }
        IEnumerable<UserFile> UserFiles { get; }
        User? GetByUsername(string username);


        User? GetByEmail(string email);

        void Add(User user);
        User GetByUId(string uid);
        
        void UpdateUserRoles(int userId, List<int> selectedRoleIds);
        void AddUserFile(UserFile userFile);

        void DeleteUserFile(UserFile userFile);
    }
}
