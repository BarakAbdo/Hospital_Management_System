using Hospital_Management_System.Models;
using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Repositories.PermissionRepo
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Permission> _dbSet;

        public PermissionRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Permission>();
        }
        public IEnumerable<Permission> Permissions => _db.Permissions.ToList();
        public void Add(Permission permission)
        { 
            _dbSet.Add(permission);
        }

       

        public void Delete(Permission permission)
        {
            _db.Remove(permission);
        }

        public IEnumerable<Permission> GetAll()
        {
            return _dbSet.ToList();
        }

        public Permission? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public Permission? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Permission permission)
        {
            _dbSet.Update(permission);
        }

    }
}
