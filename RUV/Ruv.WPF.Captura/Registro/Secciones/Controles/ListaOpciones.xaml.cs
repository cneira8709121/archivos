using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.WPF.Captura.Infrastructure;
using System.ComponentModel;
using ServiceStack.Text;

namespace Ruv.WPF.Captura.Registro.Secciones
{
    /// <summary>
    /// Lógica de interacción para ListaOpciones.xaml
    /// </summary>
    public partial class ListaOpciones : UserControl
    {
        #region CONSTRUCTOR

        public ListaOpciones()
        {
            InitializeComponent();
            NombreGrupo = string.Format("Grp{0}", Guid.NewGuid().ToString().Substring(0, 5));
            //DataContext = this;
        }

        #endregion

        #region PROPIEDADES

        public Orientation Orientacion
        {
            get { return wp01.Orientation; }
            set { wp01.Orientation = value; }
        }

        private bool _UsarDosColumnas = true;
        /// <summary>
        /// Cuando la orientación es vertical, esta propiedad define si la distribución se hace
        /// en dos columnas.
        /// </summary>
        public bool UsarDosColumnas
        {
            get { return _UsarDosColumnas; }
            set { _UsarDosColumnas = value; }
        }

        private eTipoOpciones _TipoOpciones = eTipoOpciones.Unica;
        public eTipoOpciones TipoOpciones
        {
            get { return _TipoOpciones; }
            set { _TipoOpciones = value; }
        }

        private eTipoParametros _TipoParametros = eTipoParametros.Ninguno;
        /// <summary>
        /// Los tipos de parámetros a desplegar.
        /// </summary>
        public eTipoParametros TipoParametros
        {
            get { return _TipoParametros; }
            set
            {
                _TipoParametros = value;
                PintarOpciones();
            }
        }

        private bool _UsarDobleColumna = false;
        /// <summary>
        /// True: Despliega el resultado en dos columnas si la orientación es vertical.
        /// </summary>
        public bool UsarDobleColumna
        {
            get { return _UsarDobleColumna; }
            set { _UsarDobleColumna = value; }
        }

        /// <summary>
        /// Conjunto de las entidades a mostrar.
        /// </summary>
        public eGruposParametros Conjunto
        {
            get { return (eGruposParametros)GetValue(ConjuntoProperty); }
            set { SetValue(ConjuntoProperty, value); }
        }
        public static readonly DependencyProperty ConjuntoProperty =
            DependencyProperty.Register("Conjunto", typeof(eGruposParametros),
            typeof(ListaOpciones),
            new UIPropertyMetadata(eGruposParametros.Ninguna, ConjuntoChangedCallback));

        /// <summary>
        /// El texto ingresado en la casilla otro.
        /// </summary>
        public string TextoOtro
        {
            get { return (string)GetValue(TextoOtroProperty); }
            set { SetValue(TextoOtroProperty, value); }
        }

        public static readonly DependencyProperty TextoOtroProperty =
            DependencyProperty.Register("TextoOtro", typeof(string),
            typeof(ListaOpciones), new UIPropertyMetadata(null, TextoOtroChangedCallback));

        static void TextoOtroChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private eTipoListadoCaptura listado;

        public eTipoListadoCaptura Listado
        {
            get { return listado; }
            set
            {
                listado = value;
                PintarOpciones();
            }
        }

        private int _MaxCaracteresOtro;
        /// <summary>
        /// Cantidad maxima de caracteres para el campo Otro
        /// </summary>
        public int MaxCaracteresOtro
        {
            get { return _MaxCaracteresOtro; }
            set { _MaxCaracteresOtro = value; }
        }


        /// <summary>
        /// Todos los ToggleBoxes.
        /// </summary>
        public List<ToggleButton> ListaTBs
        {
            get
            {
                return wp01.Children.OfType<ToggleButton>()
              .Union(sp01.Children.OfType<ToggleButton>()).ToList();
            }
        }

        private bool _IncluirNumero = false;
        /// <summary>
        /// True: Mostrar junto al nombre de cada elemento el número correspondiente.
        /// Default: False.
        /// </summary>
        public bool IncluirNumero
        {
            get { return _IncluirNumero; }
            set { _IncluirNumero = value; }
        }

        #endregion

        #region EVENTO DE CHEQUEO EN CUALQUIER OPCIÓN

