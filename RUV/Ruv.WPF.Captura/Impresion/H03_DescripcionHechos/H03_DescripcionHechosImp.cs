using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Ruv.WPF.Captura.Impresion
{
  /// <summary>
  /// Contiene la lógica de impersión para la Hoja03.
  /// </summary>
  public class H03_DescripcionHechosImp : ILogicaImpresion
  {
    #region CONSTRUCTOR

    public H03_DescripcionHechosImp()
    { }

    #endregion

    #region VARIABLES

    clsInformacionImpresion Resultado;
    int PasoActual;
    Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDescripcionHechos Entidad;

    /// <summary>
    /// Usado para saber el registro de una lista que debe devolverse.
    /// </summary>

    enum ePasosImpresion
    {
      TextoPregunta = 0,
      NarracionHechos = 1,
      Fin = 2
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
      //var Entidad = entidad as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsPersonasAfectadas;
      Entidad = fuenteDatos as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDescripcionHechos;

      switch ((ePasosImpresion)PasoActual)
      {
        case ePasosImpresion.TextoPregunta:
          Resultado.ObjetoCuerpo = ObtenerBloque(1);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.NarracionHechos:
          Resultado.ObjetoCuerpo = ObtenerBloque(2);
          Resultado.TipoContenido = eTipoContenido.TextoLargo;
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
    /// Retorna una nueva copia de un determinado objeto.
    /// </summary>
    /// <param name="numeroEncabezado"></param>
    /// <returns></returns>
    public object ObtenerBloque(int numeroEncabezado)
    {
      switch (numeroEncabezado)
      {
        case 0:
          return new H03_Encabezado00();
        case 1:
          return new H03_TextoPregunta01();
        case 2:
          return Entidad.Narracion;
      }
      return null;
    }

    /// <summary>
    /// Se debe invocar al inicio de la impresión de la sección.
    /// </summary>
    public void ImpresionIniciar()
    {
      PasoActual = 0;
      Resultado = new clsInformacionImpresion()
      {
        OrientacionPapel = eOrientacionPapel.Portrait
      };
      Resultado.Encabezados = new List<System.Windows.Controls.UserControl>();
      Resultado.Encabezados.Add(new H03_Encabezado00());
    }

    /// <summary>
    /// Se debe invocar al iniciar una página.
    /// </summary>
    public void PasarPagina()
    {
      Resultado.Encabezados.Clear();
      Resultado.Encabezados.Add(new H03_Encabezado00());
    }

    /// <summary>
    /// Título de la sección.
    /// </summary>
    public string NombreEntidad
    {
      get { return "HOJA 3 DE 4"; }
    }

    #endregion

  }
}
