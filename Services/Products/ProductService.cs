using App.Repositories;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int categoryId)
        {
            var products = await productRepository.GetTopPriceProductsAsync(categoryId);
            var productList = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            return new ServiceResult<List<ProductDto>> { Data = productList };
        }

        public async Task<ServiceResult<ProductDto?>> GetProductById(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
            }

            var productAsDto = new ProductDto(product.Id, product.Name, product.Price, product.Stock);
            return ServiceResult<ProductDto>.Success(productAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id));
        }

        public async Task<ServiceResult> UpdateProductAsync(int id, UpdateProductRequest request)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteProductAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();
        }
        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await productRepository.GetAll().ToListAsync();
            var productList = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            return ServiceResult<List<ProductDto>>.Success(productList);
        }
    }
}
