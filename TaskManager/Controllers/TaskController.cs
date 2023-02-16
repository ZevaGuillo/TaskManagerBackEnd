using Microsoft.AspNetCore.Mvc;
using TaskManager.Contracts.Task;

namespace TaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{

    [HttpPost()]
    public IActionResult CreateTask(CreateTaskRequest request)
    {
        return Ok();
    }

    [HttpGet()]
    public IActionResult GetTasks()
    {
        return Ok();
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