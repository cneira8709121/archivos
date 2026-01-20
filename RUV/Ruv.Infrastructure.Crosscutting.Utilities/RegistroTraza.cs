using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

/// <summary>
/// Escribe logs de la aplicación en un archivo de texto
/// Autor: Diego Alvarez
/// Fecha: 12/12/2013
/// </summary>
public class RegistroTraza
{
    private static RegistroTraza instance = null;
    private static object syncRoot = new Object();
    private string ruta;
    private bool escribir;

    /// <summary>
    /// Constructor privato para instanciar la clase una única vez
    /// </summary>
    private RegistroTraza()
	{
        this.ruta = System.Configuration.ConfigurationManager.AppSettings["RutaLogs"];
        this.escribir = System.Configuration.ConfigurationManager.AppSettings["EscribirEnLog"].ToLower().Equals("si") ? true : false;
        if (!Directory.Exists(this.ruta))
        {
            Directory.CreateDirectory(this.ruta);
        }
	}

    /// <summary>
    /// Devuelve la instancia de la clase, si no ha sido instanciada, lo hace
    /// </summary>
    /// <returns></returns>
    public static RegistroTraza I
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    instance = new RegistroTraza();
                }
            }
            return instance;
        }
    }

    /// <summary>
    /// Registra una excepción.
    /// </summary>
    /// <param name="ex"></param>
    public void Registrar(Exception ex)
    {
        if (this.escribir)
        {
            StringBuilder SB = new StringBuilder();
            while (ex != null)
            {
                SB.Append(ex.Message + "\n\n");
                ex = ex.InnerException;
            }

            try
            {
                using (System.IO.TextWriter TW = new System.IO.StreamWriter(Path.Combine(this.ruta, "RUV.txt"), true))
                {
                    TW.WriteLine(
                      string.Format("{0:D2} {1} > {2}",
                          DateTime.Now.ToShortDateString(),
                          DateTime.Now.ToString("HH:mm:ss"),
                          SB.ToString()));
                    TW.Close();
                }

                System.Diagnostics.EventLog.WriteEntry("RUV", SB.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            catch { }
        }
    }

    /// <summary>
    /// Registra un mensaje de log en un archivo local.
    /// </summary>
    /// <param name="formato"></param>
    /// <param name="valores"></param>
    public void Registrar(string formato, params string[] valores)
    {
        if (this.escribir)
        {
            using (System.IO.TextWriter TW = new System.IO.StreamWriter(ArchivoActual, true))
            {
                TW.WriteLine(
                  string.Format("{0:D2} {1} > {2}",
                      DateTime.Now.ToShortDateString(),
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
        if (this.escribir)
        {
            Registrar("{0}", valor);
        }
    }

    /// <summary>
    /// Registra una excepcion en un archivo local.
    /// </summary>
    /// <param name="formato"></param>
    /// <param name="valores"></param>
    public void Registrar(string metodo, Exception ex)
    {
        if (this.escribir)
        {
            StringBuilder Txt = new StringBuilder();
            Txt.AppendFormat("Method: {0}\n", metodo);
            while (ex != null)
            {
                Txt.AppendFormat("{0}\n", ex.Message);
                ex = ex.InnerException;
            }
            Registrar("{0}", Txt.ToString());
        }
    }

    /// <summary>
    /// El archivo actual de log.
    /// </summary>
    private string ArchivoActual
    {
        get
        {
            string Output = Path.Combine(this.ruta, string.Format("RUV_{0}.txt", DateTime.Now.ToString("yyyy_MM")));

            System.Diagnostics.Debug.WriteLine(Output);

            return Output;
        }
    }
}