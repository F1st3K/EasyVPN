namespace EasyVPN.Contracts.Authentication;

public record LoginRequest(
    string Login,
    string Password
);