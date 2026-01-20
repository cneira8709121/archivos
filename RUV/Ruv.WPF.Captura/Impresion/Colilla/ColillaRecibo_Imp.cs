using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Impresion
{
  /// <summary>
  /// Contiene la lógica de impersión para el ColillaRecibo_Imp.
  /// </summary>
  public class ColillaRecibo_Imp : ILogicaImpresion
  {
    #region CONSTRUCTOR

    public ColillaRecibo_Imp()
    { }

    #endregion

    #region VARIABLES

    clsInformacionImpresion Resultado;
    int PasoActual;
    Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDeclaracion Entidad;

    enum ePasosImpresion
    {
      Bloque01 = 0,
      Fin = 1
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
      Entidad = (fuenteDatos as clsDeclaracion);

      switch ((ePasosImpresion)PasoActual)
      {
        case ePasosImpresion.Bloque01:
          Resultado.ObjetoCuerpo = ObtenerBloque(1);
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
          // Bloque 00.
          return new ColillaRecibo_Bloque00() { DataContext = Entidad }; ;
        case 1:
          // Bloque 01.
          return new ColillaRecibo_Bloque01() { DataContext = Entidad };
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
      get { return "COLILLA RECIBO"; }
    }

    #endregion
  }
}
