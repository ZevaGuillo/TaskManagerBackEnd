namespace TaskManager.Contracts.Task;

public record CreateTaskRequest(
    string id_usuario,
    string Titulo,
    string Descripcion,
    DateTime FechaFin,
    DateTime FechaInicio
);