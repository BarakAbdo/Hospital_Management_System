using Hospital_Management_System.Application.Dtos.CategoriesDtos;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();
        CategoryDto GetById(int id);
        CategoryDto GetByUId(string uid);
        void AddCategory(CreateCategoryDto categoryDto);
        void UpdateCategory(UpdateCategoryDto categoryDto);
        void DeleteCategory(CategoryDto categoryDto);
    }
}