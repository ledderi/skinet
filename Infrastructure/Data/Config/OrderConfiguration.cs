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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(p => p.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(p => p.BuyerEmail).IsRequired();
            builder.OwnsOne(p => p.DeliveryAddress, p => p.WithOwner());
            builder.Property(p => p.Status).HasConversion(p => p.ToString(), p => (OrderStatus)Enum.Parse(typeof(OrderStatus), p));
        }
    }
}
