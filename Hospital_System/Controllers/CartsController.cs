using Hospital_Management_System.Application.Dtos.CartDtos;
using Hospital_Management_System.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            
            int userId = 1;
            var userCart = _cartService.GetUserCart(userId);
            return View(userCart);
        }

        
        [HttpPost]
        public IActionResult AddToCart(CreateCartItemDto dto)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            _cartService.AddToCart(dto);
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public IActionResult RemoveFromCart(int userId, int productId)
        {
            _cartService.RemoveFromCart(userId, productId);
            return RedirectToAction("Index");
        }

       
    }
}