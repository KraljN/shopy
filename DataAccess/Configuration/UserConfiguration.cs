using Domen.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(20);

            builder.Property(u => u.LastName).IsRequired().HasMaxLength(30);

            builder.Property(u => u.Email).IsRequired();

            builder.HasMany(u => u.UserUseCases).WithOne(uc => uc.User).HasForeignKey(uc => uc.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Orders).WithOne(o => o.User).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

