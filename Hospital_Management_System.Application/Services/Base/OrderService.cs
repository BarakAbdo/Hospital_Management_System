using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Data;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Application.Services.Base
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(AppDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            IEnumerable<Order> orders = await _unitOfWork.OrderRepo.GetAll();
            return orders;
        }

        public async Task<List<Order>> GetMyOrders(int userId)
        {
            var orders = await _unitOfWork.OrderRepo.GetOrdersByUserIdAsync(userId);
            return orders.ToList();

        }



        public async Task<int> MakeOrder(int userId)
        {
            await using var transaction =
                await _db.Database.BeginTransactionAsync();

            var cartItems = await _db.CartItems
                .Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return 0;
            }

            var order = new Order
            {
                UserId = userId,
                Status = "Pending",

                Items = cartItems.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Product.Price
                }).ToList()
            };

            order.TotalAmount = order.Items.Sum(
                x => x.Price * x.Quantity
            );

            await _unitOfWork.OrderRepo.AddOrderAsync(order);

            await _db.SaveChangesAsync();

            _db.Notifications.Add(new Notification
            {
                UserId = userId,
                OrderId = order.Id,
                Title = "Order Created",
                Message =
                    $"Your order with ID {order.Id} has been placed successfully."
            });

            _db.CartItems.RemoveRange(cartItems);

            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return order.Id;
        }

        public async Task<int?> UpdateStatus(int orderId, string newStatus)
        {
            // 1. جلب الطلب
            var order = await _unitOfWork.OrderRepo.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return null;
            }

            // 2. تحديث الحالة
            order.Status = newStatus;
            await _unitOfWork.OrderRepo.UpdateOrderAsync(order);

            // 3. إنشاء الإشعار التلقائي لصاحب الطلب
            var notification = new Notification
            {
                UserId = order.UserId, // جلب رقم المستخدم ديناميكياً من الطلب
                OrderId = orderId,
                Title = "Order Status Update",
                Message = $"Your order #{orderId} status has been updated to {newStatus}",
                IsRead = false,
                CreatedAt = DateTime.Now
            };

            _unitOfWork.NotificationRepo.Add(notification);

            // 4. الحفظ النهائي
            _unitOfWork.Save();

            // 5. إرجاع رقم المستخدم لتوجيهه لصفحته تلقائياً
            return order.UserId;
        }
    }
}
