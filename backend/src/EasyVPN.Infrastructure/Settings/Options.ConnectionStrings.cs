namespace EasyVPN.Infrastructure.Settings;

public static partial class Options
{
    public class ConnectionStrings
    {
        public const string SectionName = "ConnectionStrings";

        public string Postgres { get; init; } = null!;
    }
}