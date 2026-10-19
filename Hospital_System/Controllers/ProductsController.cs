using Hospital_Management_System.Application.Dtos.ProductsDtos;
using Hospital_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital_Management_System.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_productService.GetAllCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto product, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product, imageFile);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(_productService.GetAllCategories(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var product = _productService.GetByUId(uid);
            if (product == null)
            {
                return NotFound();
            }

            var update = new UpdateProductDto
            {
                Id = product.Id,
                UID = product.UID,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId
            };

            ViewBag.Categories = new SelectList(_productService.GetAllCategories(), "Id", "Name", product.CategoryId);
            return View(update);
        }

        [HttpPost]
        public IActionResult Edit(UpdateProductDto product, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product, imageFile);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(_productService.GetAllCategories(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var product = _productService.GetByUId(uid);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(ProductDto product)
        {
            _productService.DeleteProduct(product);
            return RedirectToAction("Index");
        }
    }
}