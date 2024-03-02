namespace EasyVPN.Infrastructure.Settings;

public static partial class Options
{
    public class Hash
    {
        public const string SectionName = "HashSettings";
    
        public string Secret { get; init; } = null!;
    }
}