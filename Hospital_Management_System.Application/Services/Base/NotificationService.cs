using Hospital_Management_System.Application.Dtos.NotificationDtos;
using Hospital_Management_System.Application.Services.Base;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital_Management_System.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // إنشاء إشعار جديد وحفظه في القاعدة
        public void CreateNotification(CreateNotificationDto dto)
        {
            var notification = new Notification
            {
                UserId = dto.UserId,
                OrderId = dto.OrderId,
                Title = dto.Title,
                Message = dto.Message,
                IsRead = false,
                CreatedAt = DateTime.Now
            };

            _unitOfWork.NotificationRepo.Add(notification);
            _unitOfWork.Save();
        }

        public IEnumerable<NotificationDto> GetUserNotifications(int userId)
        {
            var notifications = _unitOfWork.NotificationRepo.GetAll()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            return notifications.Select(n => new NotificationDto
            {
                Id = n.Id,
                UserId = n.UserId,
                OrderId = n.OrderId,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            });
        }
        public void MarkAsRead(UpdateNotificationDto dto)
        {
            var notification = _unitOfWork.NotificationRepo.GetById(dto.Id);
            if (notification != null)
            {
                notification.IsRead = dto.IsRead;
                _unitOfWork.Save();
            }
        }

        // حذف إشعار
        public void DeleteNotification(int id)
        {
            var notification = _unitOfWork.NotificationRepo.GetById(id);
            if (notification != null)
            {
                _unitOfWork.NotificationRepo.Delete(notification);
                _unitOfWork.Save();
            }
        }

        public int GetUnreadCount(int userId)
        {
            return _unitOfWork.NotificationRepo.GetAll()
                .Where(n => n.UserId == userId && !n.IsRead)
                .Count();
        }
    }
}