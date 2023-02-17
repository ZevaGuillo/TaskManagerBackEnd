namespace TaskManager.Contracts.Task;

public record CreateTaskRequest(
    string Titulo,
    string Descripcion,
    DateTime FechaFin,
    DateTime FechaInicio,
    bool Estado
);