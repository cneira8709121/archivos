using System;
using System.Linq;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using Ruv.WPF.Captura.Controles;
using System.Windows.Media;

namespace Ruv.WPF.Captura.Registro.Secciones
{
  /// <summary>
  /// Lógica de interacción para CajaIngreso.xaml
  /// </summary>
  public partial class CajaIngresoFecha : UserControl, INotifyPropertyChanged
  {

    #region CONSTRUCTOR

    public CajaIngresoFecha()
    {
      InitializeComponent();

      // Establecer los bindings manualmente.
      Extensiones.BindingEstablecer(this, "Dia", tbxDia, EntradaNumerica.ValorProperty, BindingMode.TwoWay, null);
      Extensiones.BindingEstablecer(this, "Mes", tbxMes, EntradaNumerica.ValorProperty, BindingMode.TwoWay, null);
      Extensiones.BindingEstablecer(this, "Año", tbxAño, EntradaNumerica.ValorProperty, BindingMode.TwoWay, null);
    }

    #endregion

    bool CambioManual;

    #region PROPIEDAD DE FECHA

    /// <summary>
    /// Si los datos ingresados para la fecha están completos y la fecha está correcta, 
    /// esta se retorna aqui, de lo contrario se retorna null.
    /// </summary>
    public DateTime? Fecha
    {
      get { return (DateTime?)GetValue(FechaProperty); }
      set { SetValue(FechaProperty, value); }
    }

    public static readonly DependencyProperty FechaProperty =
        DependencyProperty.Register("Fecha", typeof(DateTime?),
        typeof(CajaIngresoFecha), new UIPropertyMetadata(null, FechaPropertyChanged));

    static void FechaPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CajaIngresoFecha Este = d as CajaIngresoFecha;

      if (Este.CambioManual) return;

      DateTime? NuevoValor = (DateTime?)e.NewValue;

      Este.CambioManual = true;
      if (NuevoValor.HasValue)
      {
        Este.Año = NuevoValor.Value.Year;
        Este.Mes = NuevoValor.Value.Month;
        Este.Dia = NuevoValor.Value.Day;
      }
      else
      {
        Este.Año = null;
        Este.Mes = null;
        Este.Dia = null;
      }
      Este.CambioManual = false;
    }

    #endregion

    #region COMPONENTES DE LA FECHA

    private int? _Año;
    public int? Año
    {
      get { return _Año; }
      set
      {
        _Año = value;

        if (PropertyChanged != null)
          PropertyChanged(this, new PropertyChangedEventArgs("Año"));
        if (!CambioManual) ReportarCambioEnPropiedad();
      }
    }

    private int? _Mes;
    public int? Mes
    {
      get { return _Mes; }
      set
      {
        _Mes = value;
        if (PropertyChanged != null)
          PropertyChanged(this, new PropertyChangedEventArgs("Mes"));
        if (!CambioManual) ReportarCambioEnPropiedad();
      }
    }

    private int? _Dia;
    public int? Dia
    {
      get { return _Dia; }
      set
      {
        _Dia = value;
        if (PropertyChanged != null)
          PropertyChanged(this, new PropertyChangedEventArgs("Dia"));
        if (!CambioManual) ReportarCambioEnPropiedad();
      }
    }

    void ReportarCambioEnPropiedad()
    {
      CambioManual = true;
      DateTime F;

      if (Año.HasValue && Mes.HasValue && Dia.HasValue)
      {
        try
        {
          F = new DateTime(Año.Value, Mes.Value, Dia.Value);
          Fecha = F;
        }
        catch
        {
          Fecha = null;
        }
      }
      else
        Fecha = null;
      CambioManual = false;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region TAMAÑO DEL TEXTO

    /// <summary>
    /// El tamaño del texto.
    /// </summary>
    public double? TamañoTexto
    {
      get { return (double?)GetValue(TamañoTextoProperty); }
      set { SetValue(TamañoTextoProperty, value); }
    }

    public static readonly DependencyProperty TamañoTextoProperty =
        DependencyProperty.Register("TamañoTexto", typeof(double?),
        typeof(CajaIngresoFecha), new UIPropertyMetadata(null, TamañoTextoChanged));

    static void TamañoTextoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var Valor = (double?)e.NewValue;

      if (!Valor.HasValue) return;

      var STE = d as CajaIngresoFecha;

      STE.grdMain.Children.OfType<TextBlock>().ToList()
        .ForEach(x => x.FontSize = Valor.Value);

      STE.grdMain.Children.OfType<EntradaNumerica>().ToList()
        .ForEach(x => x.FontSize = Valor.Value);

    }


    #endregion

    #region COLOR DEL TEXTO

    /// <summary>
    /// Color del texto de entrada.
    /// </summary>
    public SolidColorBrush ColorTexto
    {
      get { return (SolidColorBrush)GetValue(ColorTextoProperty); }
      set { SetValue(ColorTextoProperty, value); }
    }

    public static readonly DependencyProperty ColorTextoProperty =
        DependencyProperty.Register("ColorTexto", typeof(SolidColorBrush),
        typeof(CajaIngresoFecha), new UIPropertyMetadata(null, ColorTextoChanged));

    static void ColorTextoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      var Valor = e.NewValue as SolidColorBrush;

      if (Valor == null) return;

      var STE = d as CajaIngresoFecha;
      STE.grdMain.Children.OfType<EntradaNumerica>().ToList()
       .ForEach(x => x.Foreground = Valor);
    }


    #endregion

    #region VALIDAR LA FECHA AL PERDER EL FOCO

    /// <summary>
    /// Detecta la pérdida de foco hacia fuera de la grilla principal de este control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void grdMain_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      var Actual = (bool)e.NewValue;
      if (Actual) return;
      ValidarFechaAlPerderFoco();
    }

    /// <summary>
    /// Valida la fecha al perder el foco.
    /// Si la fecha no es válida se cambia a nulo.
    /// </summary>
    void ValidarFechaAlPerderFoco()
    {
      int?[] Partes = new int?[] { tbxDia.Valor, tbxMes.Valor, tbxAño.Valor };
      var Valido = true;

      // Si faltan todos, es válido.
      var ConteoFaltantes = Partes.Count(x => !x.HasValue);
      if (ConteoFaltantes == 3) return;

      // Si falta 1 o 2 componentes, no es válido.
      if (ConteoFaltantes > 0)
      {
        Valido = false;
      }

      // Si el año es anterior a 1800 (o al definido en la configuración), no es válido.
      if (Valido
        && Partes[2].Value < Ruv.WPF.Captura.Properties.Settings.Default.MinimoAño)
      {
        Valido = false;
      }

      // Si las partes no representan una fecha válida, pues no es válido.
      DateTime FechaPrueba;
      if (Valido 
        && !DateTime.TryParseExact(string.Format("{0:D2}/{1:D2}/{2:D4}", Partes[0], Partes[1], Partes[2]), "dd/MM/yyyy", null,
        System.Globalization.DateTimeStyles.None, out FechaPrueba))
      {
        Valido = false;
      }

      // Borrarlo todo si no es válido.
      if (!Valido)
      {
        Fecha = null;
        tbxDia.Valor = null;
        tbxMes.Valor = null;
        tbxAño.Valor = null;
      }

    }

    #endregion

    public bool PresentarTitulos
    {
        set
        {
            //if (value)
            //    rdTitulos.Height = new GridLength(1d, GridUnitType.Star);
            //else
            //    rdTitulos.Height = new GridLength(1d, GridUnitType.Pixel);
        }
    }

  }
}