        /// <summary>
        /// Se lanza cuando se siente un cambio en alguna opción.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CambioEnOpcion(object sender, RoutedEventArgs e)
        {
            if (ActualizandoManualmente
              || (TipoOpciones == eTipoOpciones.Multiple && ValoresUsuario == null)
              ) return;

            LanzoEventoCheckedUnchecked = true;

            ToggleButton TB = sender as ToggleButton;
            clsElementoSeleccionable Dato = TB.Tag as clsElementoSeleccionable;
            if (TipoOpciones == eTipoOpciones.Multiple)
                ActualizarSeleccionMultiple(TB, Dato);
            else if (TB.IsChecked.Value)
                ActualizarSeleccionUnica(Dato.Id);

            //if (CambioEnOpciones != null)
            //  CambioEnOpciones(this, null);
        }


        /// <summary>
        /// Se lanza cuando se hace algún cambio en las opciones del control.
        /// </summary>
        //public event EventHandler CambioEnOpciones;

        #endregion

        #region LA LISTA DE VALORES DEL USUARIO

        /// <summary>
        /// Actualiza la fuente de datos del usuario en base a un cambio en la selección.
        /// </summary>
        /// <param name="TB"></param>
        /// <param name="Dato"></param>
        private void ActualizarSeleccionMultiple(ToggleButton TB, clsElementoSeleccionable Dato)
        {
            if (ActualizandoManualmente) return;
            ActualizandoManualmente = true;

            var Elemento = ValoresUsuario
              .Select((x, index) => new { Valor = x, Posicion = index })
              .Where(x => x.Valor == Dato.Id)
              .Select(x => x.Posicion);

            if (TB.IsChecked.Value && !Elemento.Any())
            {
                ValoresUsuario.Add(Dato.Id);
            }
            else if (!TB.IsChecked.Value && Elemento.Any())
                ValoresUsuario.RemoveAt(Elemento.ElementAt(0));

            //Inicio - Validaciones Discapacidades y Hechos Victimizantes
            //Si en la lista de opciones, se selecciono "Ninguna" o "No Victima" debe desmarcar las demas opciones (esto a su vez funciona como si bloqueara las demas opciones)
            //Aplica para Discapacidades y Hechos Victimizantes.
            //Discapacidades
            if (ListaTBs.Any(x => (x.Tag as clsElementoSeleccionable).Id == (int)eDiscapacidades.Ninguna
                                    && (x.Tag as clsElementoSeleccionable).Seleccionado == true))
            {
                ValoresUsuario.Clear();
                ValoresUsuario.Add((int)eDiscapacidades.Ninguna);
                ListaTBs.ForEach(x => x.IsChecked = false);
                var TBNinguno = ListaTBs.Where(x => (x.Tag as clsElementoSeleccionable).Id == (int)eDiscapacidades.Ninguna).FirstOrDefault();
                TBNinguno.IsChecked = true;
            }

            if (ListaTBs.Any(x => (x.Tag as clsElementoSeleccionable).Id == (int)eDiscapacidades.NoSabe_NS_NoResponde_NR
                                    && (x.Tag as clsElementoSeleccionable).Seleccionado == true))
            {
                ValoresUsuario.Clear();
                ValoresUsuario.Add((int)eDiscapacidades.NoSabe_NS_NoResponde_NR);
                ListaTBs.ForEach(x => x.IsChecked = false);
                var TBNS = ListaTBs.Where(x => (x.Tag as clsElementoSeleccionable).Id == (int)eDiscapacidades.NoSabe_NS_NoResponde_NR).FirstOrDefault();
                TBNS.IsChecked = true;
            }
            if (ListaTBs.Any(x => (x.Tag as clsElementoSeleccionable).Id == (int)eDiscapacidades.NoSabe_NS
                                    && (x.Tag as clsElementoSeleccionable).Seleccionado == true))
            {
                ValoresUsuario.Clear();
                ValoresUsuario.Add((int)eDiscapacidades.NoSabe_NS);
                ListaTBs.ForEach(x => x.IsChecked = false);
                var TBNS = ListaTBs.Where(x => (x.Tag as clsElementoSeleccionable).Id == (int)eDiscapacidades.NoSabe_NS).FirstOrDefault();
                TBNS.IsChecked = true;
            }

            if (ListaTBs.Any(x => (x.Tag as clsElementoSeleccionable).Id == (int)eDiscapacidades.NoResponde_NR
                                    && (x.Tag as clsElementoSeleccionable).Seleccionado == true))
            {
                ValoresUsuario.Clear();
                ValoresUsuario.Add((int)eDiscapacidades.NoResponde_NR);
                ListaTBs.ForEach(x => x.IsChecked = false);
                var TBNS = ListaTBs.Where(x => (x.Tag as clsElementoSeleccionable).Id == (int)eDiscapacidades.NoResponde_NR).FirstOrDefault();
                TBNS.IsChecked = true;
            }


            //Hechos Victimizantes.
            if (ListaTBs.Any(x => (x.Tag as clsElementoSeleccionable).Id == (int)eHechosVictimizantes.NoVictima_13
                                    && (x.Tag as clsElementoSeleccionable).Seleccionado == true))
            {
                ValoresUsuario.Clear();
                ValoresUsuario.Add((int)eHechosVictimizantes.NoVictima_13);
                ListaTBs.ForEach(x => x.IsChecked = false);
                var TBNoVictima = ListaTBs.Where(x => (x.Tag as clsElementoSeleccionable).Id == (int)eHechosVictimizantes.NoVictima_13).FirstOrDefault();
                TBNoVictima.IsChecked = true;
            }
            //Fin -  Validaciones Discapacidades y Hechos Victimizantes

            ReportarBindingDeLista(ePropiedadDeLista.ValoresUsuario);
            ActualizandoManualmente = false;
        }

