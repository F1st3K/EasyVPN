using EasyVPN.Domain.Common.Enums;
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
        
        builder.Property(s => s.Type)
            .HasConversion(
                vtype => vtype.ToString(), 
                s => Enum.Parse<VpnType>(s))
            .HasMaxLength(32);
        
        builder.Property(s => s.ConnectionString)
            .HasMaxLength(200);
    }
}