using Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
            builder.Property(p => p.ShortName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.DeliveryTime).IsRequired().HasMaxLength(100);
        }
    }
}
