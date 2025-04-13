using EasyVPN.Domain.Common.Enums;
using EasyVPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyVPN.Infrastructure.Persistence.Configurations;

public class ConnectionTicketsConfiguration : IEntityTypeConfiguration<ConnectionTicket>
{

    public void Configure(EntityTypeBuilder<ConnectionTicket> builder)
    {
        builder.ToTable("ConnectionTickets");

        builder.HasKey(ct => ct.Id);

        builder
            .HasOne<Connection>()
            .WithMany()
            .HasForeignKey(ct => ct.ConnectionId)
            .IsRequired();

        builder
            .HasOne(ct => ct.Client)
            .WithMany()
            .HasForeignKey("ClientId")
            .IsRequired();

        builder.Property(ct => ct.Status)
            .HasConversion(
                status => status.ToString(),
                s => Enum.Parse<ConnectionTicketStatus>(s))
            .HasMaxLength(32);
        builder.Property(с => с.CreationTime)
            .HasMaxLength(28);
        builder.Property(с => с.Days);
        builder.Property(ct => ct.PaymentDescription);

        builder.Property(ct => ct.Images).
            HasConversion(image =>
                    string.Join(';', image
                        .Select(i => i.ToString())),
                str =>
                    str.Split(";", StringSplitOptions.None));
    }
}