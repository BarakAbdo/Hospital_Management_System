using Hospital_Management_System.Application.Dtos.CategoriesDtos;
using Hospital_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDto category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var category = _categoryService.GetByUId(uid);
            if (category == null)
            {
                return NotFound();
            }

            var update = new UpdateCategoryDto
            {
                Id = category.Id,
                UID = category.UID,
                Name = category.Name
            };
            return View(update);
        }

        [HttpPost]
        public IActionResult Edit(UpdateCategoryDto category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var category = _categoryService.GetByUId(uid);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(CategoryDto category)
        {
            _categoryService.DeleteCategory(category);
            return RedirectToAction("Index");
        }
    }
}