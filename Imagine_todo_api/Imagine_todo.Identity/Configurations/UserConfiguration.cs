using Imagine_todo.domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imagine_todo.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                 new ApplicationUser
                 {
                     Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                     Email = "admin@email.com",
                     NormalizedEmail = "ADMIN@EMAIL.COM",
                     FirstName = "System",
                     LastName = "Admin",
                     UserName = "admin@email.com",
                     NormalizedUserName = "ADMIN@EMAIL.COM",
                     PasswordHash = hasher.HashPassword(null, "P@$$w0rd"),
                     EmailConfirmed = true
                 },
                 new ApplicationUser
                 {
                     Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                     Email = "user@email.com",
                     NormalizedEmail = "USER@EMAIL.COM",
                     FirstName = "Test",
                     LastName = "User",
                     UserName = "user@email.com",
                     NormalizedUserName = "USER@EMAIL.COM",
                     PasswordHash = hasher.HashPassword(null, "P@$$w0rd"),
                     EmailConfirmed = true
                 }
            );
        }
    }
}
