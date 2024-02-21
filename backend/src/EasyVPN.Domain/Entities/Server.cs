using System.Net;
using EasyVPN.Domain.Common.Enums;

namespace EasyVPN.Domain.Entities;

public class Server
{
    public Guid Id { get; set; }
    public VpnType Type { get; set; }
    public string Host { get; set; } = null!;
}