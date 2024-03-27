namespace EasyVPN.Infrastructure.Vpn;

public static class ConnectionStringExtensions
{
    public static (string uri, string auth) ParseConnectionString(this string connectionString)
    {
        var parts = connectionString.Split('@');
        var uri = "http://" + parts[1];
        var auth = parts[0];
        return (uri, auth);
    }
}