namespace TaskManager.Models;

public class User
{

    public Guid Uid { get; set; }
    public string token { get; set; } = string.Empty;
    public string nombre { get; set; } = string.Empty;
    public string correo { get; set; } = string.Empty;
    public string contraseña { get; set; } = string.Empty;
    public List<TaskModel> tasks { get; set; } = new List<TaskModel>();

    public User(string nombre, string correo, string contraseña)
    {
        Uid = Guid.NewGuid();
        this.nombre = nombre;
        this.correo = correo;
        this.contraseña = contraseña;
    }
    public User(Guid Uid ,string nombre, string correo)
    {
        this.Uid = Uid;
        this.nombre = nombre;
        this.correo = correo;
        this.contraseña = contraseña;
    }
    public User(){}
}