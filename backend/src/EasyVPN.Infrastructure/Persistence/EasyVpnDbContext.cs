using EasyVPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyVPN.Infrastructure.Persistence;

public class EasyVpnDbContext : DbContext
{
    public DbSet<User> Users { get; private set; } = null!;

    public EasyVpnDbContext(DbContextOptions<EasyVpnDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(EasyVpnDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}