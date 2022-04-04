using MediaMarkt.Web.Demo.Services.Product;
using MediaMarkt.Web.Demo.Services.Product.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaMarkt.Web.Demo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            IEnumerable<ProductDto> products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpPost("create")]
        public async Task<ActionResult<ProductDto>> PostNewProduct([FromBody] ProductDto product)
        {
            ProductDto productAdded = await _productService.CreateNewProductAsync(product);
            return Ok(productAdded);
        }
    }
}