        /// <summary>
        /// La lista de valores que el usuario presenta.
        /// </summary>
        public List<int> ValoresUsuario
        {
            get { return (List<int>)GetValue(ValoresUsuarioProperty); }
            set { SetValue(ValoresUsuarioProperty, value); }
        }

        public static readonly DependencyProperty ValoresUsuarioProperty =
            DependencyProperty.Register("ValoresUsuario", typeof(List<int>),
            typeof(ListaOpciones), new UIPropertyMetadata(null, ValoresUsuarioCallback));

        static void ValoresUsuarioCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListaOpciones Este = d as ListaOpciones;
            if (Este.Pintado)
            {
                Este.ActualizarFuenteAPantalla();
            }
        }

        /// <summary>
        /// Verdadero: Se están actualizando los checks en base a los datos del usuario.
        /// </summary>
        bool ActualizandoManualmente;

        /// <summary>
        /// Toma la lista de valores del usuario y las pinta (activa los checkboxes) en pantalla.
        /// </summary>
        void ActualizarFuenteAPantalla()
        {
            if (ValoresUsuario == null) return;
            ActualizandoManualmente = true;

            //var ListaOpciones = wp01.Children.OfType<ToggleButton>()
            //  .Union(sp01.Children.OfType<ToggleButton>()).ToList();

            if (TipoOpciones == eTipoOpciones.Multiple)
            {
                // Comenzar desmarcándolos todos en caso de selección múltiple.
                ListaTBs.ForEach(x => x.IsChecked = false);
            }

            foreach (var item in ValoresUsuario)
            {
                var TB = ListaTBs
                  .Where(x => (x.Tag as clsElementoSeleccionable).Id == item).FirstOrDefault();
                if (TB != null)
                {
                    TB.IsChecked = true;
                }
            }

            ActualizandoManualmente = false;
        }

        #endregion

        #region EL VALOR DEL USUARIO CUANDO ES DE SELECCIÓN ÚNICA

        /// <summary>
        /// El valor selecionado cuando las opciones son de selección única.
        /// Retorna nulo si no se ha seleccionado nada.
        /// </summary>
        public int? ValorSeleccionUnica
        {
            get { return (int?)GetValue(ValorSeleccionUnicaProperty); }
            set { SetValue(ValorSeleccionUnicaProperty, value); }
        }

        public static readonly DependencyProperty ValorSeleccionUnicaProperty =
            DependencyProperty.Register("ValorSeleccionUnica", typeof(int?),
            typeof(ListaOpciones), new UIPropertyMetadata(null, ValorSeleccionUnicaChangedCallback));

        static void ValorSeleccionUnicaChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var LO = d as ListaOpciones;
            LO.ActualizarSeleccionUnica((int?)e.NewValue);
        }

        /// <summary>
        /// Actualiza la fuente de datos del usuario en base a un cambio en la selección.
        /// </summary>
        /// <param name="TB"></param>
        /// <param name="Dato"></param>
        void ActualizarSeleccionUnica(int? valor)
        {
            if (ActualizandoManualmente) return;
            ActualizandoManualmente = true;

            if (valor.HasValue)
            {
                ValorSeleccionUnica = valor.Value;
                // Si el check no está seleccionado, seleccionarlo.
                var TB = ListaTBs.Where(x => (x.Tag as clsElementoSeleccionable).Id == valor.Value).FirstOrDefault();
                if (TB != null && !TB.IsChecked.Value)
                {
                    TB.IsChecked = true;
                }
            }
            else
            {
                ListaTBs.ForEach(x => x.IsChecked = false);
            }

            ActualizandoManualmente = false;
        }

        #endregion

        /// <summary>
        /// Se ejecuta cuando se cambia el conjunto a mostrar.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        static void ConjuntoChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ListaOpciones).PintarOpciones();
        }

        /// <summary>
        /// Genera datos temporales para mostrar en modo de diseño.
        /// </summary>
        /// <returns></returns>
        List<clsElementoSeleccionable> ObtenerListaOpciones()
        {
            var Resultado = new List<clsParametroGeneral>();


            if (TipoParametros == eTipoParametros.SiNo)
            {
                Resultado.Add(new clsParametroGeneral { Id = 1, Nombre = "Si", EsOtro = false });
                Resultado.Add(new clsParametroGeneral { Id = 0, Nombre = "No", EsOtro = false });
            }
            else if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                if (Conjunto != eGruposParametros.Ninguna)
                {
                    // Si se especificó el conjunto, darle prelación sobre el tipo de parámetro.
                    Resultado = RUV.I.InfoGeneral.ListaDetallesGrupoParam(Conjunto).OrderBy(x => x.Numero).ToList();
                }
                else // De lo contrario se asume que se solicitan parámetros no agrupados.
                {
                    if (TipoParametros == eTipoParametros.HechosVictimizantes || TipoParametros == eTipoParametros.DiscapacidadEnActividades)
                    {
                        Resultado = (from p in RUV.I.InfoGeneral.ListaParametros
                                     orderby CalcularOrdenElemento(p), p.Nombre
                                     where p.Tipo == TipoParametros
                                     select p).OrderBy(x => x.Numero).ToList();

                        
                    }
                    else if (TipoParametros == eTipoParametros.CausalesTodos ||
                             TipoParametros == eTipoParametros.CausalesLiderRadicacion ||
                             TipoParametros == eTipoParametros.CausalesCriticaN ||
                             TipoParametros == eTipoParametros.CausalesGlosas ||
                             TipoParametros == eTipoParametros.CausalesValoracion)
                    {
                        IEnumerable<clsCausal> eCausales = RUV.I.InfoGeneral.ListaCausales.Where(x => x.EParametroTipoCausal == eTipoParametros.CausalesTodos);

                        if (eCausales == null) return null;

                        if (TipoParametros == eTipoParametros.CausalesTodos)
                            return eCausales.Select(x => new clsElementoSeleccionable
                            {
                                Id = x.NId,
                                Texto = x.CNombre,
                                EsOtro = false,
                                Seleccionado = false
                            }).ToList();

                        return RUV.I.InfoGeneral.ListaCausales.Where(x => x.EParametroTipoCausal == TipoParametros)
                                .Concat(eCausales)
                                .Select(x => new clsElementoSeleccionable
                                {
                                    Id = x.NId,
                                    Texto = x.CNombre,
                                    EsOtro = false,
                                    Seleccionado = false
                                }).ToList();
                    }
                    else if (TipoParametros == eTipoParametros.PreguntaCriticaN)
                    {
                        IEnumerable<clsPreguntaCriticaN> ePreguntas = RUV.I.InfoGeneral.PreguntasCriticaN;
                        if (ePreguntas == null)
                            return null;
                        return ePreguntas.Select(x => new clsElementoSeleccionable
                        {
                            Id = x.NId,
                            Texto = x.CNombre,
                            EsOtro = false,
                            Seleccionado = false
                        }).ToList();
                    }
                    else
                    {

                        Resultado = (from p in RUV.I.InfoGeneral.ListaParametros
                                     orderby CalcularOrdenElemento(p), p.Nombre
                                     where p.Tipo == TipoParametros
                                     select p).ToList();
                    }
                }
            }
            var ResultadoParametro = new List<clsParametroGeneral>();
            foreach (var item in Resultado)
            {
                if (!string.IsNullOrEmpty(item.Valor))
                {
                    try
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
                    catch
                    {
                        Console.Write("No tiene parametro extendidos de FUD para el listado de parametros " + TipoParametros.ToString());
                    }
                }
            }
            if(ResultadoParametro.Count > 0)
                Resultado = ResultadoParametro;
            return Resultado.Select(x =>
              new clsElementoSeleccionable
              {
                  Id = x.Id.Value,
                  Texto = x.Nombre + (IncluirNumero ? string.Format(" ({0})", x.Numero) : ""),
                  EsOtro = x.EsOtro,
                  Seleccionado = false,
                  Numero = x.Numero
              }).ToList();
        }

        /// <summary>
        /// Ayuda a determinar el orden adecuado de los elementos.
        /// Al final debe ir el Otro, antes los llamados "No sabe o no responde" y primero los demás.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int CalcularOrdenElemento(clsParametroGeneral item)
        {
            if (item.EsOtro)
                return 2;
            else if (
              item.Nombre.ToLower().Contains("no responde")
              || item.Nombre.ToLower().Contains("no sabe"))
                return 1;
            else
                return 0;
        }

        #region PINTAR LAS OPCIONES

        string NombreGrupo;

        /// <summary>
        /// Verdadero: Este control ya ha pintado su contenido.
        /// </summary>
        bool Pintado = false;

        /// <summary>
        /// Sirve para determinar si hay un elemento "¿Otro?".
        /// </summary>
        bool HayElementoOtro = false;

        TextBox CajaOtroTexto;

        /// <summary>
        /// Crea las opciones de selección.
        /// </summary>
        void PintarOpciones()
        {
            Pintado = false;
            HayElementoOtro = false;
            double TamañoTexto = (double)App.Current.Resources["TamañoTextoMedio"];

            List<clsElementoSeleccionable> Lista = ObtenerListaOpciones();
            wp01.Children.Clear();
            ToggleButton TB = null;
            foreach (var item in Lista)
            {
                if (!item.EsOtro)
                {
                    if (TipoOpciones == eTipoOpciones.Unica)
                        TB = new RadioButton() { GroupName = NombreGrupo };
                    else
                        TB = new CheckBox();
                    TB.Checked += CambioEnOpcion;
                    TB.Unchecked += CambioEnOpcion;
                    TB.Click += ToggleButton_Click;
                    TB.Tag = item;

                    TextBlock Texto = new TextBlock
                    {
                        Text = item.Texto,
                        TextWrapping = System.Windows.TextWrapping.Wrap,
                        FontSize = TamañoTexto
                    };

                    TB.Content = Texto;
                    Extensiones.BindingEstablecer(
                      item, "Seleccionado", TB, RadioButton.IsCheckedProperty, BindingMode.TwoWay);
                    wp01.Children.Add(TB);
                }
                else
                {
                    HayElementoOtro = true;
                    StackPanel SP = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center
                    };

                    SP.Children.Add(new TextBlock
                    {
                        Text = item.Texto,
                        FontSize = TamañoTexto
                    });

                    // El texto '¿Cuál?' con su caja de texto para que pueda deshabilitarse.
                    StackPanel SP2 = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        VerticalAlignment = System.Windows.VerticalAlignment.Center
                    };
                    SP2.Children.Add(new TextBlock
                    {
                        Text = "¿Cuál?",
                        FontSize = TamañoTexto,
                        Margin = new Thickness(5d, 0d, 5d, 0d)
                    });
                    TextBox CajaOtro = new TextBox { MinWidth = 200d, TextWrapping = TextWrapping.Wrap, MaxWidth = 400d };
                    CajaOtro.MaxLength = MaxCaracteresOtro;
                    SP2.Children.Add(CajaOtro);
                    SP.Children.Add(SP2);

                    CajaOtroTexto = CajaOtro;
                    Extensiones.BindingEstablecer(
                      this, "TextoOtro", CajaOtro, TextBox.TextProperty,
                      BindingMode.TwoWay, reportarErroresDeValidacion: true);

                    if (TipoOpciones == eTipoOpciones.Unica)
                        TB = new RadioButton() { GroupName = NombreGrupo };
                    else
                        TB = new CheckBox();
                    TB.Checked += CambioEnOpcion;
                    TB.Unchecked += CambioEnOpcion;
                    TB.Click += ToggleButton_Click;
                    TB.Tag = item;
                    TB.Content = SP;

                    // Binding para esconder el '¿Cuál?.
                    var BinVisi = Extensiones.BindingEstablecer(
                      TB, "IsChecked", SP2,
                      StackPanel.VisibilityProperty,
                      BindingMode.OneWay,
                      this.FindResource("BoolVisiConverter") as IValueConverter);

                    wp01.Children.Add(TB);
                }
            }
            Pintado = true;
        }

        #endregion

        #region QUITAR CHEQUEO EN CASOS DE SELECCIÓN ÚNICA

        /// <summary>
        /// Permite saber si se lanzaron primero los eventos Checked o Unchecked.
        /// </summary>
        Boolean LanzoEventoCheckedUnchecked;

        /// <summary>
        /// Se debe permitir que cuando haya selección única, se pueda quitar la selección
        /// actual volviendo a hacer click sobre ella.
        /// Este evento se lanza después de los eventos Checked y/o Unchecked.
        /// En método quita la selección actual dejándola en nulo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (TipoOpciones == eTipoOpciones.Unica
              && !LanzoEventoCheckedUnchecked)
            {
                RadioButton RB = sender as RadioButton;
                if (RB.IsChecked.Value)
                {
                    RB.IsChecked = false;
                    ValorSeleccionUnica = null;
                }
            }
            LanzoEventoCheckedUnchecked = false;
        }


        #endregion

        #region REFORMATEO PARA LOS CASOS

        bool ReformateoRealizado = false;

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (Orientacion == Orientation.Vertical
              && UsarDosColumnas
              && !ReformateoRealizado
              && wp01.Children.Count > 2)
            {
                var TamañoActual = wp01.RenderSize;
                ReformateoRealizado = true;
                wp01.MaxHeight = Convert.ToInt32((TamañoActual.Height / 2) + 28);

                // Si hay opcion '¿Otros?' moverla de sitio.
                if (HayElementoOtro)
                {
                    int Pos = wp01.Children.Count - 1;
                    var Otro = wp01.Children[Pos];
                    (Otro as FrameworkElement).Margin = new Thickness(3d);
                    wp01.Children.RemoveAt(Pos);
                    sp01.Children.Add(Otro);
                }

                var AnchoOpciones = (this.RenderSize.Width / 2d) * 0.97d;
                foreach (FrameworkElement item in wp01.Children)
                {
                    item.MaxWidth = AnchoOpciones;
                }
            }
        }

        #endregion

        #region REPORTAR LA ACTUALIZACIÓN DEL BINDING

        /// <summary>
        /// Dado que las listas no reportan cambios en sus propiedades,
        /// aqui se ubica el nombre de la propiedad y se lanza el evento 
        /// "ReportarCambioPropiedad".
        /// </summary>
        /// <param name="propiedad"></param>
        void ReportarBindingDeLista(ePropiedadDeLista propiedad)
        {
            Binding Enlace1 = null;
            Binding Enlace2 = null;

            switch (propiedad)
            {
                case ePropiedadDeLista.ValoresUsuario:
                    Enlace1 = BindingOperations.GetBinding(this, ListaOpciones.ValoresUsuarioProperty);
                    Enlace2 = BindingOperations.GetBinding(this, ListaOpciones.TextoOtroProperty);
                    break;

                case ePropiedadDeLista.ValorDeSeleccionUnica:
                    Enlace1 = BindingOperations.GetBinding(this, ListaOpciones.ValorSeleccionUnicaProperty);
                    Enlace2 = BindingOperations.GetBinding(this, ListaOpciones.TextoOtroProperty);
                    break;
            }

            if (Enlace1 != null)
            {
                Ruv.Infrastructure.Crosscutting.Common.Entidades.clsEntidadBase EB =
                  DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsEntidadBase;
                if (EB != null)
                {
                    if (Enlace1 != null)
                        EB.ReportarCambioPropiedad(Enlace1.Path.Path);
                    if (Enlace2 != null)
                        EB.ReportarCambioPropiedad(Enlace2.Path.Path);
                }

            }
        }

        enum ePropiedadDeLista
        {
            ValorDeSeleccionUnica,
            ValoresUsuario
        }

        #endregion

        #region CONTROL DE PRESENTACIÓN DEL FOCO

        /// <summary>
        /// Muestra u oculta la marca que indica el foco.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool ConFoco = Convert.ToBoolean(e.NewValue);
            recFoco.Visibility = ConFoco ? Visibility.Visible : Visibility.Hidden;
            ResetearNumero();
            //if (ConFoco)
            //  borNumero.Focus();
        }

        #endregion

    }


}
