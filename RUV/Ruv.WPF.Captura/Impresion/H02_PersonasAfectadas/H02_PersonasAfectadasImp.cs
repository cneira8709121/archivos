using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Ruv.WPF.Captura.Impresion
{
  /// <summary>
  /// Contiene la lógica de impersión para las Hoja02.
  /// </summary>
  public class H02_PersonasAfectadasImp : ILogicaImpresion
  {
    #region CONSTRUCTOR

    public H02_PersonasAfectadasImp()
    { }

    #endregion

    #region VARIABLES

    clsInformacionImpresion Resultado;
    int PasoActual;
    /// <summary>
    /// Usado para saber el registro de una lista que debe devolverse.
    /// </summary>
    int ElementoLista;
    Ruv.Infrastructure.Crosscutting.Common.Entidades.clsPersonasAfectadas Entidad;

    enum ePasosImpresion
    {
      ListaParametros = 0,
      TablaPersonasEncabezado = 1,
      TablaPersonasDetalle = 2,
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
      //var Entidad = entidad as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsPersonasAfectadas;
      Entidad = fuenteDatos as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsPersonasAfectadas;

      switch ((ePasosImpresion)PasoActual)
      {
        case ePasosImpresion.ListaParametros:
          Resultado.ObjetoCuerpo = ObtenerBloque(1);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.TablaPersonasEncabezado:
          Resultado.ObjetoCuerpo = ObtenerBloque(2);
          Resultado.TipoContenido = eTipoContenido.EncabezadoLista;
          ElementoLista = 0;
          PasoActual++;
          break;

        case ePasosImpresion.TablaPersonasDetalle:
          if (!Entidad.ListaPersonas.Any())
          {
            // Si no hay datos en la grilla, terminar.
            Resultado.ObjetoCuerpo = null;
            PasoActual++;
          }
          else if (ElementoLista < Entidad.ListaPersonas.Count)
          {
            var PA = Entidad.ListaPersonas.ElementAt(ElementoLista);
            Resultado.ObjetoCuerpo = new H02_Detalle01();
            (Resultado.ObjetoCuerpo as UserControl).DataContext = PA;
            Resultado.TipoContenido = eTipoContenido.DetalleLista;

            if (ElementoLista++ == (Entidad.ListaPersonas.Count - 1))
              PasoActual++;
          }
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
          return new H02_Encabezado00();
        case 1:
          return new H02_Encabezado01();
        case 2:
          return new H02_Encabezado02();
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
        OrientacionPapel = eOrientacionPapel.Landscape
      };
      Resultado.Encabezados = new List<System.Windows.Controls.UserControl>();
      Resultado.Encabezados.Add(new H02_Encabezado00());
    }

    /// <summary>
    /// Título de la sección.
    /// </summary>
    public string NombreEntidad
    {
      get { return "HOJA 2 DE 4"; }
    }

    #endregion
  }
}
