using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Data;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hospital_Management_System.Infrastructure.Repositories.CartRepo
{
    public class CartRepository : Repository<CartItem>, ICartRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<CartItem> _dbSet;

        public CartRepository(AppDbContext db) : base(db)
        {
            _db = db;
            _dbSet = _db.Set<CartItem>();
        }

        // استخدام دالة مخصصة بدل override لمنع الأخطاء تماماً
        public IEnumerable<CartItem> GetAllWithProduct()
        {
            return _dbSet
                .Include(c => c.Product)
                .ToList();
        }

        public IEnumerable<CartItem> GetCartByUserId(int userId)
        {
            return _dbSet
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToList();
        }

        public CartItem? GetCartItem(int userId, int productId)
        {
            return _dbSet
                .FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
        }
    }
}