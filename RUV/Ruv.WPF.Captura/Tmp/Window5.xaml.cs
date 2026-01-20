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
  /// Interaction logic for Window5.xaml
  /// </summary>
  public partial class Window5 : Window
  {
    public Window5()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show(IsUserVisible(button1, canvas1).ToString());
    }

    private bool IsUserVisible(FrameworkElement element, FrameworkElement container)
    {
      if (!element.IsVisible)
        return false;
      Rect bounds = element.TransformToAncestor(container).TransformBounds(
        new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
      Rect rect = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);

      return rect.Contains(bounds);
    }

  }


}
