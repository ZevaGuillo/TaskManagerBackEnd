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
        var user = _authService.Register(request);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Login(LoginRequest request)
    {
        try
        {
            LoginResponse response = _authService.Login(request);
            return Ok(response);
        }
        catch (System.Exception)
        {
            return BadRequest("si la contraseño con coincide");
        }
    }

    [HttpGet("renovar")]
    public IActionResult RenovarToken(RenovarTokenRequest request)
    {
        try
        {
            LoginResponse response = _authService.RenovarToken(request.token);
            return Ok(response);
        }
        catch (System.Exception)
        {
            return BadRequest("si la contraseño con coincide");
        }
    }

}