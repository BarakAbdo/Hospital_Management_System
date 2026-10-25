using Hospital_Management_System.Application.Repositories.Base;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Data;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Infrastructure.Repositories.NotificationRepo
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Notification> _dbSet;
        public NotificationRepository(AppDbContext db) : base(db)
        {
            _db = db;
            _dbSet = _db.Set<Notification>();
        }

        public IEnumerable<Notification> GetUserNotifications(int userId)
        {
            return _dbSet
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }
    }
}
