using EasyZsV.Domain.Common.Enums;

namespace EasyZsV.Domain.Entities;

public class Server
{
    public Guid Id { get; set; }
    public Protocol Protocol { get; set; } = null!;
    public ZsvVersion Version { get; set; }
    public ConnectionString ConnectionString { get; set; } = null!;
}
