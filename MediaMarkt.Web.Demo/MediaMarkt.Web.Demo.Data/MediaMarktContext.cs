using MediaMarkt.Web.Demo.Data.Configuration;
using MediaMarkt.Web.Demo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaMarkt.Web.Demo.Data
{
    public class MediaMarktContext : DbContext
    {
        public MediaMarktContext(DbContextOptions<MediaMarktContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
        }

        public DbSet<ProductEntity> Products { get; set; }
    }
}
