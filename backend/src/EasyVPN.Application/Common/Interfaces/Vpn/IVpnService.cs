using ErrorOr;

namespace EasyVPN.Application.Common.Interfaces.Vpn;

public interface IVpnService
{
    public ErrorOr<string> GetConfig(Guid connectionId);
    public ErrorOr<Created> CreateClient(Guid connectionId);
    public ErrorOr<Success> EnableClient(Guid connectionId);
    public ErrorOr<Success> DisableClient(Guid connectionId);
    public ErrorOr<Deleted> DeleteClient(Guid connectionId);
    public ErrorOr<Success> TestConnect();
}
