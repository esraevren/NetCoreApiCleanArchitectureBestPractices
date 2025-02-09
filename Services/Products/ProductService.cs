using App.Repositories.Products;
using System.Net;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public async Task<ServiceResult<List<Product>>> GetTopPriceProductsAsync(int categoryId)
        {
            var products = await productRepository.GetTopPriceProductsAsync(categoryId);
            return new ServiceResult<List<Product>> { Data = products };
        }

        public async Task<ServiceResult<Product>> GetProductById(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult<Product>.Fail("Product not found", HttpStatusCode.NotFound);
            }

            return ServiceResult<Product>.Success(product);
        }
    }
}
