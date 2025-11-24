using EasyZsV.Domain.Entities;

namespace EasyZsV.Application.Common.Interfaces.Zsv;

public interface IZsvServiceFactory
{
    public IZsvService? GetZsvService(Server server);
}