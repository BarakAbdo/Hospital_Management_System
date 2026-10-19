using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hospital_Management_System.Application.Services.Base;

namespace Hospital_Management_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetAllCategories()
                .Select(c => new
                {
                    c.Id,
                    c.UID,
                    c.Name
                });

            return Ok(categories);
        }
    }
}