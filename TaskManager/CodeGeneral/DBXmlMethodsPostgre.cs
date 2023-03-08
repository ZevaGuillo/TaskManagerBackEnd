using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CodeGeneral
{
    public static class DBXmlMethodsP
    {
        public static XDocument GetXml<T>(T criterio)
        {
            XDocument resultado = new XDocument(new XDeclaration("1.0", "utf-u", "false"));
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using XmlWriter xw = resultado.CreateWriter();
                xs.Serialize(xw, criterio);
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static async Task<NpgsqlDataReader> EjecutarProcedure(string procedure, XDocument dataXML)
        {
            string connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conn_bd"];
            try
            {
                // Crear una conexión a la base de datos
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);

                await connection.OpenAsync().ConfigureAwait(false);

                // Crear un comando que ejecuta el procedimiento almacenado
                NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM {procedure}('{dataXML.ToString()}')", connection);

                command.CommandType = CommandType.Text;
                command.CommandTimeout = 120;

                // Ejecutar el procedimiento almacenado y obtener los datos
                NpgsqlDataReader reader = command.ExecuteReader();
                
                return reader;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception($"Error de PostgreSQL: {ex.Message}");
            }
        }
    }
}

