using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Description).IsRequired(true).HasMaxLength(200);
            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(100);
            builder.Property(p => p.PictureUrl).IsRequired(true).HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnType("decimal").HasPrecision(18, 2);
        }
    }
}
