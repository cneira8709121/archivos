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
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.General;
using ServiceStack.Text;

namespace Ruv.WPF.Captura.Impresion
{
    /// <summary>
    /// Interaction logic for DatoGrillaNumericaColumna.xaml
    /// </summary>
    public partial class DatoGrillaNumericaColumna : UserControl
    {
        public DatoGrillaNumericaColumna()
        {
            InitializeComponent();
        }

        static Thickness PaddingValor = new Thickness(2d);
        static Thickness MargenValor = new Thickness(1d);
        static SolidColorBrush ColorFondo = new SolidColorBrush(Colors.Gray);
        static SolidColorBrush ColorTexto = new SolidColorBrush(Colors.White);
        static GridLength AnchoColumna = new GridLength(1d, GridUnitType.Star);
        static SolidColorBrush SinColor = new SolidColorBrush(Colors.Transparent);
        static SolidColorBrush GrisSuave = new SolidColorBrush(Colors.LightGray);


        //string _PosiblesValores;
        ///// <summary>
        ///// Lista de valores separados por comas, 
        ///// cada renglón está separado con punto y coma.
        ///// </summary>
        //public string PosiblesValores
        //{
        //  get { return null; }
        //  set
        //  {
        //    _PosiblesValores = value;
        //    PintarValores();
        //  }
        //}

        public List<int> ListaValores
        {
            get { return (List<int>)GetValue(ListaValoresProperty); }
            set { SetValue(ListaValoresProperty, value); }
        }

        public static readonly DependencyProperty ListaValoresProperty =
            DependencyProperty.Register("ListaValores", typeof(List<int>),
            typeof(DatoGrillaNumericaColumna), new UIPropertyMetadata(null, ListaValoresChanged));

