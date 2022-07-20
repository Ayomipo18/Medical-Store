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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.SellingPrice).IsRequired();
            builder.Property(x => x.CostPrice).IsRequired();
            builder.Property(x => x.ProfitMargin).IsRequired();
            builder.Property(x => x.Manufacturer).IsRequired();
            builder.Property(x => x.IsAvailable).IsRequired();
        }
    }
}
