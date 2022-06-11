using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domen.Entities;

namespace DataAccess.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.Address).IsRequired().HasMaxLength(80);
            builder.Property(o => o.OrderStatus).IsRequired().HasMaxLength(20);
            builder.Property(o => o.PaymentMethod).IsRequired().HasMaxLength(20);
            builder.HasMany(o => o.OrderItems).WithOne(oi => oi.Order).HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
