using App.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return CreateActionResult(await productService.GetProductById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await productService.GetAllListAsync());
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize)
        {
            return CreateActionResult(await productService.GetPagedAllListAsync(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            return CreateActionResult(await productService.CreateProductAsync(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest request)
        {
            return CreateActionResult(await productService.UpdateProductAsync(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await productService.DeleteProductAsync(id));
        }
    }
}
