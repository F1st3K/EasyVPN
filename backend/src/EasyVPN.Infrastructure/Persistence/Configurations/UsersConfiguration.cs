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
                    str.Split(";", StringSplitOptions.None)
                        .Select(Enum.Parse<RoleType>));

        builder.Property(u => u.FirstName)
            .HasMaxLength(50);
        builder.Property(u => u.LastName)
            .HasMaxLength(50);
        builder.Property(u => u.Icon);

        builder.HasIndex(u => u.Login);
        builder.Property(u => u.HashPassword)
            .HasMaxLength(100);

    }
}