using EasyZsV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyZsV.Infrastructure.Persistence.Configurations;

public class DynamicPagesConfiguration : IEntityTypeConfiguration<DynamicPage>
{
    public void Configure(EntityTypeBuilder<DynamicPage> builder)
    {
        builder.ToTable("DynamicPages");

        builder.HasKey(p => p.Route);

        builder.Property(p => p.Title).HasMaxLength(40);

        builder.Property(p => p.Content);

        builder.Property(p => p.Created);

        builder.Property(p => p.LastModified);
    }
}