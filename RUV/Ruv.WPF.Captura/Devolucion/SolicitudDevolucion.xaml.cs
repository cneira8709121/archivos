using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ruv.WPF.Captura
{
	/// <summary>
	/// Interaction logic for SolicitudDevolucion.xaml
	/// </summary>
	public partial class SolicitudDevolucion : Window
	{
		public SolicitudDevolucion()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
		}

        public bool Cancelado { get; set; }

		private void CancelarButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Cancelado = true;
            Close();
		}

		private void AceptarButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Close();
		}

	}
}