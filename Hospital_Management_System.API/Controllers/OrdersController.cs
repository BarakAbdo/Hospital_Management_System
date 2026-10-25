using Hospital_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Make Order
        [HttpPost("{userId}")]
        public async Task<IActionResult> MakeOrder(int userId)
        {
            int orderId = await _orderService.MakeOrder(userId);

            if (orderId == 0)
                return BadRequest("Cart is empty");

            return Ok(new
            {
                message = "Order created successfully",
                orderId = orderId
            });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMyOrders(int userId)
        {
            var orders = await _orderService.GetMyOrders(userId);

            var result = orders.Select(order => new
            {
                order.Id,
                order.UserId,
                order.CreatedAt,
                order.Status,
                order.TotalAmount,

                Items = order.Items.Select(item => new
                {
                    item.Id,
                    item.ProductId,
                    item.Product?.Name,
                    item.Price,
                    item.Quantity
                }).ToList()

            }).ToList();

            return Ok(result);
        }

    }
}
