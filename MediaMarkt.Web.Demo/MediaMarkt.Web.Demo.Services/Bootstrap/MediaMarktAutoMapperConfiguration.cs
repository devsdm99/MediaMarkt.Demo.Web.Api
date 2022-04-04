using AutoMapper;
using MediaMarkt.Web.Demo.Data.Entities;
using MediaMarkt.Web.Demo.Services.Product.Models;

namespace MediaMarkt.Web.Demo.Services.Bootstrap
{
    public class MediaMarktAutoMapperConfiguration : Profile
    {
        public MediaMarktAutoMapperConfiguration()
        {
            CreateMap<ProductDto, ProductEntity>().ReverseMap();

        }
    }
}
