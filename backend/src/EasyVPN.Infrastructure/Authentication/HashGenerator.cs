using System.Security.Cryptography;
using System.Text;
using EasyVPN.Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.Options;

namespace EasyVPN.Infrastructure.Authentication;

public class HashGenerator : IHashGenerator
{
    private readonly HashSettings _settings;

    public HashGenerator(IOptions<HashSettings> settings)
    {
        _settings = settings.Value;
    }

    public string Hash(string value)
    {
        return Encoding.UTF8.GetString(
            Hash(
                Encoding.UTF8.GetBytes(value),
                Encoding.UTF8.GetBytes(_settings.Secret)));
    }

    private static byte[] Hash(IEnumerable<byte> value, IEnumerable<byte> salt)
    {
        var saltedValue = value.Concat(salt).ToArray();
        return SHA256.HashData(saltedValue);
    }
}