using TaskManager.Contracts.Task;
using TaskManager.Models;

namespace TaskManager.Services.Tasks;

public interface ITaskService
{
    Task<Object> CreateTask(CreateTaskRequest task);
    Task<Object> EditTask(UpdateTaskRequest task);
    Dictionary<Guid, TaskModel> GetTasks();
}