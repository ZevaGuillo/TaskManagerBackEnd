

using TaskManager.Contracts.Auth;
using TaskManager.Models;

namespace TaskManager.Services.Auth;
public interface IAuthService
{
    User Register(CreateUserRequest newUser);
    LoginResponse Login(LoginRequest data);
}