using ErrorOr;

namespace EasyZsV.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Authentication.InvalidCredentials",
            description: "Invalid login or password");
    }
}