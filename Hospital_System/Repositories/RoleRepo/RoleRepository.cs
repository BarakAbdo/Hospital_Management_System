using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.RoleRepo
{
    public class RoleRepository : Repository<Role> , IRoleRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Role> _dbSet;
        public RoleRepository(AppDbContext db) : base(db) 
        {
            _db = db;
            _dbSet = _db.Set<Role>();
        }

        public IEnumerable<Role> Roles => _db.Roles.ToList();
        public IEnumerable<Permission> Permissions => _db.Permissions.ToList();
        public IEnumerable<PermissionRole> PermissionRoles => _db.PermissionRoles.ToList();

        //public void Delete(Role role)
        //{
        //    _db.Remove(role);
        //}

        public Role? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void UpdatePermissions(int roleId, List<int> permissionIds)
        {
            var oldPermissions = _db.PermissionRoles.Where(pr => pr.RoleId == roleId).ToList();
            _db.PermissionRoles.RemoveRange(oldPermissions);

            if (permissionIds != null && permissionIds.Any())
            {
                foreach (var permissionId in permissionIds)
                {
                    _db.PermissionRoles.Add(new PermissionRole
                    {
                        RoleId = roleId,
                        PermissionId = permissionId
                    });
                }
            }
        }

    }
}
