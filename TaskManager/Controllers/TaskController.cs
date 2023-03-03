using CodeGeneral;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using System.Xml.Linq;
using TaskManager.Contracts.Task;
using TaskManager.Models;
using TaskManager.Services.Tasks;

namespace TaskManager.Controllers;


[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    [Route("[action]")]
    [HttpPut]
    public async Task<ActionResult<List<CreateTaskRequest>>> crear([BindRequired] string id_usuario, [BindRequired] string titulo, [BindRequired] string descripcion, [BindRequired] DateTime fecha_fin, [BindRequired] DateTime fecha_inicio, Boolean estado)
    {
        var cadCon = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conn_bd"];
        XDocument xmlParam = XDocument.Parse("<TaskModel>" +
            "<id_usuario>"+id_usuario+"</id_usuario>" +
            "<titulo>"+ titulo + "</titulo>" +
            "<descripcion>" + descripcion + "</descripcion>" +
            "<fecha_fin>" + fecha_fin  + "</fecha_fin>" +
            "<fecha_inicio>" + fecha_inicio + "</fecha_inicio>" +
            "<estado>" + estado + "</estado>" +
            "</TaskModel>");
        Console.WriteLine(xmlParam.ToString());
        DataSet dsResultados = await DBXmlMethods.EjecutaBase("TaskManager", cadCon, "crear", xmlParam);
        List<object> lista = new List<object>();
        if (dsResultados.Tables.Count > 0 && dsResultados.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsResultados.Tables[0].Rows)
            {
                var objResponse = new
                {
                    Leyenda = row["leyenda"].ToString()
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
    public async Task<ActionResult<List<CreateTaskRequest>>> editar([BindRequired] string id, [BindRequired] string titulo, [BindRequired] string descripcion, [BindRequired] DateTime fecha_fin, [BindRequired] DateTime fecha_inicio, Boolean estado)
    {
        var cadCon = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conn_bd"];
        XDocument xmlParam = XDocument.Parse("<TaskModel>" +
            "<id>" + id + "</id>" +
            "<titulo>" + titulo + "</titulo>" +
            "<descripcion>" + descripcion + "</descripcion>" +
            "<fecha_fin>" + fecha_fin + "</fecha_fin>" +
            "<fecha_inicio>" + fecha_inicio + "</fecha_inicio>" +
            "<estado>" + estado + "</estado>" +
            "</TaskModel>");
        Console.WriteLine(xmlParam.ToString());
        DataSet dsResultados = await DBXmlMethods.EjecutaBase("TaskManager", cadCon, "editar", xmlParam);
        List<object> lista = new List<object>();
        if (dsResultados.Tables.Count > 0 && dsResultados.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsResultados.Tables[0].Rows)
            {
                var objResponse = new
                {
                    Leyenda = row["leyenda"].ToString()
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
}