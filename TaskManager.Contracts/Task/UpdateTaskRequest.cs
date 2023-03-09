namespace TaskManager.Contracts.Task;

public record UpdateTaskRequest(
    string Id,
    string Titulo,
    string Descripcion,
    string FechaFin,
    string FechaInicio,
    bool Estado
);