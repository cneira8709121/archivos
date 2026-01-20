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
    /// <summary>
    /// Interaction logic for H02_Encabezado01.xaml
    /// </summary>
    public partial class H02_Encabezado01 : UserControl, IEncabezadoImpresion
    {
        public H02_Encabezado01()
        {
            InitializeComponent();
            //if (RUV.I.DeclaracionActual.VersionFUD == 2)
            //{
            //    HP1_V1.Visibility = Visibility.Collapsed;
            //    HP2_V1.Visibility = Visibility.Collapsed;
            //}
        }

        public bool RepiteEnCadaPagina
        { get { return false; } }

        public int Orden
        { get { return 1; } }

    }
}
