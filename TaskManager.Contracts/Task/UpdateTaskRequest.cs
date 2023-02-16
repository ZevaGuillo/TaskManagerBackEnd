namespace TaskManager.Contracts.Task;

public record UpdateTaskRequest(
    string titulo,
    string descripcion,
    DateTime fechaFin,
    DateTime fechaInicio,
    bool estado
);