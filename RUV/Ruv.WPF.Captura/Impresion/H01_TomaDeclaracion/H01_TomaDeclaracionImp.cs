using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Impresion
{
  /// <summary>
  /// Contiene la lógica de impersión para el Anexo 03.
  /// </summary>
  public class H01_TomaDeclaracionImp : ILogicaImpresion
  {
    #region CONSTRUCTOR

    public H01_TomaDeclaracionImp()
    { }

    #endregion

    #region VARIABLES

    clsInformacionImpresion Resultado;
    int PasoActual;
    clsTomaDeclaracion Entidad;
    bool ListaHechosActualizada = false;

    enum ePasosImpresion
    {
      Pregunta1a8 = 0,
      Pregunta9 = 1,
      Pregunta10 = 2,
      Fin = 3
    }

    #endregion

    #region LOGICA DE IMPRESIÓN

    /// <summary>
    /// Debe generar todos los objetos de la sección que se deben imprimir.
    /// </summary>
    /// <param name="entidad"></param>
    /// <returns></returns>
    public clsInformacionImpresion ProcesarImpresion(object fuenteDatos = null)
    {
      Entidad = (fuenteDatos as clsTomaDeclaracion);
      if (!ListaHechosActualizada)
      {
          RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.ActualizarConteoHechos();
        ListaHechosActualizada = true;
      }

      switch ((ePasosImpresion)PasoActual)
      {
        case ePasosImpresion.Pregunta1a8:
          Resultado.ObjetoCuerpo = ObtenerBloque(1);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Pregunta9:
          Resultado.ObjetoCuerpo = ObtenerBloque(2);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Pregunta10:
          Resultado.ObjetoCuerpo = ObtenerBloque(3);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Fin:
          Resultado.ObjetoCuerpo = null;
          Resultado.Encabezados.Clear();
          Resultado.TipoContenido = eTipoContenido.FinalSeccion;
          break;
      }

      return Resultado;
    }

    /// <summary>
    /// Retorna una nueva copia de un determinado número de encabezado.
    /// </summary>
    /// <param name="numeroEncabezado"></param>
    /// <returns></returns>
    public object ObtenerBloque(int numeroEncabezado)
    {
      switch (numeroEncabezado)
      {
        case 0:
          // encabezado 0 
          return new H01_Encabezado00() { DataContext = Entidad };
        case 1:
          // Pregunta 1 a 8 
          return new H01_Pregunta01_08() { DataContext = Entidad };
        case 2:
          // Pregunta 9 
          return new H01_Pregunta09() { DataContext = Entidad };
        case 3:
          // Pregunta 10 
          return new H01_Pregunta10() { DataContext = Entidad };
      }
      return null;
    }

    /// <summary>
    /// Se debe invocar al inicio de la impresión de la sección.
    /// </summary>
    public void ImpresionIniciar()
    {
      PasoActual = 0;
    }

    /// <summary>
    /// Se debe invocar al iniciar una página.
    /// </summary>
    public void PasarPagina()
    {
      Resultado = new clsInformacionImpresion()
      {
        OrientacionPapel = eOrientacionPapel.Portrait
      };
      Resultado.Encabezados = new List<System.Windows.Controls.UserControl>();
      Resultado.Encabezados.Add(ObtenerBloque(0) as UserControl);
    }

    /// <summary>
    /// Título de la sección.
    /// </summary>
    public string NombreEntidad
    {
      get { return "HOJA 1 DE 4"; }
    }

    #endregion
  }
}
