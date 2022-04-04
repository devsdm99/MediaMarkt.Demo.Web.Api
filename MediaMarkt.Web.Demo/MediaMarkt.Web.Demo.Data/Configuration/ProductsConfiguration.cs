using MediaMarkt.Web.Demo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaMarkt.Web.Demo.Data.Configuration
{
    public class ProductsConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        private const string TableName = "Products";
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Description);
            builder.Property(x => x.Price);
            builder.Property(x => x.Category);
            builder.Property(x => x.Name);
        }
    }
}
