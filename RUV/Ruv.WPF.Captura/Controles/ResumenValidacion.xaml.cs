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
using System.ComponentModel;

namespace Ruv.WPF.Captura.Controles
{
  public partial class ResumenValidacion : UserControl, INotifyPropertyChanged
  {
    #region CONSTRUCTOR & CARGUE

    public ResumenValidacion()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(ValidationSummary_Loaded);
    }

    /// <summary>
    /// En la carga me suscribo a cambios en la validación.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ValidationSummary_Loaded(object sender, RoutedEventArgs e)
    {
      //this.Visibility = System.Windows.Visibility.Collapsed;
      if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
        return;
      Validation.AddErrorHandler(Contenedor, ErrorEvento);
    }

    /// <summary>
    /// Se produce cuando hay cambios en los errores.s
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ErrorEvento(object sender, EventArgs e)
    {
      Validar();
      ReportarCambioPropiedad("ContenedorEsValido");

    }

    public event EventHandler CambioEnReporteError;

    #endregion

    #region NAVEGACION SOBRE LOS ERRORES

    /// <summary>
    /// El error sobre el que se solicito la última navegación.
    /// </summary>
    int? NavegacionActual;

    /// <summary>
    /// Verdadero: existen errores sobre los que se pueden navegar.
    /// </summary>
    public bool SePuedeNavegar
    {
      get
      {
        return ChildrenWithValidationErrors != null && ChildrenWithValidationErrors.Any();
      }
    }

    /// <summary>
    /// Selecciona el siguiente error.
    /// </summary>
    public void NavegarAdelante()
    {
      if (!SePuedeNavegar) return;

      if (NavegacionActual == null)
        NavegacionActual = 0;
      else
      {
        NavegacionActual++;
        if (NavegacionActual == ChildrenWithValidationErrors.Count())
          NavegacionActual = 0;
      }

      SeleccionarLink(ChildrenWithValidationErrors[NavegacionActual.Value]);
    }

    /// <summary>
    /// Selecciona el error anterior.
    /// </summary>
    public void NavegarAtras()
    {
      if (!SePuedeNavegar) return;

      if (NavegacionActual == null)
        NavegacionActual = ChildrenWithValidationErrors.Count() - 1;
      else
      {
        NavegacionActual--;
        if (NavegacionActual == -1)
          NavegacionActual = ChildrenWithValidationErrors.Count() - 1;
      }

      SeleccionarLink(ChildrenWithValidationErrors[NavegacionActual.Value]);
    }

    #endregion

    #region VALIDACIÓN

    /// <summary>
    /// Lista temporal de los controles que reportan un error de validación.
    /// </summary>
    List<FrameworkElementItem> ChildrenWithValidationErrors = null;


    public bool HasErrors
    {
        get
        {
            return ChildrenWithValidationErrors.Any();
        }
    }
    /// <summary>
    /// Ejecuta la validación manualmente.
    /// </summary>
    public void Validar()
    {
      NavegacionActual = null;

      clsUIHelper UI = new clsUIHelper();
      BindingSource = Contenedor.DataContext;
      ChildrenWithValidationErrors = UI.GetChildren(Contenedor, CriterioBusqueda);

      lbxErrores.ItemsSource = ChildrenWithValidationErrors;
      if (ChildrenWithValidationErrors.Any())
        this.Visibility = System.Windows.Visibility.Visible;
      else
        this.Visibility = System.Windows.Visibility.Collapsed;

      if (CambioEnReporteError != null)
        CambioEnReporteError(this, null);
    }

    /// <summary>
    /// El objeto que se considera el DataContext del contenedor.
    /// </summary>
    object BindingSource;

