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
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using resx = Ruv.Infrastructure.Crosscutting.Resources.Globalization;

using System.IO;
using System.ComponentModel;
using Ruv.WPF.Captura.Utils.DataSources;

namespace Ruv.WPF.Captura.ListaTareas
{
    /// <summary>
    /// Lógica de interacción para ListaTareasV2.xaml
    /// </summary>
    public partial class ListaTareasV2 : Page
    {

        #region CONSTRUCTOR

        public ListaTareasV2()
        {
            InitializeComponent();
            this.DataContext = this;

            this.Loaded += new RoutedEventHandler(ListaTareas_Loaded);
        }

        void ListaTareas_Loaded(object sender, RoutedEventArgs e)
        {
            lstListaTareas.Focus();                        
        }

        private int TareaIndice;
        private int TotalTareas;
        private bool PagingInProcess = false;

        #endregion

        #region ARMAR LISTA DE TAREAS

        clsListaTareas[] ResultadoTransmision;

        public ObservableCollection<clsListaTareas> ListaTareas
        {
            get { return (ObservableCollection<clsListaTareas>)GetValue(ListaTareasProperty); }
            set { SetValue(ListaTareasProperty, value); }
        }

        public static readonly DependencyProperty ListaTareasProperty =
            DependencyProperty.Register("ListaTareas", typeof(ObservableCollection<clsListaTareas>),
            typeof(ListaTareasV2), new UIPropertyMetadata(null));

        void DT_Tick(object sender, EventArgs e)
        {
            ListaTareas.Add(ResultadoTransmision[TareaIndice++]);
            if (TareaIndice == TotalTareas) {
                (sender as DispatcherTimer).Stop();
                PagingInProcess = false;
                this.pageControl.SetPagerEnabled(true);
                this.SetFiltersEnabled(true);
            }
        }

        #endregion

        #region VER TAREAS

        private void SeleccionarRadicacion(object sender, MouseButtonEventArgs e)
        {
            var ControlLista = sender as ListBox;
            var Tarea = ControlLista.SelectedItem as clsListaTareas;
            ProcesaApertura(Tarea);
        }

        private void PreSeleccionarRadicacion()
        {            
            var Tarea = ResultadoTransmision.Where(x => x.Declaracion == RUV.I.IdDeclaracion).FirstOrDefault() as clsListaTareas;
            ProcesaApertura(Tarea);
        }
        
        private void ProcesaApertura(clsListaTareas Tarea) {
            var Decla = new clsDeclaracion();
            Ruv.WPF.Captura.GeneralService.clsResultado Resultado = new GeneralService.clsResultado();
            RUV.I.UIPrincipal.BloquearInterfase = "Abriendo declaración";
            RUV.I.MultiTarea.EjecutarEnBackground(() => { Resultado = AbrirDeclaracion(Tarea, ref Decla); }, () => FinAbrirDeclaracion(ref Decla, Resultado));
        }
        
