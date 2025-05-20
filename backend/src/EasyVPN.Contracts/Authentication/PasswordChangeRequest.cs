namespace EasyVPN.Contracts.Authentication;

public record PasswordChangeRequest(
    string Password,
    string NewPassword
    );