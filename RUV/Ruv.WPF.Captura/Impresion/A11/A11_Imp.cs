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
  public class A11_Imp : ILogicaImpresion
  {
    #region CONSTRUCTOR

    public A11_Imp()
    { }

    #endregion

    #region VARIABLES

    clsInformacionImpresion Resultado;
    int PasoActual;
    /// <summary>
    /// Usado para saber el registro de una lista que debe devolverse.
    /// </summary>
    int ElementoLista;
    Ruv.Infrastructure.Crosscutting.Common.Entidades.clsAnexo11 Entidad;
    IEnumerable<clsAnexo11_BienInmueble> ListaBienesInmuebles;
    IEnumerable<clsAnexo11_BienMueble> ListaBienesMuebles;
    IEnumerable<clsAnexo11_CreditoPasivo> ListaCreditosPasivos;

    enum ePasosImpresion
    {
      Pregunta01 = 0,
      Pregunta02 = 1,
      Pregunta03 = 2,
      EncabezadoPregunta04_08 = 3,
      DetallePregunta04_08 = 4,
      Pregunta09_12 = 5,
      Pregunta13 = 6,
      EncabezadoPregunta14_17 = 7,
      DetallePregunta14_17 = 8,
      Pregunta18 = 9,
      EncabezadoPregunta18 = 10,
      DetallePregunta18 = 11,
      Fin = 12
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
      Entidad = (fuenteDatos as clsAnexo11);
      if (ListaBienesInmuebles == null) PrepararListas();

      switch ((ePasosImpresion)PasoActual)
      {
        case ePasosImpresion.Pregunta01:
          Resultado.ObjetoCuerpo = ObtenerBloque(1);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Pregunta02:
          Resultado.ObjetoCuerpo = ObtenerBloque(2);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Pregunta03:
          Resultado.ObjetoCuerpo = ObtenerBloque(3);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.EncabezadoPregunta04_08:
          Resultado.ObjetoCuerpo = ObtenerBloque(4);
          Resultado.TipoContenido = eTipoContenido.EncabezadoLista;
          ElementoLista = 0;
          PasoActual++;
          break;

        case ePasosImpresion.DetallePregunta04_08:
          if (!ListaBienesInmuebles.Any())
          {
            // Si no hay datos en la grilla, terminar.
            Resultado.ObjetoCuerpo = null;
            PasoActual++;
          }
          else if (ElementoLista < ListaBienesInmuebles.Count())
          {
            var PA = ListaBienesInmuebles.ElementAt(ElementoLista);
            Resultado.ObjetoCuerpo = ObtenerBloque(5);
            (Resultado.ObjetoCuerpo as UserControl).DataContext = PA;
            Resultado.TipoContenido = eTipoContenido.DetalleLista;

            if (ElementoLista++ == (ListaBienesInmuebles.Count() - 1))
              PasoActual++;
          }
          break;

        case ePasosImpresion.Pregunta09_12:
          Resultado.ObjetoCuerpo = ObtenerBloque(6);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Pregunta13:
          Resultado.ObjetoCuerpo = ObtenerBloque(7);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.EncabezadoPregunta14_17:
          Resultado.ObjetoCuerpo = ObtenerBloque(8);
          Resultado.TipoContenido = eTipoContenido.EncabezadoLista;
          ElementoLista = 0;
          PasoActual++;
          break;

        case ePasosImpresion.DetallePregunta14_17:
          if (!ListaBienesMuebles.Any())
          {
            // Si no hay datos en la grilla, terminar.
            Resultado.ObjetoCuerpo = null;
            PasoActual++;
          }
          else if (ElementoLista < ListaBienesMuebles.Count())
          {
            var PA = ListaBienesMuebles.ElementAt(ElementoLista);
            Resultado.ObjetoCuerpo = ObtenerBloque(9);
            (Resultado.ObjetoCuerpo as UserControl).DataContext = PA;
            Resultado.TipoContenido = eTipoContenido.DetalleLista;

            if (ElementoLista++ == (ListaBienesMuebles.Count() - 1))
              PasoActual++;
          }
          break;

        case ePasosImpresion.Pregunta18:
          Resultado.ObjetoCuerpo = ObtenerBloque(10);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.EncabezadoPregunta18:
          Resultado.ObjetoCuerpo = ObtenerBloque(11);
          Resultado.TipoContenido = eTipoContenido.EncabezadoLista;
          ElementoLista = 0;
          PasoActual++;
          break;

        case ePasosImpresion.DetallePregunta18:
          if (!ListaCreditosPasivos.Any())
          {
            // Si no hay datos en la grilla, terminar.
            Resultado.ObjetoCuerpo = null;
            PasoActual++;
          }
          else if (ElementoLista < ListaCreditosPasivos.Count())
          {
            var PA = ListaCreditosPasivos.ElementAt(ElementoLista);
            Resultado.ObjetoCuerpo = ObtenerBloque(12);
            (Resultado.ObjetoCuerpo as UserControl).DataContext = PA;
            Resultado.TipoContenido = eTipoContenido.DetalleLista;

            if (ElementoLista++ == (ListaCreditosPasivos.Count() - 1))
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
          // Titulo Anexo
          return new A11_Encabezado00() { DataContext = Entidad };
        case 1:
          // Pregunta 1 
          return new A11_Pregunta01() { DataContext = Entidad };
        case 2:
          // Pregunta 2 
          return new A11_Pregunta02() { DataContext = Entidad };
        case 3:
          // Pregunta 3 
          return new A11_Pregunta03() { DataContext = Entidad };
        case 4:
          // Encabezado Pregunta 4 a 8 
          return new A11_EncabezadoPregunta04_08() { DataContext = Entidad };
        case 5:
          // Detalle Pregunta 4 a 8 
          return new A11_DetallePregunta04_08();
        case 6:
          // Pregunta 9 a 12 
          return new A11_Pregunta09_12() { DataContext = Entidad };
        case 7:
          // Pregunta 13 
          return new A11_Pregunta13() { DataContext = Entidad };
        case 8:
          // Encabezado pregunta 14 a 17 
          return new A11_EncabezadoPregunta14_17() { DataContext = Entidad };
        case 9:
          // Detalle pregunta 14 a 17 
          return new A11_DetallePregunta14_17() { DataContext = Entidad };
        case 10:
          // Pregunta 18 
          return new A11_Pregunta18() { DataContext = Entidad };
        case 11:
          // Encabezado Pregunta 18 
          return new A11_EncabezadoPregunta18() { DataContext = Entidad };
        case 12:
          // Detalle Pregunta 18 
          return new A11_DetallePregunta18();
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
    /// Preparar la lista de las víctimas en orden de consecutivo,
    /// iniciando por el jefe de grupo.
    /// Para este anexo se omite el jefe de grupo, que se imprime por aparte.
    /// </summary>
    void PrepararListas()
    {
      ListaBienesInmuebles =
        from vic in Entidad.BienesInmuebles
        where vic.PersonaAfectadaId != Entidad.JefeGrupoFamiliarId
        join per in RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.PersonasAfectadas.ListaPersonas
          on vic.PersonaAfectadaId equals per.ID
        orderby per.NumeroConsecutivo
        select vic;

      ListaBienesMuebles =
        from vic in Entidad.BienesMuebles
        where vic.PersonaAfectadaId != Entidad.JefeGrupoFamiliarId
        join per in RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.PersonasAfectadas.ListaPersonas
          on vic.PersonaAfectadaId equals per.ID
        orderby per.NumeroConsecutivo
        select vic;

      ListaCreditosPasivos =
        from vic in Entidad.CreditosPasivos
        orderby vic.NombreAcreedor
        select vic;
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
      get { return "ANEXO 11"; }
    }

    #endregion
  }
}
