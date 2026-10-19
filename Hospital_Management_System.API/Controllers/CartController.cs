using Hospital_Management_System.Application.Dtos.CartDtos;
using Hospital_Management_System.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/Cart/1
        [HttpGet("{userId}")]
        public IActionResult GetCart(int userId)
        {
            var cart = _cartService.GetUserCart(userId);
            return Ok(cart);
        }

        // POST: api/Cart
        [HttpPost]
        public IActionResult AddToCart([FromBody] CreateCartItemDto dto)
        {
            var success = _cartService.AddToCart(dto);
            if (!success)
            {
                return BadRequest(new { message = "Invalid quantity or failed to add product to cart." });
            }

            return Ok(new { message = "Added to cart successfully." });
        }

        // DELETE: api/Cart/1/5
        [HttpDelete("{userId}/{productId}")]
        public IActionResult RemoveFromCart(int userId, int productId)
        {
            var success = _cartService.RemoveFromCart(userId, productId);
            if (!success)
            {
                return NotFound(new { message = "Item not found in cart." });
            }

            return Ok(new { message = "Removed from cart successfully." });
        }
    }
}