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

namespace Ruv.WPF.Captura.Seguridad
{
    /// <summary>
    /// Lógica de interacción para DesplegarExceptionControlada.xaml
    /// </summary>

    public partial class DesplegarExceptionControlada : Window
    {
        #region CONSTRUCTOR

        public DesplegarExceptionControlada()
        {
            InitializeComponent();
            this.Tipo = TipoMensaje.Error;
        }

        private TipoMensaje _Tipo;
        public TipoMensaje Tipo
        {
            get { return _Tipo; }
            set
            {
                SolidColorBrush borderBrush, backgroundBrush;
                System.Drawing.Color color;
                _Tipo = value;
                switch (value)
                {
                    case TipoMensaje.Warning:
                        borderBrush = new SolidColorBrush(Colors.Yellow);
                        //.Background = "#FFFDFFEF";
                        color = System.Drawing.ColorTranslator.FromHtml("#FFFDFFEF");
                        backgroundBrush = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
                        break;
                    default:
                        borderBrush = new SolidColorBrush(Colors.Red);
                        //.Background = "#FFFFDBDB";
                        color = System.Drawing.ColorTranslator.FromHtml("#FFFFDBDB");
                        backgroundBrush = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
                        break;
                }

                bMensaje.BorderBrush = borderBrush;
                bMensaje.Background = backgroundBrush; 
            }
        }

        public enum TipoMensaje
        {
            Error,
            Warning
        }

        private string InitialBody;
        public DesplegarExceptionControlada(Exception ex, string title, string caption, string initialBody)
        {
            InitializeComponent();
            this.Title = title;
            tbxCaption.Text = caption;
            InitialBody = initialBody;
            Excepcion = ex;
            this.Loaded += DesplegarWarning_Loaded;
            this.Tipo = TipoMensaje.Warning;
        }

        void DesplegarWarning_Loaded(object sender, RoutedEventArgs e)
        {
            Ruv.WPF.Captura.Infrastructure.clsLog Log = new Infrastructure.clsLog();
            Log.Registrar("Excepción administrada", Excepcion);

            txtHora.Text = "Hora: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            StringBuilder Txt = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(InitialBody))
                Txt.AppendLine(InitialBody + Environment.NewLine);

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

        Exception Excepcion;

        #region COPIAR MENSAJE

        private void CopiarMensaje(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(tbxMensaje.Text);
        }

        #endregion

        #region CERRAR
        private void CerrarClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
