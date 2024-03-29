using EasyVPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyVPN.Infrastructure.Persistence.Configurations;

public class ConnectionsConfiguration : IEntityTypeConfiguration<Connection>
{

    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        
        builder.ToTable("Connections");

        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.ClientId)
            .HasMaxLength(32);
        builder.Property(c => c.ServerId)
            .HasMaxLength(32);

        builder.Property(с => с.ExpirationTime)
            .HasMaxLength(28);
    }
}