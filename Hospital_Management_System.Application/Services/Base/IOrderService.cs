using Hospital_Management_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IOrderService
    {
        Task<int> MakeOrder(int userId);
        Task<List<Order>> GetMyOrders(int userId);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<int?> UpdateStatus(int orderId, string newStatus);


    }
}
