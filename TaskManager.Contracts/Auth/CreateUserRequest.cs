namespace TaskManager.Contracts.Auth;

public record CreateUserRequest(
    string nombre,
    string correo,
    string contraseña
);