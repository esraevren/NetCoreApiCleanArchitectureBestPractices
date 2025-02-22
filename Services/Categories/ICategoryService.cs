﻿using App.Services.Categories.Create;
using App.Services.Categories.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
        Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoriesWithProducts();
    }
}
