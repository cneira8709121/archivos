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

namespace Ruv.WPF.Captura.Tmp
{
  /// <summary>
  /// Lógica de interacción para Window3.xaml
  /// </summary>
  public partial class Window3 : Window
  {
    public Window3()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(Window3_Loaded);
    }

    void Window3_Loaded(object sender, RoutedEventArgs e)
    {
      this.DataContext = new clsSeleccion() { Valor = null, Texto = "Número dos" };
    }
  }

  public class clsSeleccion
  {
    private int? _Valor;
    public int? Valor
    {
      get { return _Valor; }
      set
      {
        _Valor = value;
        if (value.HasValue)
          System.Diagnostics.Debug.WriteLine(">> Valor: " + value.ToString());
        else
          System.Diagnostics.Debug.WriteLine(">> Valor: null");
      }
    }

    private string _Texto;
    public string Texto
    {
      get { return _Texto; }
      set
      {
        _Texto = value;
        System.Diagnostics.Debug.WriteLine(">> Texto: " + value);
      }
    }

  }

  public class clsPersona
  {
    public clsPersona()
    {
      Lista = new List<int>();
    }

    private DateTime? _Fecha;
    public DateTime? Fecha
    {
      get { return _Fecha; }
      set
      {
        _Fecha = value;
        if (value.HasValue)
          System.Diagnostics.Debug.WriteLine("Siento cambio: " +
            value.Value.ToString("dd/MM/yyyy"));
        else
          System.Diagnostics.Debug.WriteLine("Siento cambio: null");
      }
    }

    private string _Otro;
    public string Otro
    {
      get { return _Otro; }
      set
      {
        _Otro = value;
        System.Diagnostics.Debug.WriteLine("Otro: " + value);
      }
    }

    private List<Int32> _Lista;
    public List<Int32> Lista
    {
      get { return _Lista; }
      set { _Lista = value; }
    }

    private int? _MiHecho;
    public int? MiHecho
    {
      get { return _MiHecho; }
      set { _MiHecho = value; }
    }


  }
}
