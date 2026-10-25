using Hospital_Management_System.Application.Repositories.Base;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

namespace Hospital_Management_System.Application.Repositories.Base
{
    public interface INotificationRepository : IRepository<Notification>
    {
        IEnumerable<Notification> GetUserNotifications(int userId);
    }
}