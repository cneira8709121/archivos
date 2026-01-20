using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace Ruv.WPF.Captura.Impresion
{
  /// <summary>
  /// Contiene la lógica de impersión para las Hoja04.
  /// </summary>
  public class H04_VerificacionProdedimientoImp : ILogicaImpresion
  {
    #region CONSTRUCTOR

    public H04_VerificacionProdedimientoImp()
    { }

    #endregion

    #region VARIABLES

    clsInformacionImpresion Resultado;
    int PasoActual;
    Ruv.Infrastructure.Crosscutting.Common.Entidades.clsVerificacionProcedimiento Entidad;

    enum ePasosImpresion
    {
      Pregunta25 = 0,
      Texto25 = 1,
      Pregunta26 = 2,
      Texto32 = 3,
      Pregunta33 = 4,
      Fin = 5
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
      Entidad = fuenteDatos as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsVerificacionProcedimiento;

      switch ((ePasosImpresion)PasoActual)
      {
        case ePasosImpresion.Pregunta25:
          Resultado.ObjetoCuerpo = ObtenerBloque(1);
          Resultado.Encabezados.Clear();
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Texto25:
          Resultado.ObjetoCuerpo = ObtenerBloque(2);
          Resultado.Encabezados.Clear();
          Resultado.TipoContenido = eTipoContenido.TextoLargo;
          PasoActual++;
          break;

        case ePasosImpresion.Pregunta26:
          // Calcular el número de anexos.
          var ConteoAnexos = RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.TodosLosAnexos
            .Count();
          RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.VerificacionProcedimiento
            .NumeroTotalAnexos = ConteoAnexos;

          // Calcular el número total de folios.
          var TotalFolios =
            RUV.I.Configuraciones.Impresion.PaginaActualGenerando +
            Entidad.NumeroTotalSoportes;
          Entidad.NumeroTotalFolios = TotalFolios;

          Resultado.ObjetoCuerpo = ObtenerBloque(3);
          Resultado.Encabezados.Clear();
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Texto32:
          Resultado.ObjetoCuerpo = ObtenerBloque(4);
          Resultado.Encabezados.Clear();
          Resultado.TipoContenido = eTipoContenido.TextoLargo;
          PasoActual++;
          break;

        case ePasosImpresion.Pregunta33:
          Resultado.ObjetoCuerpo = ObtenerBloque(5);
          Resultado.Encabezados.Clear();
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
          return new H04_Encabezado00();
        case 1:
          return new H04_Bloque01() { DataContext = Entidad };
        case 2:
          return Entidad.EnmendarDeclaracionTexto;
        case 3:
          return new H04_Bloque03() { DataContext = Entidad };
        case 4:
          return Entidad.ObservacionesSobreDiligenciamiento;
        case 5:
          return new H04_Bloque05() { DataContext = Entidad };
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
      Resultado.Encabezados.Add(ObtenerBloque(0) as UserControl);
    }

    /// <summary>
    /// Se debe invocar al iniciar una página.
    /// </summary>
    public void PasarPagina()
    {
      Resultado.Encabezados.Clear();
      Resultado.Encabezados.Add(ObtenerBloque(0) as UserControl);
    }

    /// <summary>
    /// Título de la sección.
    /// </summary>
    public string NombreEntidad
    {
      get { return "HOJA 4 DE 4"; }
    }

    #endregion

    #region CAMBIAR EL TAMAÑO DE LOS TEXTOS.

    void CambiarTamañoTextos(DependencyObject contenedor)
    {
      double Incremento = 2.5d;
      var uie = contenedor as UIElement;
      uie.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
      uie.Arrange(new Rect(uie.DesiredSize));

      clsUIHelper UI = new clsUIHelper();
      var Textos = UI.GetChildren(contenedor, CriterioTextos);

      foreach (var item in Textos)
      {
        if (item.SourceControl is TextBlock)
        {
          var TB = item.SourceControl as TextBlock;
          TB.FontSize = TB.FontSize * Incremento;
        }
      }

      uie.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
      uie.Arrange(new Rect(uie.DesiredSize));

    }

    FrameworkElementItem CriterioTextos(DependencyObject child)
    {
      FrameworkElementItem Resultado = null;
      if (child is TextBlock || child is TextBox)
      {
        Resultado = new FrameworkElementItem()
        {
          SourceControl = child as FrameworkElement
        };
      }
      return Resultado;
    }

    #endregion



  }
}
