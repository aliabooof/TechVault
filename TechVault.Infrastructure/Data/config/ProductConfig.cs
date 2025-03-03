﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechVault.Core.Entities.Product;

namespace TechVault.Infrastructure.Data.config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x=> x.Id).IsRequired();
            
            builder.Property(x=>x.Name).IsRequired();

            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

        }
    }
}
