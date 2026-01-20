using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;

namespace Ruv.WPF.Captura.Controles
{
  public class ComboSoloLetrasBehavior : Behavior<ComboBox>
  {
    protected override void OnAttached()
    {
      AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
    }

    protected override void OnDetaching()
    {
      AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
    }

    /// <summary>
    /// Este filtro deja sólo escribir letras y espacios.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void AssociatedObject_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (!Ruv.WPF.Captura.Utils.clsTextBoxFilterBehavior.FiltroSoloLetrasEspacios(e))
        e.Handled = true;
    }


  }
}
