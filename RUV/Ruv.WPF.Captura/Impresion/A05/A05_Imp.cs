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
  public class A05_Imp : ILogicaImpresion
  {
    #region CONSTRUCTOR

    public A05_Imp()
    { }

    #endregion

    #region VARIABLES

    clsInformacionImpresion Resultado;
    int PasoActual;
    /// <summary>
    /// Usado para saber el registro de una lista que debe devolverse.
    /// </summary>
    int ElementoLista;
    Ruv.Infrastructure.Crosscutting.Common.Entidades.clsAnexo05 Entidad;
    IEnumerable<clsAnexo05_Victima> ListaVictimas;

    enum ePasosImpresion
    {
      Pregunta01 = 0,
      Pregunta02_04 = 1,
      Pregunta05_06 = 2,
      Pregunta07_08 = 3,
      EncabezadoPregunta09 = 4,
      DetallePregunta09 = 5,
      Pregunta10_12 = 6,
      Pregunta13_15 = 7,
      Fin = 8
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
      Entidad = (fuenteDatos as clsAnexo05);
      if (ListaVictimas == null) PrepararListaVictimas();

      switch ((ePasosImpresion)PasoActual)
      {
        case ePasosImpresion.Pregunta01:
          Resultado.ObjetoCuerpo = ObtenerBloque(1);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Pregunta02_04:
          Resultado.ObjetoCuerpo = ObtenerBloque(2);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Pregunta05_06:
          Resultado.ObjetoCuerpo = ObtenerBloque(3);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Pregunta07_08:
          Resultado.ObjetoCuerpo = ObtenerBloque(4);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.EncabezadoPregunta09:
          Resultado.ObjetoCuerpo = ObtenerBloque(5);
          Resultado.TipoContenido = eTipoContenido.EncabezadoLista;
          ElementoLista = 0;
          PasoActual++;
          break;

        case ePasosImpresion.DetallePregunta09:
          if (!ListaVictimas.Any())
          {
            // Si no hay datos en la grilla, terminar.
            Resultado.ObjetoCuerpo = null;
            PasoActual++;
          }
          else if (ElementoLista < ListaVictimas.Count())
          {
            // Se envían de a 5 víctimas por renglón.
            var RenglonVictimas = ListaVictimas.Skip(ElementoLista).Take(5).ToList();

            Resultado.ObjetoCuerpo = ObtenerBloque(6);
            (Resultado.ObjetoCuerpo as UserControl).DataContext = RenglonVictimas;
            Resultado.TipoContenido = eTipoContenido.DetalleLista;

            // Impedir que queden cajones vacíos.
            if (RenglonVictimas.Count < 5)
            {
              var Detalle = Resultado.ObjetoCuerpo as A05_DetallePregunta09;
              for (int i = 4; i >= RenglonVictimas.Count; i--)
              {
                Detalle.grdMain.Children.RemoveAt(i * 2);
                Detalle.grdMain.Children.RemoveAt(i * 2);
              }
            }

            if ((ElementoLista += 5) >= (ListaVictimas.Count() - 1))
              PasoActual++;
          }
          break;

        case ePasosImpresion.Pregunta10_12:
          Resultado.ObjetoCuerpo = ObtenerBloque(7);
          Resultado.TipoContenido = eTipoContenido.BloqueIndependiente;
          PasoActual++;
          break;

        case ePasosImpresion.Pregunta13_15:
          Resultado.ObjetoCuerpo = ObtenerBloque(8);
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
          // Titulo Anexo
          return new A05_Encabezado00() { DataContext = Entidad };
        case 1:
          // Pregunta 1
          return new A05_Pregunta01() { DataContext = Entidad };
        case 2:
          // Pregunta 2 a 4
          return new A05_Pregunta02_04() { DataContext = Entidad };
        case 3:
          // Pregunta 5 a 6
          return new A05_Pregunta05_06() { DataContext = Entidad };
        case 4:
          // Pregunta 7 a 8
          return new A05_Pregunta07_08() { DataContext = Entidad };
        case 5:
          // Encabezado Pregunta 9
          return new A05_EncabezadoPregunta09() { DataContext = Entidad };
        case 6:
          // Detalle Pregunta 9
          return new A05_DetallePregunta09() { DataContext = Entidad };
        case 7:
          // Pregunta 10 a 12
          return new A05_Pregunta10_12() { DataContext = Entidad };
        case 8:
          // Pregunta 13 a 15
          return new A05_Pregunta13_15() { DataContext = Entidad };
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
    void PrepararListaVictimas()
    {
      var GrupoFamiliar =
        from vic in Entidad.Victimas
        where vic.PersonaAfectadaId != Entidad.JefeGrupoFamiliarId
        join per in RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.PersonasAfectadas.ListaPersonas
          on vic.PersonaAfectadaId equals per.ID
        orderby per.NumeroConsecutivo
        select vic;

      ListaVictimas = GrupoFamiliar;
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
      get { return "ANEXO 5"; }
    }

    #endregion
  }
}
