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
    public partial class A08_Detalle01 : UserControl, IEncabezadoImpresion
    {
        public A08_Detalle01()
        {
            InitializeComponent();
            if (RUV.I.DeclaracionActual.VersionFUD == 2)
            {
                Columna14.SetValue(Grid.ColumnProperty, 11);
                Columna15.SetValue(Grid.ColumnProperty, 12);
                Columna16.SetValue(Grid.ColumnProperty, 13);
                Columna17.SetValue(Grid.ColumnProperty, 14);
                Columna18.SetValue(Grid.ColumnProperty, 15);
            }
        }

        public bool RepiteEnCadaPagina
        {
            get { return true; }
        }

        public int Orden
        {
            get { return 6; }
        }
    }
}
