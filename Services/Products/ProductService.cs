using App.Repositories;
using App.Repositories.Products;
using App.Services.ExceptionHandlers;
using App.Services.Products.Create;
using App.Services.Products.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int categoryId)
        {
            var products = await productRepository.GetTopPriceProductsAsync(categoryId);
            var productList = mapper.Map<List<ProductDto>>(products);

            return new ServiceResult<List<ProductDto>> { Data = productList };
        }

        public async Task<ServiceResult<ProductDto?>> GetProductById(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
            }

            var productAsDto = new ProductDto(product.Id, product.Name, product.Price, product.Stock, product.CategoryId);

            return ServiceResult<ProductDto>.Success(productAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
        {
            var anyProduct = await productRepository.Where(p => p.Name == request.Name).AnyAsync();
            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("Product already exists");
            }

            //var product = new Product
            //{
            //    Name = request.Name,
            //    Price = request.Price,
            //    Stock = request.Stock
            //};

            var product = mapper.Map<Product>(request);

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

            var isProductExist = await productRepository.Where(p => p.Name == request.Name && p.Id != product.Id ).AnyAsync();
            if (isProductExist)
            {
                return ServiceResult.Fail("Product Name already exists", HttpStatusCode.BadRequest);
            }

            //product.Name = request.Name;
            //product.Price = request.Price;
            //product.Stock = request.Stock;

            product = mapper.Map(request, product);

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(int productId, int stock)
        {
            var product = await productRepository.GetByIdAsync(productId);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }

            product.Stock = stock;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
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

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await productRepository.GetAll().ToListAsync();
            var productAsDto = mapper.Map<List<ProductDto>>(products);
            //   var productList = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var productAsDto = mapper.Map<List<ProductDto>>(products);
            //   var productList = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }
    }
}
