namespace TaskManager.Contracts.Auth;

public record LoginResponse(
    Guid uid,
    string nombre,
    string token
);