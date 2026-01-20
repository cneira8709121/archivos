using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;

namespace Ruv.WPF.Captura.Controles
{
  public class ComboMayusculaBehavior : Behavior<ComboBox>
  {
    protected override void OnAttached()
    {
      AssociatedObject.Loaded += new System.Windows.RoutedEventHandler(AssociatedObject_Loaded);
    }

    protected override void OnDetaching()
    {
      AssociatedObject.Loaded += new System.Windows.RoutedEventHandler(AssociatedObject_Loaded);
    }

    void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
      if (AssociatedObject.Template == null) return;

       // Ubicar el textbox para modificarlo.
      var CajaTexto = AssociatedObject.Template.FindName("PART_EditableTextBox", AssociatedObject) as TextBox;
      if (CajaTexto != null)
      {
        CajaTexto.CharacterCasing = CharacterCasing.Upper;
      }
    }
  }
}
