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
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace Ruv.WPF.Captura.Tmp
{
  /// <summary>
  /// Lógica de interacción para FirmaDigital.xaml
  /// </summary>
  public partial class FirmaDigital : Window
  {
    public FirmaDigital()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(FirmaDigital_Loaded);
    }

    void FirmaDigital_Loaded(object sender, RoutedEventArgs e)
    {

    }


  }
}
