using Hospital_Management_System.Domain.Models;

namespace Hospital_Management_System.Application.Dtos.CartDtos
{
    public class CreateCartItemDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class UpdateCartItemDto : CreateCartItemDto
    {
        public int Id { get; set; }
    }

    public class CartItemDto : UpdateCartItemDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}