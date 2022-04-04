using AutoMapper;
using MediaMarkt.Web.Demo.Data;
using MediaMarkt.Web.Demo.Data.Entities;
using MediaMarkt.Web.Demo.Services.Product.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaMarkt.Web.Demo.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly MediaMarktContext _mediaMarktContext;
        private readonly IMapper _mapper;

        public ProductService(MediaMarktContext mediaMarktContext, IMapper mapper)
        {
            _mediaMarktContext = mediaMarktContext;
            _mapper = mapper;
        }
       
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            List<ProductDto> products = await _mediaMarktContext.Products.Select(x => _mapper.Map<ProductDto>(x)).ToListAsync();
            return products;
        }
        public async Task<ProductDto> CreateNewProductAsync(ProductDto product)
        {
            bool exist = await _mediaMarktContext.Products.Where(x => x.Name == product.Name && x.Category == product.Category).FirstOrDefaultAsync() != null;
            if (exist)
            {
                throw new Exception("This product already exist in database");
            }

            ProductEntity entity = new ProductEntity()
            {
                Category = product.Category,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price
            };

            await _mediaMarktContext.Products.AddAsync(entity);
            await _mediaMarktContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>(entity);
        }
    }
}
