using System.Xml.Linq;
using CodeGeneral;
using Npgsql;
using TaskManager.Models;

namespace TaskManager.Services.Databse;

public class DatabaseService
{
    public static async Task<User> crearUsuario(string nombre, string correo, string contrasena)
    {
        User user = new User();

        XDocument xmlParam = XDocument.Parse("<CreateUserRequest><nombre>" + nombre + "</nombre><correo>" + correo + "</correo><contrasena>" + contrasena + "</contrasena></CreateUserRequest>");
        Console.WriteLine(xmlParam.ToString());

        NpgsqlDataReader reader = await DBXmlMethodsP.EjecutarProcedure("crear_usuario", xmlParam);
        List<object> lista = new List<object>();
        {
            // Iterar sobre los registros obtenidos e imprimirlos en la consola
            while (reader.Read())
            {
                user.Uid = reader.GetGuid(0);
                user.nombre = reader.GetString(1);
                user.correo = reader.GetString(2);
                // Console.WriteLine($" Id: {id.ToString()} Nombre: {nombred} Correo: {correod}");
            }
        }

        return user;
    }

    public static async Task<User> Login(string correo, string contrasena)
    {
        User user = new User();

        XDocument xmlParam = XDocument.Parse("<LoginRequest><correo>" + correo + "</correo><contrasena>" + contrasena + "</contrasena></LoginRequest>");
        Console.WriteLine(xmlParam.ToString());

        NpgsqlDataReader reader = await DBXmlMethodsP.EjecutarProcedure("login", xmlParam);

        while (reader.Read())
        {
            user.Uid = reader.GetGuid(0);
            user.nombre = reader.GetString(1);
            user.correo = reader.GetString(2);

            //TODO: asignar tareas
            string tareas = reader.GetString(3);

            Console.WriteLine($"  Tareas: {tareas}");

        }


        return user;
    }

    public static async Task<Object> CrearTask(string id_usuario, string titulo, string descripcion, DateTime fecha_fin, DateTime fecha_inicio)
    {
        var response = new Object();

        XDocument xmlParam = XDocument.Parse("<TaskModel>" +
                    "<id_usuario>" + id_usuario + "</id_usuario>" +
                    "<titulo>" + titulo + "</titulo>" +
                    "<descripcion>" + descripcion + "</descripcion>" +
                    "<fecha_fin>" + fecha_fin.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</fecha_fin>" +
                    "<fecha_inicio>" + fecha_inicio.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</fecha_inicio>" +
                    "</TaskModel>");

        Console.WriteLine(xmlParam.ToString());

        NpgsqlDataReader reader = await DBXmlMethodsP.EjecutarProcedure("crearTarea", xmlParam);

        while (reader.Read())
        {
            var tarea_id = reader.GetGuid(0);
            var mensaje = reader.GetString(1);

            response = new
            {
                tarea_id = tarea_id,
                mensaje = mensaje
            };

            Console.WriteLine(mensaje);

        }

        return response;
    }

    public static async Task<Object> EditarTask(string id, string titulo, string descripcion, string fecha_fin, string fecha_inicio, Boolean estado)
    {
        var response = new Object();

        XDocument xmlParam = XDocument.Parse("<TaskModel>" +
            "<id>" + id + "</id>" +
            "<titulo>" + titulo + "</titulo>" +
            "<descripcion>" + descripcion + "</descripcion>" +
            "<fecha_fin>" + fecha_fin + "</fecha_fin>" +
            "<fecha_inicio>" + fecha_inicio + "</fecha_inicio>" +
            "<estado>" + estado + "</estado>" +
            "</TaskModel>");

        Console.WriteLine(xmlParam.ToString());

        NpgsqlDataReader reader = await DBXmlMethodsP.EjecutarProcedure("editar_tarea", xmlParam);

        while (reader.Read())
        {
            var tarea_id = reader.GetGuid(0);
            var mensaje = reader.GetString(1);

            response = new
            {
                tarea_id = tarea_id,
                mensaje = mensaje
            };

            Console.WriteLine(mensaje);

        }

        return response;
    }

    public static async Task<Object> EliminarTask(string id)
    {
        var response = new Object();

        XDocument xmlParam = XDocument.Parse("<TaskModel>" +
            "<id>" + id + "</id>" +
            "</TaskModel>");

        Console.WriteLine(xmlParam.ToString());

        NpgsqlDataReader reader = await DBXmlMethodsP.EjecutarProcedure("eliminar_tarea", xmlParam);

        while (reader.Read())
        {
            var tarea_id = reader.GetGuid(0);
            var mensaje = reader.GetString(1);

            response = new
            {
                tarea_id = tarea_id,
                mensaje = mensaje
            };

            Console.WriteLine(mensaje);

        }

        return response;
    }

        public static async Task<List<Object>> GetTask(string user_id)
    {
        var response = new List<Object>();

        XDocument xmlParam = XDocument.Parse("<TaskModel>" +
            "<id_usuario>" + user_id + "</id_usuario>" +
            "</TaskModel>");

        Console.WriteLine(xmlParam.ToString());

        NpgsqlDataReader reader = await DBXmlMethodsP.EjecutarProcedure("get_user_tasks", xmlParam);

        while (reader.Read())
        {
            var tarea_id = reader.GetGuid(0);

            response.Add(new
            {
                id= tarea_id,
                titulo= reader.GetString(1),
                descripcion = reader.GetString(2),
                fechaInicio = reader.GetString(3),
                fechaFin = reader.GetString(4),
                estado = reader.GetBoolean(5),
            });

        }

        return response;
    }

}
