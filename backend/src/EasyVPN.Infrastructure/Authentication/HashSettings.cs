namespace EasyVPN.Infrastructure.Authentication;

public class HashSettings
{
    public const string SectionName = "HashSettings";
    
    public string Secret { get; init; } = null!;
}