using App.Services.Products;
using App.Services.Products.Create;
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

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize)
        {
            return CreateActionResult(await productService.GetPagedAllListAsync(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            return CreateActionResult(await productService.CreateProductAsync(request));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest request)
        {
            return CreateActionResult(await productService.UpdateProductAsync(id, request));
        }

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(int id, int stock)
        {
            return CreateActionResult(await productService.UpdateStockAsync(id, stock));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await productService.DeleteProductAsync(id));
        }
    }
}
