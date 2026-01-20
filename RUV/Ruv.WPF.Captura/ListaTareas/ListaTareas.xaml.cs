using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
//using Ruv.WPF.Captura.Infrastructure;

namespace Ruv.WPF.Captura.ListaTareas
{
  /// <summary>
  /// Lógica de interacción para ListaTareas.xaml
  /// </summary>
  public partial class ListaTareas : Page
  {
    #region CONSTRUCTOR

    public ListaTareas()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(ListaTareas_Loaded);
    }

    void ListaTareas_Loaded(object sender, RoutedEventArgs e)
    {
      // Sipod.I.Red.ServicioGeneral.
      // dataGrid1.ItemsSource=Lista
      //var Seleccionado = dataGrid1.SelectedValue as TipoObjeto;


      LlenarListaAnexos();

    }
    #endregion


    #region ARMAR LISTA DE TAREAS

    /// <summary>
    /// Llena la lista de los anexos.
    /// </summary>
    void LlenarListaAnexos()
    {

      clsListaTareas[] ListaTareas = null;

      try
      {
        ListaTareas = RUV.I.Red.ServicioGeneral.ObtenerListaTareas(
          RUV.I.Usuario.Id,
          RUV.I.Seguridad.LlaveUsuario,null,null,null,null,null);
        this.dataGrid1.ItemsSource = ListaTareas;
      }
      catch (Exception ex)
      {
        string Mensaje = "No se pudo realizar la transmisión.\n" + ex.Message;
        RUV.I.Log.Registrar("Lista de tareas", ex);
        RUV.I.UIPrincipal.ReportarErrorDeUsuario(Mensaje);
        //throw new Exception(Mensaje);
      }

    }

    #endregion

    #region VER TAREAS

    private void VerDeclaracion(object sender, RoutedEventArgs e)
    {
      int ID = (int)((Button)sender).CommandParameter;

      var Tarea = dataGrid1.SelectedItem as clsListaTareas;

      Ruv.WPF.Captura.GeneralService.clsResultado Resultado = null;

      Resultado = RUV.I.Red.ServicioGeneral.RadicacionActualizarEstado(
        ID,
        (int)eEstadoDeclaracion.RadicadoPendienteCaptura,
        RUV.I.Seguridad.LlaveUsuario, -1);

      if (!Resultado.ErroresDB.Any())
      {
        var Decla = new clsDeclaracion()
        {
          RadicacionId = ID,
          DeclaracionNumero = Tarea.Formulario
        };
        Ruv.WPF.Captura.Registro.RegistroVista RV = new Registro.RegistroVista(Decla);
        NavigationService.Navigate(RV);
        return;
      }
      else
      {
        var Ven = new Ruv.WPF.Captura.Registro.Secciones.Controles.ReporteEnvioDeclaracion(Resultado);
        Ven.ShowDialog();
      }

    }

    #endregion

  }
}
