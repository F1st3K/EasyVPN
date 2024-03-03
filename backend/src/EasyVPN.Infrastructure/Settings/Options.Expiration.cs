namespace EasyVPN.Infrastructure.Settings;

public static partial class Options
{
    public class Expiration
    {
        public const string SectionName = "ExpireSettings";

        public int CheckMinutes { get; init; } = 60;
    }
}