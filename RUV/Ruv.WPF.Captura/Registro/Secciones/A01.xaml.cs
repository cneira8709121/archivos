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
using Ruv.WPF.Captura.Controles;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.WPF.Captura.Infrastructure;

namespace Ruv.WPF.Captura.Registro.Secciones
{
    public partial class A01 : UserControl, ISeccionRegistro
    {
        #region CONSTRUCTOR

        public A01()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(A01_Loaded);
        }

        void A01_Loaded(object sender, RoutedEventArgs e)
        { Inicializar(); }

        void Inicializar()
        {
            // Dejar seleccionada siempre la primera persona.
            cbxListaCompletaPersonas.SelectedIndex = 0;
        }

        EditorObservableCollection<clsAnexo01_Victima_Bien> EditorBienes;

        #endregion

        #region ISeccionRegistro

        public eSeccionRegistro Seccion
        { get { return eSeccionRegistro.A01; } }

        public bool RequireScrollBars { get { return false; } }

        public void MostrarEnInterfase()
        {
            // Cuando se invoque la interfase, volver al modo oculto.
            EdicionVisible = false;
        }

        #endregion

        #region REACCIONES VISUALES

        GridLength AnchoColumnaEdicionVisible = new GridLength(2d, GridUnitType.Star);
        GridLength AnchoColumnaEdicionNoVisible = new GridLength(0d, GridUnitType.Pixel);

