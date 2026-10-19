using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hospital_Management_System.Application.Services.Base;

namespace Hospital_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetAllProducts()
                .Select(p => new
                {
                    p.Id,
                    p.UID,
                    p.Name,
                    p.Price,
                    p.ImageUrl,
                    p.CategoryName
                });

            return Ok(products);
        }
    }
}