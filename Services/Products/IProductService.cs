using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Products.Create;
using App.Services.Products.Update;

namespace App.Services.Products
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int categoryId);
        Task<ServiceResult<ProductDto?>> GetProductById(int id);
        Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request);
        Task<ServiceResult> UpdateProductAsync(int id, UpdateProductRequest request);
        Task<ServiceResult> UpdateStockAsync(int productId, int stock);
        Task<ServiceResult> DeleteProductAsync(int id);
        Task<ServiceResult<List<ProductDto>>> GetAllListAsync();
        Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
    }
}
