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
using Ruv.WPF.Captura.Controles;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
  /// <summary>
  /// Lógica de interacción para ListaBotonesAnexos.xaml
  /// </summary>
  public partial class ListaBotonesAnexos : UserControl
  {
    public ListaBotonesAnexos()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(ListaBotonesAnexos_Loaded);
    }

    void ListaBotonesAnexos_Loaded(object sender, RoutedEventArgs e)
    {
      RecrearListaBotones();
    }

    public BindingList<int> ListaValores
    {
      get { return (BindingList<int>)GetValue(ListaValoresProperty); }
      set { SetValue(ListaValoresProperty, value); }
    }

    public static readonly DependencyProperty ListaValoresProperty =
        DependencyProperty.Register("ListaValores", typeof(BindingList<int>),
        typeof(ListaBotonesAnexos), new UIPropertyMetadata(null, ListaValoresChanged));

    static void ListaValoresChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (e.OldValue == null && e.NewValue != null)
      {
        (d as ListaBotonesAnexos).ListaValores.ListChanged += delegate
        {
          (d as ListaBotonesAnexos).RecrearListaBotones();
        };
      }

      (d as ListaBotonesAnexos).RecrearListaBotones();
    }

    void RecrearListaBotones()
    {
      wpMain.Children.Clear();
      if (ListaValores == null || !ListaValores.Any())
        return;

      for (int i = 0; i < ListaValores.Count; i++)
        if (ListaValores[i] != 0)
          for (int j = 0; j < ListaValores[i]; j++)
          {
            BotonMenu BM = new BotonMenu() { Tag = "Anexo" };
            if (ListaValores[i] > 1)
              BM.Texto = string.Format("{0}.{1}", i + 1, j + 1);
            else
              BM.Texto = string.Format("{0}", i + 1);
            wpMain.Children.Add(BM);
          }
    }

  }
}
