using Hospital_Management_System.Application.Dtos.CategoriesDtos;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Hospital_Management_System.Application.Services.Base;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categoryDto = _unitOfWork.CategoryRepo.GetAll().Select(c => new CategoryDto
            {
                Id = c.Id,
                UID = c.UID,
                Name = c.Name
            }).ToList();
            return categoryDto;
        }

        public CategoryDto GetByUId(string uid)
        {
            var c = _unitOfWork.CategoryRepo.GetByUId(uid);
            if (c == null) return null;

            var categoryDto = new CategoryDto
            {
                Id = c.Id,
                UID = c.UID,
                Name = c.Name
            };
            return categoryDto;
        }

        public CategoryDto GetById(int id)
        {
            var c = _unitOfWork.CategoryRepo.GetAll().FirstOrDefault(a => a.Id == id);
            if (c == null) return null;

            var categoryDto = new CategoryDto
            {
                Id = c.Id,
                UID = c.UID,
                Name = c.Name
            };
            return categoryDto;
        }

        public void AddCategory(CreateCategoryDto categoryDto)
        {
            var category = new Category
            {
                UID = Guid.NewGuid().ToString(),
                Name = categoryDto.Name
            };

            _unitOfWork.CategoryRepo.Add(category);
            _unitOfWork.Save();
        }

        public void UpdateCategory(UpdateCategoryDto dto)
        {
            if (string.IsNullOrEmpty(dto.UID))
                dto.UID = Guid.NewGuid().ToString();

            var category = new Category
            {
                Id = dto.Id,
                UID = dto.UID,
                Name = dto.Name
            };

            _unitOfWork.CategoryRepo.Update(category);
            _unitOfWork.Save();
        }

        public void DeleteCategory(CategoryDto categoryDto)
        {
            var oldCategory = _unitOfWork.CategoryRepo.GetByUId(categoryDto.UID);
            if (oldCategory != null)
            {
                _unitOfWork.CategoryRepo.Delete(oldCategory);
                _unitOfWork.Save();
            }
        }
    }
}