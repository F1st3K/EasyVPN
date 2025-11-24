using EasyZsV.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyZsV.Infrastructure.Persistence;

public class EasyZsvDbContext : DbContext
{
    public DbSet<User> Users { get; private set; } = null!;
    public DbSet<Server> Servers { get; private set; } = null!;
    public DbSet<Protocol> Protocols { get; private set; } = null!;
    public DbSet<Connection> Connections { get; private set; } = null!;
    public DbSet<ConnectionTicket> ConnectionTickets { get; private set; } = null!;
    public DbSet<DynamicPage> DynamicPages { get; private set; } = null!;

    public EasyZsvDbContext(DbContextOptions<EasyZsvDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(EasyZsvDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}