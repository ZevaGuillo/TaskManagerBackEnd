namespace TaskManager.Contracts.Task;

public record TaskResponse(
    string id,
    string titulo,
    string descripcion,
    DateTime fechaFin,
    DateTime fechaInicio,
    bool estado
);