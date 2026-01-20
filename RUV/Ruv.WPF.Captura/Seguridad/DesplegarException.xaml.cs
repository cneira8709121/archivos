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
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Seguridad
{
  /// <summary>
  /// Lógica de interacción para DesplegarException.xaml
  /// </summary>
  public partial class DesplegarException : Window
  {
    #region CONSTRUCTOR

    public DesplegarException()
    {
      InitializeComponent();
      this.Closed += DesplegarException_Closed;
    }

    public DesplegarException(Exception ex)
    {
      InitializeComponent();

      Excepcion = ex;
      this.Closed += DesplegarException_Closed;
      this.Loaded += DesplegarException_Loaded;            
    }

    void DesplegarException_Loaded(object sender, RoutedEventArgs e)
    {
      Ruv.WPF.Captura.Infrastructure.clsLog Log = new Infrastructure.clsLog();
      Log.Registrar("Excepción no administrada", Excepcion);
        
      txtHora.Text = "Hora: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

      StringBuilder Txt = new StringBuilder();
      int Level = 0;
      while (Excepcion != null)
      {
        Txt.AppendFormat("{0}-EXCEPCION: {1}\n", Level, Excepcion.Message);
        Txt.AppendFormat("{0}-STACK: {1}\n\n", Level++, Excepcion.StackTrace);
        Excepcion = Excepcion.InnerException;
      }
      tbxMensaje.Text = Txt.ToString();
    }

    #endregion

    public bool EsControlada { set; get; }
    Exception Excepcion;   

    #region CERRAR

    void DesplegarException_Closed(object sender, EventArgs e)
    {
        if (!EsControlada)
            Application.Current.Shutdown();
    }

    private void CerrarClick(object sender, RoutedEventArgs e)
    {
        if (!EsControlada)
            Application.Current.Shutdown();
        this.Close();
    }

    #endregion

    #region COPIAR MENSAJE

    private void CopiarMensaje(object sender, RoutedEventArgs e)
    {
      Clipboard.SetText(tbxMensaje.Text);
    }

    #endregion
  }
}
