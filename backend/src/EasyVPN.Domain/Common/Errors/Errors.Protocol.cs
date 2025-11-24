using ErrorOr;

namespace EasyZsV.Domain.Common.Errors;

public static partial class Errors
{
    public static class Protocol
    {
        public static Error NotFound => Error.NotFound(
            code: "Protocol.NotFound",
            description: "Protocol not found");

        public static Error StillInUse => Error.Conflict(
            code: "Server.StillInUse",
            description: "Protocol is still in use by any servers");
    }
}