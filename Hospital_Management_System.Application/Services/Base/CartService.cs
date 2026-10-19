using Hospital_Management_System.Application.Dtos.CartDtos;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;

using System.Collections.Generic;
using System.Linq;

namespace Hospital_Management_System.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CartItemDto> GetUserCart(int userId)
        {
            var cartItems = _unitOfWork.CartRepo.GetCartByUserId(userId);

            return cartItems.Select(item => new CartItemDto
            {
                Id = item.Id,
                UserId = item.UserId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Name = item.Product?.Name,
                Price = item.Product?.Price ?? 0,
                ImageUrl = item.Product?.ImageUrl
            }).ToList();
        }

        public IEnumerable<CartItemDto> GetAllCartItems()
        {
            
            var cartItems = _unitOfWork.CartRepo.GetAllWithProduct();

            return cartItems.Select(item => new CartItemDto
            {
                Id = item.Id,
                UserId = item.UserId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Name = item.Product?.Name,
                Price = item.Product?.Price ?? 0,
                ImageUrl = item.Product?.ImageUrl
            }).ToList();
        }

        public bool AddToCart(CreateCartItemDto dto)
        {
            if (dto.Quantity < 1) return false;

            var existingItem = _unitOfWork.CartRepo.GetCartItem(dto.UserId, dto.ProductId);

            if (existingItem == null)
            {
                var newCartItem = new CartItem
                {
                    UserId = dto.UserId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };
                _unitOfWork.CartRepo.Add(newCartItem);
            }
            else
            {
                existingItem.Quantity += dto.Quantity;
            }

            _unitOfWork.Save(); 
            return true;
        }

        public bool RemoveFromCart(int userId, int productId)
        {
            var existingItem = _unitOfWork.CartRepo.GetCartItem(userId, productId);
            if (existingItem == null) return false;

            
            _unitOfWork.CartRepo.Delete(existingItem); 
            _unitOfWork.Save();
            return true;
        }
    }
}