using Hospital_Management_System.Application.Dtos.CartDtos;
using System.Collections.Generic;

namespace Hospital_Management_System.Application.Services
{
    public interface ICartService
    {
        IEnumerable<CartItemDto> GetUserCart(int userId);
        bool AddToCart(CreateCartItemDto dto);
        bool RemoveFromCart(int userId, int productId);
        IEnumerable<CartItemDto> GetAllCartItems();
    }
}