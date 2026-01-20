using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


  public class clsLog
  {
    /// <summary>
    /// Registra una excepción.
    /// </summary>
    /// <param name="ex"></param>
    public static void Registrar(Exception ex)
    {
      StringBuilder SB = new StringBuilder();
      while (ex != null)
      {
        SB.Append(ex.Message + "\n\n");
        ex = ex.InnerException;
      }

      try
      {
        using (System.IO.TextWriter TW = new System.IO.StreamWriter(
               Path.Combine(Path.GetTempPath(), "RUV.txt"), true))
        {
          TW.WriteLine(
            string.Format("{0:D2} {1} > {2}",
                DateTime.Now.Day,
                DateTime.Now.ToString("HH:mm:ss"),
                SB.ToString()));
          TW.Close();
        }

      }
      catch { }

      System.Diagnostics.EventLog.WriteEntry("RUV", SB.ToString(),
        System.Diagnostics.EventLogEntryType.Error);
    }

    /// <summary>
    /// Registra un mensaje de log en un archivo local.
    /// </summary>
    /// <param name="formato"></param>
    /// <param name="valores"></param>
    public void Registrar(string formato, params string[] valores)
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
      Txt.AppendFormat("Method: {0}\n", metodo);
      while (ex != null)
      {
        Txt.AppendFormat("{0}\n", ex.Message);
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
          string.Format("SIPOD_{0}.txt",
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
        return Path.GetTempPath();
      }
    }

  }
