using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    [Authorize]
    public IActionResult CreateTask(CreateTaskRequest request)
    {
        var task = new TaskModel(
            Guid.NewGuid(),
            request.Titulo,
            request.Descripcion,
            request.FechaFin,
            request.FechaInicio,
            request.Estado
        );

        // TODO: hacer base de datos
        _taskService.CreateTask(task);

        var response = new TaskResponse(
            task.Id,
            task.Titulo,
            task.Descripcion,
            task.FechaFin,
            task.FechaInicio,
            task.Estado
        );
    

        return Ok(response);
    }

    [HttpGet()]
    public IActionResult GetTasks()
    {
        Dictionary<Guid,TaskModel> task = _taskService.GetTasks();

        return Ok(task);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateTask(Guid id, UpdateTaskRequest request)
    {
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteTasks(Guid id)
    {
        return Ok();
    }

}