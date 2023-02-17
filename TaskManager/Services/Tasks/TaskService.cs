using TaskManager.Models;

namespace TaskManager.Services.Tasks;

public class TaskService : ITaskService
{

    private static readonly Dictionary<Guid, TaskModel> _tasks = new(); 
    public void CreateTask(TaskModel task)
    {
        _tasks.Add(task.Id, task);
    }

    public Dictionary<Guid, TaskModel> GetTasks()
    {
        return _tasks;
    }
}