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

        builder.Property(s => s.Version)
            .HasConversion(
                v => v.ToString(),
                s => Enum.Parse<VpnVersion>(s))
            .HasMaxLength(32);


        builder
            .HasOne(s => s.Protocol)
            .WithMany()
            .HasForeignKey("ProtocolId")
            .IsRequired();

        builder.Property(s => s.ConnectionString)
            .HasConversion(
                v => v.Endpoint.Split("://", StringSplitOptions.None).First() +
                    "://" + v.Auth + "@" +
                    v.Endpoint.Split("://", StringSplitOptions.None).Last(),
                s => new ConnectionString
                {
                    Auth = s.Split("://", StringSplitOptions.None).Last()
                        .Split("@", StringSplitOptions.None).First(),
                    Endpoint = s.Split("://", StringSplitOptions.None).First() +
                        "://" + s.Split("@", StringSplitOptions.None).Last()
                })
            .HasMaxLength(200);

    }
}
