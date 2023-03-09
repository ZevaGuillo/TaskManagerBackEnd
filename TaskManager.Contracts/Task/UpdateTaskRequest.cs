namespace TaskManager.Contracts.Task;

public record UpdateTaskRequest(
    string Id,
    string Titulo,
    string Descripcion,
    DateTime FechaFin,
    DateTime FechaInicio,
    bool Estado
);