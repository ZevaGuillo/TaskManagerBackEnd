using TaskManager.Contracts.Task;
using TaskManager.Models;

namespace TaskManager.Services.Tasks;

public interface ITaskService
{
    void CreateTask(TaskModel task);
    Dictionary<Guid,TaskModel> GetTasks();
}