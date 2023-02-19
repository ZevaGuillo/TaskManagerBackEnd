using Microsoft.AspNetCore.Mvc;
using TaskManager.Contracts.Auth;

namespace TaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase{

    [HttpPost("crear")]
    public IActionResult CreateUser(CreateUserRequest request){
        return Ok("Nuevo usuario");
    }

    [HttpPost]
    public IActionResult Login(LoginRequest request){
        return Ok("Login");
    }

    [HttpGet("renovar")]
    public IActionResult RenovarToken(LoginRequest request){
        return Ok("renovar token");
    }

}