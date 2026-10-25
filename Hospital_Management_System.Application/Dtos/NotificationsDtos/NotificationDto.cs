using System;

namespace Hospital_Management_System.Application.Dtos.NotificationDtos
{
    
    public class CreateNotificationDto
    {
        public int UserId { get; set; }
        public int? OrderId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

   
    public class NotificationDto : CreateNotificationDto
    {
        public int Id { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

   
    public class UpdateNotificationDto
    {
        public int Id { get; set; }
        public bool IsRead { get; set; }
    }
}