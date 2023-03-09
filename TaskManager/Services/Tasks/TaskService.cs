using TaskManager.Contracts.Task;
using TaskManager.Models;
using TaskManager.Services.Databse;

namespace TaskManager.Services.Tasks;

public class TaskService : ITaskService
{

    private static readonly Dictionary<Guid, TaskModel> _tasks = new(); 
    public async Task<string> CreateTask(CreateTaskRequest task)
    {

        string mensaje = await DatabaseService.CrearTask(task.id_usuario, task.Titulo, task.Descripcion, task.FechaFin, task.FechaInicio, task.Estado);

        return mensaje;
    }

    public Dictionary<Guid, TaskModel> GetTasks()
    {
        return _tasks;
    }
}