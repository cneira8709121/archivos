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
using System.Windows.Shapes;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
  /// <summary>
  /// Lógica de interacción para ObtenerDeclaracion.xaml
  /// </summary>
  public partial class ObtenerDeclaracion : Window
  {
    #region CONSTRUCTOR

    private static ObtenerDeclaracion instance = null;

    public static ObtenerDeclaracion GetInstance()
    {
        if (instance == null)
            instance = new ObtenerDeclaracion();

        return instance;
    }

    private ObtenerDeclaracion()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(ObtenerDeclaracion_Loaded);
      this.Closing += new System.ComponentModel.CancelEventHandler(ObtenerDeclaracion_Closing);

    }

    public bool Buscando { private set; get; }

    void ObtenerDeclaracion_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (!this.Buscando)
        {
            e.Cancel = true;
            this.Hide();
        }
        else
        {
            instance = null;
            RUV.I.MultiTarea.DetenerBackground();
        }
    }

    void ObtenerDeclaracion_Loaded(object sender, RoutedEventArgs e)
    {
      LimpiarBusqueda(null, null);
    }

    #endregion

    #region PROPIEDADES

    private clsBusquedaDeclaracion _BusquedaDeclaracion;
    /// <summary>
    /// Los parámetros de búsqueda de la declaración.
    /// </summary>
    public clsBusquedaDeclaracion BusquedaDeclaracion
    {
      get { return _BusquedaDeclaracion; }
      set { _BusquedaDeclaracion = value; }
    }

    List<clsBusquedaDeclaracion> ListaBusqueda;

    private int? _IdDeclaracionSeleccionada;
    /// <summary>
    /// El Id de la declaración seleccionada.
    /// </summary>
    public int? IdDeclaracionSeleccionada
    {
      get { return _IdDeclaracionSeleccionada; }
      set
      {
        _IdDeclaracionSeleccionada = value;
      }
    }

    #endregion

    #region LIMPIAR BÚSQUEDA

    /// <summary>
    /// Limpiar lo ingresado en la búsqueda de la declaración.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LimpiarBusqueda(object sender, RoutedEventArgs e)
    {
      BusquedaDeclaracion = new clsBusquedaDeclaracion();
      DataContext = BusquedaDeclaracion;
      lbxResultado.ItemsSource = null;
      this.tbxCodigoDeclaracion.Focus();
      tbHoraUltimaBusqueda.Visibility = System.Windows.Visibility.Hidden;
    }

    #endregion

    #region CERRAR ESTA VENTANA

    /// <summary>
    /// Cierra esta ventana.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Cerrar(object sender, RoutedEventArgs e)
    {
      IdDeclaracionSeleccionada = null;
      if (!this.Buscando)
          this.Hide();
      else
          this.Close();
    }

    #endregion

    #region BUSCAR DECLARACIONES

    private void Buscar(object sender, RoutedEventArgs e)
    {
      Buscando = true;
      if (RUV.I.Red.EstadoRed != eEstadoRed.Disponible)
      {
        RUV.I.UIPrincipal.ReportarInformacionDeUsuario("Esta función sólo está disponible cuando exista conexión.");
        return;
      }

      if (BusquedaDeclaracion.DeclarantePrimerNombre!= null && BusquedaDeclaracion.DeclarantePrimerNombre.Length > 0 && BusquedaDeclaracion.DeclarantePrimerNombre.Length < 3)
      {
          RUV.I.UIPrincipal.ReportarInformacionDeUsuario("Debe digitar al menos 3 caracteres para realizar la búsqueda por primer nombre.");
          return;
      }

      if (BusquedaDeclaracion.DeclaranteDemasNombres != null && BusquedaDeclaracion.DeclaranteDemasNombres.Length > 0 && BusquedaDeclaracion.DeclaranteDemasNombres.Length < 3)
      {
          RUV.I.UIPrincipal.ReportarInformacionDeUsuario("Debe digitar al menos 3 caracteres para realizar la búsqueda por segundo nombre.");
          return;
      }

      if (BusquedaDeclaracion.DeclarantePrimerApellido != null && BusquedaDeclaracion.DeclarantePrimerApellido.Length > 0 && BusquedaDeclaracion.DeclarantePrimerApellido.Length < 3)
      {
          RUV.I.UIPrincipal.ReportarInformacionDeUsuario("Debe digitar al menos 3 caracteres para realizar la búsqueda por primer apellido.");
          return;
      }

      if (BusquedaDeclaracion.DeclaranteSegundoApellido != null && BusquedaDeclaracion.DeclaranteSegundoApellido.Length > 0 && BusquedaDeclaracion.DeclaranteSegundoApellido.Length < 3)
      {
          RUV.I.UIPrincipal.ReportarInformacionDeUsuario("Debe digitar al menos 3 caracteres para realizar la búsqueda por segundo apellido.");
          return;
      }

      lbxResultado.ItemsSource = null;

      ControlBloqueoInterfase.TextoBloqueo = "Buscando";

      RUV.I.MultiTarea.EjecutarEnBackground((() =>
        ListaBusqueda = RUV.I.Red.ServicioGeneral.BuscarDeclaracion(
        BusquedaDeclaracion, RUV.I.Seguridad.LlaveUsuario).ToList()
        ),
        (() => FinBusqueda()));

    }

    void FinBusqueda()
    {
        Buscando = false;
        if (ListaBusqueda != null)
        {
            if (!ListaBusqueda.Any())
            {
                lbxResultado.ItemsSource = null;
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario("No se encontraron declaraciones");
                tbHoraUltimaBusqueda.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                lbxResultado.ItemsSource = ListaBusqueda;
                tbHoraUltimaBusqueda.Visibility = System.Windows.Visibility.Visible;
                tbHoraUltimaBusqueda.Text = "Hora búsqueda: " + DateTime.Now.ToString("h:mm tt");
            }
        }

      ControlBloqueoInterfase.TextoBloqueo = null;
    }

    #endregion

    #region OBTENER LA DECLARACIÓN

    /// <summary>
    /// Selección al hacer doble click sobre la lista de las declaraciones.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SeleccionarLista(object sender, MouseButtonEventArgs e)
    {
      Obtener(null, null);
    }

    /// <summary>
    /// Carga la declaración solicitada.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Obtener(object sender, RoutedEventArgs e)
    {
      var Seleccion = lbxResultado.SelectedItem as clsBusquedaDeclaracion;
      if (Seleccion == null)
      {
        RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
          "Primero debe seleccionar la declaración,\no hacer doble-click sobre la deseada.");
        return;
      }
      IdDeclaracionSeleccionada = Seleccion.Id;
      //this.Close();
      this.Hide();
    }

    #endregion


  }
}
