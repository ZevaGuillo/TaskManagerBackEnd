using Microsoft.AspNetCore.Mvc;
using System.Net;
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

    [Route("[action]")]
    [HttpPut]
    public async Task<ActionResult<List<CreateUserRequest>>> crearUsuario(CreateUserRequest request)
    {

        try
        {
            var lista = await _authService.Register(request);
            return Ok(lista);

        }
        catch (System.Exception ex)
        {

            return BadRequest(new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Error en la petición",
                Detail = ex.Message
            });
        }

    }

    [Route("[action]")]
    [HttpPost]
    public async Task<ActionResult<List<LoginRequest>>> login(LoginRequest request)
    {
        try
        {
            var lista = await _authService.Login(request);
            return Ok(lista);
        }
        catch (System.Exception ex)
        {

            return BadRequest(new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Error en la petición",
                Detail = ex.Message
            });
        }

    }

}