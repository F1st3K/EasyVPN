using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyVPN.Infrastructure.Persistence.Configurations;

public class ProtocolsConfiguration : IEntityTypeConfiguration<Protocol>
{
    public void Configure(EntityTypeBuilder<Protocol> builder)
    {
        builder.ToTable("Protocols");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasMaxLength(20);

        builder.Property(p => p.Icon);


    }
}