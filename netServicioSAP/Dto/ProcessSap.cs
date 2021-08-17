using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netServicioSAP.DAO;
using netServicioSAP.Utils;
using netServicioSAP.ServiceReference1;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.ServiceModel;
using System.Diagnostics;

namespace netServicioSAP.Dto
{
    public class ProcessSap
    {
        servicioSAPDao sAPDao = new servicioSAPDao();
        CreateLog createLog = new CreateLog();
        private string CodigoSec = "",secuenciaXML="";
        public void StartProcessSap()
        {
            try {

              DataTable inforSendSap =  sAPDao.getInformationSend();
                if (inforSendSap != null)
                {
                    for (int i = 0; i < inforSendSap.Rows.Count; i++)
                    {
                        string xml = inforSendSap.Rows[i][2].ToString();
                        CodigoSec = inforSendSap.Rows[i][0].ToString();
                        secuenciaXML = inforSendSap.Rows[i][1].ToString();
                        //Stopwatch stotwatch = new Stopwatch();
                        //stotwatch.Start();
                                        
                        readXMLSend(xml);
                        createLog.insertLog("Ruta enviada: " + secuenciaXML + " " + DateTime.Now.ToString());
                        Console.WriteLine("Ruta enviada: "+ secuenciaXML +" "+DateTime.Now.ToString());
                       



                    }


                }


            } catch (Exception ex)
            {
                createLog.insertLog(ex.Message);
            }
        }

        public void readXMLSend(string xmlRequest)
        {
            try {

                var doc = new XmlDocument();
                doc.LoadXml(xmlRequest);

                //Proceso
               
                ImportacionPesosPedidosTypeControlProceso typeControlProceso = new ImportacionPesosPedidosTypeControlProceso();
                ImportacionPesosPedidosTypeControlProcesoERPServidor controlProcesoERPServidor = new ImportacionPesosPedidosTypeControlProcesoERPServidor();
                List<ImportacionPesosPedidosTypeControlProcesoERPServidor> ListcontrolProcesoERPServidor = new List<ImportacionPesosPedidosTypeControlProcesoERPServidor>();
                ImportacionPesosPedidosTypeControlProcesoERPSesion controlProcesoERPSesion = new ImportacionPesosPedidosTypeControlProcesoERPSesion();
                List<ImportacionPesosPedidosTypeControlProcesoERPSesion> ListcontrolProcesoERPSesion = new List<ImportacionPesosPedidosTypeControlProcesoERPSesion>();
                
                List<ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet> ListparametrosSetSesion = new List<ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet>();
                ImportacionPesosPedidosTypeControlProcesoERP controlProcesoERP = new ImportacionPesosPedidosTypeControlProcesoERP();
                List<ImportacionPesosPedidosTypeControlProcesoERP> ListcontrolProcesoERP = new List<ImportacionPesosPedidosTypeControlProcesoERP>();

                ImportacionPesosPedidosTypeCabecera pesosPedidosTypeCabecera = new ImportacionPesosPedidosTypeCabecera();

                List<ImportacionPesosPedidosTypeDetalleCabecera> ListtypeDetalleCabecera = new List<ImportacionPesosPedidosTypeDetalleCabecera>();


                foreach (XmlNode item in doc.ChildNodes) //PROCESO "GesExpCapPesLn"
                {
                    foreach (XmlNode subitem in item.ChildNodes) // NODOS RAIZ ControlProceso, Cabecera, DetalleCabecera
                    {
                        

                        foreach (XmlNode subitemDetalle in subitem.ChildNodes) // NODOS RAIZ ControlProceso, Cabecera, DetalleCabecera
                        {

                            if (subitemDetalle.Name == "CodigoCompania") typeControlProceso.CodigoCompania = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "CodigoSistema") typeControlProceso.CodigoSistema = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "CodigoSistema") typeControlProceso.CodigoSistema = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "CodigoServicio") typeControlProceso.CodigoServicio = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "Proceso") typeControlProceso.Proceso = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "ERP")
                            {
                                foreach (XmlNode detERP in subitemDetalle.ChildNodes)
                                {
                                    if (detERP.Name == "Version") controlProcesoERP.Version = detERP.InnerText.ToString();
                                    else if (detERP.Name == "Servidor")
                                    {
                                        foreach (XmlNode detErpServer in detERP.ChildNodes)
                                        {
                                            if (detErpServer.Name == "Nombre") controlProcesoERPServidor.Nombre = detErpServer.InnerText.ToString();
                                            else if (detErpServer.Name == "Usuario") controlProcesoERPServidor.Usuario = detErpServer.InnerText.ToString();
                                            else if (detErpServer.Name == "Clave") controlProcesoERPServidor.Clave = detErpServer.InnerText.ToString();
                                            else if (detErpServer.Name == "ClaveEncriptada") controlProcesoERPServidor.ClaveEncriptada = detErpServer.InnerText.ToString();
                                        }

                                    }
                                    else if (detERP.Name == "Sesion")
                                    {
                                        foreach (XmlNode detErpSesion in detERP.ChildNodes)
                                        {
                                            if (detErpSesion.Name == "Shell") controlProcesoERPSesion.Shell = detErpSesion.InnerText.ToString();
                                            else if (detErpSesion.Name == "Programa") controlProcesoERPSesion.Programa = detErpSesion.InnerText.ToString();
                                            else if (detErpSesion.Name == "Parametros")
                                            {
                                                foreach (XmlNode detErpSesionParam in detErpSesion.ChildNodes)
                                                {
                                                    if (detErpSesionParam.Name == "Set")
                                                    {
                                                        ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet parametrosSetSesion = new ImportacionPesosPedidosTypeControlProcesoERPSesionParametrosSet();
                                                        foreach (XmlNode detSetErpSesionParam in detErpSesionParam.ChildNodes)
                                                        {
                                                           
                                                            if (detSetErpSesionParam.Name == "Nombre") parametrosSetSesion.Nombre = detSetErpSesionParam.InnerText.ToString();
                                                            else if (detSetErpSesionParam.Name == "Valor") parametrosSetSesion.Valor = detSetErpSesionParam.InnerText.ToString();
                                                            

                                                        }
                                                        ListparametrosSetSesion.Add(parametrosSetSesion);
                                                    }
                                                   
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            //data cabecera
                            else if (subitemDetalle.Name == "Secuencial") pesosPedidosTypeCabecera.Secuencial = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "ViajeCompleto") pesosPedidosTypeCabecera.ViajeCompleto = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "FechaEntrega") pesosPedidosTypeCabecera.FechaEntrega = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "Almacen") pesosPedidosTypeCabecera.Almacen = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "Usuario") pesosPedidosTypeCabecera.Usuario = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "Fase") pesosPedidosTypeCabecera.Fase = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "Estado") pesosPedidosTypeCabecera.Estado = subitemDetalle.InnerText.ToString();
                            else if (subitemDetalle.Name == "Error") pesosPedidosTypeCabecera.Error = subitemDetalle.InnerText.ToString();

