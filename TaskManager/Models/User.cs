namespace TaskManager.Models;

public class User{
    
    public Guid Uid { get; set;}
    public string token { get; set; }
    public string nombre { get; set; }
    public string correo { get; set; }
    public string contraseña { get; set; }
    public List<TaskModel> tasks { get; set; }

    public User(Guid uid, string token, string nombre, string correo, string contraseña, List<TaskModel> tasks)
    {
        Uid = uid;
        this.token = token;
        this.nombre = nombre;
        this.correo = correo;
        this.contraseña = contraseña;
        this.tasks = tasks;
    }
}