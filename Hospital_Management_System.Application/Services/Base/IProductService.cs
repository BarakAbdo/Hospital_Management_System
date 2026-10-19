using Hospital_Management_System.Application.Dtos.ProductsDtos;
using Hospital_Management_System.Application.Dtos.CategoriesDtos;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts();
        IEnumerable<CategoryDto> GetAllCategories(); 
        ProductDto GetById(int id);
        ProductDto GetByUId(string uid);
        void AddProduct(CreateProductDto productDto, IFormFile imageFile);
        void UpdateProduct(UpdateProductDto productDto, IFormFile imageFile);
        void DeleteProduct(ProductDto productDto);
    }
}