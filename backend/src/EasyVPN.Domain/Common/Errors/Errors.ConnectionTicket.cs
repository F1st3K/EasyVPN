using ErrorOr;

namespace EasyVPN.Domain.Common.Errors;

public static partial class Errors
{
    public static class ConnectionTicket
    {
        public static Error NotFound => Error.NotFound(
            code: "ConnectionTicket.NotFound",
            description: "ConnectionTicket not found");

        public static Error AlreadyProcessed => Error.Conflict(
            code: "ConnectionTicket.AlreadyProcessed",
            description: "ConnectionTicket cannot be processed again");

        public static Error NotProcessed => Error.Conflict(
            code: "ConnectionTicket.NotProcessed",
            description: "ConnectionTicket cannot be deleted while pending");
    }
}