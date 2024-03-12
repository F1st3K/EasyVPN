using ErrorOr;

namespace EasyVPN.Domain.Common.Errors;

public static partial class Errors
{
    public static class ConnectionTicket
    {
        public static Error NotFound => Error.NotFound(
            code: "ConnectionTicket.NotFound",
            description: "ConnectionTicket not found");

        public static Error IsPending => Error.Conflict(
            code: "ConnectionTicket.StatusIsPending",
            description: "ConnectionTicket cannot be deleted while pending");
    }
}