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

        public static async Task<DataSet> EjecutaBase(string funcion, string conexion, string transaccion, XDocument dataXML)
        {
            using var con = new NpgsqlConnection(conexion);
            try
            {
                await con.OpenAsync().ConfigureAwait(false);

                using var cmd = new NpgsqlCommand($"SELECT {funcion}('{dataXML.ToString()}', '{transaccion}')", con);
                cmd.CommandType = CommandType.Text;

                // cmd.Parameters.AddWithValue("@itransaccion", NpgsqlDbType.Varchar, transaccion);
                // cmd.Parameters.AddWithValue("@ixml", NpgsqlDbType.Xml, dataXML.ToString());

                using var adapter = new NpgsqlDataAdapter(cmd);
                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                return dataSet;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar la función '{funcion}': {ex.Message}");
                throw;
            }
        }
    }
}

