using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DbContext.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.CostPrice).IsRequired();
            builder.Property(x => x.ProfitMargin).IsRequired();
            builder.Property(x => x.SellingPrice).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();
        }
    }
}
