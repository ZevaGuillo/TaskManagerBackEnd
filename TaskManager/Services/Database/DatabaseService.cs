using System.Data;
using System.Xml.Linq;
using CodeGeneral;

namespace TaskManager.Services.Databse;

public class DatabaseService 
{
    public static async Task<List<Object>> crearUsuario(string nombre, string correo, string contrasena)
    {
        
        var cadCon = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conn_bd"];
        XDocument xmlParam = XDocument.Parse("<CreateUserRequest><nombre>"+nombre+"</nombre><correo>"+correo+"</correo><contrasena>"+contrasena+"</contrasena></CreateUserRequest>");
        Console.WriteLine(xmlParam.ToString());
        DataSet dsResultados = await DBXmlMethodsP.EjecutaBase("auth", cadCon, "crear", xmlParam);
        List<object> lista = new List<object>();
        if (dsResultados.Tables.Count > 0 && dsResultados.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsResultados.Tables[0].Rows)
            {
                var objResponse = new
                {
                    Leyenda = "pribando"
                };
                lista.Add(objResponse);
            }
        }
        else
        {
            var objResponse = new
            {
                Leyenda = "Error... No se pudo procesar la operaciòn..."
            };
            lista.Add(objResponse);
        }
        return lista;
    }
}
