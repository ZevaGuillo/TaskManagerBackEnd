using Microsoft.AspNetCore.Mvc;
using TaskManager.Contracts.Auth;
using TaskManager.Services.Auth;

namespace TaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("crear")]
    public IActionResult CreateUser(CreateUserRequest request)
    {
        return Ok("Nuevo usuario");
    }

    [HttpPost]
    public IActionResult Login(LoginRequest request)
    {
        return Ok("Login");
    }

    [HttpGet("renovar")]
    public IActionResult RenovarToken()
    {
        return Ok("renovar token");
    }

}