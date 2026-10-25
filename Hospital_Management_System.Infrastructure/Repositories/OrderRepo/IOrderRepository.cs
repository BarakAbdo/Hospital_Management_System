using Hospital_Management_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Infrastructure.Repositories.OrderRepo
{
    public interface IOrderRepository
    {

        Task<IEnumerable<Order>> GetAll();

        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
    }
}
