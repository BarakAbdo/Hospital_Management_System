using System;
using System.Collections.Generic;

namespace Hospital_Management_System.Application.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
    }

    public class UpdateOrderStatusDto : CreateOrderDto
    {
        public int OrderId { get; set; } 
        public string Status { get; set; } = string.Empty;
    }

    public class OrderDto : CreateOrderDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
    }
}