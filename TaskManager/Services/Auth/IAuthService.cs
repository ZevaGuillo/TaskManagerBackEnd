

namespace TaskManager.Services.Auth;
public interface IAuthService
{
    string Authenticate(string correo, string contraseña);
}