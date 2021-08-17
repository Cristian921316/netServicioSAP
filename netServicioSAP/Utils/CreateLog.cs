using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netServicioSAP.Utils
{
    public class CreateLog
    {


        public void insertLog(string mensaje)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["Log"].ToString(CultureInfo.InvariantCulture);

                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    string cadena = DateTime.Now.ToString(CultureInfo.InvariantCulture) + ", Proceso: " + mensaje;
                    byte[] bytes = Encoding.UTF8.GetBytes(cadena.ToString(CultureInfo.InvariantCulture));
                    fs.Write(bytes, 0, 1);
                    fs.Flush();
                    fs.Close();
                }
                else
                {
                    TextWriter tw = new StreamWriter(path, true);
                    tw.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture) + ", Proceso: " + mensaje);
                    tw.Close();
                }
            }
            catch (Exception ex)
            {
                //this.Invoke((MethodInvoker)delegate ()
                //{
                //    Inserta_Log(ex.Message.ToString(CultureInfo.InvariantCulture));
                //});
            }


        }


    }
}