        /// <summary>
        /// Cambia la visibilidad del formulario de edición.
        /// </summary>
        public bool EdicionVisible
        {
            set
            {
                if (value && lbxListaPersonas.SelectedItems.Count > 1)
                    value = false;

                if (value && gsSeparador.Visibility != System.Windows.Visibility.Visible)
                {
                    cdAcciones.Width = AnchoColumnaEdicionVisible;
                    cdEdicion.Width = AnchoColumnaEdicionVisible;
                    gsSeparador.Visibility = System.Windows.Visibility.Visible;
                    spAcciones.Visibility = System.Windows.Visibility.Visible;
                    svEdicion.Visibility = System.Windows.Visibility.Visible;
                }
                else if (!value && gsSeparador.Visibility != System.Windows.Visibility.Collapsed)
                {
                    cdAcciones.Width = AnchoColumnaEdicionNoVisible;
                    cdEdicion.Width = AnchoColumnaEdicionNoVisible;
                    gsSeparador.Visibility = System.Windows.Visibility.Collapsed;
                    spAcciones.Visibility = System.Windows.Visibility.Collapsed;
                    svEdicion.Visibility = System.Windows.Visibility.Collapsed;
                    svEdicion.DataContext = null;
                    PersonaInsercion = null;
                    if (lbxListaPersonas.SelectedItems.Count == 1)
                        lbxListaPersonas.SelectedItem = null;
                }
            }
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
        clsAnexo01_Victima PersonaInsercion;

        /// <summary>
        /// La persona que se está editando.
        /// </summary>
        clsAnexo01_Victima PersonaEdicion;

        /// <summary>
        /// El DataContext de este anexo.
        /// </summary>
        public clsAnexo01 EsteAnexo
        {
            get
            {
                return DataContext as clsAnexo01;
            }
        }

        #endregion

        #region QUITAR UNA O VARIAS PERSONAS

        /// <summary>
        /// Eliminar una o varias personas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EiminarPersonas(object sender, RoutedEventArgs e)
        {
            // ¿Hay alguien seleccionado?
            if (lbxListaPersonas.SelectedItems.Count == 0) return;

            // Confirmar.
            if (!RUV.I.UIPrincipal.UsuarioConfirmar(
              "¿Desea quitar esta(s) persona(s) de este anexo?"))
                return;

            // Quitar el registro.
            for (int i = lbxListaPersonas.SelectedItems.Cast<clsAnexo01_Victima>().Count() - 1; i >= 0; i--)
            {
                RUV.I.Util.BorrarEntidad<clsAnexo01_Victima>(
                  EsteAnexo.Victimas,
                  lbxListaPersonas.SelectedItems.Cast<clsAnexo01_Victima>().ElementAt(i));             
            }
            lbxListaPersonas.Items.Refresh();
            EdicionVisible = false;
        }

        #endregion

        #region 1) PREPARAR AGREGAR UNA NUEVA PERSONA

        bool PrepararAgregarNuevaPersonaExitoso;

        private void PrepararAgregarNuevaPersona(object sender, RoutedEventArgs e)
        {
            if (cbxListaCompletaPersonas.SelectedValue == null)
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario("Debe seleccionar una persona de la lista.");
                return;
            }

            PrepararAgregarNuevaPersonaExitoso = false;

            // No permitir personas duplicadas.
            if (EsteAnexo.Victimas.Any(x => x.PersonaAfectadaId ==
              (cbxListaCompletaPersonas.SelectedValue as clsPersonaAfectada).ID))
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                  "'{0}' ya ha sido agregado(a)\ny no puede duplicarse.",
                  (cbxListaCompletaPersonas.SelectedItem as clsPersonaAfectada).NombreCompleto);
                return;
            }

            clsPersonaAfectada PA = cbxListaCompletaPersonas.SelectedValue as clsPersonaAfectada;

            // Marcarla como 'Insertar'.
            PersonaInsercion = new clsAnexo01_Victima()
            {
                EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar,
                PersonaAfectadaId = PA.ID,
                AnexoPadre = EsteAnexo
            };
            PersonaInsercion.DenunciaPrevia.AnexoPadre = EsteAnexo;

            PrepararEdicionBienes(PersonaInsercion);

            //PrepararVistaBienes(PersonaInsercion);
            svEdicion.DataContext = PersonaInsercion;
            EdicionVisible = true;
            OperacionRegistroActual = eTipoOperacionRegistro.Insertar;
            chkVictimaDelHecho.Focus();



            PrepararAgregarNuevaPersonaExitoso = true;

            // Lanzar la validación.
            if (RUV.I.UIPrincipal.ValidadorActual != null)
                RUV.I.MultiTarea.PosponerEjecucion(1000,
                  new Action(() =>
                        RUV.I.UIPrincipal.ValidadorActual.Validar()
                    ));
        }

        #endregion

        #region 2) ACEPTAR

        /// <summary>
        /// Se presionó el botón aceptar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AceptarEdicion(object sender, RoutedEventArgs e)
        {
            switch (OperacionRegistroActual)
            {
                case eTipoOperacionRegistro.Insertar:
                    InsertarNuevoRegistro();
                    break;

                case eTipoOperacionRegistro.Editar:
                    EditarRegistro();
                    break;
            }
        }

        #endregion

        #region 3) INSERTAR

        /// <summary>
        /// Se procede a insertar un nuevo registro.
        /// </summary>
        void InsertarNuevoRegistro()
        {
            // Validar el objeto antes de ponerlo en la colección de personas.
            List<eEstadoValidacion> Requeridas = Ruv.WPF.Captura.Infrastructure.clsUtil.ValidacionesRequeridas();
            int validacionesSaltadas = 0;
            if (!RUV.I.ValidadorEntidades.EntidadEsValida(PersonaInsercion, Requeridas, ref validacionesSaltadas))
            {
                //if(!Sipod.I.UIPrincipal.UsuarioConfirmar("Existen inconsistencias.\nDesea Agregar la persona sin corregirlas."))
                RUV.I.UIPrincipal.ReportarErrorDeUsuario("Existen inconsistencias.\nCorríjalas antes de continuar.");
                return;
            }

            // Agregar el registro a la colección.
            RUV.I.Util.EntidadEstablecerSiguienteId(
              EsteAnexo.Victimas,
              PersonaInsercion);

            EsteAnexo.Victimas.Add(PersonaInsercion.ObtenerCopia<clsAnexo01_Victima>());
            PersonaInsercion.EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar;

            lbxListaPersonas.SelectedItem = null;
            EdicionVisible = false;
            PersonaInsercion = null;
        }

        #endregion

        #region A) PREPARAR EDICION

        /// <summary>
        /// Prepararse para editar un persona seleccionada desde la lista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonaSeleccionada(object sender, SelectionChangedEventArgs e)
        {
            var Seleccionado = (sender as ListBox).SelectedItem as clsAnexo01_Victima;
            if (Seleccionado == null) return;

            // Crear una copia, que será la que se edite.
            PersonaEdicion = Seleccionado.ObtenerCopia<clsAnexo01_Victima>();
            //Sipod.I.Util.CrearCopia<clsAnexo01_Victima>(Seleccionado);
            //PrepararVistaBienes(PersonaEdicion);
            PrepararEdicionBienes(PersonaEdicion);

            svEdicion.DataContext = PersonaEdicion;
            EdicionVisible = true;
            OperacionRegistroActual = eTipoOperacionRegistro.Editar;
            chkVictimaDelHecho.Focus();

            // Forzar la validación.
            RUV.I.UIPrincipal.ValidadorActual.Validar();
        }

        #endregion

        #region B) FINALIZAR EDITAR

        /// <summary>
        /// Se procede a actualizar un registro existente.
        /// </summary>
        void EditarRegistro()
        {
            // Validar el objeto antes de ponerlo en la colección de personas.
            List<eEstadoValidacion> Requeridas = Ruv.WPF.Captura.Infrastructure.clsUtil.ValidacionesRequeridas();
            int validacionesSaltadas = 0;
            if (!RUV.I.ValidadorEntidades.EntidadEsValida(PersonaEdicion, Requeridas, ref validacionesSaltadas))
            {
                RUV.I.UIPrincipal.ReportarErrorDeUsuario(
                  "Existen inconsistencias.\nCorríjalas antes de continuar.");
                return;
            }

            // Quitar la persona de la lista de v´ctimas y volverla a poner.
            // ! Es lo más fácil !

            int? JefeGrupoId = EsteAnexo.JefeGrupoFamiliarId;
            var temporal = lbxListaPersonas.SelectedItem as clsAnexo01_Victima;
            EsteAnexo.Victimas.Remove(temporal);
            var Nuevo = PersonaEdicion.ObtenerCopia<clsAnexo01_Victima>();
            if (temporal.ID > 0)
            {
                Nuevo.EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.SinModificaciones;
            }
            if (Nuevo.EstadoRegistro == Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.SinModificaciones)
                Nuevo.EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Modificado;
            EsteAnexo.Victimas.Add(Nuevo);
            EsteAnexo.JefeGrupoFamiliarId = JefeGrupoId;
            lbxListaPersonas.SelectedItem = null;
            PersonaEdicion = null;
            EdicionVisible = false;
        }

        #endregion

        #region CANCELAR LA EDICIÓN

        private void CancelarEdicion(object sender, RoutedEventArgs e)
        {
            EdicionVisible = false;
            PersonaEdicion = null;
            PersonaInsercion = null;
            OperacionRegistroActual = eTipoOperacionRegistro.Ninguna;
        }

        #endregion

        #region CREAR COPIA

        /// <summary>
        /// Permite agregar una nueva persona alimentándola con información de
        /// la persona actulamente seleccionada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AgregarCopia(object sender, RoutedEventArgs e)
        {
            if (lbxListaPersonas.SelectedItems.Count != 1)
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario("Para crear una copia debe seleccionar\nuna persona de la lista.");
                return;
            }

            clsAnexo01_Victima OrigenCopia = lbxListaPersonas.SelectedItem as clsAnexo01_Victima;

            // Invocar la adición.
            PrepararAgregarNuevaPersona(null, null);
            if (!PrepararAgregarNuevaPersonaExitoso) return;

            int? IdActual = PersonaInsercion.ID;
            PersonaInsercion = OrigenCopia.ObtenerCopia<clsAnexo01_Victima>();
            PersonaInsercion.ID = IdActual;
            PersonaInsercion.PersonaAfectadaId = (cbxListaCompletaPersonas.SelectedItem as clsPersonaAfectada).ID;
            PersonaInsercion.EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar;

            svEdicion.DataContext = PersonaInsercion;
        }

        #endregion

        #region EDICIÓN DE LOS BIENES

        /// <summary>
        /// Preparar la clase que se encarga de la edíciónd de los bienes en la grilla.
        /// </summary>
        void PrepararEdicionBienes(clsAnexo01_Victima persona)
        {
            if (EditorBienes != null) EditorBienes.Dispose();

            EditorBienes = new EditorObservableCollection<clsAnexo01_Victima_Bien>()
            {
                BotonAgregar = btnAgregarBien,
                BotonQuitar = btnQuitarBien
            };

            EditorBienes.ListaDatos = persona.Bienes;

            dgrBienes.ItemsSource = EditorBienes.ListaDatosVista;
            dgrBienes.LostFocus += delegate
            {
                EditorBienes.PostearCambios();
            };

            if (EditorBienes.ListaDatosVista != null)
                EditorBienes.ListaDatosVista.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ListaDatosVista_CollectionChanged);
        }

        void ListaDatosVista_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Forzar la validación.
            RUV.I.UIPrincipal.ValidadorActual.Validar();
        }
        #endregion
    }
}
