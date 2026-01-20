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
using static Ruv.WPF.Captura.Registro.Secciones.Controles.SeleccionAnexo;
using System.Windows.Media;

namespace Ruv.WPF.Captura.Registro.Secciones
{
    /// <summary>
    /// Anexo 13 Desplazamiento Masivo
    /// </summary>
    public partial class A13 : UserControl, ISeccionRegistro
    {

        #region CONSTRUCTOR
        public bool ActivoAsocialA13 { get; set; }

        public A13()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(A13_Loaded);
        }

        void A13_Loaded(object sender, RoutedEventArgs e)
        {
            //var personas = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsPersonasAfectadas;
            //var personas = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsAnexo13;
            //if (personas != null)
            //{
            //    Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDeclaracion DeclaracionActual = personas.Declaracion;
            //    if (DeclaracionActual.AutoGeneradoPorRadicacion)
            //    {
            //        if (personas.DeclaranteId > 0)
            //            cmbDeclarante.IsEnabled = false;
            //    }
            //}
            ActivoAsocialA13 = RUV.I.Red.ServicioGeneral.AsociarAnexo13Activo();
            if (ActivoAsocialA13)
                LlenarListaAnexos();
            else
            {
                lbxAnexos.Visibility = Visibility.Collapsed;
                txtNombreAnexos.Visibility = Visibility.Collapsed;
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

            PersonaActual = new clsAnexo13_Victima()
            {
                EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar,
                AnexoPadre = EsteAnexo
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
            clsAnexo13 EsteAnexo = DataContext as clsAnexo13;

            // Agregar el registro a la colección.
            RUV.I.Util.EntidadEstablecerSiguienteId(
              EsteAnexo.ListaPersonas,
              PersonaActual);

            //PersonaActual.PersonasAfectadas = Sipod.I.DeclaracionActual.PersonasAfectadas;
            //Sipod.I.DeclaracionActual.PersonasAfectadas.ListaPersonas.Add(PersonaActual);
            PersonaActual.PersonasAfectadas = EsteAnexo;
            EsteAnexo.ListaPersonas.Add(PersonaActual);

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
            var Seleccionado = (sender as ListBox).SelectedItem as clsAnexo13_Victima;
            if (Seleccionado == null) return;

            Seleccionado.AnexoPadre = EsteAnexo;

            // Crear una copia, que será la que se edite.
            PersonaEdicion =
              RUV.I.Util.CrearCopia<clsAnexo13_Victima>(Seleccionado);
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

            //var personas = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsPersonasAfectadas;
            //if (personas != null)
            //{
            //    Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDeclaracion DeclaracionActual = personas.Declaracion;
            //    if (DeclaracionActual.AutoGeneradoPorRadicacion)
            //    {
            //        if (personas.DeclaranteId > 0 && personas.DeclaranteId == PersonaEdicion.ID)
            //        {
            //            this.tbxPrimerNombre.IsEnabled = H01_TomaDeclaracion.EnableDeclarantePN;
            //            this.tbxSegundoNombre.IsEnabled = H01_TomaDeclaracion.EnableDeclaranteSN;
            //            this.tbxPrimerApellido.IsEnabled = H01_TomaDeclaracion.EnableDeclarantePA;
            //            this.tbxSegundoApellido.IsEnabled = H01_TomaDeclaracion.EnableDeclaranteSA;
            //            this.cb01TipoDoc.IsEnabled = H01_TomaDeclaracion.EnableDeclaranteTD;
            //            this.tbxNumeroDocumento.IsEnabled = H01_TomaDeclaracion.EnableDeclaranteND;
            //        }
            //    }
            //}
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
            clsAnexo13_Victima Persona = lbxListaPersonas.SelectedItem as clsAnexo13_Victima;
            RUV.I.Util.AlimentarDesde<clsAnexo13_Victima>(PersonaEdicion, Persona);
            Persona.CopiarColeccionesDesde(PersonaEdicion);

            // Solicitar la actualización de la interfaze de TomaDeclaracion
            if (Persona.ID == RUV.I.DeclaracionActual.TomaDeclaracion.DeclaranteId)
            {
                RUV.I.DeclaracionActual.TomaDeclaracion.DeclaranteId = Persona.ID;
            }

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
        clsAnexo13_Victima PersonaActual;

        /// <summary>
        /// La persona que se está editando.
        /// </summary>
        //clsPersonaAfectada PersonaEdicion;
        clsAnexo13_Victima PersonaEdicion;


        /// <summary>
        /// El DataContext de este anexo.
        /// </summary>
        public clsAnexo13 EsteAnexo
        {
            get
            {
                return DataContext as clsAnexo13;
            }
        }

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
        { get { return eSeccionRegistro.A13; } }

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
            // ¿Hay alguien seleccionado?
            if (lbxListaPersonas.SelectedItems.Count == 0) return;

            // Confirmar.
            if (!RUV.I.UIPrincipal.UsuarioConfirmar(
              "¿Desea quitar esta(s) persona(s) de este anexo?"))
                return;

            //var personas = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsPersonasAfectadas;
            var personas = DataContext as clsAnexo13;

            Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDeclaracion DeclaracionActual = personas.Declaracion;

            if (personas != null && DeclaracionActual != null && DeclaracionActual.AutoGeneradoPorRadicacion
                && lbxListaPersonas.SelectedItems.Cast<clsAnexo13_Victima>().Any(x => x.ID == personas.DeclaranteId))
            {
                string msgQuitarMasPersonas = lbxListaPersonas.SelectedItems.Cast<clsAnexo13_Victima>().Count() > 1 ? " Se intentará quitar el resto de personas." : string.Empty;
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario("No se puede eliminar al declarante de la declaración." + msgQuitarMasPersonas);
            }

            // Quitar el registro.
            for (int i = lbxListaPersonas.SelectedItems.Cast<clsAnexo13_Victima>().Count() - 1; i >= 0; i--)
            {
                var persona = lbxListaPersonas.SelectedItems.Cast<clsAnexo13_Victima>().ElementAt(i);

                if (personas != null && DeclaracionActual != null && DeclaracionActual.AutoGeneradoPorRadicacion)
                    if (personas.DeclaranteId > 0 && personas.DeclaranteId == persona.ID)
                        continue;
                //RUV.I.Util.BorrarEntidad<clsAnexo13_Victima>( 
                //    persona.PersonasAfectadas.ListaPersonas,
                //  lbxListaPersonas.SelectedItems.Cast<clsAnexo13_Victima>().ElementAt(i));
                RUV.I.Util.BorrarEntidad<clsAnexo13_Victima>(
                    personas.ListaPersonas,
                  lbxListaPersonas.SelectedItems.Cast<clsAnexo13_Victima>().ElementAt(i));

                personas.ListaPersonas.Where(x => x.ID == persona.ID).FirstOrDefault().EstadoRegistro = eEstadoRegistro.Eliminado;
                //metodo para retirar de la lista visualmente los registros que se seleccionan y se les da en el boton "quitar"
                int fila = personas.ListaPersonas.IndexOf(persona);
                if (fila == 0)
                    personas.ListaPersonas.Move(fila, fila);
                else
                    personas.ListaPersonas.Move(fila, fila - 1);
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
            var personas = DataContext as clsAnexo13;

            var Lista = personas.ListaPersonas
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


        static SolidColorBrush EstadoComenzado = new SolidColorBrush(Colors.Green);
        static SolidColorBrush EstadoSinComenzar = new SolidColorBrush(Colors.Red);



        /// <summary>
        /// Llena la lista de los anexos.
        /// </summary>
        void LlenarListaAnexos()
        {
            var Hechos = RUV.I.DeclaracionActual.TomaDeclaracion.Hechos;
            clsValidadorEntidades Validador = new clsValidadorEntidades();
            List<ElementoAnexo> ListaAnexos = new List<ElementoAnexo>();

            string NombreJefeHogar = null;
            SolidColorBrush EstadoAnexo = null;

            for (int i = 0; i < RUV.I.DeclaracionActual.TodosLosAnexos.Where(x => x.Numero != 13).Count(); i++)
            {
                int NumeroAnexo = i + 1;
                var Decla = RUV.I.DeclaracionActual;
                var LosAnexos = Decla.TodosLosAnexos;
                IAnexo ElAnexo = null;

                ElAnexo = LosAnexos.ElementAt(i);
                var Persona = Decla.PersonasAfectadas.ListaPersonas
                  .Where(x => x.ID == (LosAnexos.ElementAt(i).JefeGrupoFamiliarId)
                    && x.ID.HasValue).FirstOrDefault();

                List<eEstadoValidacion> Requeridas = Ruv.WPF.Captura.Infrastructure.clsUtil.ValidacionesRequeridas();
                int validacionesSaltadas = 0;
                if (!RUV.I.ValidadorEntidades.EntidadEsValida(ElAnexo as clsEntidadBase, Requeridas, ref validacionesSaltadas))
                {
                    EstadoAnexo = EstadoSinComenzar;
                    if (Persona != null && !string.IsNullOrWhiteSpace(Persona.NombreCompleto))
                        NombreJefeHogar = string.Format("{0} (no completado)", Persona.NombreCompleto);
                    else
                        NombreJefeHogar = "(no completado)";
                }
                else
                {
                    if (Persona != null)
                    {
                        EstadoAnexo = EstadoComenzado;
                        NombreJefeHogar = Persona.NombreCompleto;
                    }
                    else
                    {
                        if (ElAnexo.Numero == 11)
                        {
                            EstadoAnexo = EstadoComenzado;
                            NombreJefeHogar = "(Anexo11)";
                        }
                        else if (ElAnexo.Numero == 13)
                        {
                            clsAnexo13 Anexo13 = ElAnexo as clsAnexo13;
                            Anexo13.Declaracion = Decla;
                            var PersonaA13 = Anexo13.ListaPersonas
                            .Where(x => x.Relacion == (int)eRelacion.Jefe_de_hogar).FirstOrDefault();

                            if (PersonaA13 != null)
                            {
                                EstadoAnexo = EstadoComenzado;
                                NombreJefeHogar = PersonaA13.NombreCompleto;
                            }
                            else
                            {
                                EstadoAnexo = EstadoSinComenzar;
                                NombreJefeHogar = "(no completado)";
                            }
                        }
                        else
                        {
                            EstadoAnexo = EstadoSinComenzar;
                            NombreJefeHogar = "(no completado)";
                        }
                    }
                }

                ListaAnexos.Add(new ElementoAnexo()
                {
                    NombreAnexo = string.Format("Anexo {0:D2}", LosAnexos.ElementAt(i).Numero),
                    JefeDeHogar = NombreJefeHogar,
                    ColorEstado = EstadoAnexo,
                    Anexo = ElAnexo,
                    NumeroAnexo = LosAnexos.ElementAt(i).Numero
                });
            }

            lbxAnexos.ItemsSource = ListaAnexos.OrderBy(x => x.NombreAnexo).ThenBy(x => x.JefeDeHogar);
            var anexoactual = this.DataContext as clsAnexo13;
            foreach (ElementoAnexo item in lbxAnexos.Items)
            {
                var anexoRealacionado = item.Anexo as clsEntidadBase;
                if (anexoactual.AnexosRelacionados != null)
                {
                    if (anexoactual.AnexosRelacionados.Contains(anexoRealacionado.ID_Interno))
                        lbxAnexos.SelectedItem = item;
                }
            }
        }

        private void lbxAnexos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var anexoactual = this.DataContext as clsAnexo13;
            anexoactual.AnexosRelacionados = new List<Guid>();
            foreach (ElementoAnexo anexo in lbxAnexos.SelectedItems)
            {
                var anexoRealacionado = anexo.Anexo as clsEntidadBase;
                if (!anexoactual.AnexosRelacionados.Contains(anexoRealacionado.ID_Interno))
                    anexoactual.AnexosRelacionados.Add(anexoRealacionado.ID_Interno);
            }
        }

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