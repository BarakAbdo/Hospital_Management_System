using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Data;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Infrastructure.Repositories.UserRepo
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<User> _dbSet;

        public UserRepository(AppDbContext db) : base(db) 
        {
            _db = db;
            _dbSet = _db.Set<User>();
        }
        public IEnumerable<User> Users => _db.Users.ToList();
        public IEnumerable<Role> Roles => _db.Roles.ToList();
        public IEnumerable<UserRole> RoleUsers => _db.RoleUsers;
        public IEnumerable<UserFile> UserFiles => _db.UserFiles.ToList();


        public User? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }


        public void UpdateUserRoles(int userId, List<int> selectedRoleIds)
        {
            var oldRoles = _db.RoleUsers.Where(x => x.UserId == userId).ToList();
            _db.RoleUsers.RemoveRange(oldRoles);

            if (selectedRoleIds != null && selectedRoleIds.Count > 0)
            {
                foreach (var roleId in selectedRoleIds)
                {
                    _db.RoleUsers.Add(new UserRole
                    {
                        UserId = userId,
                        RoleId = roleId
                    });
                }
            }
        }
        public void AddUserFile(UserFile userFile)
        {
            _db.UserFiles.Add(userFile);
        }
        public void DeleteUserFile(UserFile userFile)
        {
            _db.UserFiles.Remove(userFile);
        }
    }
}