    /// <summary>
    /// Criterio de búsqueda para encontrar sub-controles 
    /// </summary>
    /// <param name="child"></param>
    /// <returns></returns>
    FrameworkElementItem CriterioBusqueda(DependencyObject child)
    {
      FrameworkElementItem Resultado = null;

      List<DependencyProperty> DP = new List<DependencyProperty>();
      FrameworkElement FE = child as FrameworkElement;
      object Contexto = null;
      if (FE != null)
        Contexto = FE.DataContext;

      //if ((child as TextBlock) != null && (child as TextBlock).Name == "A11_FechaContrajoObligacion") System.Diagnostics.Debugger.Break();
      
      // Determinar si el control tiene propiedades que deben ser 
      // motivo de validación.
      if (child is EntradaNumerica)
          DP.Add(EntradaNumerica.ValorProperty);
      else if (child is EntradaDouble)
          DP.Add(EntradaDouble.ValorProperty);
      else if (child is TextBox)
        DP.Add(TextBox.TextProperty);
      else if (child is ComboBox)
        DP.Add(ComboBox.SelectedValueProperty);
      else if (child is ListBox)
        DP.Add(ListBox.ItemsSourceProperty);
      else if (child is Ruv.WPF.Captura.Impresion.DatoSiNoColumna)
        DP.Add(Ruv.WPF.Captura.Impresion.DatoSiNoColumna.ValorProperty);
      else if (child is Ruv.WPF.Captura.Registro.Secciones.CajaIngresoFecha)
        DP.Add(Ruv.WPF.Captura.Registro.Secciones.CajaIngresoFecha.FechaProperty);
      else if (child is DummyControl)
        DP.Add(DummyControl.FuenteDeDatosProperty);
      else if (child is DatePicker)
          DP.Add(DatePicker.SelectedDateProperty);
      else if (child is TextBlock)
          DP.Add(TextBlock.TextProperty);
      else if (child is Ruv.WPF.Captura.Registro.Secciones.ListaOpciones)
      {
          DP.Add(Ruv.WPF.Captura.Registro.Secciones.ListaOpciones.ValorSeleccionUnicaProperty);
          DP.Add(Ruv.WPF.Captura.Registro.Secciones.ListaOpciones.ValoresUsuarioProperty);
      }
      else if (child is Geografia)
      {
          DP.Add(Geografia.DepartamentoIdProperty);
          DP.Add(Geografia.MunicipioIdProperty);
      }


      foreach (var OneDP in DP)
      {
        if (OneDP != null)
        {
          Binding Bin = BindingOperations.GetBinding(child, OneDP);

          if (Bin != null)
          {
            foreach (var item in Validation.GetErrors(child))
              if (item.ErrorContent != null)
              {
                string Descripcion = item.ErrorContent.ToString();
                if (item.RuleInError.ValidationStep == ValidationStep.ConvertedProposedValue)
                {
                  if (child is TextBox)
                    Descripcion = string.Format("El valor en este campo no es válido",
                      (child as TextBox).Text);
                }

                Resultado = new FrameworkElementItem
                     {
                       SourceControl = FE,
                       Description = Descripcion
                     };

                return Resultado;
              }
          }
        }
      }

      return Resultado;
    }

    #endregion

    #region PROPIEDADES

    /// <summary>
    /// El control que tiene los controles que se van a validar.
    /// </summary>
    public FrameworkElement Contenedor
    {
      get { return (FrameworkElement)GetValue(ContenedorProperty); }
      set { SetValue(ContenedorProperty, value); }
    }

    public static readonly DependencyProperty ContenedorProperty =
        DependencyProperty.Register("Contenedor", typeof(FrameworkElement),
        typeof(ResumenValidacion), new UIPropertyMetadata(null));

    /// <summary>
    /// Verdadero: No hay errores en los controles que se validan.
    /// </summary>
    public Boolean ContenedorEsValido
    {
      set { }
      get
      {
        if (ChildrenWithValidationErrors == null)
          Validar();
        return !ChildrenWithValidationErrors.Any();
      }
    }

    private FocusAdorner _AdornoFoco;
    /// <summary>
    /// El control encargado de mostrar el foco.
    /// </summary>
    public FocusAdorner AdornoFoco
    {
      get { return _AdornoFoco; }
      set { _AdornoFoco = value; }
    }



    #endregion

    #region SELECCIONAR UN ERROR

    /// <summary>
    /// Sirve para determinar si se muestra el mensaje recordando que se puede 
    /// hacer click sobre un error para buscar su origen.
    /// </summary>
    Boolean ClickPorPresionar = true;

    /// <summary>
    /// Se solicita pasar el foco al error.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Hyperlink_Click(object sender, RoutedEventArgs e)
    {
      if (ClickPorPresionar)
      {
        ClickPorPresionar = false;
        txtTitulo.Text = "Datos pendientes de corregir";
      }

      var item = ((e.Source as Hyperlink).DataContext as FrameworkElementItem);
      SeleccionarLink(item);
    }

    /// <summary>
    /// Procede a seleccionar el control que deslpiega el error.
    /// </summary>
    /// <param name="item"></param>
    void SeleccionarLink(FrameworkElementItem item)
    {
      if (item == null) return;

      // Si existe el adorner, utilizarlo.
      if (AdornoFoco != null)
      {
        AdornoFoco.MostrarFoco(item.SourceControl);
      }
      else
      {
        item.SourceControl.Focus();
        if (item.SourceControl is TextBox)
          (item.SourceControl as TextBox).SelectAll();
      }

      ChildrenWithValidationErrors.ForEach(x => x.Seleccionado = false);
      item.Seleccionado = true;
    }

    #endregion

    #region INotifyPropertyChanged

    public void ReportarCambioPropiedad(string nombrePropiedad)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}