                            //data details
                            else if (subitemDetalle.Name == "DetalleCabecera")
                            {
                                ImportacionPesosPedidosTypeDetalleCabecera typeDetalleCabecera = new ImportacionPesosPedidosTypeDetalleCabecera();
                                foreach (XmlNode detalles in subitemDetalle.ChildNodes)
                                {
                                    
                                    if (detalles.Name == "OrdenVenta") typeDetalleCabecera.OrdenVenta = detalles.InnerText.ToString();
                                    else if (detalles.Name == "Posicion") typeDetalleCabecera.Posicion = detalles.InnerText.ToString();
                                    else if (detalles.Name == "SecuenciaSecuencial") typeDetalleCabecera.SecuenciaSecuencial = detalles.InnerText.ToString();
                                    else if (detalles.Name == "RutaEstandar") typeDetalleCabecera.RutaEstandar = detalles.InnerText.ToString();
                                    else if (detalles.Name == "Cliente") typeDetalleCabecera.Cliente = detalles.InnerText.ToString();
                                    else if (detalles.Name == "SecuenciaEntrega") typeDetalleCabecera.SecuenciaEntrega = detalles.InnerText.ToString();
                                    else if (detalles.Name == "Articulo") typeDetalleCabecera.Articulo = detalles.InnerText.ToString();
                                    else if (detalles.Name == "UnidadAlmacenamiento") typeDetalleCabecera.UnidadAlmacenamiento = detalles.InnerText.ToString();
                                    else if (detalles.Name == "CantidadUnidadStock") typeDetalleCabecera.CantidadUnidadStock = detalles.InnerText.ToString();
                                    else if (detalles.Name == "CantidadUnidadAlmacenamiento") typeDetalleCabecera.CantidadUnidadAlmacenamiento = detalles.InnerText.ToString();
                                    else if (detalles.Name == "PesoBruto") typeDetalleCabecera.PesoBruto = detalles.InnerText.ToString();
                                    else if (detalles.Name == "Conjunto") typeDetalleCabecera.Conjunto = detalles.InnerText.ToString();
                                    else if (detalles.Name == "NumeroSecuencia") typeDetalleCabecera.NumeroSecuencia = detalles.InnerText.ToString();
                                    else if (detalles.Name == "NumeroSugerencia") typeDetalleCabecera.NumeroSugerencia = detalles.InnerText.ToString();
                                    else if (detalles.Name == "ViajeCompleto") typeDetalleCabecera.ViajeCompleto = detalles.InnerText.ToString();
                                    else if (detalles.Name == "Almacen") typeDetalleCabecera.Almacen = detalles.InnerText.ToString();
                                    
                                }
                                ListtypeDetalleCabecera.Add(typeDetalleCabecera);
                            }


                        }
                    }
                }

                // set tags object
                
                controlProcesoERPSesion.Parametros = ListparametrosSetSesion.ToArray();
                ListcontrolProcesoERPServidor.Add(controlProcesoERPServidor);
                ListcontrolProcesoERPSesion.Add(controlProcesoERPSesion);
                ListcontrolProcesoERP.Add(controlProcesoERP);
                //
                controlProcesoERP.Servidor = ListcontrolProcesoERPServidor.ToArray();
                controlProcesoERP.Sesion = ListcontrolProcesoERPSesion.ToArray();
                typeControlProceso.ERP = ListcontrolProcesoERP.ToArray();
                ImportacionPesosPedidosType importacionPesosPedidos = new ImportacionPesosPedidosType();                
                importacionPesosPedidos.Cabecera = pesosPedidosTypeCabecera;
                importacionPesosPedidos.ControlProceso = typeControlProceso;
                importacionPesosPedidos.DetallesCabecera = ListtypeDetalleCabecera.ToArray();

               
                //SEND SAP
                sendToSap(importacionPesosPedidos);
               
                

            } catch (Exception ex)
            {
                createLog.insertLog(ex.Message);
            }
        
        }

        public void sendToSap(ImportacionPesosPedidosType pedidosSend)
        {
            try {

                BasicHttpsBinding binding = new BasicHttpsBinding();
                binding.Security.Mode = BasicHttpsSecurityMode.Transport;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                binding.OpenTimeout = new TimeSpan(0, 10, 0);
                binding.CloseTimeout = new TimeSpan(0, 10, 0);
                binding.SendTimeout = new TimeSpan(0, 10, 0);
                binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                EndpointAddress endpoint = new EndpointAddress("https://ndddev100.gcp.pronaca.com:50001/XISOAPAdapter/MessageServlet?senderParty=&senderService=BS_MMS_DEV_EC&receiverParty=&receiverService=&interface=ImportacionPesosPedidos_OS&interfaceNamespace=http://pronaca.com/INT011/mms/1.0/ImportacionPesosPedidos");
                ImportacionPesosPedidos_OSClient oSClient = new ImportacionPesosPedidos_OSClient(binding, endpoint);
                oSClient.ClientCredentials.UserName.UserName = "MMS_USER";
                oSClient.ClientCredentials.UserName.Password = "D?#7;kK3e9g7.9";
                oSClient.Open();


                ImportacionPesosPedidosType_resp respuesta = oSClient.ImportacionPesosPedidos_OS(pedidosSend);

                //convert to XML
                string XML_resp = "";
                using (var stringwriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(respuesta.GetType());
                    serializer.Serialize(stringwriter, respuesta);
                    XML_resp = stringwriter.ToString();
                }


                oSClient.Close();

                // insert resp DB
                sAPDao.InsertarXMLRespuestaDespachoSubidaPesos(CodigoSec,secuenciaXML, XML_resp,1);

            }
            catch (Exception ex)
            {
                createLog.insertLog(ex.Message);
            }
        }

       

    }
}
