using Hospital_Management_System.Application.Dtos.NotificationDtos;
using Hospital_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsApiController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsApiController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // 1. جلب إشعارات المستخدم
        [HttpGet("user/{userId}")]
        public IActionResult GetUserNotifications(int userId)
        {
            var notifications = _notificationService.GetUserNotifications(userId);
            return Ok(notifications);
        }

        // 2. جلب عدد الإشعارات غير المقروءة (مفيد جداً للـ Badge في الـ Navbar)
        [HttpGet("unread-count/{userId}")]
        public IActionResult GetUnreadCount(int userId)
        {
            int count = _notificationService.GetUnreadCount(userId);
            return Ok(new { count });
        }

        // 3. تحديث حالة الإشعار إلى مقروء
        [HttpPost("mark-as-read")]
        public IActionResult MarkAsRead([FromBody] UpdateNotificationDto dto)
        {
            _notificationService.MarkAsRead(dto);
            return Ok(new { success = true, message = "Notification marked as read successfully." });
        }
    }
}