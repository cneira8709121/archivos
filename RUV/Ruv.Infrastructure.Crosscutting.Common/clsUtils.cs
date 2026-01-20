using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Collections.ObjectModel;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.Infrastructure.Crosscutting.Common
{
  public class clsUtils
  {

    /// <summary>
    /// Borra un registro si está perviamente marcado como "Insertar" o 
    /// lo marca como borrado en los demás casos.
    /// </summary>
    /// <param name="coleccion"></param>
    /// <param name="entidad"></param>
    public static void BorrarEntidad<T1>(ObservableCollection<T1> coleccion,
      T1 entidad)  where T1: clsEntidadBase
    {
      if (entidad.EstadoRegistro == eEstadoRegistro.Insertar)
        coleccion.Remove(entidad);
      else
        entidad.EstadoRegistro = eEstadoRegistro.Eliminado;
    }

    /// <summary>
    /// Crea una copia de una ObservableCollection para un tipo sencillo.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="propiedad"></param>
    /// <returns></returns>
    public static ObservableCollection<T1> CopiarObservableCollectionOf<T1>(
      ObservableCollection<T1> propiedad)
    {
      if (propiedad == null) return null;

      ObservableCollection<T1> Resultado = new ObservableCollection<T1>();
      foreach (T1 item in propiedad)
      {
        Resultado.Add(item);
      }

      return Resultado;
    }

    /// <summary>
    /// Crea una copia de una List para un tipo genérico sencillo.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="propiedad"></param>
    /// <returns></returns>
    public static List<T1> CopiarListOf<T1>(
      List<T1> propiedad)
    {
      if (propiedad == null) return null;

      List<T1> Resultado = new List<T1>();
      foreach (T1 item in propiedad)
      {
        Resultado.Add(item);
      }

      return Resultado;
    }

    public static void LogToDesktop(string texto)
    {
      try
      {
        using (System.IO.TextWriter TW = new
          System.IO.StreamWriter(@"C:\Users\nestor.fernandez\Desktop\SiopdWPF_Log.txt",
          true))
        {
          TW.WriteLine(
            string.Format("{0}: {1}",
                DateTime.Now.ToLongTimeString(),
                texto));
          TW.Close();
        }
      }
      catch
      {
      }

    }
  }
}
