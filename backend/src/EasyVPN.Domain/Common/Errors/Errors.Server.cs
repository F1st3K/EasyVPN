using ErrorOr;

namespace EasyVPN.Domain.Common.Errors;

public static partial class Errors
{
    public static class Server
    {
        public static Error NotFound => Error.NotFound(
            code: "Server.NotFound",
            description: "Server not found");
    }
}