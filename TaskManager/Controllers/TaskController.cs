using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using TaskManager.Contracts.Task;
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
        try
        {
            var mensaje = await _taskService.EliminarTask(id);

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
    
}