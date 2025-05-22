namespace EasyVPN.Infrastructure.Settings;

public static partial class Options
{
    public class Features
    {
        public const string SectionName = "Features";

        public bool UseDocumentationEndpoint { get; init; }
        public bool UseExceptionHandler { get; init; }
        public bool UseCors { get; init; }
        public bool MigrateDatabase { get; init; }
        public bool AddScheduledTasks { get; init; }
    }
}