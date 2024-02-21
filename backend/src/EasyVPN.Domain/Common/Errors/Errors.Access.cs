using ErrorOr;

namespace EasyVPN.Domain.Common.Errors;

public static partial class Errors
{
    public static class Access
    {
        public static Error ClientsOnly => Error.Conflict(
            code: "Access.ClientsOnly",
            description: "Conflict access, operation is for clients only");
    }
}