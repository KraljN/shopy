using Domen.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configuration
{
    public class UseCaseLogConfiguration : IEntityTypeConfiguration<UseCaseLog>
    {
        public void Configure(EntityTypeBuilder<UseCaseLog> builder)
        {
            builder.Property(u => u.CreatedAt).IsRequired();

            builder.Property(u => u.Data).IsRequired();

            builder.Property(u => u.UseCaseName) .IsRequired();

            builder.Property(u => u.Actor).IsRequired();
        }
    }
}
