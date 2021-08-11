using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using netServicioSAP.ServiceReference1;
namespace netServicioSAP
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BasicHttpsBinding binding = new BasicHttpsBinding();
                binding.Security.Mode = BasicHttpsSecurityMode.Transport;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                binding.OpenTimeout = new TimeSpan(0, 10, 0);
                binding.CloseTimeout = new TimeSpan(0, 10, 0);
                binding.SendTimeout = new TimeSpan(0, 10, 0);
                binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                EndpointAddress endpoint = new EndpointAddress("https://ndddev100.gcp.pronaca.com:50001/XISOAPAdapter/MessageServlet?senderParty=&senderService=BS_MMS_DEV_EC&receiverParty=&receiverService=&interface=ImportacionPesosPedidos_OS&interfaceNamespace=http://pronaca.com/INT011/mms/1.0/ImportacionPesosPedidos");
                ImportacionPesosPedidos_OSClient oSClient = new ImportacionPesosPedidos_OSClient(binding,endpoint);
                oSClient.ClientCredentials.UserName.UserName = "MMS_USER";
                oSClient.ClientCredentials.UserName.Password = "D?#7;kK3e9g7.9";
                oSClient.Open();
                

                 //Proceso
                 ImportacionPesosPedidosTypeControlProceso typeControlProceso = new ImportacionPesosPedidosTypeControlProceso();
                typeControlProceso.CodigoCompania = "261";
                typeControlProceso.CodigoSistema = "MMS";
                typeControlProceso.CodigoServicio = "GESIMPPESPEDBV";
                typeControlProceso.Proceso = "ACTUALIZAR/EJECUTAR";
                typeControlProceso.Resultado = "";

                //SERVIDOR 
                ImportacionPesosPedidosTypeControlProcesoERPServidor controlProcesoERPServidor = new ImportacionPesosPedidosTypeControlProcesoERPServidor();
                controlProcesoERPServidor.Nombre = "192.168.50.109";
                controlProcesoERPServidor.Usuario = "genmmsdz";
                controlProcesoERPServidor.Clave = "Genmms05";
                controlProcesoERPServidor.ClaveEncriptada = "false";
                //lista controlProcesoERPServidor
                List<ImportacionPesosPedidosTypeControlProcesoERPServidor> ListcontrolProcesoERPServidor = new List<ImportacionPesosPedidosTypeControlProcesoERPServidor>();
                ListcontrolProcesoERPServidor.Add(controlProcesoERPServidor);
                
                //SESSION
                ImportacionPesosPedidosTypeControlProcesoERPSesion controlProcesoERPSesion = new ImportacionPesosPedidosTypeControlProcesoERPSesion();
                controlProcesoERPSesion.Programa = "whcpe9294m100";
                controlProcesoERPSesion.Shell = "ba6.2";
                ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet parametrosSet = new ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet();
                parametrosSet.Nombre = "CIA";
                parametrosSet.Nombre = "261";
                ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet parametrosSet1 = new ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet();
                parametrosSet1.Nombre = "COSI";
                parametrosSet1.Nombre = "MMS";
                ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet parametrosSet2 = new ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet();
                parametrosSet2.Nombre = "SECINI";
                parametrosSet2.Nombre = "7199";
                ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet parametrosSet3 = new ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet();
                parametrosSet3.Nombre = "SECFIN";
                parametrosSet3.Nombre = "7199";
                List<ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet> listParametrosSet = new List<ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet>();
                listParametrosSet.Add(parametrosSet);
                listParametrosSet.Add(parametrosSet1);
                listParametrosSet.Add(parametrosSet2);
                listParametrosSet.Add(parametrosSet3);
                controlProcesoERPSesion.Parametros = listParametrosSet.ToArray();
                List<ImportacionPesosPedidosTypeControlProcesoERPSesion> ListcontrolProcesoERPSesion = new List<ImportacionPesosPedidosTypeControlProcesoERPSesion>();
                ListcontrolProcesoERPSesion.Add(controlProcesoERPSesion);
                //ERP
                ImportacionPesosPedidosTypeControlProcesoERP controlProcesoERP = new ImportacionPesosPedidosTypeControlProcesoERP();
                controlProcesoERP.Version = "V";
                controlProcesoERP.Servidor = ListcontrolProcesoERPServidor.ToArray();
                controlProcesoERP.Sesion = ListcontrolProcesoERPSesion.ToArray();
                List<ImportacionPesosPedidosTypeControlProcesoERP> ListcontrolProcesoERP = new List<ImportacionPesosPedidosTypeControlProcesoERP>();
                ListcontrolProcesoERP.Add(controlProcesoERP);
                typeControlProceso.ERP = ListcontrolProcesoERP.ToArray();
                //

                //CABECERA
                ImportacionPesosPedidosTypeCabecera typeCabecera = new ImportacionPesosPedidosTypeCabecera();
                typeCabecera.Almacen = "A1";
                typeCabecera.Secuencial = "7199";
                typeCabecera.ViajeCompleto = "05082021024";
                typeCabecera.FechaEntrega = "2021-08-06";
                typeCabecera.RutaEstandar = "024";
                typeCabecera.Usuario = "genmmsdz";
                typeCabecera.Fase = "3";
                typeCabecera.Estado = "1";
                typeCabecera.Error = "";
                //DETALLE CABECERA
                ImportacionPesosPedidosTypeDetalleCabecera detalleCabecera = new ImportacionPesosPedidosTypeDetalleCabecera();
                detalleCabecera.SecuenciaSecuencial = "1";
                detalleCabecera.ViajeCompleto = "05082021024";
                detalleCabecera.OrdenVenta = "VN2965334";
                detalleCabecera.Conjunto = "1";
                detalleCabecera.Posicion = "1";
                detalleCabecera.NumeroSecuencia = "0";
                detalleCabecera.NumeroSugerencia = "0";
                detalleCabecera.RutaEstandar = "024";
                detalleCabecera.Cliente = "000822100";
                detalleCabecera.SecuenciaEntrega = "0";
                detalleCabecera.Articulo = "1132W";
                detalleCabecera.Almacen = "A1";
                detalleCabecera.UnidadAlmacenamiento = "kg";
                detalleCabecera.CantidadUnidadStock = "34";
                detalleCabecera.CantidadUnidadStock = "33.20";
                detalleCabecera.CantidadUnidadStock = "37.24";
                detalleCabecera.Fase = "3";
                detalleCabecera.Estado = "1";
                detalleCabecera.Error = "";
                List<ImportacionPesosPedidosTypeDetalleCabecera> arrayDetCabecera = new List<ImportacionPesosPedidosTypeDetalleCabecera>();
                arrayDetCabecera.Add(detalleCabecera);
                


                ImportacionPesosPedidosType importacionPesosPedidos = new ImportacionPesosPedidosType();
                importacionPesosPedidos.ControlProceso = typeControlProceso;
                importacionPesosPedidos.Cabecera = typeCabecera;
                importacionPesosPedidos.DetallesCabecera = arrayDetCabecera.ToArray();
                ImportacionPesosPedidosType_resp respuesta = oSClient.ImportacionPesosPedidos_OS(importacionPesosPedidos);

                oSClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            


        }
    }
}
