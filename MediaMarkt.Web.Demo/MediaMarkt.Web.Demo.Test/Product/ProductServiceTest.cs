using AutoMapper;
using FluentAssertions;
using MediaMarkt.Web.Demo.Data;
using MediaMarkt.Web.Demo.Services.Bootstrap;
using MediaMarkt.Web.Demo.Services.Product;
using MediaMarkt.Web.Demo.Services.Product.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaMarkt.Web.Demo.Test.Product
{
    [TestFixture]
    public class ProductServiceTest
    {
        private MediaMarktContext _context;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<MediaMarktContext>()
                                    .UseInMemoryDatabase("MediaMarktFakeDb")
                                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                                    .Options;

            _context = new MediaMarktContext(contextOptions);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Products.AddRange(
                new Data.Entities.ProductEntity { Name = "MMP1", Category = "MMC1" },
                new Data.Entities.ProductEntity { Name = "MMP2", Category = "MMC2" },
                new Data.Entities.ProductEntity { Name = "MMP3", Category = "MMC3" },
                new Data.Entities.ProductEntity { Name = "MMP4", Category = "MMC4" });

            _context.SaveChanges();

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<MediaMarktAutoMapperConfiguration>());
            _mapper = config.CreateMapper();
        }

        [Test]
        public async Task AddProduct_WhenProductExistThenThrowException()
        {
            //arrange
            ProductDto productToInsert = new ProductDto()
            {
                Name = "MMP1",
                Category = "MMC1"
            };

            //act
            IProductService sut = new ProductService(_context, _mapper);

            //assert
            Func<Task> funcs = async () => await sut.CreateNewProductAsync(productToInsert);
            await funcs.Should().ThrowAsync<Exception>();
        }
        [Test]
        public async Task AddProduct_WhenProductNotExistThenAdd()
        {
            //arrange
            ProductDto productToInsert = new ProductDto()
            {
                Name = "MMP5",
                Category = "MMC5",
                Price = 5,
                Description = "MMD5",                
            };

            //act
            IProductService sut = new ProductService(_context, _mapper);
            //assert
            ProductDto addedProduct = await sut.CreateNewProductAsync(productToInsert);
            addedProduct.Name.Should().Be(productToInsert.Name);
            addedProduct.Category.Should().Be(productToInsert.Category);
            addedProduct.Price.Should().Be(productToInsert.Price);
            addedProduct.Description.Should().Be(productToInsert.Description);
        }
    }
}
