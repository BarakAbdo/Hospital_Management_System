using Hospital_Management_System.Application.Dtos.OrderDtos;
using Hospital_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        // نحتاج فقط الـ _orderService لأن الخدمة أصبحت مسؤولة عن الطلبات والإشعارات معاً
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Dashboard - All Orders
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrders();

            // تحويل البيانات من Model الأساسي إلى OrderDto لكي يتوافق مع الـ View
            var orderDtos = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                Status = order.Status
            }).ToList();

            return View(orderDtos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            int? userId = await _orderService.UpdateStatus(orderId, status);

            if (userId == null)
            {
                return NotFound();
            }

            // التوجيه التلقائي لصفحة إشعارات المستخدم صاحب الطلب فوراً
            return RedirectToAction(nameof(Index));
        }
    }
}