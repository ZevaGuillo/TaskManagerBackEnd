namespace TaskManager.Contracts.Auth;

public record LoginRequest(
    string correo,
    string contraseña
);