using Hospital_Management_System.Application.Dtos.NotificationDtos;
using Hospital_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // Display notifications for a specific user (إذا ضغطتي من القائمة الجانبية سيأخذ القيمة 1 افتراضياً)
        public IActionResult Index(int userId =1002)
        {
            var notifications = _notificationService.GetUserNotifications(userId);
            return View(notifications);
        }

        // Mark a notification as read
        [HttpPost]
        public IActionResult MarkAsRead(int id, int userId)
        {
            var dto = new UpdateNotificationDto
            {
                Id = id,
                IsRead = true
            };

            _notificationService.MarkAsRead(dto);

            return RedirectToAction(nameof(Index), new { userId });
        }

        // Delete a notification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, int userId)
        {
            _notificationService.DeleteNotification(id);
            return RedirectToAction(nameof(Index), new { userId });
        }
    }
}