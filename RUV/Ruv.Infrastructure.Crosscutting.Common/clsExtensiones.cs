using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
  public static class clsExtensiones
  {

    /// <summary>
    /// Concatena varias cadenas separándolas con espacios.
    /// </summary>
    /// <returns></returns>
    public static string UnirCadenas(this string objeto, params string[] cadenas)
    {
      StringBuilder resultado = null;
      foreach (var item in cadenas)
      {
        if (!string.IsNullOrWhiteSpace(item))
          if (resultado == null)
            resultado = new StringBuilder(item);
          else
            resultado.AppendFormat(" {0}", item);
      }

      if (resultado == null)
        return null;
      else
        return resultado.ToString();
    }
  }
}
