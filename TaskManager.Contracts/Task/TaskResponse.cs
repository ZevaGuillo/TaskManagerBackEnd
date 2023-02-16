namespace TaskManager.Contracts.Task;

public record TaskResponse(
    Guid id,
    string titulo,
    string descripcion,
    DateTime fechaFin,
    DateTime fechaInicio,
    bool estado
);