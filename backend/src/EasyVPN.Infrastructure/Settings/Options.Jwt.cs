namespace EasyVPN.Infrastructure.Settings;

public static partial class Options
{
    public class Jwt
    {
        public const string SectionName = "JwtSettings";
    
        public string Secret { get; init; } = null!;
        public int ExpiryMinutes { get; init; }
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
    }
}