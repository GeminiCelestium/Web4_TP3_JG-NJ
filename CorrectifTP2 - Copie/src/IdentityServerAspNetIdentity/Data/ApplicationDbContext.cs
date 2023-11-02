using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityServerAspNetIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            var managerRole = new IdentityRole { Id = "653bb917-ac53-464e-9e41-1be6fa6cf9e1", Name = "manager", NormalizedName = "MANAGER" };
            var adminRole = new IdentityRole { Id = "b4a17d23-2b27-4a35-950a-d97382cb90f4", Name = "admin", NormalizedName = "ADMIN" };

            var admin = new ApplicationUser
            {
                Id = "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3",
                UserName = "admin@cegeplimoilou.ca",
                Email = "admin@cegeplimoilou.ca",
                FirstName = "admin",
                LastName = "admin",
                NormalizedUserName = "ADMIN@CEGEPLIMOILOU.CA",
                NormalizedEmail = "ADMIN@CEGEPLIMOILOU.CA",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var manager = new ApplicationUser
            {
                Id = "420b58eb-032f-4f21-8460-a1e3adeba461",
                UserName = "manager@cegeplimoilou.ca",
                FirstName = "manager",
                LastName = "manager",
                Email = "manager@cegeplimoilou.ca",
                NormalizedUserName = "MANAGER@CEGEPLIMOILOU.CA",
                NormalizedEmail = "MANAGER@CEGEPLIMOILOU.CA",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");
            manager.PasswordHash = hasher.HashPassword(manager, "Manager123!");

            modelBuilder.Entity<IdentityRole>().HasData(managerRole);
            modelBuilder.Entity<IdentityRole>().HasData(adminRole);
            modelBuilder.Entity<ApplicationUser>().HasData(admin);
            modelBuilder.Entity<ApplicationUser>().HasData(manager);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = admin.Id, RoleId = adminRole.Id });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = manager.Id, RoleId = managerRole.Id });

        }
    }
}
