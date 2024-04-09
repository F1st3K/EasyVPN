using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyVPN.Infrastructure.Persistence.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Roles)
            .HasConversion(role =>
                    string.Join(';', role
                        .Select(r => r.ToString())),
                str => 
                    str.Split(";", StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => Enum.Parse<RoleType>(s)));
        
        /*builder.OwnsMany(u => u.Roles, dib =>
        {
            dib.ToTable("UserRoles");

            dib.WithOwner().HasForeignKey("UserId");

            dib.HasKey("Id");

            dib.Property(r => r.Value)
                .HasColumnName("Role")
                .HasConversion(
                    role => role.ToString(),
                    s => Enum.Parse<RoleType>(s))
                .HasMaxLength(32);

        });*/

        builder.Property(u => u.FirstName)
            .HasMaxLength(50);
        builder.Property(u => u.LastName)
            .HasMaxLength(50);

        builder.HasIndex(u => u.Login);
        builder.Property(u => u.HashPassword)
            .HasMaxLength(100);

    }
}