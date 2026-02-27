using ECommerceApp.DTOs;
using ECommerceApp.DTOs.ProductDTOs;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace ECommerceApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("api/Product/CreateProduct")]
        public async Task<ActionResult<ApiResponse<ProductCreateDTO>>> CreateProductAsync([FromBody] ProductCreateDTO productDto)
        {
            var response = await _productService.CreateProductAsync(productDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }


        [HttpGet("api/Product/GetProductById/{id}")]
        public async Task<ActionResult<ApiResponse<ProductResponseDTO>>> GetProductByIdAsync(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }


        [HttpPut("api/Product/UpdateProduct")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateProductAsync([FromBody] ProductUpdateDTO productDto)
        {
            var response = await _productService.UpdateProductAsync(productDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return (response);
        }


        [HttpDelete("api/DeleteProduct/{id}")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteProductAsync(int id)
        {
            var response = await _productService.DeleteProductAsync(id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }

            return (response);

        }

        [HttpGet("api/GetAllProduct")]
        public async Task<ActionResult<ApiResponse<List<ProductResponseDTO>>>> GetAllProductsAsync()
        {
            var response= await _productService.GetAllProductsAsync();
            if (response.StatusCode != 200)
            {
                    return StatusCode(response.StatusCode,response);
            }
            return response;

                 
        }

        [HttpGet("api/GetAllProductsByCategory")]
        public async Task<ActionResult<ApiResponse<List<ProductResponseDTO>>>> GetAllProductsByCategoryAsync(int categoryId)
        {
            var response = await _productService.GetAllProductsByCategoryAsync(categoryId);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return response;
        }

    }
}
