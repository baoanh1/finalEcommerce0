using Ecommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Data.Configurations
{
    class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategories");
            builder.HasKey(x => new { x.ProductID, x.ProductCategoryID});
            //builder.HasOne(p => p.Product).WithMany(b => b.ProductInCategories).HasForeignKey(k => k.ProductID);
            builder.HasOne(p => p.ProductCategory).WithMany(b => b.ProductInCategories).HasForeignKey(k => k.ProductCategoryID);

        }
    }
}
