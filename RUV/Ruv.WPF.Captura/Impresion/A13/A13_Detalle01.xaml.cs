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
    /// Lógica de interacción para A13_Detalle01.xaml
    /// </summary>
    public partial class A13_Detalle01 : UserControl
    {
        public A13_Detalle01()
        {
            InitializeComponent();
            if (RUV.I.DeclaracionActual.VersionFUD == 1)
            {
                cSexo.Width = new GridLength(0);
                cOrientacionSexual.Width = new GridLength(0);
                cIdentidadGenero.Width = new GridLength(0);
                cCampesinado.Width = new GridLength(0);
                cPersonaBuscadora.Width = new GridLength(0);
            }
            else
            {
                cGenero.Width = new GridLength(0);
            }
        }
    }
}
