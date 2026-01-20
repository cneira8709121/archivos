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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ruv.WPF.Captura.Impresion
{
    public partial class A07_EncabezadoPregunta09_17 : UserControl, IEncabezadoImpresion
    {
        public A07_EncabezadoPregunta09_17()
        {
            InitializeComponent();
            if (RUV.I.DeclaracionActual.VersionFUD == 2)
            {
                SubTitulo12.Visibility = Visibility.Collapsed;
                Titulo12.Visibility = Visibility.Collapsed;
                GColumna12.Width = new GridLength(0);
            }
        }

        public bool RepiteEnCadaPagina
        {
            get { return true; }
        }

        public int Orden
        {
            get { return 4; }
        }
    }
}
