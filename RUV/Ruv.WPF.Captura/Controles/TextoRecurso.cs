using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Documents;

namespace Ruv.WPF.Captura.Controles
{
  /// <summary>
  /// Hereda de TextBlock. En la propiedad "Recurso" recibe un texto 
  /// y lo convierte en los inlines correcpondientes.
  /// </summary>
  public class TextoRecurso : TextBlock
  {

    public string Recurso
    {
      set
      {
        Inlines.Clear();
        Text = null;

        if (value == null) return;
        StringBuilder SB = new StringBuilder();

        SB.Append("<TextBlock xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">");
        SB.Append(value.ToString().Replace("\r\n", "<LineBreak/>"));
        SB.Append("</TextBlock>");

        var TB =
          XamlReader.Load(new System.Xml.XmlTextReader(
            new System.IO.StringReader(SB.ToString()))) as TextBlock;
        var Lista = TB.Inlines.ToArray();

        foreach (var item in Lista)
        {
          this.Inlines.Add(item);
        }
      }
    }

  }
}
