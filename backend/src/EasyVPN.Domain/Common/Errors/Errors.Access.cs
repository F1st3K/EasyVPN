using ErrorOr;

namespace EasyVPN.Domain.Common.Errors;

public static partial class Errors
{
    public static class Access
    {
        public static Error ClientsOnly => Error.Forbidden(
            code: "Access.ClientsOnly",
            description: "Access denied, operation is for clients only");

        public static Error AnotherUserOnly => Error.Forbidden(
            code: "Access.AnotherUserOnly",
            description: "Access denied, operation is intended for another user only");

        public static Error NotIdentified => Error.Forbidden(
            code: "Access.NotIdentified",
            description: "Access denied, could not be identified");
    }
}
