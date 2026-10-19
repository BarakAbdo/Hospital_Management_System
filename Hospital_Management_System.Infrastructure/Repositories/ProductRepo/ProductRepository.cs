using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Data;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Infrastructure.Repositories.ProductRepo
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Product> _dbSet;

        public ProductRepository(AppDbContext db) : base(db) 
        {
            _db = db;
            _dbSet = _db.Set<Product>();
        }

        public IEnumerable<Product> GetAllCategory()
        {
            return _dbSet.Include(p => p.Category).ToList();
        }

        public Product? GetByUId(string uid)
        {
            return _dbSet.Include(p => p.Category).FirstOrDefault(p => p.UID == uid);
        }
    }
}
