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
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
  /// <summary>
  /// Lógica de interacción para EdicionComunidadEtnica.xaml
  /// </summary>
  public partial class EdicionComunidadEtnica : UserControl
  {
    #region CONSTRUCTOR

    public EdicionComunidadEtnica()
    {
      InitializeComponent();
      VistaComunidades = CollectionViewSource.GetDefaultView(RUV.I.InfoGeneral.ListaComunidadesEtnicas);
      VistaComunidades.Filter = new Predicate<object>(MetodoFiltro);
      DataContext = this;
    }

    /// <summary>
    /// Vista que provee el filtro para buscar una comunidad.
    /// </summary>
    ICollectionView VistaComunidades;

    #endregion

    #region FILTRO PARA LAS COMUNIDADES

    public string TextoFiltro
    {
      get { return (string)GetValue(TextoFiltroProperty); }
      set { SetValue(TextoFiltroProperty, value); }
    }

    public static readonly DependencyProperty TextoFiltroProperty =
        DependencyProperty.Register("TextoFiltro", typeof(string),
        typeof(EdicionComunidadEtnica), new UIPropertyMetadata(null, TextoFiltroChanged));

    /// <summary>
    /// Actualizar la lista.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    static void TextoFiltroChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      (d as EdicionComunidadEtnica).LlenarListaComunidades();
    }

    /// <summary>
    /// El filtro a aplicar a todas las comunidades dependiendo de la caja de texto.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    Boolean MetodoFiltro(object value)
    {
      var Comunidad = value as clsComunidadEtnica;
      return Comunidad.GrupoEtnicoId == GrupoEtnico
        &&
        (
          string.IsNullOrWhiteSpace(TextoFiltro)
          || Comunidad.Nombre.ToLower().Contains(TextoFiltro.ToLower())
        );
    }

    #endregion

    #region EL GRUPO ETNICO

    public int GrupoEtnico
    {
      get { return (int)GetValue(GrupoEtnicoProperty); }
      set { SetValue(GrupoEtnicoProperty, value); }
    }

    public static readonly DependencyProperty GrupoEtnicoProperty =
        DependencyProperty.Register("GrupoEtnico", typeof(int),
        typeof(EdicionComunidadEtnica), new UIPropertyMetadata(0, GrupoEtnicoChanged));

    static void GrupoEtnicoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      (d as EdicionComunidadEtnica).LlenarListaComunidades();
    }

    /// <summary>
    /// LLenar la lista de las comunidades étnicas.
    /// </summary>
    void LlenarListaComunidades()
    {
      DateTime Inicio = DateTime.Now;

      VistaComunidades.Refresh();
      lbxComunidadesEtnicas.ItemsSource = VistaComunidades;

      DateTime Fin = DateTime.Now;

      System.Diagnostics.Debug.WriteLine((Fin - Inicio).TotalMilliseconds);

      SeleccionarComunidad();
    }

    /// <summary>
    /// Selecciona de la lista la comunidad indicada.
    /// </summary>
    private void SeleccionarComunidad()
    {
      // Si ya hay una comunidad indicada, seleccionarla.
      if (ComunidadEtnicaSeleccionada.HasValue)
      {
        var Seleccionado = RUV.I.InfoGeneral.ListaComunidadesEtnicas.Where(
          x => x.Id == ComunidadEtnicaSeleccionada.Value).FirstOrDefault();
        if (Seleccionado != null)
        {
          lbxComunidadesEtnicas.SelectedItem = Seleccionado;
        }
      }
    }

    #endregion

    #region COMUNIDAD ETNICA

    public int? ComunidadEtnicaSeleccionada
    {
      get { return (int?)GetValue(ComunidadEtnicaSeleccionadaProperty); }
      set { SetValue(ComunidadEtnicaSeleccionadaProperty, value); }
    }

    public static readonly DependencyProperty ComunidadEtnicaSeleccionadaProperty =
        DependencyProperty.Register("ComunidadEtnicaSeleccionada", typeof(int?),
        typeof(EdicionComunidadEtnica),
        new UIPropertyMetadata(null, ComunidadEtnicaSeleccionadChanged));

    static void ComunidadEtnicaSeleccionadChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      (d as EdicionComunidadEtnica).SeleccionarComunidad();
    }

    #endregion

    #region QUITAR EL FILTRO

    /// <summary>
    /// Quitar el filtro aplicado.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void QuitarFiltro(object sender, RoutedEventArgs e)
    {
      TextoFiltro = "";
    }

    #endregion

    #region SELECCION DE LA COMUNIDAD POR PARTE DEL USUARIO

    private void ComunidadSeleccionada(object sender, SelectionChangedEventArgs e)
    {
      if (lbxComunidadesEtnicas.SelectedItem == null)
        ComunidadEtnicaSeleccionada = null;
      else
      {
        clsComunidadEtnica CE = lbxComunidadesEtnicas.SelectedItem as clsComunidadEtnica;
        ComunidadEtnicaSeleccionada = CE.Id;
      }
    }

    #endregion

  }
}
