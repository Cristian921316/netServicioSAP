using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using netServicioSAP.ServiceReference1;
using netServicioSAP.Dto;
using netServicioSAP.Utils;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace netServicioSAP
{
    class Program
    {
        static CreateLog create;
        static ProcessSap processSap;
        static void Main(string[] args)
        {
            create = new CreateLog();
            
            try
            {
                

                if (IsExecutingApplication() == false)
                {
                    create.insertLog("******************************** START SERVICES SEND SAP**************************************");
                    Timer timer1 = new Timer(timer1_Tick, null, 0, 2000);
                    Console.ReadLine();
                }
                else
                {
                    create.insertLog("The service is executing already");
                    
                }
                
            }
            catch (Exception ex)
            {
                create.insertLog(ex.Message);
            }
            


        }

        private static void timer1_Tick(Object o)
        {
            try
            {
                processSap = new ProcessSap();
                processSap.StartProcessSap();
                //Console.WriteLine("Prueba Timer");
                //Inserta_Log("Timer Finalizandooo " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                
                create.insertLog(ex.Message.ToString(CultureInfo.InvariantCulture));
                
                
            }
        }


        private static bool IsExecutingApplication()
        {

            // Proceso actual
            Process currentProcess = Process.GetCurrentProcess();

            // Matriz de procesos
            Process[] processes = Process.GetProcesses();

            // Recorremos los procesos en ejecución
            foreach (Process p in processes)
            {
                if (p.Id != currentProcess.Id)
                {
                    if (p.ProcessName == currentProcess.ProcessName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


    }
}
