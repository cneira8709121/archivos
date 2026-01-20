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
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
  /// <summary>
  /// Lógica de interacción para EdicionPertenenciaEtnica.xaml
  /// </summary>
  public partial class EdicionPertenenciaEtnica : Window
  {
    public EdicionPertenenciaEtnica()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(EdicionPertenenciaEtnica_Loaded);
    }

    void EdicionPertenenciaEtnica_Loaded(object sender, RoutedEventArgs e)
    {
      // Actualizar los datos de edición.
      if (PersonaAfectada.PertenenciaEtnica.HasValue)
      {
        var EtniaActual = RUV.I.InfoGeneral.ListaEtnias.
          FirstOrDefault(x => x.Id == PersonaAfectada.PertenenciaEtnica.Value);
        if (EtniaActual != null)
        {
          cbxEtnias.SelectedItem = EtniaActual;
        }
      }
    }

    #region CAMBIO EN LA PERTENENCIA ÉTNICA

    private void PertenenciaEtnica_Cambio(object sender, SelectionChangedEventArgs e)
    {
      ComboBox CB = sender as ComboBox;
      var EtniaSeleccionada = CB.SelectedItem as clsParametroGeneral;

      if (EtniaSeleccionada == null)
      {
        tcMain.Items.Clear();
        return;
      }

      // Los grupos de esta étnia.
      var Grupos = RUV.I.InfoGeneral.ListaGruposEtnicos
        .Where(x => x.EtniaId == EtniaSeleccionada.Id);

      tcMain.Items.Clear();

      if (!Grupos.Any())
        return;

      int Indice = 1;
      foreach (var UnGrupo in Grupos)
      {
        TabItem TI = new TabItem()
        {
          Header = UnGrupo.Nombre,
          IsSelected = Indice == 1
        };

        EdicionComunidadEtnica ECE = new EdicionComunidadEtnica()
        {
          GrupoEtnico = UnGrupo.Id,
        };
        switch (Indice++)
        {
          //John.Henao@globant.com -- Cambio para que siempre que se cambie el valor del combo se eliminen los valores previos y la persona de elegir de nuevo el grupo etnico.
          case 1:
            ECE.ComunidadEtnicaSeleccionada = null;//PersonaAfectada.ComunidadEtnica1;
            break;
          case 2:
            ECE.ComunidadEtnicaSeleccionada = null;//PersonaAfectada.ComunidadEtnica2;
            break;
        }

        TI.Content = ECE;
        tcMain.Items.Add(TI);
      }
    }

    #endregion
/*
    private clsAnexo13_Victima _PersonaAfectada;
    /// <summary>
    /// La persona cuya información étnica se está editando.
    /// </summary>
    public clsAnexo13_Victima PersonaAfectada
    {
      get { return _PersonaAfectada; }
      set { _PersonaAfectada = value; }
    }
      */

    private IPertenenciaEtnica _PersonaAfectada;
    /// <summary>
    /// La persona cuya información étnica se está editando.
    /// </summary>
    public IPertenenciaEtnica PersonaAfectada
    {
        get { return _PersonaAfectada; }
        set { _PersonaAfectada = value; }
    }
      
    /// <summary>
    /// Cerrar sin postear los cambios.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Cancelar(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    /// <summary>
    /// Cerrar pero postear los cambios.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Aceptar(object sender, RoutedEventArgs e)
    {
      // Verificar la etnia.
      clsParametroGeneral Etnia = cbxEtnias.SelectedItem as clsParametroGeneral;

      if (Etnia == null)
      {
        RUV.I.UIPrincipal.ReportarErrorDeUsuario(
          "Debe seleccionar una etnia.");
        return;
      }

      // Verificar cada comunidad.
      var Comunidades =
        tcMain.Items.OfType<TabItem>().Select(x => x.Content as EdicionComunidadEtnica);

      var UnaFaltante = Comunidades.FirstOrDefault(x => !x.ComunidadEtnicaSeleccionada.HasValue);
      if (UnaFaltante != null)
      {
        RUV.I.UIPrincipal.ReportarErrorDeUsuario(
          string.Format("Falta hacer la selección para '{0}'.",
          (UnaFaltante.Parent as TabItem).Header));
        return;
      }

      // Postear los valores.
      PersonaAfectada.PertenenciaEtnica = Etnia.Id;

      //Limpiar valores
      PersonaAfectada.ComunidadEtnica1 = null;
      PersonaAfectada.ComunidadEtnica2 = null;

      if (Comunidades.Any(z=> z.ComunidadEtnicaSeleccionada.HasValue))
        PersonaAfectada.ComunidadEtnica1 = Comunidades.ElementAt(0).ComunidadEtnicaSeleccionada.Value;
      else
      {
          if ((PersonaAfectada.PertenenciaEtnica == 166) || (PersonaAfectada.PertenenciaEtnica ==169)|| (PersonaAfectada.PertenenciaEtnica ==4541))
          {
              this.Close();
          }
          else
          {
              RUV.I.UIPrincipal.ReportarErrorDeUsuario(
                string.Format("Falta hacer la selección correcta para '{0}'.",
                (UnaFaltante.Parent as TabItem).Header));
              return;
          }
      }
      if (Comunidades.Count() > 1)
        PersonaAfectada.ComunidadEtnica2 = Comunidades.ElementAt(1).ComunidadEtnicaSeleccionada.Value;

      this.Close();
    }

  }
}
