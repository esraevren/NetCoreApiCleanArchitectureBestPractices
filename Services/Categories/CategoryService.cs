using App.Repositories;
using App.Repositories.Categories;
using App.Repositories.Products;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using App.Services.Products.Create;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
        {
            var category = await repository.GetCategoryWithProductsAsync(categoryId);

            if (category is null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Category not found");
            }

            var categoryWithProductsDtos = mapper.Map<CategoryWithProductsDto>(category);

            return ServiceResult<CategoryWithProductsDto>.Success(categoryWithProductsDtos);
        }
        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoriesWithProducts()
        {
            var categoryList = await repository.GetCategoryWithProducts().ToListAsync();

            if (categoryList is null)
            {
                return ServiceResult<List<CategoryWithProductsDto>>.Fail("Category list not found");
            }

            var categoryWithProductsDtos = mapper.Map<List<CategoryWithProductsDto>>(categoryList);

            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryWithProductsDtos);
        }
        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = repository.GetAll();
            var categoryDtos = mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Success(categoryDtos);
        }

        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await repository.GetByIdAsync(id);

            if (category is null)
            {
                return ServiceResult<CategoryDto>.Fail("Category not found");
            }

            var categoryDto = mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoryDto);
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {
            var anyCategory = await repository.Where(p => p.Name == request.Name).AnyAsync();
            if (anyCategory)
            {
                return ServiceResult<int>.Fail("Category already exists");
            }

            var newCategory = new Category { Name = request.Name };

            await repository.AddAsync(newCategory);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.Success(newCategory.Id);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var category = await repository.GetByIdAsync(id);

            if (category is null)
            {
                return ServiceResult.Fail("Category not found");
            }

            var isCategoryNameExist = await repository.Where(p => p.Name == request.Name && p.Id != category.Id).AnyAsync();
            if (isCategoryNameExist)
            {
                return ServiceResult.Fail("Category name already exists");
            }

            category = mapper.Map(request, category);

            repository.Update(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await repository.GetByIdAsync(id);

            if (category is null)
            {
                return ServiceResult.Fail("Category not found");
            }

            repository.Delete(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();
        }
    }
}
