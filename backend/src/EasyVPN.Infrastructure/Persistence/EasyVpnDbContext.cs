using EasyVPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyVPN.Infrastructure.Persistence;

public class EasyVpnDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public EasyVpnDbContext(DbContextOptions<EasyVpnDbContext> options)
        : base(options)
    {
        
    }
}