using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Ruv.WPF.Captura.Registro.Secciones;
using System.Windows.Input;

namespace Ruv.WPF.Captura.Tmp
{
  /// <summary>
  /// Lógica de interacción para Page1.xaml
  /// </summary>
  public partial class Page1 : Page
  {
    public Page1()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(Page1_Loaded);
      var P = new clsPersona { Otro = "Mi texto del otro" };
      P.Lista.Add(9017);
      P.Lista.Add(9012);
      P.MiHecho = null;
      DataContext = P;
    }

    void Page1_Loaded(object sender, RoutedEventArgs e)
    {
      var Bin = BindingOperations.GetBindingExpressionBase(listaOpciones1,
        ListaOpciones.TextoOtroProperty);
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("========================");
      var dato = DataContext as clsPersona;
      System.Diagnostics.Debug.WriteLine(">> " + dato.MiHecho);
    }
  }
}
