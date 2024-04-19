using System.Net.Http.Headers;
using System.Text;
using EasyVPN.Application.Common.Interfaces.Vpn;

namespace EasyVPN.Infrastructure.Vpn;

public class HttpV1 : IVpnService
{
    public static HttpV1? GetService(string connectionString)
    {
        try
        {
            var (uri, auth) = connectionString.ParseConnectionString();
            
            var client = new HttpClient(){ BaseAddress = new Uri(uri)};
            var basicAuth = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(auth)));
            
            var request = new HttpRequestMessage(HttpMethod.Get, "/");
            request.Headers.Authorization = basicAuth;
            var test = client.Send(request);
            return test.IsSuccessStatusCode ? new HttpV1(client, basicAuth) : null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private readonly HttpClient _client;
    private readonly AuthenticationHeaderValue _auth;

    private HttpV1(HttpClient client, AuthenticationHeaderValue auth)
    {
        _client = client;
        _auth = auth;
    }
    
    public string GetConfig(Guid connectionId)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, $"connections/{connectionId}/config");
        req.Headers.Authorization = _auth;
        var res = _client.Send(req);
        return res.Content.ReadAsStringAsync().Result;
    }

    public void CreateClient(Guid connectionId)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, $"connections?id={connectionId}");
        req.Headers.Authorization = _auth;
        _client.Send(req);
    }

    public void EnableClient(Guid connectionId)
    {
        var req = new HttpRequestMessage(HttpMethod.Put, $"connections/{connectionId}/enable");
        req.Headers.Authorization = _auth;
        _client.Send(req);
    }

    public void DisableClient(Guid connectionId)
    {
        var req = new HttpRequestMessage(HttpMethod.Put, $"connections/{connectionId}/disable");
        req.Headers.Authorization = _auth;
        _client.Send(req);
    }

    public void DeleteClient(Guid connectionId)
    {
        var req = new HttpRequestMessage(HttpMethod.Delete, $"connections/{connectionId}");
        req.Headers.Authorization = _auth;
        _client.Send(req);
    }
}