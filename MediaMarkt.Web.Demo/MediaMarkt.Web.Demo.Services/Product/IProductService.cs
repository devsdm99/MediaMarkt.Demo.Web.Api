using MediaMarkt.Web.Demo.Services.Product.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaMarkt.Web.Demo.Services.Product
{
    public interface IProductService
    {
        Task<ProductDto> CreateNewProductAsync(ProductDto product);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}