        static void ListaValoresChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DatoGrillaNumericaColumna).PintarValores();
        }

        /// <summary>
        /// Pinta los valores del usuario.
        /// </summary>
        void PintarValores()
        {
            grdMain.ColumnDefinitions.Clear();
            grdMain.RowDefinitions.Clear();

            List<clsParametroGeneral> Params = null;

            if (Conjunto != eGruposParametros.Ninguna)
                Params = RUV.I.InfoGeneral.ListaDetallesGrupoParam(Conjunto)
                  .OrderBy(x => x.Numero).ToList();
            else if (TiposParametros != eTipoParametros.Ninguno)
                Params = RUV.I.InfoGeneral.ListaParametros
                  .Where(x => x.Tipo == TiposParametros)
                  .OrderBy(x => x.Numero).ToList();
            else
                return;

            // Preparar las grillas.
            int MaxColumnas = Convert.ToInt32(Math.Round(Params.Count / 2d, MidpointRounding.AwayFromZero));

            int FilaActual = 0;
            int ColumnaActual = 0;

            if (TiposParametros == eTipoParametros.DiscapacidadEnActividades)
            {
                var ResultadoParametro = new List<clsParametroGeneral>();
                foreach (var item in Params)
                {
                    if (!string.IsNullOrEmpty(item.Valor))
                    {
                        var variableDiscapacidades = JsonSerializer.DeserializeFromString<clsParametrosExtendidosVersionFUD>(item.Valor);
                        var VersionFUD = RUV.I.DeclaracionActual.VersionFUD;
                        if (VersionFUD == 1 && variableDiscapacidades.fud1)
                        {
                            ResultadoParametro.Add(item);
                        }
                        if (VersionFUD == 2 && variableDiscapacidades.fud2)
                        {
                            ResultadoParametro.Add(item);
                        }
                    }
                }
                Params = ResultadoParametro;
            }
            foreach (var Param in Params)
            {
                
                TextBlock TB = new TextBlock()
                {
                    Text = Param.Numero.ToString(),
                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                    TextAlignment = TextAlignment.Center,
                    FontWeight = FontWeights.Bold,
                    Padding = PaddingValor,
                    Margin = MargenValor,
                    Background = ColorFondo,
                    Foreground = ColorTexto
                };
                Grid.SetColumn(TB, ColumnaActual);
                Grid.SetRow(TB, FilaActual);

                grdMain.Children.Add(TB);

                //CAMBIO: Int?
                if (Param.Id.HasValue && !ListaValores.Contains(Param.Id.Value))
                {
                    TB.Background = SinColor;
                    TB.Foreground = GrisSuave;
                }

                ColumnaActual++;
                if (ColumnaActual >= MaxColumnas)
                {
                    ColumnaActual = 0;
                    FilaActual = 1;
                }
            }

            for (int i = 0; i < MaxColumnas; i++)
                grdMain.ColumnDefinitions.Add(new ColumnDefinition() { Width = AnchoColumna });
            grdMain.RowDefinitions.Add(new RowDefinition());
            grdMain.RowDefinitions.Add(new RowDefinition());


            //for (int FilaNum = 0; FilaNum < Params.Count; FilaNum++)
            //{
            //  string[] Columnas = Params[FilaNum].Split(',');
            //  MaxColumnas = Math.Max(MaxColumnas, Columnas.Length);
            //  grdMain.RowDefinitions.Add(new RowDefinition());
            //  for (int ColNum = 0; ColNum < Columnas.Length; ColNum++)
            //  {
            //    TextBlock TB = new TextBlock()
            //    {
            //      Text = Columnas[ColNum],
            //      VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
            //      TextAlignment = TextAlignment.Center,
            //      FontWeight = FontWeights.Bold,
            //      Padding = PaddingValor,
            //      Margin = MargenValor,
            //      Background = ColorFondo,
            //      Foreground = ColorTexto
            //    };
            //    Grid.SetColumn(TB, ColNum);
            //    Grid.SetRow(TB, FilaNum);
            //    grdMain.Children.Add(TB);
            //    if (!ListaValores.Contains(Convert.ToInt32(Columnas[ColNum])))
            //    {
            //      TB.Background = SinColor;
            //      TB.Foreground = GrisSuave;
            //    }
            //  }
            //}

            //for (int i = 0; i < MaxColumnas; i++)
            //  grdMain.ColumnDefinitions.Add(new ColumnDefinition()
            //  {
            //    Width = AnchoColumna
            //  });

            //Grid.SetRowSpan(borMain, Filas.Length);
            //Grid.SetColumnSpan(borMain, MaxColumnas);


            // _------------------------------------

            //if (string.IsNullOrWhiteSpace(_PosiblesValores)
            //  || ListaValores == null
            //  || !ListaValores.Any()) return;

            //string[] Filas = _PosiblesValores.Split(';');
            //int MaxColumnas = -1;
            //for (int FilaNum = 0; FilaNum < Filas.Length; FilaNum++)
            //{
            //  string[] Columnas = Filas[FilaNum].Split(',');
            //  MaxColumnas = Math.Max(MaxColumnas, Columnas.Length);
            //  grdMain.RowDefinitions.Add(new RowDefinition());
            //  for (int ColNum = 0; ColNum < Columnas.Length; ColNum++)
            //  {
            //    TextBlock TB = new TextBlock()
            //    {
            //      Text = Columnas[ColNum],
            //      VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
            //      TextAlignment = TextAlignment.Center,
            //      FontWeight = FontWeights.Bold,
            //      Padding = PaddingValor,
            //      Margin = MargenValor,
            //      Background = ColorFondo,
            //      Foreground = ColorTexto
            //    };
            //    Grid.SetColumn(TB, ColNum);
            //    Grid.SetRow(TB, FilaNum);
            //    grdMain.Children.Add(TB);
            //    if (!ListaValores.Contains(Convert.ToInt32(Columnas[ColNum])))
            //    {
            //      TB.Background = SinColor;
            //      TB.Foreground = GrisSuave;
            //    }
            //  }
            //}

            //for (int i = 0; i < MaxColumnas; i++)
            //  grdMain.ColumnDefinitions.Add(new ColumnDefinition()
            //  {
            //    Width = AnchoColumna
            //  });

            //Grid.SetRowSpan(borMain, Filas.Length);
            //Grid.SetColumnSpan(borMain, MaxColumnas);
        }

        private eGruposParametros _Conjunto = eGruposParametros.Ninguna;
        /// <summary>
        /// El conjunto de parámetros a utilizar.
        /// </summary>
        public eGruposParametros Conjunto
        {
            get { return _Conjunto; }
            set { _Conjunto = value; }
        }


        eTipoParametros _TiposParametros = eTipoParametros.Ninguno;
        /// <summary>
        /// El tipo de parámetros.
        /// </summary>
        public eTipoParametros TiposParametros
        {
            get { return _TiposParametros; }
            set { _TiposParametros = value; }
        }


    }
}
