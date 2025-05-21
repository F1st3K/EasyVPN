namespace EasyVPN.Infrastructure.Settings;

public static partial class Options
{
    public class Featuers
    {
        public const string SectionName = "Featuers";

        public bool UseDocumentationEndpoint { get; init; } = true;
        public bool UseExceptionHandler { get; init; } = true;
        public bool UseCors { get; init; } = true;
        public bool MigrateDatabase { get; init; } = true;
        public bool AddScheduledTasks { get; init; } = true;
    }
}