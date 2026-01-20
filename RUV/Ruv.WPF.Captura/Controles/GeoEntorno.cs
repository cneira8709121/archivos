using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.WPF.Captura.Infrastructure;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Controles
{
  public class GeoEntorno : Control
  {
    #region CONSTRUCTOR Y VARIABLES

    public GeoEntorno()
    {
      Focusable = false;
      ControlesEnlazados = new bool[3];
      ControlesListos = false;
    }

    bool CambioManual = false;

    /// <summary>
    /// Indica si los controles están todos enlazados.
    /// </summary>
    bool[] ControlesEnlazados;
    /// <summary>
    /// Señala si todos los 4 controles han sido enlazados.
    /// </summary>
    bool ControlesListos;

    #endregion

    #region CONTROLES

    void EnlazarControl(int indiceControl)
    {
      ControlesEnlazados[indiceControl] = true;
      if (ControlesEnlazados.All(x => x))
        ControlesListos = true;
    }

    public ComboBox ComboMunicipio
    {
      get { return (ComboBox)GetValue(ComboMunicipioProperty); }
      set { SetValue(ComboMunicipioProperty, value); }
    }

    public static readonly DependencyProperty ComboMunicipioProperty =
        DependencyProperty.Register("ComboMunicipio", typeof(ComboBox),
        typeof(GeoEntorno), new UIPropertyMetadata(null, ComboMunicipioChanged));

    static void ComboMunicipioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (e.NewValue != null)
      {
        var CB = e.NewValue as ComboBox;
        var GE = d as GeoEntorno;
        GE.EnlazarControl(0);

        CB.SelectionChanged += GE.ComboMuncipio_SelectionChanged;
      }
    }

    public ComboBox ComboBarrioVereda
    {
      get { return (ComboBox)GetValue(ComboBarrioVeredaProperty); }
      set { SetValue(ComboBarrioVeredaProperty, value); }
    }

    public static readonly DependencyProperty ComboBarrioVeredaProperty =
        DependencyProperty.Register("ComboBarrioVereda", typeof(ComboBox),
        typeof(GeoEntorno), new UIPropertyMetadata(null, ComboBarrioVeredaChanged));

    static void ComboBarrioVeredaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (e.NewValue != null)
      {
        var CB = e.NewValue as ComboBox;
        var GE = d as GeoEntorno;
        GE.EnlazarControl(1);

        CB.SelectionChanged -= GE.ComboBarrioVereda_SelectionChanged;
        CB.SelectionChanged += GE.ComboBarrioVereda_SelectionChanged;

        CB.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent,
          new TextChangedEventHandler(GE.ComboBarrioVereda_TextChanged));
      }
    }

    void ComboBarrioVereda_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      ComboBarrioVereda_Cambio();
    }

    void ComboBarrioVereda_TextChanged(object sender, TextChangedEventArgs e)
    {
      ComboBarrioVereda_Cambio();
    }

    public ComboBox ComboLocalidadCorregimiento
    {
      get { return (ComboBox)GetValue(ComboLocalidadCorregimientoProperty); }
      set { SetValue(ComboLocalidadCorregimientoProperty, value); }
    }

    public static readonly DependencyProperty ComboLocalidadCorregimientoProperty =
        DependencyProperty.Register("ComboLocalidadCorregimiento", typeof(ComboBox),
        typeof(GeoEntorno), new UIPropertyMetadata(null, ComboLocalidadCorregimientoChanged));

    static void ComboLocalidadCorregimientoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (e.NewValue != null)
      {
        var CB = e.NewValue as ComboBox;
        var GE = d as GeoEntorno;
        GE.EnlazarControl(2);

        CB.SelectionChanged -= GE.ComboLocalidadCorregimiento_SelectionChanged;
        CB.SelectionChanged += GE.ComboLocalidadCorregimiento_SelectionChanged;

        CB.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent,
          new TextChangedEventHandler(GE.ComboLocalidadCorregimiento_TextChanged));
      }
    }

    void ComboLocalidadCorregimiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      ComboLocalidadCorregimiento_Cambio();
    }

    void ComboLocalidadCorregimiento_TextChanged(object sender, TextChangedEventArgs e)
    {
      ComboLocalidadCorregimiento_Cambio();
    }


    #endregion

    #region PROPIEDADES

    public eTipoEntorno? TipoEntornoId
    {
      get { return (eTipoEntorno?)GetValue(TipoEntornoIdProperty); }
      set { SetValue(TipoEntornoIdProperty, value); }
    }

    public static readonly DependencyProperty TipoEntornoIdProperty =
        DependencyProperty.Register("TipoEntornoId", typeof(eTipoEntorno?),
        typeof(GeoEntorno), new UIPropertyMetadata(null, TipoEntornoIdChanged));

    public int? BarrioVeredaId
    {
      get { return (int?)GetValue(BarrioVeredaIdProperty); }
      set { SetValue(BarrioVeredaIdProperty, value); }
    }

    public static readonly DependencyProperty BarrioVeredaIdProperty =
        DependencyProperty.Register("BarrioVeredaId", typeof(int?),
        typeof(GeoEntorno), new UIPropertyMetadata(null, BarrioVeredaIdChanged));

    public string BarrioVeredaNombre
    {
      get { return (string)GetValue(BarrioVeredaNombreProperty); }
      set { SetValue(BarrioVeredaNombreProperty, value); }
    }

    public static readonly DependencyProperty BarrioVeredaNombreProperty =
        DependencyProperty.Register("BarrioVeredaNombre", typeof(string),
        typeof(GeoEntorno), new UIPropertyMetadata(null, BarrioVeredaNombreChanged));

    public int? LocalidadCorregimientoId
    {
      get { return (int?)GetValue(LocalidadCorregimientoIdProperty); }
      set { SetValue(LocalidadCorregimientoIdProperty, value); }
    }

    public static readonly DependencyProperty LocalidadCorregimientoIdProperty =
        DependencyProperty.Register("LocalidadCorregimientoId", typeof(int?),
        typeof(GeoEntorno), new UIPropertyMetadata(null, LocalidadCorregimientoIdChanged));

    public string LocalidadCorregimientoNombre
    {
      get { return (string)GetValue(LocalidadCorregimientoNombreProperty); }
      set { SetValue(LocalidadCorregimientoNombreProperty, value); }
    }

    public static readonly DependencyProperty LocalidadCorregimientoNombreProperty =
        DependencyProperty.Register("LocalidadCorregimientoNombre", typeof(string),
        typeof(GeoEntorno), new UIPropertyMetadata(null, LocalidadCorregimientoNombreChanged));

    #endregion

    #region CAMBIO EN COMBO MUNICIPIO

    void ComboMuncipio_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (CambioManual || !ControlesListos) return;
      CambioManual = true;
      // -----------------------

      if (MunicipioActual == null || !TipoEntornoId.HasValue)
      {
        VaciarComboBV();
        VaciarComboLC();
        BarrioVeredaNulo();
        LocalidadCorregimientoNulo();
        CambioManual = false;
        return;
      }

      LlenarComboBV();
      LlenarComboLC();

      SeleccionarBarrioVereda();
      SeleccionarLocalidadCorregimiento();

      // -----------------------
      CambioManual = false;
    }

    #endregion

    #region CAMBIO EN TIPO ENTORNO ID

    static void TipoEntornoIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var GE = d as GeoEntorno;

      if (GE.CambioManual || !GE.ControlesListos) return;
      GE.CambioManual = true;
      // -----------------------

      if (!GE.TipoEntornoId.HasValue)
      {
        GE.VaciarComboBV();
        GE.VaciarComboLC();
        GE.BarrioVeredaNulo();
        GE.LocalidadCorregimientoNulo();
        GE.CambioManual = false;
        return;
      }

      GE.LlenarComboBV();
      GE.LlenarComboLC();

      GE.SeleccionarBarrioVereda();
      GE.SeleccionarLocalidadCorregimiento();

      // -----------------------
      GE.CambioManual = false;
    }

    #endregion

    #region CAMBIO EN COMBO BARRIO VEREDA

    void ComboBarrioVereda_Cambio()
    {
      if (CambioManual || !ControlesListos) return;
      CambioManual = true;
      // -----------------------

      var ValorCombo = ValorSeleccionadoCombo(ComboBarrioVereda);
      if (ValorCombo == null && string.IsNullOrWhiteSpace(ComboBarrioVereda.Text))
      {
        BarrioVeredaNulo();
        CambioManual = false;
        return;
      }

      if (ValorCombo != null)
      {
        BarrioVeredaId = ValorCombo.Id;
        BarrioVeredaNombre = null;
        CambioManual = false;
        return;
      }

      if (!string.IsNullOrWhiteSpace(ComboBarrioVereda.Text))
      {
        BarrioVeredaId = null;
        BarrioVeredaNombre = ComboBarrioVereda.Text;
        CambioManual = false;
        return;
      }

      // -----------------------
      CambioManual = false;
    }

    #endregion

    #region CAMBIO EN COMBO LOCALIDAD CORREGIMIENTO

    void ComboLocalidadCorregimiento_Cambio()
    {
      if (CambioManual || !ControlesListos) return;
      CambioManual = true;
      // -----------------------

      var ValorCombo = ValorSeleccionadoCombo(ComboLocalidadCorregimiento);
      if (ValorCombo == null && string.IsNullOrWhiteSpace(ComboLocalidadCorregimiento.Text))
      {
        LocalidadCorregimientoNulo();
        CambioManual = false;
        return;
      }

      if (ValorCombo != null)
      {
        LocalidadCorregimientoId = ValorCombo.Id;
        LocalidadCorregimientoNombre = null;
        CambioManual = false;
        return;
      }

      if (!string.IsNullOrWhiteSpace(ComboLocalidadCorregimiento.Text))
      {
        LocalidadCorregimientoId = null;
        LocalidadCorregimientoNombre = ComboLocalidadCorregimiento.Text;
        CambioManual = false;
        return;
      }

      // -----------------------
      CambioManual = false;
    }

    #endregion

    #region CAMBIO EN BARRIO VEREDA ID

    static void BarrioVeredaIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var GE = d as GeoEntorno;

      if (GE.CambioManual || !GE.ControlesListos) return;
      GE.CambioManual = true;
      // -----------------------

      if (!GE.BarrioVeredaId.HasValue)
      {
        GE.DejarSeleccionNula(GE.ComboBarrioVereda);
        GE.CambioManual = false;
        return;
      }

      GE.SeleccionarValorEnCombo(GE.ComboBarrioVereda, GE.BarrioVeredaId.Value);
      GE.BarrioVeredaNombre = null;

      // -----------------------
      GE.CambioManual = false;
    }

    #endregion

    #region CAMBIO EN BARRIO VEREDA NOMBRE

    static void BarrioVeredaNombreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var GE = d as GeoEntorno;

      if (GE.CambioManual || !GE.ControlesListos) return;
      GE.CambioManual = true;
      // -----------------------

      if (string.IsNullOrWhiteSpace(GE.BarrioVeredaNombre))
      {
        GE.DejarSeleccionNula(GE.ComboBarrioVereda);
        GE.CambioManual = false;
        return;
      }

      // El texto existe en la selección?
      var Texto = Convert.ToString(e.NewValue);
      var Item = GE.TextoExisteEnCombo(GE.ComboBarrioVereda, Texto);
      if (Item == null)
      {
        GE.ComboBarrioVereda.SelectedItem = null;
        GE.ComboBarrioVereda.Text = Texto;
        GE.BarrioVeredaId = null;
        GE.CambioManual = false;
        return;
      }

      GE.ComboBarrioVereda.SelectedItem = Item;
      GE.BarrioVeredaId = Item.Id;

      // -----------------------
      GE.CambioManual = false;
    }

    #endregion

    #region CAMBIO EN LOCALIDAD CORREGIMIENTO ID

    static void LocalidadCorregimientoIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var GE = d as GeoEntorno;

      if (GE.CambioManual || !GE.ControlesListos) return;
      GE.CambioManual = true;
      // -----------------------

      if (!GE.LocalidadCorregimientoId.HasValue)
      {
        GE.DejarSeleccionNula(GE.ComboLocalidadCorregimiento);
        GE.CambioManual = false;
        return;
      }

      GE.SeleccionarValorEnCombo(GE.ComboLocalidadCorregimiento, GE.LocalidadCorregimientoId.Value);
      GE.LocalidadCorregimientoNombre = null;

      // -----------------------
      GE.CambioManual = false;
    }

    #endregion

    #region CAMBIO EN LOCALIDAD CORREGIMIENTO NOMBRE

    static void LocalidadCorregimientoNombreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var GE = d as GeoEntorno;

      if (GE.CambioManual || !GE.ControlesListos) return;
      GE.CambioManual = true;
      // -----------------------

      if (string.IsNullOrWhiteSpace(GE.LocalidadCorregimientoNombre))
      {
        GE.DejarSeleccionNula(GE.ComboLocalidadCorregimiento);
        GE.CambioManual = false;
        return;
      }

      // El texto existe en la selección?
      var Texto = Convert.ToString(e.NewValue);
      var Item = GE.TextoExisteEnCombo(GE.ComboLocalidadCorregimiento, Texto);
      if (Item == null)
      {
        GE.ComboLocalidadCorregimiento.SelectedItem = null;
        GE.ComboLocalidadCorregimiento.Text = Texto;
        GE.LocalidadCorregimientoId = null;
        GE.CambioManual = false;
        return;
      }

      GE.ComboLocalidadCorregimiento.SelectedItem = Item;
      GE.LocalidadCorregimientoId = Item.Id;

      // -----------------------
      GE.CambioManual = false;
    }

    #endregion

    #region UTIL

    clsPoblacion TextoExisteEnCombo(ComboBox combo, string texto)
    {
      if (combo == null || combo.ItemsSource == null) return null;

      var lista = combo.ItemsSource as IEnumerable<clsPoblacion>;
      var Resultado = lista.FirstOrDefault(x => x.Nombre.ToLower() == texto.ToLower());
      return Resultado;
    }

    void VaciarComboBV()
    {
      if (ComboBarrioVereda == null) return;
      ComboBarrioVereda.ItemsSource = null;
      ComboBarrioVereda.Text = string.Empty;
    }

    void VaciarComboLC()
    {
      if (ComboLocalidadCorregimiento == null) return;
      ComboLocalidadCorregimiento.ItemsSource = null;
      ComboLocalidadCorregimiento.Text = string.Empty;

    }

    void LocalidadCorregimientoNulo()
    {
      LocalidadCorregimientoId = null;
      LocalidadCorregimientoNombre = null;
    }

    void BarrioVeredaNulo()
    {
      BarrioVeredaId = null;
      BarrioVeredaNombre = null;
    }

    clsParametroMunicipio MunicipioActual
    {
      get
      {
        return ComboMunicipio.SelectedItem as clsParametroMunicipio;
      }
    }

    void LlenarComboBV()
    {
      if (ComboBarrioVereda == null
        || ComboMunicipio == null
        || MunicipioActual == null
        || !TipoEntornoId.HasValue) return;

      eTipoPoblacion TP = TipoEntornoId.Value == eTipoEntorno.Urbano ?
        eTipoPoblacion.Urbano_Barrio : eTipoPoblacion.Rural_Vereda;

      var Lista = from x in RUV.I.InfoGeneral.ListaPoblacionesPorIndice
                  where
                    x.Index.Item1 == MunicipioActual.Id
                    && x.Index.Item2 == (int)TP
                  select x.LazyValue.Value;

      ComboBarrioVereda.ItemsSource = Lista;
    }

    void LlenarComboLC()
    {
      if (ComboLocalidadCorregimiento == null
        || ComboMunicipio == null
        || MunicipioActual == null
        || !TipoEntornoId.HasValue) return;

      eTipoPoblacion TP = TipoEntornoId.Value == eTipoEntorno.Urbano ?
        eTipoPoblacion.Urbano_Localidad : eTipoPoblacion.Rural_Corregimiento;

      var Lista = from x in RUV.I.InfoGeneral.ListaPoblacionesPorIndice
                  where
                    x.Index.Item1 == MunicipioActual.Id
                    && x.Index.Item2 == (int)TP
                  select x.LazyValue.Value;

      ComboLocalidadCorregimiento.ItemsSource = Lista;
    }

    bool ValorExisteEnCombo(ComboBox combo, int valor)
    {
      var Lista = combo.ItemsSource as IEnumerable<clsPoblacion>;
      if (Lista == null) return false;
      return Lista.Any(x => x.Id == valor);
    }

    void SeleccionarValorEnCombo(ComboBox combo, int valor)
    {
      var Lista = combo.ItemsSource as IEnumerable<clsPoblacion>;
      if (Lista == null) return;
      var Item = Lista.FirstOrDefault(x => x.Id == valor);
      if (Item == null) return;

      combo.SelectedItem = Item;
    }

    void SeleccionarBarrioVereda()
    {
      if (BarrioVeredaId != null && ValorExisteEnCombo(ComboBarrioVereda, BarrioVeredaId.Value))
      {
        SeleccionarValorEnCombo(ComboBarrioVereda, BarrioVeredaId.Value);
      }
      else if (!string.IsNullOrWhiteSpace(BarrioVeredaNombre))
      {
        ComboBarrioVereda.SelectedItem = null;
        ComboBarrioVereda.Text = BarrioVeredaNombre;
      }
    }

    void SeleccionarLocalidadCorregimiento()
    {
      if (LocalidadCorregimientoId != null && ValorExisteEnCombo(ComboLocalidadCorregimiento, LocalidadCorregimientoId.Value))
      {
        SeleccionarValorEnCombo(ComboLocalidadCorregimiento, LocalidadCorregimientoId.Value);
      }
      else if (!string.IsNullOrWhiteSpace(LocalidadCorregimientoNombre))
      {
        ComboLocalidadCorregimiento.SelectedItem = null;
        ComboLocalidadCorregimiento.Text = LocalidadCorregimientoNombre;
      }
    }

    clsPoblacion ValorSeleccionadoCombo(ComboBox combo)
    {
      if (combo == null) return null;
      var Resultado = combo.SelectedItem as clsPoblacion;
      return Resultado;
    }

    void DejarSeleccionNula(ComboBox combo)
    {
      if (combo == null || combo.ItemsSource == null) return;
      combo.SelectedIndex = 0;
      combo.Text = null;
    }

    //=========================

    //void VaciarComboNombre()
    //{
    //  if (ComboBarrioVereda != null) ComboBarrioVereda.ItemsSource = null;
    //}

    //bool ComboTipoVacio()
    //{
    //  if (ComboTipo == null
    //    || ComboTipo.SelectedItem == null)
    //    return true;

    //  clsItem Item = ComboTipo.SelectedItem as clsItem;
    //  if (Item == null || !Item.Id.HasValue)
    //    return true;

    //  return false;
    //}

    //bool ComboMunicipioVacio()
    //{
    //  if (ComboMunicipio == null
    //    || ComboMunicipio.SelectedItem == null)
    //    return true;

    //  var Mcpio = ComboMunicipio.SelectedItem as clsParametroMunicipio;
    //  if (Mcpio == null)
    //    return true;

    //  return false;
    //}

    //void LLenarComboNombre()
    //{
    //  var Municipio = ComboMunicipio.SelectedItem as clsParametroMunicipio;
    //  if (Municipio == null)
    //  {
    //    ComboBarrioVereda.ItemsSource = null;
    //    return;
    //  }

    //  if (TipoPoblacionId.HasValue
    //    && ComboTipo.SelectedItem == null)
    //  {
    //    ComboTipo.SelectedValue = TipoPoblacionId.Value;
    //  }

    //  var Tipo = ComboTipo.SelectedItem as clsItem;
    //  if (Tipo == null)
    //  {
    //    ComboBarrioVereda.ItemsSource = null;
    //    return;
    //  }

    //  var Poblaciones = Sipod.I.InfoGeneral.ListaPoblacionesPorIndice
    //      .Where(x => x.Index.Item1 == Municipio.Id
    //        && x.Index.Item2 == Tipo.Id)
    //      .Select(x => x.LazyValue.Value).ToList();

    //  ComboBarrioVereda.ItemsSource = Poblaciones;
    //}

    //bool EntornoIdExisteEnLista(int entornoId)
    //{
    //  if (ComboBarrioVereda == null) return false;

    //  // En algunas ocasiones la lista no alcanza a llenarse...
    //  if (ComboBarrioVereda.ItemsSource == null) LLenarComboNombre();

    //  var Poblaciones = ComboBarrioVereda.ItemsSource as List<clsPoblacion>;
    //  if (Poblaciones == null) return false;

    //  return Poblaciones.Any(x => x.Id == entornoId);
    //}

    //void SeleccionarTipo(eTipoPoblacion tipoId)
    //{
    //  if (ComboTipo == null) return;

    //  var Tipos = ComboTipo.ItemsSource as List<clsItem>;
    //  if (Tipos == null) return;

    //  var Tipo = Tipos.FirstOrDefault(x => x.Id == (int)tipoId);
    //  if (Tipo == null) return;

    //  ComboTipo.SelectedItem = Tipo;
    //}

    //void SeleccionarNombre(int nombreId)
    //{
    //  var Nombres = ComboBarrioVereda.ItemsSource as List<clsPoblacion>;
    //  if (Nombres == null) return;

    //  var Nombre = Nombres.FirstOrDefault(x => x.Id == nombreId);
    //  if (Nombre == null) return;

    //  ComboBarrioVereda.SelectedItem = Nombre;
    //}

    //clsPoblacion NombreOtroExisteEnCombo(string texto)
    //{
    //  var Lista = ComboBarrioVereda.ItemsSource as List<clsPoblacion>;
    //  if (Lista == null) return null;

    //  var Resultado = Lista.FirstOrDefault(x => x.Nombre.ToLower() == texto.ToLower());
    //  return Resultado;
    //}


    #endregion

  }
}