        private void FinAbrirDeclaracion(ref clsDeclaracion Decla, Ruv.WPF.Captura.GeneralService.clsResultado Resultado)
        {
            // Desbloquear la interfaz
            RUV.I.UIPrincipal.BloquearInterfase = null;

            // Verificacion General. Respuesta nula
            if (Decla == null)
            {
                MessageBox.Show("No se puede cargar la declaración");
                return;
            }
            else
            {
                if (Resultado.ErroresDB != null && Resultado.ErroresDB.Any())
                {
                    string message = "No se puede cargar la declaración: " + Environment.NewLine;
                    MessageBox.Show(message + ComponerMensaje(Resultado.ErroresDB));
                    return;
                }
                else if (Resultado.AdvertenciasDB != null && Resultado.AdvertenciasDB.Any())
                {
                    string message = "Advertencia: " + Environment.NewLine;
                    MessageBox.Show(message + ComponerMensaje(Resultado.AdvertenciasDB));
                }

                // Reorganizar: Errores en Resultado no deben permitir carga
                if (Decla.EstadoDeclaracion == eEstadoDeclaracion.RadicadoPendienteCaptura || RUV.I.IdDeclaracion > 0)
                {
                    if (Resultado.ErroresDB == null || !Resultado.ErroresDB.Any())
                    {
                        Ruv.WPF.Captura.Registro.RegistroVista RV = new Registro.RegistroVista(Decla);
                        NavigationService.Navigate(RV);
                        return;
                    }
                    else
                    {
                        var Ven = new Ruv.WPF.Captura.Registro.Secciones.Controles.ReporteEnvioDeclaracion(Resultado);
                        Ven.ShowDialog();
                    }
                }
                else
                {
                    if (Decla.EstadoDeclaracion == eEstadoDeclaracion.RadicadoPendienteCaptura || Decla.EstadoDeclaracion == eEstadoDeclaracion.CapturaPendientePorValidar)
                    {
                        Ruv.WPF.Captura.Registro.RegistroVista RV = new Registro.RegistroVista(Decla);
                        NavigationService.Navigate(RV);
                        return;
                    }
                    else
                    {
                        if (Decla.EstadoDeclaracion == eEstadoDeclaracion.RadicacionPendienteCritica5)
                        {
                            Ruv.WPF.Captura.CriticaN.GestionDeclaracion RV = new CriticaN.GestionDeclaracion();
                            RV.NIdDeclaracion = Decla.ID.Value;
                            RV.NIdRadicacion = Decla.RadicacionId.Value;
                            RV.NIdRadicacion = Decla.RadicacionId.Value;
                            RV.Declaracion = Decla;
                            NavigationService.Navigate(RV);
                            return;
                        }
                        else
                        {
                            if (Decla.EstadoDeclaracion == eEstadoDeclaracion.RadicacionPendientePorVerificar)
                            {
                                //TODO john HEnao. colocar la referencia para el nuevo formulario
                                Ruv.WPF.Captura.Radicacion.LiderRadicacion RV = new Radicacion.LiderRadicacion();
                                RUV.I.DeclaracionActual = Decla;
                                RV.NIdDeclaracion = Decla.ID;
                                NavigationService.Navigate(RV);
                                // MessageBox.Show("No puede cargar la declaración en este estado");
                            }
                            else
                            {
                                if (Decla.EstadoDeclaracion == eEstadoDeclaracion.DeclaracionPendientePorDevolucion)
                                {
                                    GestionDevolucion gDev = new GestionDevolucion(Decla.ID.Value);
                                    NavigationService.Navigate(gDev);
                                    //No se encontró devolución relacionado con este ID de declaración
                                }
                                // Diego Alvarez - 04/10/2013 - No se debe mostrar mensaje cuando se hace click por fuera de la declaración
                                else if (Decla.EstadoDeclaracion == eEstadoDeclaracion.Ninguno)
                                {
                                }
                                else
                                {
                                    MessageBox.Show("No puede cargar la declaración en este estado");
                                }
                            }
                        }

                    }
                }
            }
        }

        private Ruv.WPF.Captura.GeneralService.clsResultado AbrirDeclaracion(clsListaTareas Tarea, ref clsDeclaracion Declaracion)
        {
            Ruv.WPF.Captura.GeneralService.clsResultado Resultado = new GeneralService.clsResultado();
            if (Tarea != null && Tarea.Declaracion > 0)
            {
                try
                {
                    Declaracion = RUV.I.Red.ServicioGeneral.ObtenerDeclaracion(Tarea.Declaracion, RUV.I.Seguridad.LlaveUsuario);
                }
                catch (Exception ex)
                {
                    Resultado.ErroresDB = new string[] { ex.Message };
                    return Resultado;
                }

                // Servicio puede fallar. Arrojar error, pero no cerrar la aplicaciíon
                if (Declaracion == null)
                {
                    Resultado.ErroresDB = new string[] { "No se pudo obtener la declaración. Por favor, intente mas tarde." };
                    return Resultado;
                }

                if (Declaracion.DocumentoDigital == null)
                    Resultado.AdvertenciasDB = new string[] { "No se pudo encontrar el documento digital asociado a la radicación solicitada." };
                else
                {
                    string fileName = string.Format("{0}/{1}", RUV.I.Util.RutaArchivosLocales, Declaracion.DocumentoDigitalNombre);
                    Declaracion.DocumentoDigitalNombre = fileName;
                    File.WriteAllBytes(fileName, Declaracion.DocumentoDigital);
                }

                Declaracion.CrearEnlacesPostCargue();
                Declaracion.TomaDeclaracion.InicializarHechos();
                Declaracion.AutoGeneradoPorRadicacion = true;
            }
            return Resultado;

        }

        private string ComponerMensaje(string[] lineas)
        {
            var message = string.Empty;
            foreach (var error in lineas)
            {
                message += error + Environment.NewLine;
            }
            return message;
        }

        #endregion


        private void btn_Filtrar_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)cbbFiltros.SelectedValue;
            if (cbi == null) return;

            if (pageControl.PageContract == null) return;
            ListaTareasDataSource dtsListaTareas = pageControl.PageContract as ListaTareasDataSource;

