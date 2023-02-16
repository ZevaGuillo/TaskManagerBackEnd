namespace TaskManager.Models;

public class TaskModel
{

    public Guid Id { get; }
    public string Titulo { get; }
    public string Descripcion { get; }
    public DateTime FechaFin { get; }
    public DateTime FechaInicio { get; }
    public bool Estado { get; }

    public TaskModel(Guid id, string titulo, string descripcion, DateTime fechaFin, DateTime fechaInicio, bool estado)
    {
        Id = id;
        Titulo = titulo;
        Descripcion = descripcion;
        FechaFin = fechaFin;
        FechaInicio = fechaInicio;
        Estado = estado;
    }
}