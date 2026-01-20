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

namespace Ruv.WPF.Captura.Registro.Secciones
{
    public partial class A05 : UserControl, ISeccionRegistro
    {
        #region CONSTRUCTOR

        public A05()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(A05_Loaded);
            this.Unloaded += new RoutedEventHandler(A05_Unloaded);
        }

        void A05_Loaded(object sender, RoutedEventArgs e)
        {
            RegistrarTipoDesplazamiento();
        }


        void A05_Unloaded(object sender, RoutedEventArgs e)
        {
            BindingOperations.ClearBinding(this, ValorSeleccionUnica_TipoDesplazamientoProperty);
        }

        #endregion

        #region ISeccionRegistro

        public eSeccionRegistro Seccion
        { get { return eSeccionRegistro.A05; } }

        public bool RequireScrollBars { get { return false; } }

        public void MostrarEnInterfase()
        {
            // Cuando se invoque la interfase, volver al modo oculto.
            //EdicionVisible = false;
        }

        #endregion

        #region PROPIEDADES & CAMPOS

        /// <summary>
        /// El tipo de operación que se realiza sobre el registro actual.
        /// </summary>
        public eTipoOperacionRegistro OperacionRegistroActual { get; set; }

        /// <summary>
        /// La persona que se está insertando.
        /// </summary>
        clsAnexo05_Victima PersonaInsercion;

        /// <summary>
        /// La persona que se está editando.
        /// </summary>
        clsAnexo05_Victima PersonaEdicion;

        /// <summary>
        /// El DataContext de este anexo.
        /// </summary>
        public clsAnexo05 EsteAnexo
        {
            get
            {
                return DataContext as clsAnexo05;
            }
        }

        #endregion

        #region AGREGAR PERSONAS SELECCIONADAS

        /// <summary>
        /// Agrega las personas seleccionadas a la lista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AgregarPersona(object sender, RoutedEventArgs e)
        {
            if (lbxPersonasAfectadas.SelectedItems.Count == 0)
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                  "Debe seleccionar una o varias personas de la lista 'Personas Afectadas'");
                return;
            }

            var NuevasPersonas = lbxPersonasAfectadas.SelectedItems.OfType<clsPersonaAfectada>()
              .Select(x => x.ID)
              .Except(EsteAnexo.Victimas
                .Where(x => x.EstadoRegistro != Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Eliminado)
                .Select(x => x.PersonaAfectadaId));

            if (!NuevasPersonas.Any())
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                  "Todas personas seleccionadas ya están en la lista.");
                return;
            }

            foreach (var NuevaPersona in NuevasPersonas)
            {
                var Nuevo = new clsAnexo05_Victima { PersonaAfectadaId = NuevaPersona };
                RUV.I.Util.EntidadEstablecerSiguienteId(
                  EsteAnexo.Victimas,
                  Nuevo);
                Nuevo.EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar;
                EsteAnexo.Victimas.Add(Nuevo);
            }

        }

        #endregion

        #region QUITAR PERSONAS SELECCIONADAS

        private void QuitarPersonas(object sender, RoutedEventArgs e)
        {
            var Seleccionados = lpdPersonasHogar.PersonasSeleccionadas.ToList();
            for (int i = Seleccionados.Count - 1; i >= 0; i--)
            {
                if (Seleccionados[i].EstadoRegistro == Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar)
                    EsteAnexo.Victimas.Remove(Seleccionados[i]);
                else
                {
                    Seleccionados[i].EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Eliminado;
                    //metodo para retirar de la lista visualmente los registros que se seleccionan y se les da en el boton "quitar"
                    int fila = lpdPersonasHogar.Victimas.IndexOf(Seleccionados[i]);
                    if (fila == 0)
                        lpdPersonasHogar.Victimas.Move(fila, fila);
                    else
                        lpdPersonasHogar.Victimas.Move(fila, fila - 1);
                }
            }
        }

        #endregion


        #region MOSTRAR MENSAJE DE ADVERTENCIA SI NO SE SABE FIRMAR

        public static readonly DependencyProperty ValorSeleccionUnica_TipoDesplazamientoProperty = DependencyProperty.Register("ValorSeleccionUnica_TipoDesplazamiento", typeof(int?),
                                                             typeof(A05), new UIPropertyMetadata(null, A05.ValorSeleccionUnica_TipoDesplazamiento_Changed));

        public int? ValorSeleccionUnica_TipoDesplazamiento
        {
            get { return (int?)GetValue(ValorSeleccionUnica_TipoDesplazamientoProperty); }
            set { SetValue(ValorSeleccionUnica_TipoDesplazamientoProperty, value); }
        }

        void RegistrarTipoDesplazamiento()
        {
            Extensiones.BindingEstablecer(DataContext, "TipoDesplazamiento", this, ValorSeleccionUnica_TipoDesplazamientoProperty);
        }

        private static void ValorSeleccionUnica_TipoDesplazamiento_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int? NuevoValor = (int?)e.NewValue;
            MostrarMensajeDesplazamientoMasivo(NuevoValor);
        }

        private static void MostrarMensajeDesplazamientoMasivo(int? NuevoValor)
        {
            if (NuevoValor == (int)Ruv.Infrastructure.Crosscutting.Common.eTipoDesplazamientoA05.Masivo)
                MessageBox.Show("Para desplazamiento \"Masivo\" debe diligenciar el anexo 13");
        }

        #endregion

        private void FechaLugar_CambioPais(object sender, CambioPaisEventArgs e)
        {
            var paisesTransfronterisos = RUV.I.InfoGeneral.ListaParametros.Where(x => x.Tipo == Ruv.Infrastructure.Crosscutting.Common.eTipoParametros.PaisesTransfronterizos);
            if (paisesTransfronterisos.Any())
            {
                var cantidadPaises = paisesTransfronterisos.First().Nombre.Split(',').Count(x => x == e.NuevoPais.Value.ToString());
                if (cantidadPaises > 0)
                    this.EsteAnexo.NuevoTipoDesplazamiento = 10148;
                else
                {
                    if (e.NuevoPais.Value != Ruv.Infrastructure.Crosscutting.Common.ePaises.Colombia.GetHashCode())
                        this.EsteAnexo.NuevoTipoDesplazamiento = 10147;
                    else
                        this.EsteAnexo.NuevoTipoDesplazamiento = null;
                }
            }
        }
    }
}
