using CodeGeneral;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using TaskManager.Contracts.Auth;
using TaskManager.Models;
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

        var lista = await _authService.Register(request);

        return Ok(lista);
    }

    [Route("[action]")]
    [HttpPost]
    public async Task<ActionResult<List<LoginRequest>>> login([BindRequired] string correo, [BindRequired] string contraseña)
    {
        var cadCon = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conn_bd"];
        XDocument xmlParam = XDocument.Parse("<LoginRequest><correo>" + correo + "</correo><contraseña>" + contraseña + "</contraseña></LoginRequest>");
        Console.WriteLine(xmlParam.ToString());
        DataSet dsResultados = await DBXmlMethods.EjecutaBase("Auth", cadCon, "login", xmlParam);
        List<object> lista = new List<object>();
        if (dsResultados.Tables.Count > 0 && dsResultados.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsResultados.Tables[0].Rows)
            {
                if (row["leyenda"].ToString() == "Contraseña valida")
                {
                    var objResponse = new
                    {
                        id = row["idusuario"].ToString(),
                        nombre = row["nombres"].ToString(),
                        correo = row["correo"].ToString(),
                        tasks = JsonObject.Parse(row["tareas"].ToString()),
                        Leyenda = row["leyenda"].ToString()
                    };
                    lista.Add(objResponse);
                }
                else
                {
                    var objResponse = new
                    {
                        Leyenda = row["leyenda"].ToString()
                    };
                    lista.Add(objResponse);
                }
                
            }
        }
        else
        {
            var objResponse = new
            {
                Leyenda = "Error... No se pudo procesar la operaciòn..."
            };
            lista.Add(objResponse);
        }
        return Ok(lista);
    }

}