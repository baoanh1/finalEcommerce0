using Ecommerce.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData
            (
                new Product { ID = 1, Name = "Dien thoai" },
                new Product { ID = 2, Name = "Labtop" }
            );

            modelBuilder.Entity<Product>().HasData
           (
               new Product { ID = 1, Name = "Iphone 6", categoryID = 1},
               new Product { ID = 2, Name = "Iphone 7" , categoryID = 2}
           );
            modelBuilder.Entity<ProductInCategory>().HasData
              (
                  new ProductInCategory { ProductID = 1, ProductCategoryID = 1},
                  new ProductInCategory { ProductID = 1, ProductCategoryID = 2 },
                   new ProductInCategory { ProductID = 2, ProductCategoryID = 1 }
              );



            var ADMIN_ID = Guid.NewGuid();
            // any guid, but nothing is against to use the same one
            var ROLE_ID = Guid.NewGuid();
            var ROLE_ID1 = Guid.NewGuid();
            modelBuilder.Entity<AppRole>().HasData(
            new AppRole
            {
                Id = ROLE_ID,
                Name = "admin",
                NormalizedName = "ADMIN"
            },
            new AppRole
            {
                Id = ROLE_ID1,
                Name = "member",
                NormalizedName = "MEMBER"
            }
            );
            
            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "phungnhatphu4@gmail.com",
                NormalizedEmail = "phungnhatphu4@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "NxP@2020"),
                SecurityStamp = Guid.NewGuid().ToString("D")
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
            modelBuilder.Entity <IdentityRoleClaim<Guid>>().HasData(new IdentityRoleClaim<Guid>
            {
                Id = 1,
                RoleId = ROLE_ID,
                ClaimType = "AdminClaim",
                ClaimValue = "Admin role claim"
            },
            new IdentityRoleClaim<Guid>
            {
                Id = 2,
                RoleId = ROLE_ID1,
                ClaimType = "MemberClaim",
                ClaimValue = "Menber role claim"
            });
            modelBuilder.Entity<IdentityUserClaim<Guid>>().HasData(
            new IdentityUserClaim<Guid>
            {
                Id = 1,
                ClaimType = "AdminClaim",
                ClaimValue = "Admin role claim"
            });
        }
    }
}
