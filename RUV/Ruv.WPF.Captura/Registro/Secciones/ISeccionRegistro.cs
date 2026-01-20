using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.WPF.Captura.Registro.Secciones
{

  public interface ISeccionRegistro
  {
    eSeccionRegistro Seccion { get; }
    bool RequireScrollBars { get; }
    void MostrarEnInterfase();
  }
}
