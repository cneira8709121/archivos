using System;
using System.IO;
using System.Text;

namespace Ruv.WPF.Captura.Infrastructure
{
    public class clsLog
    {
        static object LockRegistrar = new object();

        /// <summary>
        /// Registra un mensaje de log en un archivo local.
        /// </summary>
        /// <param name="formato"></param>
        /// <param name="valores"></param>
        public void Registrar(string formato, params string[] valores)
        {
            lock (LockRegistrar)
            {
                if (!Directory.Exists(Ruta))
                {
                    Directory.CreateDirectory(Ruta);
                }

                using (System.IO.TextWriter TW = new System.IO.StreamWriter(ArchivoActual, true))
                {
                    TW.WriteLine(
                      string.Format("{0:D2} {1} > {2}",
                          DateTime.Now.Day,
                          DateTime.Now.ToString("HH:mm:ss"),
                          string.Format(formato, valores)));
                    TW.Close();
                }
            }
        }

        /// <summary>
        /// Registra un mensaje de log en un archivo local.
        /// </summary>
        /// <param name="formato"></param>
        /// <param name="valores"></param>
        public void Registrar(string valor)
        {
            Registrar("{0}", valor);
        }

        /// <summary>
        /// Registra una excepcion en un archivo local.
        /// </summary>
        /// <param name="formato"></param>
        /// <param name="valores"></param>
        public void Registrar(string metodo, Exception ex)
        {
            StringBuilder Txt = new StringBuilder();
            Txt.AppendFormat("Method: {0} \n\r", metodo);
            while (ex != null)
            {
                Txt.AppendFormat("{0} \n\rSTACK: {1}", ex.Message, ex.StackTrace);
                ex = ex.InnerException;
            }
            Registrar("{0}", Txt.ToString());
        }

        /// <summary>
        /// El archivo actual de log.
        /// </summary>
        public string ArchivoActual
        {
            get
            {
                string Output = Path.Combine(Ruta,
                  string.Format("RUV_{0}.txt",
                  DateTime.Now.ToString("yyyy_MM")));

                System.Diagnostics.Debug.WriteLine(Output);

                return Output;
            }
        }

        /// <summary>
        /// Ruta actual para los archivos de log.
        /// </summary>
        public string Ruta
        {
            get
            {
                return Path.Combine(RUV.I.Util.RutaArchivosLocales, "Log");
                //return Path.GetTempPath();
            }
        }

    }
}
