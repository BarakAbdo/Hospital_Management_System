using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.PermissionRepo
{
    public class PermissionRepository :Repository<Permission>, IPermissionRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Permission> _dbSet;

        public PermissionRepository(AppDbContext db) : base(db) 
        {
            _db = db;
            _dbSet = _db.Set<Permission>();
        }
        public IEnumerable<Permission> Permissions => _db.Permissions.ToList();

        public Permission? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

    }
}
