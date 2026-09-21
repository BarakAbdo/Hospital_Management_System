using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<User> _dbSet;

        public UserRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<User>();
        }
        public void Add(User user)
        {
            _dbSet.Add(user);
        }

        public IEnumerable<User> Users => _db.Users.ToList();
        public IEnumerable<Role> Roles => _db.Roles.ToList();
        public IEnumerable<UserRole> RoleUsers => _db.RoleUsers.ToList();
        public IEnumerable<UserFile> UserFiles => _db.UserFiles.ToList();

        public void Delete(User user)
        {
            _db.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbSet.ToList();
        }

        public User? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public User? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(User user)
        {
            _dbSet.Update(user);
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
