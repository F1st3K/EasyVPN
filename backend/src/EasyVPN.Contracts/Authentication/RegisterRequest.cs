namespace EasyVPN.Contracts.Authentication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Login,
    string Password
    );