using CodeGeneral;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using System.Net;
using System.Xml.Linq;
using TaskManager.Contracts.Task;
using TaskManager.Models;
using TaskManager.Services.Tasks;

namespace TaskManager.Controllers;


[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{

    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [Route("[action]")]
    [HttpPost]
    public async Task<ActionResult> crear(CreateTaskRequest request)
    {
        try
        {
            var mensaje = await _taskService.CreateTask(request);

            return StatusCode(201, new { Status = (int)HttpStatusCode.Created, Result = mensaje });
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
    [HttpPut]
    public async Task<ActionResult> editar(UpdateTaskRequest request)
    {
        try
        {
            var mensaje = await _taskService.EditTask(request);

            return Ok( new { Status = (int)HttpStatusCode.OK, Result = mensaje } );
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
    [HttpDelete]
    public async Task<ActionResult<List<CreateTaskRequest>>> eliminar([BindRequired] string id)
    {
        var cadCon = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conn_bd"];
        XDocument xmlParam = XDocument.Parse("<TaskModel>" +
            "<id>" + id + "</id>" +
            "</TaskModel>");
        Console.WriteLine(xmlParam.ToString());
        DataSet dsResultados = await DBXmlMethods.EjecutaBase("TaskManager", cadCon, "eliminar", xmlParam);
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
                Leyenda = "Error... No se pudo procesar la operaci�n..."
            };
            lista.Add(objResponse);
        }
        return Ok(lista);
    }
    [Route("[action]")]
    [HttpPut]
    public async Task<ActionResult<List<CreateTaskRequest>>> cambiarEstado([BindRequired] string id)
    {
        var cadCon = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conn_bd"];
        XDocument xmlParam = XDocument.Parse("<TaskModel>" +
            "<id>" + id + "</id>" +
            "</TaskModel>");
        Console.WriteLine(xmlParam.ToString());
        DataSet dsResultados = await DBXmlMethods.EjecutaBase("TaskManager", cadCon, "cambiarEstado", xmlParam);
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
                Leyenda = "Error... No se pudo procesar la operaci�n..."
            };
            lista.Add(objResponse);
        }
        return Ok(lista);
    }
}