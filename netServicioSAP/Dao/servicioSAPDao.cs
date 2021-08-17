using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netServicioSAP.Utils;

namespace netServicioSAP.DAO
{
    public class servicioSAPDao
    {
        readonly string _conex = ConfigurationManager.ConnectionStrings["XMLConnectionString"].ConnectionString;
        SqlConnection _sqlConnection1 = new SqlConnection();

        CreateLog createLog = new CreateLog();
        public DataTable getInformationSend()
        {
            try {

                if (_sqlConnection1.State == ConnectionState.Open)
                {
                    _sqlConnection1.Close();
                }
                var listadoXML_DT = new DataTable();
                _sqlConnection1 = new SqlConnection(_conex);
                var cmd = new SqlCommand
                {
                    CommandText = "XML_1_INTERFAZ_SAP_BUSCAR",
                    CommandType = CommandType.StoredProcedure,
                    Connection = _sqlConnection1
                };
                _sqlConnection1.Open();
                var da = new SqlDataAdapter(cmd);
                da.Fill(listadoXML_DT);
                _sqlConnection1.Close();

                return listadoXML_DT;


            } catch (Exception ex)
            {
                
                createLog.insertLog(ex.Message);
                return null;
            }
        
        }

        public string InsertarXMLRespuestaDespachoSubidaPesos(string codigoSec, string secuencialXML, string XMLrespuestaServicio, int tiempoInicioEnSegundos)
        {
            string resp = "ERROR";
            try
            {
                _sqlConnection1 = new SqlConnection(_conex);
                var cmd = new SqlCommand
                {
                    CommandText = "XML_2_DESPACHO_INTERFAZ_IMQ_INSERTAR_RESP_TRANS",
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ou_codSec", codigoSec);
                cmd.Parameters.AddWithValue("@ou_secuencialXML", secuencialXML);
                cmd.Parameters.AddWithValue("@ou_XMLrespuestaServicio", XMLrespuestaServicio);
                cmd.Parameters.AddWithValue("@ou_segundosInicioProceso", tiempoInicioEnSegundos);
                //Seccion declarar la variable de salida OUPUT
                cmd.Parameters.Add(new SqlParameter("@respuesta", SqlDbType.NVarChar, 50));
                cmd.Parameters["@respuesta"].Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _sqlConnection1;

                _sqlConnection1.Open();
                cmd.ExecuteNonQuery();

                resp = cmd.Parameters["@respuesta"].Value.ToString();
                _sqlConnection1.Close();
            }
            catch (Exception ex)
            {
                createLog.insertLog("ERROR AL INSERTAR RESPUESTA: PROCESS INSERTAR DESPACHO_TRANS     " + secuencialXML + ex.Message.ToString() + '\r');
                CambioEstados(secuencialXML.ToString(), "ACTUALIZAR/EJECUTAR", "SERVICIO");
            }
            finally
            {
                if (_sqlConnection1.State == ConnectionState.Open)
                    _sqlConnection1.Close();
            }
            return resp;
        }

        public string CambioEstados(string secuencialXML, string poceso, string servicio)
        {
            string resp = "ERROR";
            try
            {
                _sqlConnection1 = new SqlConnection(_conex);
                var cmd = new SqlCommand
                {
                    CommandText = "XML_3_INTERFAZ_IMQ_CAMBIO_ESTADOS",
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ou_secuencialXML", secuencialXML);
                cmd.Parameters.AddWithValue("@ou_proceso", poceso);
                cmd.Parameters.AddWithValue("@ou_servicio", servicio);
                //Para declarar el parametro output
                cmd.Parameters.Add(new SqlParameter("@respuesta", SqlDbType.NVarChar, 50));
                cmd.Parameters["@respuesta"].Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _sqlConnection1;

                _sqlConnection1.Open();
                cmd.ExecuteNonQuery();

                resp = cmd.Parameters["@respuesta"].Value.ToString();
                _sqlConnection1.Close();

                return resp;
            }
            catch (Exception e)
            {
                createLog.insertLog("Consulta_estado " + e.Message.ToString(CultureInfo.InvariantCulture));
                return "Error";
            }
            finally
            {
                if (_sqlConnection1.State == ConnectionState.Open)
                    _sqlConnection1.Close();
            }
        }


    }
}
