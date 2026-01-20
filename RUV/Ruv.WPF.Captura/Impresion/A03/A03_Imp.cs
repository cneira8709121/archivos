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
  public class A03_Imp : ILogicaImpresion
  {
    #region CONSTRUCTOR

    public A03_Imp()
    { }

    #endregion

    #region VARIABLES

    clsInformacionImpresion Resultado;
    int PasoActual;
    /// <summary>
    /// Usado para saber el registro de una lista que debe devolverse.
    /// </summary>
    int ElementoLista;
    Ruv.Infrastructure.Crosscutting.Common.Entidades.clsAnexo03 Entidad;
    IEnumerable<clsAnexo03_Victima> ListaVictimas;

    enum ePasosImpresion
    {
      Pregunta1 = 0,
      EncabezadoPregunta2 = 1,
      DetallePregunta2 = 2,
      Pregunta3a8 = 3,
      EncabezadoPregunta9a14 = 4,
      DetallePregunta9a14 = 5,
      Fin = 6
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
      Entidad = (fuenteDatos as clsAnexo03);
      if (ListaVictimas == null) PrepararListaVictimas();

      switch ((ePasosImpresion)PasoActual)
      {
        case ePasosImpresion.Pregunta1:
          Resultado.ObjetoCuerpo = ObtenerBloque(1);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.EncabezadoPregunta2:
          Resultado.ObjetoCuerpo = ObtenerBloque(2);
          Resultado.TipoContenido = eTipoContenido.EncabezadoLista;
          ElementoLista = 0;
          PasoActual++;
          break;

        case ePasosImpresion.DetallePregunta2:
          if (!Entidad.NiñosNacidosPorAbusoSexual.Any())
          {
            // Si no hay datos en la grilla, terminar.
            Resultado.ObjetoCuerpo = null;
            PasoActual++;
          }
          else if (ElementoLista < Entidad.NiñosNacidosPorAbusoSexual.Count())
          {
            var PA = Entidad.NiñosNacidosPorAbusoSexual.ElementAt(ElementoLista);
            Resultado.ObjetoCuerpo = ObtenerBloque(3);
            (Resultado.ObjetoCuerpo as UserControl).DataContext = PA;
            Resultado.TipoContenido = eTipoContenido.DetalleLista;

            if (ElementoLista++ == (Entidad.NiñosNacidosPorAbusoSexual.Count() - 1))
              PasoActual++;
          }
          break;

        case ePasosImpresion.Pregunta3a8:
          Resultado.ObjetoCuerpo = ObtenerBloque(4);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          ElementoLista = 0;
          PasoActual++;
          break;

        case ePasosImpresion.EncabezadoPregunta9a14:
          Resultado.ObjetoCuerpo = ObtenerBloque(5);
          Resultado.TipoContenido = eTipoContenido.EncabezadoLista;
          ElementoLista = 0;
          PasoActual++;
          break;

        case ePasosImpresion.DetallePregunta9a14:
          if (!ListaVictimas.Any())
          {
            // Si no hay datos en la grilla, terminar.
            Resultado.ObjetoCuerpo = null;
            PasoActual++;
          }
          else if (ElementoLista < ListaVictimas.Count())
          {
            var PA = ListaVictimas.ElementAt(ElementoLista);
            Resultado.ObjetoCuerpo = ObtenerBloque(6);
            (Resultado.ObjetoCuerpo as UserControl).DataContext = PA;
            Resultado.TipoContenido = eTipoContenido.DetalleLista;

            if (ElementoLista++ == (ListaVictimas.Count() - 1))
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
          return new A03_Encabezado00() { DataContext = Entidad };
        case 1:
          // Pregunta 1
          return new A03_Encabezado01() { DataContext = Entidad };
        case 2:
          // Encabezado pregunta 2
          return new A03_Encabezado02() { DataContext = Entidad };
        case 3:
          // Detalle pregunta 2
          return new A03_Detalle01();
        case 4:
          // Preguntas 3  a 8
          return new A03_Encabezado03() { DataContext = Entidad };
        case 5:
          // Encabezado preguntas 9 a 14
          return new A03_Encabezado04() { DataContext = Entidad };
        case 6:
          // Detalle preguntas 9 a 14
          return new A03_Detalle02();

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
    ///  Preparar la lista de las víctimas en orden de consecutivo,
    /// iniciando por el jefe de grupo.
    /// </summary>
    void PrepararListaVictimas()
    {
      var GrupoFamiliar =
        from vic in Entidad.Victimas
        where vic.PersonaAfectadaId != Entidad.JefeGrupoFamiliarId
        join per in RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.PersonasAfectadas.ListaPersonas
          on vic.PersonaAfectadaId equals per.ID
        orderby per.NumeroConsecutivo
        select vic;

      var JefeGrupo = Entidad.Victimas.Where(x => x.PersonaAfectadaId == Entidad.JefeGrupoFamiliarId);

      ListaVictimas = JefeGrupo.Concat(GrupoFamiliar);
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
      Resultado.Encabezados.Add(ObtenerBloque(0) as UserControl);
    }

    /// <summary>
    /// Título de la sección.
    /// </summary>
    public string NombreEntidad
    {
      get { return "ANEXO 3"; }
    }

    #endregion
  }
}
