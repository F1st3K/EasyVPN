using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Common.Errors;
using EasyVPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyVPN.Infrastructure.Persistence.Configurations;

public class ServersConfiguration : IEntityTypeConfiguration<Server>
{
    public void Configure(EntityTypeBuilder<Server> builder)
    {
        builder.ToTable("Servers");

        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Version)
            .HasConversion(
                v => v.ToString(), 
                s => Enum.Parse<VpnVersion>(s))
            .HasMaxLength(32);

        
        builder
            .HasOne(s => s.Protocol)
            .WithOne()
            .HasForeignKey<Server>("ProtocolId");
        
        builder.Property(s => s.ConnectionString)
            .HasMaxLength(200);

    }
}