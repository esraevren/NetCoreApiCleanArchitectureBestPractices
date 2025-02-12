using App.Services.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProductById(int id)
        {
            return CreateActionResult(await productService.GetProductById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await productService.GetAllListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            return CreateActionResult(await productService.CreateProductAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id,UpdateProductRequest request)
        {
            return CreateActionResult(await productService.UpdateProductAsync(id, request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int count)
        {
            var result = await productService.DeleteProductAsync(count);
            return Ok(result);
        }


    }
}
