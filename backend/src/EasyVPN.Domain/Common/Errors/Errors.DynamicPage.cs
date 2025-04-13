using ErrorOr;

namespace EasyVPN.Domain.Common.Errors;

public static partial class Errors
{
    public static class DynamicPage
    {
        public static Error NotFound => Error.NotFound(
            code: "DynamicPage.NotFound",
            description: "Page not found");

        public static Error AlreadyExist => Error.Conflict(
            code: "DynamicPage.AlreadyExist",
            description: "Page with same Route already exist");
    }
}