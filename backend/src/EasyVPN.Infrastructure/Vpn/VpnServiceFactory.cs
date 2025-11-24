using EasyZsV.Application.Common.Interfaces.Zsv;
using EasyZsV.Domain.Common.Enums;
using EasyZsV.Domain.Entities;
using EasyZsV.Infrastructure.Zsv.Versions;

namespace EasyZsV.Infrastructure.Zsv;

public class ZsvServiceFactory : IZsvServiceFactory
{
    public IZsvService? GetZsvService(Server server)
    {
        return server.Version switch
        {
            ZsvVersion.V1 => ZsvV1.GetService(server.ConnectionString),
            _ => throw new NotSupportedException(
                $"Unsupported {nameof(ZsvVersion)}: {server.Version.ToString()}")
        };
    }
}
