using ErrorOr;

namespace EasyVPN.Domain.Common.Errors;

public static partial class Errors
{
    public static class Connection
    {
        public static Error NotFound => Error.NotFound(
            code: "Connection.NotFound",
            description: "Connection not found");
    }
}