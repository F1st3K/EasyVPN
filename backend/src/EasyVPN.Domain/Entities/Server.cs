using EasyVPN.Domain.Common.Enums;

namespace EasyVPN.Domain.Entities;

public class Server
{
    public Guid Id { get; set; }
    public Protocol Protocol { get; set; } = null!;
    public VpnVersion Version { get; set; }
    public string ConnectionString { get; set; } = null!;
}