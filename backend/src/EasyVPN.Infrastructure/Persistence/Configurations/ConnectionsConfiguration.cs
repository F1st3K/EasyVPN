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


        builder
            .HasOne(c => c.Client)
            .WithMany()
            .HasForeignKey("ClientId")
            .IsRequired();

        builder
            .HasOne(c => c.Server)
            .WithMany()
            .HasForeignKey("ServerId")
            .IsRequired();

        builder.Property(с => с.ExpirationTime)
            .HasMaxLength(28);
    }
}