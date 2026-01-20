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
using Ruv.WPF.Captura.GeneralService;
using System.Collections.Specialized;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
  /// <summary>
  /// Lógica de interacción para ReporteEnvioDeclaracion.xaml
  /// </summary>
  public partial class ReporteEnvioDeclaracion : Window
  {
    public ReporteEnvioDeclaracion(string[] erroresDB, string[] advertenciasDB)
    {
      InitializeComponent();

      ObtenerReferencias(erroresDB, advertenciasDB);

      this.Loaded += new RoutedEventHandler(ReporteEnvioDeclaracion_Loaded);
    }


    public ReporteEnvioDeclaracion(clsResultado resultado)
    {
      InitializeComponent();

      ObtenerReferencias(resultado.ErroresDB, resultado.AdvertenciasDB);

      this.Loaded += new RoutedEventHandler(ReporteEnvioDeclaracion_Loaded);
    }

    private void ObtenerReferencias(string[] erroresDB, string[] advertenciasDB)
    {
      if (erroresDB != null)
      {
        SCErroresDB = new StringCollection();
        erroresDB.ToList().ForEach(x => SCErroresDB.Add(x));
      }

      if (advertenciasDB != null)
      {
        SCAdvertenciasDB = new StringCollection();
        advertenciasDB.ToList().ForEach(x => SCAdvertenciasDB.Add(x));



      }
    }

    StringCollection SCErroresDB;
    StringCollection SCAdvertenciasDB;


    void ReporteEnvioDeclaracion_Loaded(object sender, RoutedEventArgs e)
    {
      if (SCErroresDB != null && SCErroresDB.Count > 0)
      {
        tabErrores.Visibility = System.Windows.Visibility.Visible;
        lbxErrores.ItemsSource = SCErroresDB;
      }

      if (SCAdvertenciasDB != null && SCAdvertenciasDB.Count > 0)
      {
        tabAdvertencias.Visibility = System.Windows.Visibility.Visible;
        lbxAdvertencias.ItemsSource = SCAdvertenciasDB;

        if (tabErrores.Visibility != System.Windows.Visibility.Visible)
        {
          tabAdvertencias.IsSelected = true;
        }
      }
    }

    private void Cerrar(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

  }
}
