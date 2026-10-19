using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Data;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Infrastructure.Repositories.CategoryRepo
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Category> _dbSet;

        public CategoryRepository(AppDbContext db) : base(db)
        {
            _db = db;
            _dbSet = db.Set<Category>();
        }

        public Category? GetCategory(int id)
        {
            return _dbSet.Include(c => c.Products) .FirstOrDefault(c => c.Id == id);
        }

        public Category? GetByUId(string uid)
        {
            return _dbSet.Include(c => c.Products) .FirstOrDefault(c => c.UID == uid);
        }
    }
}
