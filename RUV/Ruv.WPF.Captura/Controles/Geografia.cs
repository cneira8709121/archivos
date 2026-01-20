using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using Ruv.Infrastructure.Crosscutting.Common.General;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Controles
{
    /// <summary>
    /// Maneja los combos departamento & municipio.
    /// </summary>
    public class Geografia : Control
    {
        public delegate void MunicipioCambioHandler(object sender, EventArgs e);
        public event MunicipioCambioHandler MunicipioCambio;
        protected virtual void OnMunicipioCambio(EventArgs e)
        {
            // Verificar si hay suscriptores al evento antes de invocarlo.
            if (MunicipioCambio != null)
                MunicipioCambio(this, e);
        }
        public Geografia()
        {
            this.Focusable = false;
        }

        /// <summary>
        /// Control para determninar cuando esté listos los cuatro bindings.
        /// </summary>

        bool ConfiguracionManual = false;

        /// <summary>
        /// País que se preselecciona en el cargue inicial
        /// </summary>
        private const string PaisPreseleccionado = "COLOMBIA";
        /// <summary>
        /// Bindings[0] - Combo Departamento
        /// Bindings[1] - Combo Municipio
        /// Bindings[2] - Departamento Id
        /// Bindings[3] - Municipio Id
        /// Bindings[4] - Combo País
        /// Bindings[5] - Pais Id
        /// Bindings[6] - Texbox Indicativo
        /// Bindings[7] - Id Indicativo
        /// Bindings[6] - Combo Entidad Municipio
        /// Bindings[7] - Entidad Municipio Id
        /// </summary>
        /// <remarks>Los binding [6] y [7] deben ser dinámicos</remarks>
        bool[] Bindings = new bool[] { false, false, false, false, false, false };
        #region COMBO PAIS

        public ComboBox ComboPais
        {
            get { return (ComboBox)GetValue(ComboPaisProperty); }
            set { SetValue(ComboPaisProperty, value); }
        }

        public static readonly DependencyProperty ComboPaisProperty =
            DependencyProperty.Register("ComboPais", typeof(ComboBox),
            typeof(Geografia), new UIPropertyMetadata(null, ComboPaisCallback));

        static void ComboPaisCallback(
          DependencyObject d,
          DependencyPropertyChangedEventArgs e)
        {
            Geografia Geo = d as Geografia;
            if (e.NewValue == null) return;

            Geo.Bindings[4] = true;


            Geo.Configurar();

            if (Geo.ComboPais.ItemsSource == null)
            {
                Geo.ComboPais.ItemsSource = RUV.I.InfoGeneral.ListaPaises;
                Geo.ComboPais.SelectedValuePath = "Id";
                Geo.ComboPais.DisplayMemberPath = "Nombre";
            }

            if (RUV.I.Usuario != null)
            {
                //if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.RuvDigitador)) Geo.ComboPais.IsEnabled = false;
                //else Geo.ComboPais.IsEnabled = true;
                Geo.ComboPais.IsEnabled = true;
            }
        }

        #endregion

        #region COMBO DEPARTAMENTO

        public ComboBox ComboDepartamento
        {
            get { return (ComboBox)GetValue(ComboDepartamentoProperty); }
            set { SetValue(ComboDepartamentoProperty, value); }
        }

        public static readonly DependencyProperty ComboDepartamentoProperty =
            DependencyProperty.Register("ComboDepartamento", typeof(ComboBox),
            typeof(Geografia), new UIPropertyMetadata(null, ComboDepartamentoCallback));

        static void ComboDepartamentoCallback(
          DependencyObject d,
          DependencyPropertyChangedEventArgs e)
        {
            Geografia Geo = d as Geografia;
            if (e.NewValue == null) return;

            Geo.Bindings[0] = true;

            if (Geo.ComboDepartamento.ItemsSource == null)
            {
                //Geo.ComboDepartamento.ItemsSource = Sipod.I.InfoGeneral.ListaDepartamentos;       TODO: Revisar Pais
                Geo.ComboDepartamento.SelectedValuePath = "Id";
                Geo.ComboDepartamento.DisplayMemberPath = "Nombre";
            }

            Geo.Configurar();
        }

        #endregion

        #region COMBO MUNICIPIO

        public ComboBox ComboMunicipio
        {
            get { return (ComboBox)GetValue(ComboMunicipioProperty); }
            set { SetValue(ComboMunicipioProperty, value); }
        }

        public static readonly DependencyProperty ComboMunicipioProperty =
            DependencyProperty.Register("ComboMunicipio", typeof(ComboBox),
            typeof(Geografia), new UIPropertyMetadata(null, ComboMunicipioCallback));

        static void ComboMunicipioCallback(
          DependencyObject d,
          DependencyPropertyChangedEventArgs e)
        {
            Geografia Geo = d as Geografia;
            if (e.NewValue == null) return;

            Geo.Bindings[1] = true;

            if (string.IsNullOrWhiteSpace(Geo.ComboMunicipio.SelectedValuePath))
            {
                Geo.ComboMunicipio.SelectedValuePath = "Id";
                Geo.ComboMunicipio.DisplayMemberPath = "Nombre";
            }

            Geo.Configurar();
        }

        #endregion

        #region COMBO ENTIDAD MUNICIPIO

        public ComboBox ComboEntidadMunicipio
        {
            get { return (ComboBox)GetValue(ComboEntidadMunicipioProperty); }
            set { SetValue(ComboEntidadMunicipioProperty, value); }
        }

        public static readonly DependencyProperty ComboEntidadMunicipioProperty =
            DependencyProperty.Register("ComboEntidadMunicipio", typeof(ComboBox),
            typeof(Geografia), new UIPropertyMetadata(null, ComboEntidadMunicipioCallback));

        static void ComboEntidadMunicipioCallback(
          DependencyObject d,
          DependencyPropertyChangedEventArgs e)
        {
            Geografia Geo = d as Geografia;
            if (e.NewValue == null) return;

            if (Geo.Bindings.Length <= 6)
            {
                bool[] binding = new bool[] { false, false };
                Geo.Bindings = Geo.Bindings.Concat(binding).ToArray();
            }
            Geo.Bindings[6] = true;

            if (string.IsNullOrWhiteSpace(Geo.ComboEntidadMunicipio.SelectedValuePath))
            {
                Geo.ComboEntidadMunicipio.SelectedValuePath = "NId";
                Geo.ComboEntidadMunicipio.DisplayMemberPath = "CNombreOtraEntidad";
            }

            Geo.Configurar();
        }

        #endregion

        #region TextBoxIndicativo

        public TextBox TextBoxIndicativo
        {
            get { return (TextBox)GetValue(TextBoxIndicativoProperty); }
            set { SetValue(TextBoxIndicativoProperty, value); }
        }

        public static readonly DependencyProperty TextBoxIndicativoProperty =
            DependencyProperty.Register("TextBoxIndicativo", typeof(TextBox),
            typeof(Geografia), new UIPropertyMetadata(null, TextBoxIndicativoCallback));

        static void TextBoxIndicativoCallback(
          DependencyObject d,
          DependencyPropertyChangedEventArgs e)
        {
            Geografia Geo = d as Geografia;
            if (e.NewValue == null) return;

            if (Geo.Bindings.Length <= 6)
            {
                bool[] binding = new bool[] { false, false };
                Geo.Bindings = Geo.Bindings.Concat(binding).ToArray();
            }
            Geo.Bindings[6] = true;


            if (!string.IsNullOrWhiteSpace(Geo.IndicativoMunicipio))
                Geo.TextBoxIndicativo.Text = Geo.IndicativoMunicipio;

            Geo.Configurar();
        }
        #endregion

        public bool? TodosLosPaises
        {
            get { return (bool?)GetValue(TodosLosPaisesProperty); }
            set { SetValue(TodosLosPaisesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TodosLosPaises.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TodosLosPaisesProperty =
            DependencyProperty.Register("TodosLosPaises", typeof(bool?), typeof(Geografia), new UIPropertyMetadata(true));


        #region Pais ID

        public Int64? PaisId
        {
            get { return (Int64?)GetValue(PaisIdProperty); }
            set { SetValue(PaisIdProperty, value); }
        }

        public static readonly DependencyProperty PaisIdProperty =
            DependencyProperty.Register("PaisId", typeof(Int64?),
            typeof(Geografia), new UIPropertyMetadata(-1000L, PaisIdCallback));

        static void PaisIdCallback(
         DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            var Geo = (d as Geografia);
            Geo.Bindings[5] = true;

            Geo.Configurar();

            if (Geo.ConfiguracionManual) return;

            Geo.ConfiguracionManual = true;
            if (Geo.Configurado)
            {
                // Cambiar manualmente el Pais sobre el combo.
                Geo.ComboPais.SelectionChanged -= Geo.ComboPais_SelectionChanged;
                Geo.ComboDepartamento.SelectionChanged -= Geo.ComboDepartamento_SelectionChanged;
                Geo.ComboMunicipio.SelectionChanged -= Geo.ComboMunicipio_SelectionChanged;
                if (Geo.ComboEntidadMunicipio != null)
                    Geo.ComboEntidadMunicipio.SelectionChanged -= Geo.ComboEntidadMunicipio_SelectionChanged;

                Int64? NuevoValorPais = (Int64?)e.NewValue;

                if (!NuevoValorPais.HasValue)
                {
                    // El Pais es nulo.
                    Geo.ComboPais.SelectedItem = null;
                    Geo.ComboDepartamento.ItemsSource = null;
                    Geo.ComboMunicipio.ItemsSource = null;
                    if (Geo.ComboEntidadMunicipio != null)
                        Geo.ComboEntidadMunicipio.ItemsSource = null;
                }
                else
                {
                    // Se conoce el Pais.
                    Geo.ComboPais.SelectedValue = NuevoValorPais.Value;
                    var LosDptos = RUV.I.InfoGeneral.ListaDepartamentos(NuevoValorPais.Value);
                    Geo.ComboDepartamento.ItemsSource = LosDptos;

                    // Si hay departamento definido, seleccionarlo.
                    if (!Geo.DepartamentoId.HasValue || !LosDptos.Any(x => x.Id == Geo.DepartamentoId.Value))
                    {
                        Geo.ComboDepartamento.SelectedIndex = 0;
                        Geo.ComboMunicipio.SelectedIndex = 0;
                        if (Geo.ComboEntidadMunicipio != null)
                            Geo.ComboEntidadMunicipio.SelectedIndex = 0;
                    }
                    else
                        Geo.ComboDepartamento.SelectedValue = Geo.DepartamentoId.Value;

                    var Item = Geo.ComboDepartamento.SelectedItem as clsParametroDepartamento;
                    if (Item == null)
                    {
                        Geo.DepartamentoId = null;
                        Geo.MunicipioId = null;
                        if (Geo.ComboEntidadMunicipio != null)
                            Geo.ComboEntidadMunicipio.ItemsSource = null;
                    }
                    else
                    {
                        Geo.DepartamentoId = Item.Id;
                    }
                }

                Geo.ComboPais.SelectionChanged += Geo.ComboPais_SelectionChanged;
                Geo.ComboDepartamento.SelectionChanged += Geo.ComboDepartamento_SelectionChanged;
                Geo.ComboMunicipio.SelectionChanged += Geo.ComboMunicipio_SelectionChanged;
                if (Geo.ComboEntidadMunicipio != null)
                    Geo.ComboEntidadMunicipio.SelectionChanged += Geo.ComboEntidadMunicipio_SelectionChanged;
            }

            Geo.ConfiguracionManual = false;
        }

        #endregion

        #region DEPARTAMENTO ID

        public Int64? DepartamentoId
        {
            get { return (Int64?)GetValue(DepartamentoIdProperty); }
            set { SetValue(DepartamentoIdProperty, value); }
        }

        public static readonly DependencyProperty DepartamentoIdProperty =
            DependencyProperty.Register("DepartamentoId", typeof(Int64?),
            typeof(Geografia), new UIPropertyMetadata(-1000L, DepartamentoIdCallback));

        static void DepartamentoIdCallback(
         DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            var Geo = (d as Geografia);
            Geo.Bindings[2] = true;
            Geo.Configurar();

            if (Geo.ConfiguracionManual) return;

            Geo.ConfiguracionManual = true;

            if (Geo.Configurado)
            {
                // Cambiar manualmente el departamento sobre el combo.
                Geo.ComboDepartamento.SelectionChanged -= Geo.ComboDepartamento_SelectionChanged;
                Geo.ComboMunicipio.SelectionChanged -= Geo.ComboMunicipio_SelectionChanged;
                if (Geo.ComboEntidadMunicipio != null)
                    Geo.ComboEntidadMunicipio.SelectionChanged -= Geo.ComboEntidadMunicipio_SelectionChanged;

                Int64? NuevoValorDepto = (Int64?)e.NewValue;

                if (!NuevoValorDepto.HasValue)
                {
                    // El departamento es nulo.
                    Geo.ComboDepartamento.SelectedItem = null;
                    Geo.ComboMunicipio.ItemsSource = null;
                    if (Geo.ComboEntidadMunicipio != null)
                        Geo.ComboEntidadMunicipio.ItemsSource = null;
                }
                else
                {
                    // Se conoce el departamento.
                    Geo.ComboDepartamento.SelectedValue = NuevoValorDepto.Value;
                    var LosMcpios = RUV.I.InfoGeneral.ListaMunicipios(NuevoValorDepto.Value);
                    Geo.ComboMunicipio.ItemsSource = LosMcpios;

                    // Si hay municipio definido, seleccionarlo.
                    if (!Geo.MunicipioId.HasValue || !LosMcpios.Any(x => x.Id == Geo.MunicipioId.Value))
                        Geo.ComboMunicipio.SelectedIndex = 0;
                    else
                        Geo.ComboMunicipio.SelectedValue = Geo.MunicipioId.Value;

                    var Item = Geo.ComboMunicipio.SelectedItem as clsParametroMunicipio;
                    if (Item == null)
                        Geo.MunicipioId = null;
                    else
                        Geo.MunicipioId = Item.Id;
                }

                Geo.ComboDepartamento.SelectionChanged += Geo.ComboDepartamento_SelectionChanged;
                Geo.ComboMunicipio.SelectionChanged += Geo.ComboMunicipio_SelectionChanged;
                if (Geo.ComboEntidadMunicipio != null)
                    Geo.ComboEntidadMunicipio.SelectionChanged += Geo.ComboEntidadMunicipio_SelectionChanged;
            }

            Geo.ConfiguracionManual = false;
        }

        #endregion

        #region MUNICIPIO ID

        public Int64? MunicipioId
        {
            get { return (Int64?)GetValue(MunicipioIdProperty); }
            set { SetValue(MunicipioIdProperty, value); }
        }

        public static readonly DependencyProperty MunicipioIdProperty =
            DependencyProperty.Register("MunicipioId", typeof(Int64?),
            typeof(Geografia), new UIPropertyMetadata(-1000L, MunicipioIdCallback));

        static void MunicipioIdCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var Geo = (d as Geografia);
            Geo.Bindings[3] = true;


            Geo.Configurar();
            if (Geo.ConfiguracionManual || !Geo.Configurado) return;

            Geo.ConfiguracionManual = true;

            Geo.ComboMunicipio.SelectionChanged -= Geo.ComboMunicipio_SelectionChanged;
            if (Geo.ComboEntidadMunicipio != null)
                Geo.ComboEntidadMunicipio.SelectionChanged -= Geo.ComboEntidadMunicipio_SelectionChanged;

            if (Geo.MunicipioId.HasValue)
            {
                if (Geo.ComboMunicipio.ItemsSource != null)
                {
                    var Mcpio = (Geo.ComboMunicipio.ItemsSource as List<clsParametroMunicipio>)
                    .FirstOrDefault(x => x.Id == (Int64?)e.NewValue);
                    Geo.ComboMunicipio.SelectedItem = Mcpio;
                }
            }

            Geo.ComboMunicipio.SelectionChanged += Geo.ComboMunicipio_SelectionChanged;
            if (Geo.ComboEntidadMunicipio != null)
                Geo.ComboEntidadMunicipio.SelectionChanged += Geo.ComboEntidadMunicipio_SelectionChanged;

            Geo.ConfiguracionManual = false;
        }


        #endregion

        #region ENTIDAD MUNICIPIO ID

        public short? EntidadMunicipioId
        {
            get { return (short?)GetValue(EntidadMunicipioIdProperty); }
            set { SetValue(EntidadMunicipioIdProperty, value); }
        }

        public static readonly DependencyProperty EntidadMunicipioIdProperty =
            DependencyProperty.Register("EntidadMunicipioId", typeof(short?),
            typeof(Geografia), new UIPropertyMetadata((short?)-1, EntidadMunicipioIdCallback));

        static void EntidadMunicipioIdCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Geografia Geo = d as Geografia;

            if (Geo.Bindings.Length <= 6)
            {
                bool[] binding = new bool[] { false, false };
                Geo.Bindings = Geo.Bindings.Concat(binding).ToArray();
            }
            Geo.Bindings[7] = true;

            Geo.Configurar();
            if (Geo.ConfiguracionManual || !Geo.Configurado) return;

            Geo.ConfiguracionManual = true;

            Geo.ComboMunicipio.SelectionChanged -= Geo.ComboEntidadMunicipio_SelectionChanged;

            if (Geo.EntidadMunicipioId.HasValue && Geo.EntidadMunicipioId.Value > 0)
            {
                if (Geo.ComboEntidadMunicipio.ItemsSource == null && Geo.MunicipioId.HasValue)
                {
                    Geo.ComboEntidadMunicipio.ItemsSource = RUV.I.InfoGeneral.ListaEntidadesMunicipios((long?)Geo.MunicipioId);
                }
                var EntMcpio = (Geo.ComboEntidadMunicipio.ItemsSource as List<clsEntidadMunicipio>)
                      .FirstOrDefault(x => x.NId == (short?)e.NewValue);
                Geo.ComboEntidadMunicipio.SelectedItem = EntMcpio;
            }

            Geo.ComboEntidadMunicipio.SelectionChanged += Geo.ComboEntidadMunicipio_SelectionChanged;

            Geo.ConfiguracionManual = false;
        }

        #endregion

        #region IndicativoMunicipio

        public string IndicativoMunicipio
        {
            get { return (string)GetValue(IndicativoMunicipioProperty); }
            set { SetValue(IndicativoMunicipioProperty, value); }
        }

        public static readonly DependencyProperty IndicativoMunicipioProperty =
            DependencyProperty.Register("IndicativoMunicipio", typeof(string),
            typeof(Geografia), new UIPropertyMetadata(string.Empty, IndicativoMunicipioPropertyCallback));

        static void IndicativoMunicipioPropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var Geo = (d as Geografia);

            if (Geo.Bindings.Length <= 6)
            {
                bool[] binding = new bool[] { false, false };
                Geo.Bindings = Geo.Bindings.Concat(binding).ToArray();
            }
            Geo.Bindings[7] = true;
            Geo.Configurar();

            if (Geo.ConfiguracionManual || !Geo.Configurado) return;
            Geo.ConfiguracionManual = true;

            Geo.TextBoxIndicativo.TextChanged -= Geo.TextBoxIndicativo_TextChanged;

            if (Geo.TextBoxIndicativo != null)
            {
                if (Geo.IndicativoMunicipio != null)
                {
                    Geo.TextBoxIndicativo.Text = Geo.IndicativoMunicipio;
                }
                Geo.TextBoxIndicativo.TextChanged += Geo.TextBoxIndicativo_TextChanged;
            }

            Geo.ConfiguracionManual = false;

        }


        #endregion

        #region SELECCION DE ENTIDAD MUNICIPIO POR COMBO

        private void ComboEntidadMunicipio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConfiguracionManual) return;
            ConfiguracionManual = true;


            if (ComboEntidadMunicipio == null) return;

            ComboEntidadMunicipio.SelectionChanged -= ComboEntidadMunicipio_SelectionChanged;
            if (ComboEntidadMunicipio.SelectedItem == null)
            {
                EntidadMunicipioId = null;
            }
            else
            {
                clsEntidadMunicipio PEM = ComboEntidadMunicipio.SelectedItem as clsEntidadMunicipio;
                if (PEM == null)
                    EntidadMunicipioId = null;
                else
                {
                    EntidadMunicipioId = (short?)PEM.NId;
                }
            }
            ComboEntidadMunicipio.SelectionChanged += ComboEntidadMunicipio_SelectionChanged;
            ConfiguracionManual = false;
        }

        #endregion

        #region SELECCION DE MUNICIPIO POR COMBO

        void ComboMunicipio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConfiguracionManual) return;
            ConfiguracionManual = true;
            ComboMunicipio.SelectionChanged -= ComboMunicipio_SelectionChanged;
            if (ComboEntidadMunicipio != null)
                ComboEntidadMunicipio.SelectionChanged -= ComboEntidadMunicipio_SelectionChanged;

            if (ComboMunicipio.SelectedItem == null)
            {
                MunicipioId = null;
            }
            else
            {
                clsParametroMunicipio PM = ComboMunicipio.SelectedItem as clsParametroMunicipio;
                if (ComboMunicipio == null)
                {
                    ConfiguracionManual = false;
                    return;
                }

                if (PM == null || !PM.Id.HasValue)
                {
                    MunicipioId = null;
                    ComboMunicipio.SelectionChanged += ComboMunicipio_SelectionChanged;
                    if (ComboEntidadMunicipio != null)
                    {
                        EntidadMunicipioId = null;
                        ComboEntidadMunicipio.ItemsSource = null;
                        ComboEntidadMunicipio.SelectionChanged += ComboEntidadMunicipio_SelectionChanged;
                    }

                    ConfiguracionManual = false;

                    MunicipioId = PM.Id;
                    return;
                }

                MunicipioId = PM.Id;
                OnMunicipioCambio(EventArgs.Empty);
                if (ComboEntidadMunicipio != null)
                {
                    List<clsEntidadMunicipio> EntidadesMun = new List<clsEntidadMunicipio>();
                    EntidadesMun = RUV.I.InfoGeneral.ListaEntidadesMunicipios(MunicipioId);
                    ComboEntidadMunicipio.ItemsSource = EntidadesMun;

                    if (EntidadMunicipioId.HasValue && EntidadesMun.Any(x => x.NId == EntidadMunicipioId.Value))
                    {
                        var EnMun = EntidadesMun.FirstOrDefault(x => x.NId == EntidadMunicipioId.Value);
                        ComboEntidadMunicipio.SelectedItem = EnMun;
                    }
                    else
                    {
                        ComboEntidadMunicipio.SelectedIndex = 0;
                        var EntiMunici = ComboEntidadMunicipio.SelectedItem as clsEntidadMunicipio;
                        if (EntiMunici == null)
                            EntidadMunicipioId = null;
                        else
                            EntidadMunicipioId = (short?)EntiMunici.NId;
                    }
                    
                }
                if (PM.CodigoTelefono.HasValue && TextBoxIndicativo != null)
                {
                    TextBoxIndicativo.Text = PM.CodigoTelefono.Value.ToString();
                    IndicativoMunicipio = PM.CodigoTelefono.Value.ToString();
                }


                ComboMunicipio.SelectionChanged += ComboMunicipio_SelectionChanged;
                if (ComboEntidadMunicipio != null)
                    ComboEntidadMunicipio.SelectionChanged += ComboEntidadMunicipio_SelectionChanged;
            }

            ConfiguracionManual = false;
        }

        #endregion

        #region SELECCION DE DEPARTAMENTO POR COMBO

        /// <summary>
        /// Actualizar la lista de municipios.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ComboDepartamento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConfiguracionManual) return;
            ConfiguracionManual = true;

            // ---------------------------------------
            ComboDepartamento.SelectionChanged -= ComboDepartamento_SelectionChanged;
            ComboMunicipio.SelectionChanged -= ComboMunicipio_SelectionChanged;
            if (ComboEntidadMunicipio != null)
                ComboEntidadMunicipio.SelectionChanged -= ComboEntidadMunicipio_SelectionChanged;

            var Depto = ComboDepartamento.SelectedItem as clsParametroDepartamento;
            //if (Depto == null || ComboMunicipio == null)
            if (ComboMunicipio == null)
            {
                ConfiguracionManual = false;
                return;
            }

            // ---------------------------------------
            if (Depto == null || !Depto.Id.HasValue)
            {
                DepartamentoId = null;
                MunicipioId = null;
                ComboMunicipio.ItemsSource = null;
                ComboDepartamento.SelectionChanged += ComboDepartamento_SelectionChanged;
                ComboMunicipio.SelectionChanged += ComboMunicipio_SelectionChanged;

                if (ComboEntidadMunicipio != null)
                {
                    EntidadMunicipioId = null;
                    ComboEntidadMunicipio.ItemsSource = null;
                    ComboEntidadMunicipio.SelectionChanged += ComboEntidadMunicipio_SelectionChanged;
                }
                ConfiguracionManual = false;

                DepartamentoId = Depto.Id;
                return;
            }


            DepartamentoId = Depto.Id;

            //CAMBIO: Int?

            List<clsParametroMunicipio> LosMcpios = new List<clsParametroMunicipio>();
            if (TodosLosPaises.HasValue && TodosLosPaises.Value)
            {
                if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
                {
                    LosMcpios = RUV.I.InfoGeneral.ListaMunicipios(Depto.Id.Value).Where(x => x.Nombre != "SIN INFORMACIÓN").ToList();
                }
                else
                {
                    LosMcpios = RUV.I.InfoGeneral.ListaMunicipios(Depto.Id.Value);
                }
                ComboMunicipio.ItemsSource = LosMcpios;
            }
            else
            {
                if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
                {
                    LosMcpios = RUV.I.InfoGeneral.ListaMunicipios(Depto.Id.Value).Where(x => x.Nombre != "SIN INFORMACIÓN").ToList();
                }
                else
                {
                    LosMcpios = RUV.I.InfoGeneral.ListaMunicipios(Depto.Id.Value).Where(x => x.TieneRepresentacion == true).ToList();
                }
                ComboMunicipio.ItemsSource = LosMcpios;
            }

            if (MunicipioId.HasValue && LosMcpios.Any(x => x.Id == MunicipioId.Value))
            {
                var Mcpio = LosMcpios.FirstOrDefault(x => x.Id == MunicipioId.Value);
                ComboMunicipio.SelectedItem = Mcpio;
                MunicipioId = Mcpio.Id;

                if (ComboEntidadMunicipio != null)
                {
                    List<clsEntidadMunicipio> EntidadesMun = new List<clsEntidadMunicipio>();
                    EntidadesMun = RUV.I.InfoGeneral.ListaEntidadesMunicipios(MunicipioId);
                    ComboEntidadMunicipio.ItemsSource = EntidadesMun;

                    if (EntidadMunicipioId.HasValue && EntidadesMun.Any(x => x.NId == EntidadMunicipioId.Value))
                    {
                        var EnMun = EntidadesMun.FirstOrDefault(x => x.NId == EntidadMunicipioId.Value);
                        ComboEntidadMunicipio.SelectedItem = EnMun;
                    }
                    else
                    {
                        ComboEntidadMunicipio.SelectedIndex = 0;
                        var EntiMcpio = ComboEntidadMunicipio.SelectedItem as clsEntidadMunicipio;
                        if (EntiMcpio == null)
                            EntidadMunicipioId = null;
                        else
                            EntidadMunicipioId = (short?)EntiMcpio.NId;
                    }
                }

            }
            else
            {
                ComboMunicipio.SelectedIndex = 0;
                var ElMcpio = ComboMunicipio.SelectedItem as clsParametroMunicipio;
                if (ElMcpio == null)
                    MunicipioId = null;
                else
                    MunicipioId = ElMcpio.Id;

                if (ComboEntidadMunicipio != null)
                {
                    ComboEntidadMunicipio.SelectedIndex = 0;
                    var EntMcpio = ComboEntidadMunicipio.SelectedItem as clsEntidadMunicipio;
                    if (EntMcpio == null)
                        EntidadMunicipioId = null;
                    else
                        EntidadMunicipioId = (short?)EntMcpio.NId;
                }
            }


            ComboDepartamento.SelectionChanged += ComboDepartamento_SelectionChanged;
            ComboMunicipio.SelectionChanged += ComboMunicipio_SelectionChanged;
            if (ComboEntidadMunicipio != null)
                ComboEntidadMunicipio.SelectionChanged += ComboEntidadMunicipio_SelectionChanged;

            // ---------------------------------------
            ConfiguracionManual = false;
        }

        #endregion

        #region SELECCION DE PAIS POR COMBO

        /// <summary>
        /// Actualizar la lista de paises.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ComboPais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConfiguracionManual) return;
            ConfiguracionManual = true;

            // ---------------------------------------
            ComboPais.SelectionChanged -= ComboPais_SelectionChanged;
            ComboDepartamento.SelectionChanged -= ComboDepartamento_SelectionChanged;
            ComboMunicipio.SelectionChanged -= ComboMunicipio_SelectionChanged;
            if (ComboEntidadMunicipio != null)
                ComboEntidadMunicipio.SelectionChanged -= ComboEntidadMunicipio_SelectionChanged;

            var pais = ComboPais.SelectedItem as clsParametroPais;
            //if (pais == null || ComboDepartamento == null)
            if (ComboDepartamento == null)
            {
                ConfiguracionManual = false;
                return;
            }

            // ---------------------------------------
            if (pais == null || !pais.Id.HasValue)
            {
                PaisId = null;
                DepartamentoId = null;
                ComboDepartamento.ItemsSource = null;

                //Limpiar combo de Municipios
                MunicipioId = null;
                ComboMunicipio.SelectedIndex = 0;
                ComboMunicipio.ItemsSource = null;

                //Limpiar Combo EntidadMunicipio
                EntidadMunicipioId = null;
                if (ComboEntidadMunicipio != null)
                {
                    ComboEntidadMunicipio.SelectedIndex = 0;
                    ComboEntidadMunicipio.ItemsSource = null;
                }

                ComboPais.SelectionChanged += ComboPais_SelectionChanged;
                ComboDepartamento.SelectionChanged += ComboDepartamento_SelectionChanged;
                ComboMunicipio.SelectionChanged += ComboMunicipio_SelectionChanged;
                if (ComboEntidadMunicipio != null)
                    ComboEntidadMunicipio.SelectionChanged += ComboEntidadMunicipio_SelectionChanged;

                ConfiguracionManual = false;

                PaisId = pais.Id;
                return;
            }


            PaisId = pais.Id;

            //CAMBIO: Int?
            List<clsParametroDepartamento> LosDptos = new List<clsParametroDepartamento>(); ;
            if (TodosLosPaises.HasValue && TodosLosPaises.Value)
            {
                LosDptos = RUV.I.InfoGeneral.ListaDepartamentos(pais.Id.Value);
                ComboDepartamento.ItemsSource = LosDptos;
            }
            else
            {
                LosDptos = RUV.I.InfoGeneral.ListaDepartamentos(pais.Id.Value).Where(x => x.TieneRepresentacion == true).ToList();
                ComboDepartamento.ItemsSource = LosDptos;
            }

            if (DepartamentoId.HasValue && LosDptos.Any(x => x.Id == DepartamentoId.Value))
            {
                var Dpto = LosDptos.FirstOrDefault(x => x.Id == DepartamentoId.Value);
                ComboDepartamento.SelectedItem = Dpto;
                DepartamentoId = Dpto.Id;
            }
            else
            {
                ComboDepartamento.SelectedIndex = 0;
                var ElDpto = ComboDepartamento.SelectedItem as clsParametroDepartamento;
                if (ElDpto == null)
                    DepartamentoId = null;
                else
                    DepartamentoId = ElDpto.Id;
            }

            //Limpiar combo de Municipios
            MunicipioId = null;
            ComboMunicipio.SelectedIndex = 0;
            ComboMunicipio.ItemsSource = null;

            if (ComboEntidadMunicipio != null)
            {
                EntidadMunicipioId = null;
                ComboEntidadMunicipio.SelectedIndex = 0;
                ComboEntidadMunicipio.ItemsSource = null;
            }

            ComboPais.SelectionChanged += ComboPais_SelectionChanged;
            ComboDepartamento.SelectionChanged += ComboDepartamento_SelectionChanged;
            ComboMunicipio.SelectionChanged += ComboMunicipio_SelectionChanged;
            if (ComboEntidadMunicipio != null)
                ComboEntidadMunicipio.SelectionChanged += ComboEntidadMunicipio_SelectionChanged;

            // ---------------------------------------
            ConfiguracionManual = false;
        }

        #endregion

        #region PRIMERA CONFIGURACIÓN

        bool Configurado = false;
        void Configurar()
        {
            //if (this.Name == "geoDeclaranteContacto")
            //{
            //    System.Diagnostics.Debugger.Break();
            //}

            bool BindingsConfigurados = Bindings.All(x => x);

            if (!Configurado && BindingsConfigurados)
            {
                Configurado = true;

                // Llenar la lista de departamentos
                if (TodosLosPaises.HasValue && TodosLosPaises.Value)
                {
                    ComboPais.ItemsSource = RUV.I.InfoGeneral.ListaPaises; // Trae todos los paises
                }
                else
                {
                    ComboPais.ItemsSource = RUV.I.InfoGeneral.ListaPaises.Where(x => x.TieneRepresentacion == true); //Trae los paises donde hay representacion consular
                }

                //ComboDepartamento.ItemsSource = Sipod.I.InfoGeneral.ListaDepartamentos;       //TODO Revisar Pais
                //ComboDepartamento.ItemsSource = null;
                //ComboMunicipio.ItemsSource = null;

                // ¿Hay algún país elegido?
                if (PaisId.HasValue)
                {
                    ComboPais.SelectedValue = PaisId.Value;
                }
                else
                {
                    // Diego Alvarez - 12/09/2013 - Preseleccionar país
                    clsParametroPais Pais = RUV.I.InfoGeneral.ListaPaises.Where(x => x.TieneRepresentacion == true && x.Nombre.ToUpper().Equals(PaisPreseleccionado)).First();
                    PaisId = 0;// Pais.Id;
                }
                // LLenar la lista de los departamentos.
                if (TodosLosPaises.HasValue && TodosLosPaises.Value)
                {
                    ComboDepartamento.ItemsSource = RUV.I.InfoGeneral.ListaDepartamentos(PaisId.Value);
                }
                else
                {
                    ComboDepartamento.ItemsSource = RUV.I.InfoGeneral.ListaDepartamentos(PaisId.Value).Where(x => x.TieneRepresentacion == true);
                }

                // ¿Hay algún departamento elegido?
                if (DepartamentoId.HasValue)
                {
                    ComboDepartamento.SelectedValue = DepartamentoId.Value;
                    // LLenar la lista de los municipios.
                    if (TodosLosPaises.HasValue && TodosLosPaises.Value)
                    {
                        ComboMunicipio.ItemsSource = RUV.I.InfoGeneral.ListaMunicipios(DepartamentoId.Value);
                    }
                    else
                    {
                        ComboMunicipio.ItemsSource = RUV.I.InfoGeneral.ListaMunicipios(DepartamentoId.Value).Where(x => x.TieneRepresentacion == true);
                    }
                    // ¿Hay un municipio elegido?
                    if (MunicipioId.HasValue)
                    {
                        ComboMunicipio.SelectedValue = MunicipioId.Value;

                        if (ComboEntidadMunicipio != null)
                        {
                            ComboEntidadMunicipio.ItemsSource = RUV.I.InfoGeneral.ListaEntidadesMunicipios((long?)MunicipioId);
                            if (EntidadMunicipioId.HasValue)
                            {
                                ComboEntidadMunicipio.SelectedValue = EntidadMunicipioId.Value;
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(IndicativoMunicipio) && TextBoxIndicativo != null)
                        TextBoxIndicativo.Text = IndicativoMunicipio;
                }



                // Finalizar suscribiendo a los eventos de los combos.
                ComboPais.SelectionChanged += ComboPais_SelectionChanged;
                ComboDepartamento.SelectionChanged += ComboDepartamento_SelectionChanged;
                ComboMunicipio.SelectionChanged += ComboMunicipio_SelectionChanged;
                if (ComboEntidadMunicipio != null)
                    ComboEntidadMunicipio.SelectionChanged += ComboEntidadMunicipio_SelectionChanged;
                if (TextBoxIndicativo != null)
                    TextBoxIndicativo.TextChanged += TextBoxIndicativo_TextChanged;
            }
        }

        #endregion
        void TextBoxIndicativo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ConfiguracionManual) return;
            ConfiguracionManual = true;

            //if (string.IsNullOrWhiteSpace(TextBoxIndicativo.Text))
            IndicativoMunicipio = TextBoxIndicativo.Text;

            ConfiguracionManual = false;
        }
    }
}
