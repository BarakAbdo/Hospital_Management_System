using Hospital_Management_System.Application.Dtos.CategoriesDtos;
using Hospital_Management_System.Application.Dtos.ProductsDtos;
using Hospital_Management_System.Application.Services.Base;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var products = _unitOfWork.ProductRepo.GetAllCategory().Select(p => new ProductDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : null
            }).ToList();

            return products;
        }


        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categories = _unitOfWork.CategoryRepo.GetAll().Select(c => new CategoryDto
            {
                Id = c.Id,
                UID = c.UID,
                Name = c.Name
            }).ToList();

            return categories;
        }


        public ProductDto GetByUId(string uid)
        {
            var p = _unitOfWork.ProductRepo.GetByUId(uid);

            var productDto = new ProductDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : null
            };
            return productDto;
        }

        public ProductDto GetById(int id)
        {
            var p = _unitOfWork.ProductRepo.GetAll().FirstOrDefault(a => a.Id == id);

            var productDto = new ProductDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId
            };
            return productDto;
        }

        public void AddProduct(CreateProductDto productDto, IFormFile image)
        {
            var product = new Product
            {
                UID = Guid.NewGuid().ToString(),
                Name = productDto.Name,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId
            };

            if (image != null)
            {
                product.ImageUrl = UploadImage(image, "Products");
            }

            _unitOfWork.ProductRepo.Add(product);
            _unitOfWork.Save();
        }

        public void UpdateProduct(UpdateProductDto dto, IFormFile image)
        {
            if (string.IsNullOrEmpty(dto.UID))
                dto.UID = Guid.NewGuid().ToString();

            var product = new Product
            {
                Id = dto.Id,
                UID = dto.UID,
                Name = dto.Name,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl, 
                CategoryId = dto.CategoryId
            };

            if (image != null)
            {
                product.ImageUrl = UploadImage(image, "Products");
            }

            _unitOfWork.ProductRepo.Update(product);
            _unitOfWork.Save();
        }

        public void DeleteProduct(ProductDto productDto)
        {
            var oldProduct = _unitOfWork.ProductRepo.GetByUId(productDto.UID);
            if (oldProduct != null)
            {
                _unitOfWork.ProductRepo.Delete(oldProduct);
                _unitOfWork.Save();
            }
        }

        private string UploadImage(IFormFile image, string folderName)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);

            Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return $"/images/{folderName}/" + fileName;
        }
    }
}