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

    //private readonly IAuthService _authService;

    [Route("[action]")]
    [HttpPost]
    public async Task<ActionResult<List<CreateUserRequest>>> crearUsuario([BindRequired]string nombre, [BindRequired] string correo, [BindRequired] string contraseña)
    {
        var cadCon = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conn_bd"];
        XDocument xmlParam = XDocument.Parse("<CreateUserRequest><nombre>"+nombre+"</nombre><correo>"+correo+"</correo><contraseña>"+contraseña+"</contraseña></CreateUserRequest>");
        Console.WriteLine(xmlParam.ToString());
        DataSet dsResultados = await DBXmlMethods.EjecutaBase("Auth", cadCon, "crear", xmlParam);
        List<object> lista = new List<object>();
        if (dsResultados.Tables.Count > 0 && dsResultados.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsResultados.Tables[0].Rows)
            {
                var objResponse = new
                {
                    Leyenda = row["estado"].ToString()
                };
                lista.Add(objResponse);
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
                if (row["estado"].ToString() == "Contraseña valida")
                {
                    var objResponse = new
                    {
                        Nombre = row["nombres"].ToString(),
                        Correo = row["correo"].ToString(),
                        Leyenda = row["estado"].ToString()
                    };
                    lista.Add(objResponse);
                }
                else
                {
                    var objResponse = new
                    {
                        Leyenda = row["estado"].ToString()
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