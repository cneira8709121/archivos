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
    /// Lógica de interacción para H02_Encabezado02.xaml
    /// </summary>
    public partial class H02_Encabezado02 : UserControl, IEncabezadoImpresion
    {
        public H02_Encabezado02()
        {
            InitializeComponent();
            if (RUV.I.DeclaracionActual.VersionFUD == 1)
            {
                cSexo.Width = new GridLength(0);
                cOrientacion.Width = new GridLength(0);
                cIdentidad.Width = new GridLength(0);
                cPadreCabeza.Width = new GridLength(0);
                cCampesinado.Width = new GridLength(0);
                cPersonaBuscadora.Width = new GridLength(0);
            }
            else
            {
                cGenero.Width = new GridLength(0);
            }
        }

        public bool RepiteEnCadaPagina
        { get { return true; } }

        public int Orden
        { get { return 2; } }

    }
}
