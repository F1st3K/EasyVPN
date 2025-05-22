namespace EasyVPN.Infrastructure.Settings;

public static partial class Options
{
    public class Featuers
    {
        public const string SectionName = "Featuers";

        public bool UseDocumentationEndpoint { get; init; }
        public bool UseExceptionHandler { get; init; }
        public bool UseCors { get; init; }
        public bool MigrateDatabase { get; init; }
        public bool AddScheduledTasks { get; init; }
    }
}