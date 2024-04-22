using ErrorOr;

namespace EasyVPN.Domain.Common.Errors;

public static partial class Errors
{
    public static class Protocol
    {
        public static Error NotFound => Error.NotFound(
            code: "Protocol.NotFound",
            description: "Protocol not found");
    }
}