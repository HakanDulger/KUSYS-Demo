using System;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser,
                                                ApplicationRole, string, IdentityUserClaim<string>,
                                                ApplicationUserRole, IdentityUserLogin<string>,
                                                IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roladmin = new ApplicationRole
            {
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = "881de979-4598-4a79-be5b-b211f1634631",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            var roluser = new ApplicationRole
            {
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = "94a4b4ee-ec70-44a7-b5d8-cd7e290c76d9",
                Name = "User",
                NormalizedName = "USER"
            };

            builder.Entity<ApplicationRole>().HasData(roladmin, roluser);

            var admin = new ApplicationUser
            {
                Id = "B6423ABB-7883-4CD1-B254-EEB8D7BDB026",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "hakandulger91@gmail.com",
                NormalizedEmail = "HAKANDULGER@GMAIL.COM",
                PhoneNumber = "",
                Name = "Hakan Dulger",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                IdentityId = Guid.Parse("04c5efcb-fb2a-4676-ad47-17171bf52945"),
                IsActive = true
            };
            admin.PasswordHash = CreatePasswordHash(admin, "Ha_140791");

            var user = new ApplicationUser
            {
                Id = "F01DCF7E-FE61-44B0-8D1E-487F4FD2AC70",
                UserName = "hakan",
                NormalizedUserName = "HAKAN",
                Email = "hakandulger@outlook.com",
                NormalizedEmail = "HAKANDULGER@OUTLOOK.COM",
                PhoneNumber = "",
                Name = "Hakan Dulger",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                IdentityId = Guid.Parse("ae6e7788-8e4a-470d-92e1-8858b20ecd35"),
                IsActive = true
            };
            user.PasswordHash = CreatePasswordHash(admin, "Ha_123987");

            builder.Entity<ApplicationUser>().HasData(admin, user);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                .IsRequired();

                builder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole
                {
                    RoleId = "881de979-4598-4a79-be5b-b211f1634631",
                    UserId = "B6423ABB-7883-4CD1-B254-EEB8D7BDB026"
                });

                builder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole
                {
                    RoleId = "94a4b4ee-ec70-44a7-b5d8-cd7e290c76d9",
                    UserId = "F01DCF7E-FE61-44B0-8D1E-487F4FD2AC70"
                });

            });
        }

        private string CreatePasswordHash(ApplicationUser user, string password)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}

