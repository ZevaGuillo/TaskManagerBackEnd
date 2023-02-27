using CodeGeneral;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using System.Xml.Linq;
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

    [Route("[action]")]
    [HttpPost]
    public async Task<ActionResult<List<CreateUserRequest>>> crearUsuario([BindRequired]string nombre, [BindRequired] string correo, [BindRequired] string contraseña)
    {
        var cadCon = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conn_bd"];
        XDocument xmlParam = XDocument.Parse("<CreateUserRequest><nombre>"+nombre+"</nombre><correo>"+correo+"</correo><contraseña>"+contraseña+"</contraseña></CreateUserRequest>");
        Console.WriteLine(xmlParam.ToString());
        DataSet dsResultados = await DBXmlMethods.EjecutaBase("creaUsuario", cadCon, "crea", xmlParam);
        return Ok();
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