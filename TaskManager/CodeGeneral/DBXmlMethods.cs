using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Xml;

namespace CodeGeneral
{
    public static class DBXmlMethods
    {
        public static XDocument GetXml<T>(T criterio)
        {
            XDocument resultado = new XDocument(new XDeclaration("1.0", "utf-u", "false"));
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using XmlWriter xw = resultado.CreateWriter(); xs.Serialize(xw, criterio);
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public static async Task<DataSet> EjecutaBase(string Procedimiento, string conexion, string transaccion, XDocument dataXML)
        {
            DataSet dsResultado = new DataSet();
            SqlConnection con= new SqlConnection(conexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter adt;
                cmd.CommandText = Procedimiento;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandTimeout = 120;
                cmd.Parameters.Add("@iTransaccion",SqlDbType.VarChar).Value = transaccion;
                cmd.Parameters.Add("@iXml",SqlDbType.Xml).Value = dataXML.ToString();
                await con.OpenAsync().ConfigureAwait(false);
                adt = new SqlDataAdapter(cmd);
                adt.Fill(dsResultado);
                cmd.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Logs", "EjecutaBase", e.ToString());
                con.Close();
            }
            finally
            {
                if (con.State == ConnectionState.Open) {
                    con.Close() ;
                }
            }
            return dsResultado;
        }
    }
}