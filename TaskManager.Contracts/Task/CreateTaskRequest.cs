namespace TaskManager.Contracts.Task;

public record CreateTaskRequest(
    string titulo,
    string descripcion,
    DateTime fechaFin,
    DateTime fechaInicio,
    bool estado
);