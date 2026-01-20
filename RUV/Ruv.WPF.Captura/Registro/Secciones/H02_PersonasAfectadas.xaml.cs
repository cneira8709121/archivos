using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ruv.WPF.Captura.Registro.Secciones.Controles;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.Windows.Threading;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.WPF.Captura.Infrastructure;
using System.Collections.Generic;
using System.Collections;

namespace Ruv.WPF.Captura.Registro.Secciones
{
    /// <summary>
    /// Hoja 2 de 4.
    /// </summary>
    public partial class H02_PersonasAfectadas : UserControl, ISeccionRegistro
    {

        #region CONSTRUCTOR

        public H02_PersonasAfectadas()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(H02_PersonasAfectadas_Loaded);
        }

        void H02_PersonasAfectadas_Loaded(object sender, RoutedEventArgs e)
        {
            var personas = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsPersonasAfectadas;
            if (personas != null)
            {
                Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDeclaracion DeclaracionActual = personas.Declaracion;
                if (DeclaracionActual.AutoGeneradoPorRadicacion)
                {
                    if (personas.DeclaranteId > 0)
                        cmbDeclarante.IsEnabled = false;
                }
            }
        }

        #endregion

        #region 1) PREPARAR AGREGAR UNA NUEVA PERSONA

        /// <summary>
        /// Se prepara para agregar una nueva persona.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrepararAgregarNuevaPersona(object sender, RoutedEventArgs e)
        {
            this.tbxPrimerNombre.IsEnabled = true;
            this.tbxSegundoNombre.IsEnabled = true;
            this.tbxPrimerApellido.IsEnabled = true;
            this.tbxSegundoApellido.IsEnabled = true;
            this.cb01TipoDoc.IsEnabled = true;
            this.tbxNumeroDocumento.IsEnabled = true;

            PersonaActual = new clsPersonaAfectada()
            {
                EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar
            };
            svEdicion.DataContext = PersonaActual;
            EdicionVisible = true;
            OperacionRegistroActual = eTipoOperacionRegistro.Insertar;
            cb01TipoDoc.Focus();

            // Lanzar la validación.
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
            if (!RUV.I.ValidadorEntidades.EntidadEsValida(PersonaActual, Requeridas, ref validacionesSaltadas))
            {
                RUV.I.UIPrincipal.ReportarErrorDeUsuario("Existen inconsistencias.\nCorríjalas antes de continuar.");
                return;
            }

            // Agregar el registro a la colección.
            RUV.I.Util.EntidadEstablecerSiguienteId(
              RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas,
              PersonaActual);

            PersonaActual.PersonasAfectadas = RUV.I.DeclaracionActual.PersonasAfectadas;
            RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas.Add(PersonaActual);

            //Sipod.I.UIPrincipal.Dispatcher.Invoke(
            //new Action(() =>
            //  {
            //    Sipod.I.DeclaracionActual.PersonasAfectadas.ListaPersonas.Add(PersonaActual);
            //  }
            //  ), System.Windows.Threading.DispatcherPriority.Normal, null);

            //Sipod.I.MultiTarea.EjecutarEnBackground(
            //new Action(() =>
            //  {
            //    Sipod.I.DeclaracionActual.PersonasAfectadas.ListaPersonas.Add(PersonaActual);
            //  }
            //  ));

            CorregirLosConsecutivos();

            lbxListaPersonas.SelectedItem = null;
            EdicionVisible = false;
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
            var Seleccionado = (sender as ListBox).SelectedItem as clsPersonaAfectada;
            if (Seleccionado == null) return;

            // Crear una copia, que será la que se edite.
            PersonaEdicion =
              RUV.I.Util.CrearCopia<clsPersonaAfectada>(Seleccionado);
            PersonaEdicion.CopiarColeccionesDesde(Seleccionado);
            PersonaEdicion.ID = Seleccionado.ID;

            svEdicion.DataContext = PersonaEdicion;
            EdicionVisible = true;
            OperacionRegistroActual = eTipoOperacionRegistro.Editar;
            tbxPrimerNombre.Focus();

            // Forzar la validación.
            if (RUV.I.UIPrincipal.ValidadorActual != null)
                RUV.I.UIPrincipal.ValidadorActual.Validar();

            if (PersonaEdicion != null && PersonaEdicion.PertenenciaEtnica == (int)Ruv.Infrastructure.Crosscutting.Common.ePertenenciaEtnica.Ninguna)
                txtOtraComunidadEtnica.IsEnabled = false;
            else
                txtOtraComunidadEtnica.IsEnabled = true;

            this.tbxPrimerNombre.IsEnabled = true;
            this.tbxSegundoNombre.IsEnabled = true;
            this.tbxPrimerApellido.IsEnabled = true;
            this.tbxSegundoApellido.IsEnabled = true;
            this.cb01TipoDoc.IsEnabled = true;
            this.tbxNumeroDocumento.IsEnabled = true;

            var personas = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsPersonasAfectadas;
            if (personas != null)
            {
                Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDeclaracion DeclaracionActual = personas.Declaracion;
                if (DeclaracionActual.AutoGeneradoPorRadicacion)
                {
                    if (personas.DeclaranteId > 0 && personas.DeclaranteId == PersonaEdicion.ID)
                    {
                        this.tbxPrimerNombre.IsEnabled = H01_TomaDeclaracion.EnableDeclarantePN;
                        this.tbxSegundoNombre.IsEnabled = H01_TomaDeclaracion.EnableDeclaranteSN;
                        this.tbxPrimerApellido.IsEnabled = H01_TomaDeclaracion.EnableDeclarantePA;
                        this.tbxSegundoApellido.IsEnabled = H01_TomaDeclaracion.EnableDeclaranteSA;
                        this.cb01TipoDoc.IsEnabled = H01_TomaDeclaracion.EnableDeclaranteTD;
                        this.tbxNumeroDocumento.IsEnabled = H01_TomaDeclaracion.EnableDeclaranteND;
                    }
                }
            }
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

            // Fusionar el registro en edición con la colección principal.
            clsPersonaAfectada Persona = lbxListaPersonas.SelectedItem as clsPersonaAfectada;
            RUV.I.Util.AlimentarDesde<clsPersonaAfectada>(PersonaEdicion, Persona);
            Persona.CopiarColeccionesDesde(PersonaEdicion);

            // Solicitar la actualización de la interfaze de TomaDeclaracion
            if (Persona.ID == RUV.I.DeclaracionActual.TomaDeclaracion.DeclaranteId)
            {
                RUV.I.DeclaracionActual.TomaDeclaracion.DeclaranteId = Persona.ID;
            }

            //Se reporta el cambio en algunas proiedades para que se validan los datos actualizados sobre la persona
            RUV.I.DeclaracionActual.PersonasAfectadas.ReportarCambioPropiedadAlEditar();

            lbxListaPersonas.SelectedItem = null;
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
                }
                else if (!value && gsSeparador.Visibility != System.Windows.Visibility.Collapsed)
                {
                    cdAcciones.Width = AnchoColumnaEdicionNoVisible;
                    cdEdicion.Width = AnchoColumnaEdicionNoVisible;
                    gsSeparador.Visibility = System.Windows.Visibility.Collapsed;
                    spAcciones.Visibility = System.Windows.Visibility.Collapsed;

                    svEdicion.DataContext = null;
                    PersonaActual = null;
                    if (lbxListaPersonas.SelectedItems.Count == 1)
                        lbxListaPersonas.SelectedItem = null;
                }
            }
        }

        #endregion

        #region CANCELAR LA EDICIÓN

        private void CancelarEdicion(object sender, RoutedEventArgs e)
        {
            EdicionVisible = false;
            OperacionRegistroActual = eTipoOperacionRegistro.Ninguna;
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
        clsPersonaAfectada PersonaActual;

        /// <summary>
        /// La persona que se está editando.
        /// </summary>
        clsPersonaAfectada PersonaEdicion;


        #endregion

        #region INVOCAR LA EDICIÓN DE LA ETNIA

        /// <summary>
        /// Se solicitó modificar la etnia del usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PertenenciaEtnica_Cambiar(object sender, RoutedEventArgs e)
        {
            EdicionPertenenciaEtnica EPE = new EdicionPertenenciaEtnica()
            {
                PersonaAfectada =
                  OperacionRegistroActual == eTipoOperacionRegistro.Insertar ?
                    PersonaActual : PersonaEdicion
            };
            EPE.ShowDialog();

            if (EPE.PersonaAfectada != null && EPE.PersonaAfectada.PertenenciaEtnica == (int)Ruv.Infrastructure.Crosscutting.Common.ePertenenciaEtnica.Ninguna)
                txtOtraComunidadEtnica.IsEnabled = false;
            else
                txtOtraComunidadEtnica.IsEnabled = true;
        }

        #endregion

        #region ISeccionRegistro

        public eSeccionRegistro Seccion
        { get { return eSeccionRegistro.H02_PersonasAfectadas; } }

        public bool RequireScrollBars { get { return false; } }

        public void MostrarEnInterfase()
        {
            // Cuando se invoque la interfase, volver al modo oculto.
            EdicionVisible = false;
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
            Ruv.WPF.Captura.Infrastructure.clsUtil cls = new Ruv.WPF.Captura.Infrastructure.clsUtil();

            // ¿Hay alguien seleccionado?
            if (lbxListaPersonas.SelectedItems.Count == 0) return;

            // Confirmar.
            if (!RUV.I.UIPrincipal.UsuarioConfirmar(
              "¿Desea quitar esta(s) persona(s) de la declaración?"))
                return;

            var personas = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsPersonasAfectadas;
            Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDeclaracion DeclaracionActual = personas.Declaracion;

            if (personas != null && DeclaracionActual != null && DeclaracionActual.AutoGeneradoPorRadicacion
                && lbxListaPersonas.SelectedItems.Cast<clsPersonaAfectada>().Any(x => x.ID == personas.DeclaranteId))
            {
                string msgQuitarMasPersonas = lbxListaPersonas.SelectedItems.Cast<clsPersonaAfectada>().Count() > 1 ? " Intente quitar el resto de personas." : string.Empty;
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario("No se puede eliminar al declarante de la declaración." + msgQuitarMasPersonas);
                return;
            }

            // Quitar el registro.
            ArrayList msgQuitarPersona = new ArrayList();
            ArrayList msgQuitarAnexo = new ArrayList();
            for (int i = lbxListaPersonas.SelectedItems.Cast<clsPersonaAfectada>().Count() - 1; i >= 0; i--)
            {
                bool entro = false;
                var persona = lbxListaPersonas.SelectedItems.Cast<clsPersonaAfectada>().ElementAt(i);

                if (personas != null && DeclaracionActual.AutoGeneradoPorRadicacion)
                    if (personas.DeclaranteId > 0 && personas.DeclaranteId == persona.ID)
                        continue;

                //remueve la persona
                clsPersonaAfectada personaRemover = persona; //lbxListaPersonas.SelectedItems.Cast<clsPersonaAfectada>().FirstOrDefault();
                if (personaRemover != null)
                {
                    //RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas.Remove(personaRemover);
                    // se valida que la victima no este en ningun anexo
                    bool vicA01 = RUV.I.DeclaracionActual.A01.Any(x => x.Victimas.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    if (vicA01) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("1"); entro = true; }
                    bool vicA02 = RUV.I.DeclaracionActual.A02.Any(x => x.Victimas.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    if (vicA02) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("2"); entro = true; }
                    bool vicA03 = RUV.I.DeclaracionActual.A03.Any(x => x.Victimas.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    if (vicA03) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("3"); entro = true; }
                    bool vicA04 = RUV.I.DeclaracionActual.A04.Any(x => x.Victimas.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    if (vicA04) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("4"); entro = true; }
                    bool vicA05 = RUV.I.DeclaracionActual.A05.Any(x => x.Victimas.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    if (vicA05) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("5"); entro = true; }
                    bool vicA06 = RUV.I.DeclaracionActual.A06.Any(x => x.Victimas.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    if (vicA06) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("6"); entro = true; }
                    bool vicA07 = RUV.I.DeclaracionActual.A07.Any(x => x.Victimas.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    if (vicA07) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("7"); entro = true; }
                    bool vicA08 = RUV.I.DeclaracionActual.A08.Any(x => x.Victimas.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    if (vicA08) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("8"); entro = true; }
                    bool vicA09 = RUV.I.DeclaracionActual.A09.Any(x => x.Victimas.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    if (vicA09) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("9"); entro = true; }
                    bool vicA10 = RUV.I.DeclaracionActual.A10.Any(x => x.Victimas.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    if (vicA10) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("10"); entro = true; }
                    bool vicA11i = RUV.I.DeclaracionActual.A11.Any(x => x.BienesInmuebles.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    bool vicA11m = RUV.I.DeclaracionActual.A11.Any(x => x.BienesMuebles.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    /*bool vicA11c = RUV.I.DeclaracionActual.A11.Any(x => x.CreditosPasivos.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));*/
                    if (vicA11i || vicA11m/* || vicA11c*/) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("11"); entro = true; }
                    bool vicA13 = RUV.I.DeclaracionActual.A13.Any(x => x.ListaPersonas.Any(y => y.PersonaAfectadaId == personaRemover.ID && y.EstadoRegistro != eEstadoRegistro.Eliminado));
                    if (vicA13) { msgQuitarPersona.Add(personaRemover.NombreCompleto); msgQuitarAnexo.Add("13"); entro = true; }

                }
                if (!entro)
                {
                    //RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas.Where(x => x.ID == personaRemover.ID).FirstOrDefault().EstadoRegistro = eEstadoRegistro.Eliminado;
                    //metodo para retirar de la lista visualmente los registros que se seleccionan y se les da en el boton "quitar"
                    int fila = RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas.IndexOf(personaRemover);
                    if (fila == 0)
                        RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas.Move(fila, fila);
                    else
                        RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas.Move(fila, fila - 1);

                    RUV.I.Util.BorrarEntidad<clsPersonaAfectada>(
                            RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas,
                            lbxListaPersonas.SelectedItems.Cast<clsPersonaAfectada>().ElementAt(i));
                }
            }
            if (msgQuitarPersona.Count > 0)
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario("No se pueden eliminar las personas " + cls.ConcatenaValoresSinDuplicados(msgQuitarPersona, ", ") + " de la declaración porque estan relacionadas a los anexos: " + cls.ConcatenaValoresSinDuplicados(msgQuitarAnexo, ",") + ". Primero eliminelas de su respectivo anexo.");
                return;
            }
            CorregirLosConsecutivos();

            EdicionVisible = false;
        }

        #endregion

        #region CORRECCIÓN DE LOS CONSECUTIVOS.

        /// <summary>
        /// Corregir el número de consecutivo en caso de presentarse algún salto.
        /// </summary>
        void CorregirLosConsecutivos()
        {
            var Lista = RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas
              .Where(x => x.EstadoRegistro != Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Eliminado)
              .OrderBy(x => x.NumeroConsecutivo == 0 ? int.MaxValue : x.NumeroConsecutivo)
              .ToArray();

            int Numero = 1;
            foreach (var item in Lista)
            {
                if (item.NumeroConsecutivo != Numero)
                    item.NumeroConsecutivo = Numero;
                Numero++;
            }

        }

        #endregion

        private void tbxNumeroDocumento_LostFocus(object sender, RoutedEventArgs e)
        {
            RUV.I.Red.VerificarEstadoRed();
            if (PersonaActual != null && PersonaActual.TipoDocumento != null)
            {
                var tipoDocumento = PersonaActual.TipoDocumento;
                if (tipoDocumento.HasValue && RUV.I.Red.EstadoRed == eEstadoRed.Disponible)
                {
                    BuscarPersona();
                }
                else
                {
                    tbxPrimerNombre.IsEnabled = tbxSegundoNombre.IsEnabled = tbxPrimerApellido.IsEnabled = tbxSegundoApellido.IsEnabled = cifFechaNacimiento.IsEnabled = true;
                }
            }
        }

        private void cb01TipoDoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var seleccionado = sender as ComboBox;
            if (seleccionado.SelectedValue != null)
            {
                RUV.I.Red.VerificarEstadoRed();
                if (PersonaActual != null && PersonaActual.TipoDocumento != null && Convert.ToInt32(seleccionado.SelectedValue) != PersonaActual.TipoDocumento)
                {
                    var tipoDocumento = PersonaActual.TipoDocumento;
                    if (tipoDocumento.HasValue && RUV.I.Red.EstadoRed == eEstadoRed.Disponible && !string.IsNullOrEmpty(PersonaActual.NumeroDocumento))
                    {
                        BuscarPersona();
                    }
                    else
                    {
                        tbxPrimerNombre.IsEnabled = tbxSegundoNombre.IsEnabled = tbxPrimerApellido.IsEnabled = tbxSegundoApellido.IsEnabled = cifFechaNacimiento.IsEnabled = true;
                    }
                }
            }
        }

        public void BuscarPersona()
        {
            bool encontrado = false;
            RUV.I.UIPrincipal.BloquearInterfase = "Buscando Persona en Registraduria";
            RUV.I.MultiTarea.EjecutarEnBackground(
            () =>
            {
                try
                {

                    var personas = RUV.I.Red.ServicioGeneral.BuscarPersonaRNEC(PersonaActual.NumeroDocumento, PersonaActual.TipoDocumento.Value);
                    var lstPersonas = personas.ToList();
                    if (lstPersonas.Count > 0 && lstPersonas.First().estado_cedula != "SIN INFORMACION" && !string.IsNullOrEmpty(lstPersonas.First().nom1))
                    {
                        this.Dispatcher.Invoke(
                       new Action(() =>
                       {
                           ConsultaRNEC consultaRNEC = new ConsultaRNEC(personas.ToList()) { Owner = RUV.I.UIPrincipal };
                           consultaRNEC.ShowDialog();
                           if (consultaRNEC.DialogResult.HasValue && consultaRNEC.DialogResult.Value)
                           {
                               var resultado = consultaRNEC.PersonaSeleccionada;
                               if (resultado.estado_cedula != "SIN INFORMACION" && resultado.estado_cedula != null && PersonaActual != null)
                               {
                                   PersonaActual.PrimerNombre = resultado.nom1.Trim();
                                   PersonaActual.SegundoNombre = resultado.nom2;
                                   PersonaActual.PrimerApellido = resultado.ape1;
                                   PersonaActual.SegundoApellido = resultado.ape2;
                                   PersonaActual.FechaNacimiento = !string.IsNullOrEmpty(resultado.fechaNacimiento) ? Convert.ToDateTime(resultado.fechaNacimiento) : DateTime.MinValue;
                                   encontrado = true;
                               }
                               else
                               {
                                   if (PersonaActual != null)
                                   {
                                       PersonaActual.PrimerNombre = string.Empty;
                                       PersonaActual.SegundoNombre = string.Empty;
                                       PersonaActual.PrimerApellido = string.Empty;
                                       PersonaActual.SegundoApellido = string.Empty;
                                       PersonaActual.FechaNacimiento = null;
                                       encontrado = false;
                                   }
                               }
                           }
                       }
                       ), System.Windows.Threading.DispatcherPriority.Normal, null);
                    }
                    else
                    {
                        this.Dispatcher.Invoke(
                       new Action(() =>
                       {
                           RUV.I.UIPrincipal.BloquearInterfase = null;
                           tbxPrimerNombre.IsEnabled = tbxSegundoNombre.IsEnabled = tbxPrimerApellido.IsEnabled = tbxSegundoApellido.IsEnabled = cifFechaNacimiento.IsEnabled = true;
                       }
                       ), System.Windows.Threading.DispatcherPriority.Normal, null);
                    }
                }
                catch (Exception)
                {
                    encontrado = false;
                }
                finally
                {
                    this.Dispatcher.Invoke(
                    new Action(() =>
                    {
                        RUV.I.UIPrincipal.BloquearInterfase = null;
                        if (encontrado)
                        {
                            tbxPrimerNombre.IsEnabled = tbxSegundoNombre.IsEnabled = tbxPrimerApellido.IsEnabled = tbxSegundoApellido.IsEnabled = cifFechaNacimiento.IsEnabled = false;
                        }
                        else
                        {
                            tbxPrimerNombre.IsEnabled = tbxSegundoNombre.IsEnabled = tbxPrimerApellido.IsEnabled = tbxSegundoApellido.IsEnabled = cifFechaNacimiento.IsEnabled = true;
                        }
                    }
                    ), System.Windows.Threading.DispatcherPriority.Normal, null);
                }
            }
            );
        }
    }
}
