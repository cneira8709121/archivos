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
using System.Collections.ObjectModel;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
  /// <summary>
  /// Lógica de interacción para ListaPersonasDesplazadas.xaml
  /// </summary>
  public partial class ListaPersonasDesplazadas : UserControl
  {
    public ListaPersonasDesplazadas()
    {
      InitializeComponent();
    }

    public ObservableCollection<clsAnexo05_Victima> Victimas
    {
      get { return (ObservableCollection<clsAnexo05_Victima>)GetValue(VictimasProperty); }
      set { SetValue(VictimasProperty, value); }
    }

    public static readonly DependencyProperty VictimasProperty =
        DependencyProperty.Register("Victimas", typeof(ObservableCollection<clsAnexo05_Victima>),
        typeof(ListaPersonasDesplazadas), new UIPropertyMetadata(null, VictimasChanged));

    static void VictimasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var LPD = d as ListaPersonasDesplazadas;
      if (e.NewValue == null) return;

      LPD.Victimas.CollectionChanged += LPD.ColeccionModificada;

      if (e.OldValue == null)
        LPD.LLenadoCompletoDeLista();
    }

    void LLenadoCompletoDeLista()
    {
      wpMain.Children.Clear();
        var victimaslista = Victimas
        .Where(x => x.EstadoRegistro != Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Eliminado); 
        var personasAfectadas = RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas;
      foreach (var item in victimaslista)
      {
          var persona = personasAfectadas.FirstOrDefault(a => a.ID == item.PersonaAfectadaId);
          if (persona != null)
          {
            var PD = new PersonaDesplazada() { DataContext = item };
            wpMain.Children.Add(PD);
          }
      }
    }

    void ColeccionModificada(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      // Los nuevos.
      if (e.NewItems != null)
        foreach (var item in e.NewItems
          .Cast<clsAnexo05_Victima>()
          .Where(x => x.EstadoRegistro != Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Eliminado))
        {
          var PD = new PersonaDesplazada() { DataContext = item };
          wpMain.Children.Add(PD);
        }

      // Los que se quitan.
      if (e.OldItems != null)
        foreach (var item in e.OldItems.Cast<clsAnexo05_Victima>())
        {
          var PD = wpMain.Children.OfType<PersonaDesplazada>()
            .Where(x => x.DataContext == item).FirstOrDefault();

          if (PD != null) wpMain.Children.Remove(PD);
        }
      //}
    }

    /// <summary>
    /// Retorna las víctimas chequeadas (clsAnexo05_Victima).
    /// </summary>
    public IEnumerable<clsAnexo05_Victima> PersonasSeleccionadas
    {
      get
      {
        return wpMain.Children.OfType<PersonaDesplazada>()
          .Where(x => x.Seleccionado)
          .Select(x => (x.DataContext as clsAnexo05_Victima));
      }
    }

  }
}
