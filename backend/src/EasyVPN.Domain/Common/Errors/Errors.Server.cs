using ErrorOr;

namespace EasyVPN.Domain.Common.Errors;

public static partial class Errors
{
    public static class Server
    {
        public static Error NotFound => Error.NotFound(
            code: "Server.NotFound",
            description: "Server not found");

        public static Error FailedGetService => Error.Failure(
            code: "Server.FailedGetService",
            description: "Failed to get remote service for this server");
    }
}