            if ((string)cbi.Content == "Numero Formulario")
            {
                if ((string.IsNullOrWhiteSpace(txbFiltro.Text)) || (string.IsNullOrEmpty(txbFiltro.Text)))
                {
                    MessageBox.Show("Debe Ingresar un valor de Formulario valido");
                    return;
                }
                else
                {
                    dtsListaTareas.NumeroFormulario = txbFiltro.Text;
                    dtsListaTareas.FechaInicialRadicado = null;
                    dtsListaTareas.FechaFinalRadicado = null;
                }
            }
            else if ((string)cbi.Content == "Fecha Radicado")
            {
                if (string.IsNullOrEmpty(dpkFechaFinal.Text) || string.IsNullOrWhiteSpace(dpkFechaIncial.Text)
                    || string.IsNullOrWhiteSpace(dpkFechaFinal.Text) || string.IsNullOrEmpty(dpkFechaIncial.Text)
                    || (dpkFechaIncial.SelectedDate > dpkFechaFinal.SelectedDate))
                {
                    MessageBox.Show("Las fechas inicial y final deben tener valores validos");
                    return;
                }
                else
                {
                    dtsListaTareas.NumeroFormulario = null;
                    dtsListaTareas.FechaInicialRadicado = DateTime.Parse(dpkFechaIncial.Text);
                    dtsListaTareas.FechaFinalRadicado = DateTime.Parse(dpkFechaFinal.Text);
                }
            }

            pageControl.Navigate(PageChanges.First);
        }

        private void pageControl_PreviewPageChange(object sender, PageChangedEventArgs args)
        {
            List<object> items = pageControl.ItemsSource.ToList();
            int count = items.Count;
        }

        private void pageControl_PageChanged(object sender, PageChangedEventArgs args)
        {
            if (!PagingInProcess)
            {
                bool TransmisionExitosa = false;

                List<object> items = null;
                try
                {
                    items = pageControl.ItemsSource.ToList();

                    List<clsListaTareas> tmp = new List<clsListaTareas>();
                    foreach (object item in items)
                        tmp.Add((clsListaTareas)item);

                    ResultadoTransmision = tmp.ToArray();

                    TransmisionExitosa = true;
                }
                catch (Exception ex)
                {
                    string Mensaje = "No se pudo realizar la transmisión.\n" + ex.Message;
                    RUV.I.Log.Registrar("Lista de tareas", ex);
                    RUV.I.UIPrincipal.ReportarErrorDeUsuario(Mensaje);
                }

                if (!TransmisionExitosa) return;

                // Animar la aparición de las tareas.
                int count = items.Count;
                if (ListaTareas == null)
                    ListaTareas = new ObservableCollection<clsListaTareas>();
                else
                    ListaTareas.Clear();

                if (ResultadoTransmision == null || !ResultadoTransmision.Any())
                    return;

                this.pageControl.SetPagerEnabled(false);
                this.SetFiltersEnabled(false);
                PagingInProcess = true;
                TareaIndice = 0;
                TotalTareas = ResultadoTransmision.Count();

                DispatcherTimer DT = new DispatcherTimer();
                DT.Interval = new TimeSpan(0, 0, 0, 0, 25);
                DT.Tick += new EventHandler(DT_Tick);
                DT.Start();
                if (RUV.I.IdDeclaracion > 0)
                {
                    PreSeleccionarRadicacion();
                }
            }
        }

        private void cbbFiltros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)cbbFiltros.SelectedValue;
            if (cbi == null) return;

            if ((string)cbi.Content == "Numero Formulario")
            {
                txbFiltro.Visibility = Visibility.Visible;
                stpFechas.Visibility = Visibility.Collapsed;
            }
            else if ((string)cbi.Content == "Fecha Radicado")
            {
                txbFiltro.Visibility = Visibility.Collapsed;
                stpFechas.Visibility = Visibility.Visible;
            }
        }

        private void btn_RestablecerFiltro_Click(object sender, RoutedEventArgs e)
        {
            if (pageControl.PageContract == null) return;

            cbbFiltros.SelectedIndex = -1;

            txbFiltro.Text = null;
            dpkFechaIncial.Text = null;
            dpkFechaFinal.Text = null;

            txbFiltro.Visibility = Visibility.Collapsed;
            stpFechas.Visibility = Visibility.Collapsed;

            ListaTareasDataSource dtsListaTareas = pageControl.PageContract as ListaTareasDataSource;
            dtsListaTareas.NumeroFormulario = null;
            dtsListaTareas.FechaInicialRadicado = null;
            dtsListaTareas.FechaFinalRadicado = null;

            pageControl.Navigate(PageChanges.First);
        }

        private void SetFiltersEnabled(bool enable)
        {
            cbbFiltros.IsEnabled = enable;
            btn_Filtrar.IsEnabled = enable;
            btn_RestablecerFiltro.IsEnabled = enable;
        }

    }
}
