using Hospital_Management_System.Application.Dtos.NotificationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface INotificationService
    {
        void CreateNotification(CreateNotificationDto dto);
        IEnumerable<NotificationDto> GetUserNotifications(int userId);
        void MarkAsRead(UpdateNotificationDto dto);
        void DeleteNotification(int id);
        int GetUnreadCount(int userId);
    }
}
