using TaskManager.Contracts.Task;
using TaskManager.Models;

namespace TaskManager.Services.Tasks;

public interface ITaskService
{
    Task<string> CreateTask(CreateTaskRequest task);
    Dictionary<Guid, TaskModel> GetTasks();
}