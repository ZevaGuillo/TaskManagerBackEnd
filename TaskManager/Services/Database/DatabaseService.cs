using System.Data;
using System.Text.Json.Nodes;
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

                // Console.WriteLine($" Id: {id.ToString()} Nombre: {nombre} Correo: {correoe} Tareas: {tareas}");
                
            }


        return user;
    }
}
