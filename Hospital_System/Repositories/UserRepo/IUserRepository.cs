using Hospital_Management_System.Models;
using Hospital_System.Models;

namespace Hospital_System.Repositories.UserRepo
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Role> Roles { get; }
        IEnumerable<UserRole> RoleUsers { get; }
        IEnumerable<UserFile> UserFiles { get; }
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByUId(string uid);

        void Add(User user);

        void Update(User user);

        void Delete(User user);


        void UpdateUserRoles(int userId, List<int> selectedRoleIds);
        void AddUserFile(UserFile userFile);

        void DeleteUserFile(UserFile userFile);
        void Save();
    }
}
