using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using System.Collections.Generic;

namespace Hospital_Management_System.Infrastructure.Repositories.CartRepo
{
    public interface ICartRepository : IRepository<CartItem>
    {
        IEnumerable<CartItem> GetAllWithProduct();
        IEnumerable<CartItem> GetCartByUserId(int userId);    
        CartItem? GetCartItem(int userId, int productId);
    }
}