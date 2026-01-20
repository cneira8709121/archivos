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
    public partial class A08_Encabezado02 : UserControl, IEncabezadoImpresion
    {
        public A08_Encabezado02()
        {
            InitializeComponent();
            if (RUV.I.DeclaracionActual.VersionFUD == 2)
            {
                SubTituloHomicidioMasacre.SetValue(Grid.ColumnSpanProperty, 3);
                Titulo_11.Visibility = Visibility.Collapsed;
                Titulo_12.Visibility = Visibility.Collapsed;
                Titulo_13.Visibility = Visibility.Collapsed;
                Titulo_14.SetValue(Grid.ColumnProperty, 11);
                Titulo_15.SetValue(Grid.ColumnProperty, 12);
                Titulo_16.SetValue(Grid.ColumnProperty, 13);
                SubTitulo_17.SetValue(Grid.ColumnProperty, 14);
                SubTitulo_17.SetValue(Grid.ColumnSpanProperty, 2);
                Titulo_17.SetValue(Grid.ColumnProperty, 14);
                Titulo_18.SetValue(Grid.ColumnProperty, 15);
            }
        }

        public bool RepiteEnCadaPagina
        {
            get { return true; }
        }

        public int Orden
        {
            get { return 5; }
        }
    }
}
