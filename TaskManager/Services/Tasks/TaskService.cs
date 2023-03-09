using TaskManager.Contracts.Task;
using TaskManager.Models;
using TaskManager.Services.Databse;

namespace TaskManager.Services.Tasks;

public class TaskService : ITaskService
{

    private static readonly Dictionary<Guid, TaskModel> _tasks = new(); 
    public async Task<Object> CreateTask(CreateTaskRequest task)
    {

        var response = await DatabaseService.CrearTask(task.id_usuario, task.Titulo, task.Descripcion, task.FechaFin, task.FechaInicio, task.Estado);

        return response;
    }

    public Dictionary<Guid, TaskModel> GetTasks()
    {
        return _tasks;
    }
}