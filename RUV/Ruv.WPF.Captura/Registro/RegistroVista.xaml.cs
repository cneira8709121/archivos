using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Devolucion;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.FirmaDeclaracion;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.WPF.Captura.Controles;
using Ruv.WPF.Captura.Registro.Secciones;
using Ruv.WPF.Captura.Registro.Secciones.Controles;
using ServiceStack.Text;
using resx = Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using resxGeneral = Ruv.Infrastructure.Crosscutting.Resources;
using w = Ruv.Infrastructure.Crosscutting.Utilities.Wacom;

namespace Ruv.WPF.Captura.Registro
{
    public partial class RegistroVista : Page
    {

        #region CONSTRUCTOR

        /// <summary>
        /// Aqui se deja una referencia a la declaración obtenida desde el servidor.
        /// </summary>
        clsDeclaracion DeclaracionObtenida;
        //DatoRadicacion = new clsRadicacion();
        //DataContext = DatoRadicacion;

        public RegistroVista()
        {
            InitializeComponent();
            DeclaracionObtenida = null;
            this.Loaded += new RoutedEventHandler(RegistroVista_Loaded);
            this.Unloaded += new RoutedEventHandler(RegistroVista_Unloaded);
        }

        public RegistroVista(clsDeclaracion declaracion = null)
        {
            InitializeComponent();
            DeclaracionObtenida = declaracion;
            this.Loaded += new RoutedEventHandler(RegistroVista_Loaded);
            this.Unloaded += new RoutedEventHandler(RegistroVista_Unloaded);
        }

        public RegistroVista(clsDeclaracion declaracion = null, bool declaracionObtenida = false)
            : this(declaracion)
        {

            if (declaracion != null)
                this.btnFinalizar.IsEnabled = !declaracionObtenida;
        }

        void RegistroVista_Loaded(object sender, RoutedEventArgs e)
        {
            HayBorradorCargado = false;

            CargueInicialTomaDeclaracion();

            // Diego Alvarez - 18/10/2013 - Actualizar los hechos que se han cargado para que no muestre la validación si ya hay por lo menos uno cargado
            RUV.I.DeclaracionActual.ActualizarConteoHechos();
            MostrarOcultarMenu();
        }

        void RegistroVista_Unloaded(object sender, RoutedEventArgs e)
        {
            // Cerrar la ventana de validación si está abierta.
            RUV.I.Util.CerrarVentanaValidacion();
        }

        #endregion

        #region CARGUE INICIAL

        /// <summary>
        /// Realizar el cargue inicial de la toma de la declaración.
        /// </summary>
        /// 
        void CargueInicialTomaDeclaracion(bool crearNuevaDeclaracion = true)
        {
            // Se crea la nueva declaración.
            if (crearNuevaDeclaracion)
            {
                CrearNuevaDeclaracion();


                if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
                {

                    var Resultado = (from p in RUV.I.InfoGeneral.ListaParametros
                                     where p.Tipo == eTipoParametros.ConfiguracionRUV
                                     && p.Id == (int)eTipoConfiguracionRUV.MensajesTomaEnLinea
                                     select p).FirstOrDefault();
                    if (Resultado != null)
                    {
                        var mensajeTomaEnLinea = JsonSerializer.DeserializeFromString<clsParametrosExtendidosMensajesTL>(Resultado.Valor);

                        if (DateTime.Now >= mensajeTomaEnLinea.fechaInicio && DateTime.Now <= mensajeTomaEnLinea.fechaFin)
                        {
                            MessageBox.Show(mensajeTomaEnLinea.mensaje, "Mensaje toma en linea", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }

                if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
                    RUV.I.DeclaracionActual.VersionFUD = 2;

                RUV.I.DeclaracionActual._Versiones = new List<Versiones>();
                RUV.I.DeclaracionActual.Versiones = new List<Versiones>();
                RUV.I.DeclaracionActual.Versiones.Add(new Versiones { Id = 1, Nombre = "Version 1", Seleccionado = RUV.I.DeclaracionActual.VersionFUD == 1 });
                RUV.I.DeclaracionActual.Versiones.Add(new Versiones { Id = 2, Nombre = "Version 2", Seleccionado = RUV.I.DeclaracionActual.VersionFUD == 2 });


            }
            if (RUV.I.DeclaracionActual.Versiones.Count > 0)
            {
                var ver1 = RUV.I.DeclaracionActual.Versiones.Last(x => x.Id == 1);
                var ver2 = RUV.I.DeclaracionActual.Versiones.Last(x => x.Id == 2);
                RUV.I.DeclaracionActual.Versiones = new List<Versiones>();
                RUV.I.DeclaracionActual.Versiones.Add(ver1);
                RUV.I.DeclaracionActual.Versiones.Add(ver2);
            }
            //Determinar si carga o descarga declaracion
            if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.Requerir_Validaciones_Obcionales))
            {
                bmDescargarDeclaEscaneada.Visibility = System.Windows.Visibility.Collapsed;
                bmCargarDeclaEscaneada.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                bmDescargarDeclaEscaneada.Visibility = System.Windows.Visibility.Visible;
                bmCargarDeclaEscaneada.Visibility = System.Windows.Visibility.Collapsed;
            }
            var Decla = RUV.I.DeclaracionActual;
            if (Decla.DocumentoAnexo != null &&
                Decla.DocumentoDigitalNombre != null &&
                Decla.DocumentoDigitalNombre.StartsWith(Decla.RadicacionId.HasValue ?
                Decla.RadicacionId.Value.ToString() :
                string.Empty))
            {
                bmDescargarDeclaEscaneada.Visibility = System.Windows.Visibility.Visible;
            }
            //Determinar si se muestra el boton de devolucion
            if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.Glosas))
                btnDevolver.Visibility = System.Windows.Visibility.Visible;
            else
                btnDevolver.Visibility = System.Windows.Visibility.Collapsed;

            //Determinar si se muestra el boton de Guardar
            if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.DeclaracionGuardar))
                btnGrabarDeclaracion.Visibility = System.Windows.Visibility.Visible;
            else
                btnGrabarDeclaracion.Visibility = System.Windows.Visibility.Collapsed;

            //Determinar si se muestra el boton de Cargar
            if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.DeclaracionCargar))
                btnCargarDeclaracion.Visibility = System.Windows.Visibility.Visible;
            else
                btnCargarDeclaracion.Visibility = System.Windows.Visibility.Collapsed;

            //Determinar si se muestra el boton de Finalizar
            if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.DeclaracionFinalizar))
                btnFinalizar.Visibility = System.Windows.Visibility.Visible;
            else
                btnFinalizar.Visibility = System.Windows.Visibility.Collapsed;

            //Determinar si se muestra el boton de Imprimir
            if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.DeclaracionImprimir))
                btnImprimirDeclaracion.Visibility = System.Windows.Visibility.Visible;
            else
                btnImprimirDeclaracion.Visibility = System.Windows.Visibility.Collapsed;

            //Determinar si se muestra el boton de Toma firma digital
            if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.FirmaDeclaracion))
                btnFirmaDigital.Visibility = System.Windows.Visibility.Visible;
            else
                btnFirmaDigital.Visibility = System.Windows.Visibility.Collapsed;

            // Establecerla como el Data Context.
            DataContext = RUV.I.DeclaracionActual;

            ListaSecciones =
              new Dictionary<eSeccionRegistro, Tuple<UserControl, Controles.ResumenValidacion, BotonMenu>>();
            ListaSecciones.Add(
              eSeccionRegistro.H01_TomaDeclaracion,
              InstanciarNuevaSeccion(eSeccionRegistro.H01_TomaDeclaracion));



            svMain.Content = ListaSecciones[eSeccionRegistro.H01_TomaDeclaracion].Item1;

            spValidadores.Children.Add(ListaSecciones[eSeccionRegistro.H01_TomaDeclaracion].Item2);


            RUV.I.MultiTarea.PosponerEjecucion(100,
              new Action(() =>
              ListaSecciones[eSeccionRegistro.H01_TomaDeclaracion].Item2.Validar()));

            // Vincula la primera sección.
            (svMain.Content as UserControl).Focus();


            // Generar las demás hojas en el background.
            GenerarHojasDeclaracion();

            CompletarInformacionDeFuncionario(RUV.I.DeclaracionActual);

            RUV.I.MultiTarea.PosponerEjecucion(500,
              new Action(() =>
              HabilitarControlesEdicion()));

            GC.Collect();

            svMain.Focus();
        }

        /// <summary>
        /// Genera las 3 restantes hojas iniciales de la declaración.
        /// </summary>
        void GenerarHojasDeclaracion()
        {
            int Tiempo = 100;
            foreach (var item in new eSeccionRegistro[] {
        eSeccionRegistro.H02_PersonasAfectadas,
        eSeccionRegistro.H03_DescripcionHechos,
        eSeccionRegistro.H04_VerificacionProcedimiento})
            {
                ListaSecciones.Add(item, InstanciarNuevaSeccion(item));

                // Invocar la validación.
                var LaSeccion = item;
                RUV.I.MultiTarea.PosponerEjecucion(Tiempo += 100,
                  new Action(() =>
                    ListaSecciones[LaSeccion].Item2.Validar()
                  // TODO: ¿Que la validación inicial sea contra el DataContext?
                  ));
            }
        }

        #endregion

        #region MANEJO DE SECCIONES

        /// <summary>
        /// Contiene una referencia a cada sección que se digita.
        /// </summary>

        Dictionary<eSeccionRegistro,
          Tuple<UserControl, Ruv.WPF.Captura.Controles.ResumenValidacion, BotonMenu>> ListaSecciones;


        static object Lock_PrecargarSeccion = new object();

        /// <summary>
        /// Se procede a cargar en memoria una sección, en el background
        /// </summary>
        /// <param name="seccion"></param>
        void PrecargarSeccionAsync(eSeccionRegistro seccion)
        {
            BackgroundWorker BW = new BackgroundWorker();
            BW.DoWork += new DoWorkEventHandler(PrecargarSeccion_DoWork);
            BW.RunWorkerAsync(seccion);
        }

        /// <summary>
        /// Crea y adiciona la sección a la lista de secciones.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PrecargarSeccion_DoWork(object sender, DoWorkEventArgs e)
        {
            eSeccionRegistro Seccion = (eSeccionRegistro)e.Argument;
            lock (Lock_PrecargarSeccion)
            {
                if (!ListaSecciones.ContainsKey(Seccion))
                {
                    ListaSecciones.Add(Seccion, InstanciarNuevaSeccion(Seccion));
                }
            }
        }

        /// <summary>
        /// Crea una nueva sección en memoria.
        /// </summary>
        /// <param name="seccion"></param>
        /// <returns></returns>
        Tuple<UserControl, ResumenValidacion, BotonMenu> InstanciarNuevaSeccion(eSeccionRegistro seccion)
        {
            Tuple<UserControl, ResumenValidacion, BotonMenu> Resultado = null;
            UserControl ElControl = null;
            BotonMenu ElBoton = null;

            switch (seccion)
            {
                case eSeccionRegistro.H01_TomaDeclaracion:
                    ElControl = new Secciones.H01_TomaDeclaracion();
                    ElBoton = bmH01_TomaDeclaracion;
                    break;
                case eSeccionRegistro.H02_PersonasAfectadas:
                    ElControl = new Secciones.H02_PersonasAfectadas();
                    ElBoton = bmH02_PersonasAfectadas;
                    break;
                case eSeccionRegistro.H03_DescripcionHechos:
                    ElControl = new Secciones.H03_DescripcionHechos();
                    ElBoton = bmH03_DescripcionHechos;
                    break;
                case eSeccionRegistro.H04_VerificacionProcedimiento:
                    ElControl = new Secciones.H04_VerificacionProcedimiento();
                    ElBoton = bmH04_VerificacionProcedimiento;
                    break;
                    // ============================================================
                    // ============================================================
                    //case eSeccionRegistro.Amenaza:
                    //  ElControl = new Secciones.S01_II_Amenaza();
                    //  break;
                    //case eSeccionRegistro.DelitosContraLaLibertad:
                    //  ElControl = new Secciones.S01_III_DelitosLibertad();
                    //  break;
                    //case eSeccionRegistro.DesapariciónForzada:
                    //  ElControl = new Secciones.S01_IV_DesaparicionForzada();
                    //  break;
                    //case eSeccionRegistro.DesplazamientoForzado:
                    //  ElControl = new Secciones.S01_V_DesplazamientoForzado();
                    //  break;
                    //case eSeccionRegistro.Homocidio:
                    //  ElControl = new Secciones.S01_VI_Homicidio();
                    //  break;
                    //case eSeccionRegistro.MinasAntipersonal:
                    //  ElControl = new Secciones.S01_VIII_MinasAntipersona();
                    //  break;
                    //case eSeccionRegistro.VinculacionNiños:
                    //  ElControl = new Secciones.S01_IX_NiñosDesvinculados();
                    //  break;
                    //case eSeccionRegistro.Secuestro:
                    //  ElControl = new Secciones.S01_X_Secuestro();
                    //  break;
                    //case eSeccionRegistro.Tortura:
                    //  ElControl = new Secciones.S01_XI_Tortura();
                    //  break;
            }

            if (ElControl != null)
            {
                ResumenValidacion RV = new ResumenValidacion();

                Extensiones.BindingEstablecer(ElControl, null, RV, ResumenValidacion.ContenedorProperty, BindingMode.OneWay, null, true);
                // Vincular el ResumenValidacion con el focus.
                RV.AdornoFoco = RUV.I.UIPrincipal.AdornoFocoValidacion;

                RV.CambioEnReporteError += RV_CambioEnReporteError;
                Resultado = new Tuple<UserControl, ResumenValidacion, BotonMenu>(ElControl, RV, ElBoton);


            }

            return Resultado;
        }

        /// <summary>
        /// Cada vez que se detecte cambio en las validaciones se revisa para 
        /// marca como completo o incompleto un botón del menú.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RV_CambioEnReporteError(object sender, EventArgs e)
        {
            ResumenValidacion RV = sender as ResumenValidacion;
            ISeccionRegistro SR = RV.Contenedor as ISeccionRegistro;
            eEstadoIngreso Estado = RV.ContenedorEsValido ?
              eEstadoIngreso.IngresoCompleto : eEstadoIngreso.IngresoIncompleto;

            if (ListaSecciones.ContainsKey(SR.Seccion))
            {
                var Seccion = ListaSecciones[SR.Seccion];
                if (Seccion.Item3 != null)
                    Seccion.Item3.EstadoIngreso = Estado;
            }
        }

        #endregion

        #region DETECCIÓN EN EL CAMBIO DE ERRORES


        #endregion

        #region MANEJO DE LA DECLARACIÓN ACTUAL

        /// <summary>
        /// Crea una nueva declaración.
        /// </summary>
        void CrearNuevaDeclaracion()
        {
            ModoSoloLectura = false;

            if (DeclaracionObtenida == null)
            {
                RUV.I.DeclaracionActual = new clsDeclaracion()
                {
                    EstadoDeclaracion = Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion.FinalizaCapturaSinRadicar
                };

                RUV.I.DeclaracionActual.TomaDeclaracion.LugarDeclaracionPais = RUV.I.Usuario.ID_PAIS;

                if (RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.Cargar_Lugar_Declaracion))
                {
                    if (RUV.I.Usuario.ID_PAIS == 0)
                    {
                        RUV.I.Usuario.ID_DEPARTAMENTO = 0;
                        RUV.I.Usuario.ID_MUNICIPIO = 0;
                    }

                    RUV.I.DeclaracionActual.TomaDeclaracion =
                      new clsTomaDeclaracion(RUV.I.DeclaracionActual)
                      {
                          FechaDeclaracion = DateTime.Now.Date,
                          LugarDeclaracionPais = RUV.I.Usuario.ID_PAIS,
                          LugarDeclaracionDepartamento = RUV.I.Usuario.ID_DEPARTAMENTO,
                          LugarDeclaracionMunicipio = RUV.I.Usuario.ID_MUNICIPIO,
                      };

                }
                else
                {
                    RUV.I.DeclaracionActual.TomaDeclaracion =
                    new clsTomaDeclaracion(RUV.I.DeclaracionActual)
                    {
                        FechaDeclaracion = DateTime.Now.Date
                    };
                }
            }
            else
            {
                RUV.I.DeclaracionActual = DeclaracionObtenida;
                DeclaracionObtenida = null;
                if (RUV.I.DeclaracionActual.SoloLectura)
                {
                    ModoSoloLectura = true;
                    HabilitarBotonesOperaciones();
                }

            }
            clsDeclaracion.UsuarioActual = RUV.I.Usuario;
            clsDeclaracion.ConfiguracionValidaciones = RUV.I.InfoGeneral.ListaValidaciones;
            clsDeclaracion.DeclaracionActual.AutoGeneradoPorRadicacion = true;
            CrearDeclarantePrimeraVez();

        }

        /// <summary>
        /// La primera vez que se crea una declaración se crea el declarante
        /// como una persona afectada.
        /// </summary>
        void CrearDeclarantePrimeraVez(clsDeclaracion declaracion = null)
        {
            // El declarante queda automáticamente creado e incluído en la lista de personas afectadas.
            var Decla = declaracion ?? RUV.I.DeclaracionActual;

            Decla.TomaDeclaracion.Declaracion = RUV.I.DeclaracionActual;
            Decla.PersonasAfectadas.Declaracion = RUV.I.DeclaracionActual;
            Decla.PersonasAfectadas.ListaPersonas.ToList()
              .ForEach(x => x.PersonasAfectadas = Decla.PersonasAfectadas);

            if (Decla.PersonasAfectadas.ListaPersonas.Any())
            {
                Decla.TomaDeclaracion.DeclaranteId = Decla.TomaDeclaracion.DeclaranteId;
                return;
            }

            clsPersonaAfectada ElDeclarante = new clsPersonaAfectada()
            {
                PersonasAfectadas = Decla.PersonasAfectadas,
                NumeroConsecutivo = 1
            };

            RUV.I.Util.EntidadEstablecerSiguienteId(
              Decla.PersonasAfectadas.ListaPersonas,
              ElDeclarante);
            Decla.PersonasAfectadas.ListaPersonas.Add(ElDeclarante);
            ElDeclarante.EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar;

            Decla.PersonasAfectadas.DeclaranteId = ElDeclarante.ID;
            Decla.TomaDeclaracion.DeclaranteId = ElDeclarante.ID;
            if (RUV.I.UIPrincipal.PersonaEncontrada != null)
            {
                Decla.TomaDeclaracion.DeclarantePrimerNombre = RUV.I.UIPrincipal.PersonaEncontrada.PrimerNombre;
                Decla.TomaDeclaracion.DeclaranteSegundoNombre = RUV.I.UIPrincipal.PersonaEncontrada.SegundoNombre;
                Decla.TomaDeclaracion.DeclarantePrimerApellido = RUV.I.UIPrincipal.PersonaEncontrada.PrimerApellido;
                Decla.TomaDeclaracion.DeclaranteSegundoApellido = RUV.I.UIPrincipal.PersonaEncontrada.SegundoApellido;
                Decla.TomaDeclaracion.DeclaranteTipoDocumento = RUV.I.UIPrincipal.PersonaEncontrada.IdTipoDocumento;
                Decla.TomaDeclaracion.DeclaranteNumeroDocumento = RUV.I.UIPrincipal.PersonaEncontrada.NumeroDocumento;
                RUV.I.UIPrincipal.PersonaEncontrada = null;
            }

        }

        #endregion

        #region SELECCION DE UN FORMULARIO, ANEXO O ACCION

        /// <summary>
        /// Selección de un formulario.
        /// Se trata de ponerlo sobre la interfase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BontonMenu_Seleccion(object sender, RoutedEventArgs e)
        {


            BotonMenu BM = e.Source as BotonMenu;
            if (BM == null)
                BM = e.OriginalSource as BotonMenu;

            if (BM != null)
                if (BM.Tag != null && BM.Tag is string)
                {
                    if (BM.Tag.ToString() == "Anexos")
                    {
                        LimpiarListaDeSecciones();
                        InvocarAnexo();
                    }
                    else
                        InvocarAccion(BM.Tag.ToString());
                }
                else
                {

                    LimpiarListaDeSecciones();
                    InvocarHoja(BM);
                }
        }

        /// <summary>
        /// Procesar el acceso a una hoja.
        /// </summary>
        /// <param name="BM"></param>
        private void InvocarHoja(BotonMenu BM)
        {
            eSeccionRegistro TipoSeccion = (eSeccionRegistro)
              Enum.Parse(typeof(eSeccionRegistro), BM.Name.Substring(2));
            InvocarHoja(TipoSeccion);
        }

        /// <summary>
        /// Procesar el acceso a una hoja.
        /// </summary>
        /// <param name="BM"></param>
        private void InvocarHoja(eSeccionRegistro tipoSeccion)
        {
            // Si la sección aun no está lista, salir.
            if (!ListaSecciones.ContainsKey(tipoSeccion))
                return;

            // Si es la misma sección, no hacer nada.
            var LaSeccion = ListaSecciones[tipoSeccion];
            if (svMain.Content == LaSeccion.Item1) return;
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
            {
                if (!string.IsNullOrEmpty(RUV.I.DeclaracionActual.DescripcionHechos.Narracion) && (svMain.Content as ISeccionRegistro).Seccion.ToString().Equals("H03_DescripcionHechos"))
                {
                    if (!RUV.I.DeclaracionActual.DescripcionHechos.MostroMensaje)
                    {
                        MessageBox.Show("Lea y verifique que la narración de los hechos corresponde al declarante");
                        RUV.I.DeclaracionActual.DescripcionHechos.MostroMensaje = true;
                    }
                }
            }


            // Reemplazar la actual por la que está en la lista.
            svMain.Content = LaSeccion.Item1;
            (LaSeccion.Item1 as ISeccionRegistro).MostrarEnInterfase();
            spValidadores.Children.Clear();
            spValidadores.Children.Add(LaSeccion.Item2);

            // Quitar o poner las barras de desplazamiento.
            ISeccionRegistro SR = LaSeccion.Item1 as ISeccionRegistro;
            svMain.VerticalScrollBarVisibility = SR.RequireScrollBars ?
              ScrollBarVisibility.Auto : ScrollBarVisibility.Disabled;

            // Lanzar la validación.
            if (RUV.I.DeclaracionActual.SoloLectura)
                HabilitarControlesEdicion();
            else
                RUV.I.MultiTarea.PosponerEjecucion(1000,
                  new Action(() =>
                        LaSeccion.Item2.Validar()
                    ));

            svMain.Focus();
        }

        /// <summary>
        /// Proceso que genera el XPS mientras inicia proceso de transmisión
        /// </summary>
        /// <param name="isValid"></param>
        void HiloTransmision_GeneracionXML(bool isValid)
        {
            RUV.I.UIPrincipal.BloquearInterfase = "Generando XPS...";
            RUV.I.MultiTarea.PosponerEjecucion(1,
                (() =>
                {
                    AdjuntarXPS();
                    if (RUV.I.DeclaracionActual.DocumentoAnexo != null) isValid = true;
                    RUV.I.UIPrincipal.BloquearInterfase = null;

                    if (isValid)
                    {
                        GrabarBorradorAntesTransmicion();
                        EnviarDeclaracionAsync(RUV.I.DeclaracionActual);
                        BorrarBorrador();
                    }
                })
                );
        }

        /// <summary>
        /// Invocar una acción no relacionada con el contenido de la declaración actual.
        /// </summary>
        /// <param name="accion"></param>
        void InvocarAccion(string accion)
        {
            eAccion Accion;
            if (!Enum.TryParse<eAccion>(accion, out Accion))
                return;

            switch (Accion)
            {
                case eAccion.GrabarBorrador:
                    if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
                    {
                        if (!string.IsNullOrEmpty(RUV.I.DeclaracionActual.DescripcionHechos.Narracion) && (svMain.Content as ISeccionRegistro).Seccion.ToString().Equals("H03_DescripcionHechos"))
                        {
                            if (!RUV.I.DeclaracionActual.DescripcionHechos.MostroMensaje)
                            {
                                MessageBox.Show("Lea y verifique que la narración de los hechos corresponde al declarante");
                                RUV.I.DeclaracionActual.DescripcionHechos.MostroMensaje = true;
                            }
                        }
                    }
                    GrabarBorradorDeclaracion();
                    break;
                case eAccion.CargarBorrador:
                    CargarBorradorDeclaracion();
                    break;
                case eAccion.Finalizar:
                    if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
                    {
                        RUV.I.DeclaracionActual.TomaDeclaracion.SeFinaliza = true;
                        if (!RUV.I.DeclaracionActual.DescripcionHechos.MostroMensaje)
                        {
                            if (!string.IsNullOrEmpty(RUV.I.DeclaracionActual.DescripcionHechos.Narracion))
                            {
                                if (MessageBox.Show("Lea y verifique que la narración corresponde al declarante, desea continuar", "", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                                {
                                    RUV.I.DeclaracionActual.DescripcionHechos.MostroMensaje = true;
                                    return;
                                }
                            }
                        }
                    }
                    string msgException = String.Empty;
                    bool isValid = false;

                    //Si no está activo el Pad, la validación del adjunto es obligatoria
                    if (RUV.I.DeclaracionActual.TomaDeclaracion.TieneCorreoElectronico.HasValue && RUV.I.DeclaracionActual.TomaDeclaracion.TieneCorreoElectronico.Value == 1)
                    {
                        if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea)
                                        || RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.RuvDigitador))
                        {
                            NotificacionElectronica notificacionElectronica = new NotificacionElectronica();
                            notificacionElectronica.ShowDialog();
                            if (notificacionElectronica.DialogResult.HasValue && notificacionElectronica.DialogResult.Value)
                            {
                                RUV.I.DeclaracionActual.NotificacionElectronica = notificacionElectronica.Notificacion;
                            }
                            else
                                break;
                        }
                    }
                    if (!IsWacomActive())
                    {
                        if (ValidarArchivoDeclaracionActual())
                        {
                            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
                            {
                                HiloTransmision_GeneracionXML(isValid);

                            }
                            else
                                isValid = true;
                        }
                    }
                    else
                    {
                        if (ValidarFirmas())
                            HiloTransmision_GeneracionXML(isValid);
                    }

                    if (isValid)
                    {

                        //Luis.Esteban 19Jun2012 Antes de trasmitir la declaracion se graba un archivo con la declaración actual
                        //por si ocurre un error sea fácil replicarlo usando dicho archivo.
                        GrabarBorradorAntesTransmicion();
                        EnviarDeclaracionAsync(RUV.I.DeclaracionActual);
                        /* Implementación Esigna*/
                        clsRadicacion objRad = new clsRadicacion();
                        var DeclaracionFile = RUV.I.Configuraciones.Impresion.GenerarXPS(RUV.I.DeclaracionActual);
                        //PdfDocument doc = new PdfDocument();
                        //doc.LoadFromFile(DeclaracionFile, FileFormat.XPS);
                        // TODO: JUAN Llenar los valores del objeto objRad, convertir el XPS de la declaración en PDF.

                        /* Fin Implementacion Esigna*/
                        BorrarBorrador();
                    }
                    break;
                case eAccion.Imprimir:
                    new Imprimir().ShowDialog();
                    //ImprimiDeclaraciónActual();
                    break;
                case eAccion.DocumentoEscaneado:
                    TrabajarDocumentoEscaneado();
                    break;
                case eAccion.VerDocumento:
                    DescargarDocumento();
                    break;
                case eAccion.Glosas:
                    TrabajarGlosas();
                    break;
                case eAccion.Pruebas:
                    var W = new Tmp.Window6();
                    W.ShowDialog();
                    break;
                case eAccion.Devolver:
                    SolicitudDevolucion sDev = new SolicitudDevolucion();
                    clsDevolucion dev = new clsDevolucion()
                    {
                        NIdDeclaracion = RUV.I.DeclaracionActual.ID,
                        NIdUsuario = RUV.I.Usuario.Id
                    };
                    sDev.ucCausales.DataContext = dev;
                    sDev.ucCausales.EParametroTipoCausal = eTipoParametros.CausalesGlosas;
                    sDev.ShowDialog();

                    if (sDev.Cancelado)
                        return;

                    string cError = string.Empty;
                    DevolucionServiceReference.DevolucionServiceClient devService = RUV.I.Red.ServicioDevolucion;
                    devService.SolicitarDevolucion(dev, ref cError);
                    if (!string.IsNullOrEmpty(cError)) MessageBox.Show(string.Format(resx::Errores.General, cError), resx::Errores.ErrorTitulo);
                    else
                    {
                        MessageBox.Show(resx::Informacion.CambiosGuardados, resx::Controles.Informacion);
                        RUV.I.UIPrincipal.NavegarAListaDeTareas();
                    }
                    break;
                case eAccion.TomarFirma:
                    clsDeclaracion decla = (clsDeclaracion)DataContext;
                    // Si no tiene representante, el tipo viene NULL.
                    int? nTipoRepresentante = decla.TomaDeclaracion.Encargado.RepresentanteTipo;

                    // Si no ha respondido si sabe firmar o no, viene NULL y si no sabe firmar viene 0.
                    int? nSabeFirmar = decla.VerificacionProcedimiento.DeclaranteSabeFirmar;
                    List<clsFirma> lstFirma = new List<clsFirma>();

                    if (nTipoRepresentante.HasValue) lstFirma.Add(new clsFirma { firmaOwner = FirmaOwner.TUTOR });
                    if (nSabeFirmar.HasValue && nSabeFirmar.Value == 1) lstFirma.Add(new clsFirma { firmaOwner = FirmaOwner.DECLARANTE });

                    if (lstFirma.Count >= 1)
                    {
                        TomaFirma tf = new TomaFirma();
                        tf.DataContext = lstFirma;
                        tf.ShowDialog();

                        decla.Firmas = (List<clsFirma>)tf.DataContext;
                    }
                    break;
            }
        }

        private void AdjuntarXPS()
        {
            RUV.I.DeclaracionActual.DocumentoAnexo = RUV.I.Configuraciones.Impresion.GenerarXPS(RUV.I.DeclaracionActual);
        }

        private void DescargarDocumento()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = resxGeneral::General.FiltrosCargasDescargas;
            saveFile.DefaultExt = ".pdf";
            saveFile.InitialDirectory = RUV.I.Configuraciones.Ubicaciones.DestinoDescargas;
            saveFile.FileName = System.IO.Path.GetFileName(RUV.I.DeclaracionActual.DocumentoDigitalNombre);
            if (string.IsNullOrEmpty(saveFile.FileName))
            {
                MessageBox.Show(resx::Informacion.NoDocumentoEscaneado, resx::Controles.Advertencia);
                return;
            }
            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    if (RUV.I.DeclaracionActual.DocumentoDigital == null)
                    {
                        string errorFile = string.Empty;
                        string NombreArchivo = string.Empty;
                        RUV.I.DeclaracionActual.DocumentoDigital = RUV.I.Red.ServicioCriticaN.ObtenerImagenRadicacion(RUV.I.DeclaracionActual.RadicacionId.Value, ref NombreArchivo, ref errorFile);
                        File.WriteAllBytes(saveFile.FileName, RUV.I.DeclaracionActual.DocumentoDigital);
                        Notificaciones notifica = new Notificaciones(saveFile.FileName, resx::Informacion.DescargadoCorrectamente);
                        RUV.I.UIPrincipal.Notificar(notifica, string.Empty);
                    }

                    else if (RUV.I.DeclaracionActual.DocumentoDigital != null)
                    {
                        File.WriteAllBytes(saveFile.FileName, RUV.I.DeclaracionActual.DocumentoDigital);
                        Notificaciones notifica = new Notificaciones(saveFile.FileName, resx::Informacion.DescargadoCorrectamente);
                        RUV.I.UIPrincipal.Notificar(notifica, string.Empty);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(resx::Errores.General, ex.Message), resx::Controles.Error);
                }
            }

        }


        bool ValidarArchivoDeclaracionActual()
        {
            //Jhon 19/02/2014 Validamos que la persona este seleccionado para Validacion Captura
            if (RUV.I.DeclaracionActual.A11.Count > 0 && RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.validar_Enmendar_corregir_declaración))
            {
                foreach (clsAnexo11 anex11 in RUV.I.DeclaracionActual.A11)
                {
                    foreach (clsAnexo11_BienMueble bienMueble in anex11.BienesMuebles)
                    {
                        if (!bienMueble.PersonaAfectadaId.HasValue || bienMueble.PersonaAfectadaId == 0)
                        {
                            MessageBox.Show(" Aun no ha seleccionado la persona\n en el anexo 11 pregunta 13.");
                            return false;
                        }
                    }
                }
            }

            if (RUV.I.Usuario.Permisos.Contains(ePermisosUsuario.Requerir_Validaciones_Obcionales))
            {
                if (RUV.I.DeclaracionActual.VerificacionProcedimiento.NumeroTotalSoportes > 0)
                {
                    if (string.IsNullOrWhiteSpace(RUV.I.DeclaracionActual.DocumentosSoporteNombre))
                    {
                        MessageBox.Show("Aun no seleccionan los soportes.\nSeleccione el archivo correspondiente.");
                        return false;
                    }

                    if (RUV.I.DeclaracionActual.UsuarioId == null)
                        RUV.I.DeclaracionActual.UsuarioId = RUV.I.Usuario.Id;

                    var ArchivoAdjunto =
                      System.IO.Path.Combine(
                      RUV.I.Util.RutaArchivosLocales,
                      RUV.I.DeclaracionActual.DocumentosSoporteNombre);

                    if (string.IsNullOrWhiteSpace(RUV.I.DeclaracionActual.DocumentosSoporteNombre)
                      || !System.IO.File.Exists(ArchivoAdjunto))
                    {
                        MessageBox.Show("Aun no seleccionan los soportes.\nSeleccione el archivo correspondiente.");
                        return false;
                    }
                }

                if (((RUV.I.DeclaracionActual.VerificacionProcedimiento.DeclaranteSabeFirmar.HasValue && RUV.I.DeclaracionActual.VerificacionProcedimiento.DeclaranteSabeFirmar.Value == 1) && !IsWacomActive())
                    || (!RUV.I.DeclaracionActual.VerificacionProcedimiento.DeclaranteSabeFirmar.HasValue || RUV.I.DeclaracionActual.VerificacionProcedimiento.DeclaranteSabeFirmar.Value == 0))
                {
                    if (string.IsNullOrWhiteSpace(RUV.I.DeclaracionActual.DocumentoDigitalNombre))
                    {
                        MessageBox.Show("Aun no selecciona el archivo correspondiente a la hoja 4 debidamente firmada");
                        return false;
                    }

                    if (RUV.I.DeclaracionActual.UsuarioId == null)
                        RUV.I.DeclaracionActual.UsuarioId = RUV.I.Usuario.Id;

                    var ArchivoAdjunto =
                      System.IO.Path.Combine(
                      RUV.I.Util.RutaArchivosLocales,
                      RUV.I.DeclaracionActual.DocumentoDigitalNombre);

                    if (string.IsNullOrWhiteSpace(RUV.I.DeclaracionActual.DocumentoDigitalNombre)
                      || !System.IO.File.Exists(ArchivoAdjunto))
                    {
                        MessageBox.Show("Aun no selecciona el archivo correspondiente a la hoja 4 debidamente firmada");
                        return false;
                    }
                    else
                        return true;
                }
                else
                    return true;
            }
            else
                return true;
        }

        private bool ValidarFirmas()
        {
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
            {
                if (RUV.I.DeclaracionActual.VerificacionProcedimiento.NumeroTotalSoportes > 0 &&
                    (RUV.I.DeclaracionActual.DocumentosSoporteNombre == null || !File.Exists(RUV.I.DeclaracionActual.DocumentosSoporteNombre)))
                {
                    MessageBox.Show("Debe cargar los soportes indicados");
                    return false;
                }
                if (RUV.I.DeclaracionActual.Firmas != null && RUV.I.DeclaracionActual.Firmas.Count > 0)
                {
                    if (RUV.I.DeclaracionActual.Firmas.Count == 0)
                    {
                        // Si no ha respondido si sabe firmar o no, viene NULL y si no sabe firmar viene 0.
                        int? nSabeFirmar = RUV.I.DeclaracionActual.VerificacionProcedimiento.DeclaranteSabeFirmar;
                        if (!nSabeFirmar.HasValue)
                        {
                            MessageBox.Show("Deben guardarse las firmas.");
                            return false;
                        }
                        if (nSabeFirmar.Value == 1)
                        {
                            MessageBox.Show("Deben guardarse las firmas.");
                            return false;
                        }
                        return true;
                    }
                    bool bFirmado = false;
                    foreach (clsFirma f in RUV.I.DeclaracionActual.Firmas)
                    {
                        bFirmado = f.firma != null;
                        if (!bFirmado) break;
                    }
                    if (bFirmado) return true;
                    else
                    {
                        MessageBox.Show("Deben guardarse las firmas.");
                        return false;
                    }
                }
                else
                {
                    if (!RUV.I.DeclaracionActual.VerificacionProcedimiento.DeclaranteSabeFirmar.HasValue)
                    {
                        MessageBox.Show("Debe especificar si el declarante sabe firmar");
                        return false;
                    }
                    else if (RUV.I.DeclaracionActual.VerificacionProcedimiento.DeclaranteSabeFirmar.Value == 1)
                    {
                        if (RUV.I.DeclaracionActual.DocumentoDigitalNombre == null || !File.Exists(RUV.I.DeclaracionActual.DocumentoDigitalNombre))
                        {
                            MessageBox.Show("Debe capturar la firma del declarante");
                            return false;
                        }
                    }
                    else if (RUV.I.DeclaracionActual.VerificacionProcedimiento.DeclaranteSabeFirmar.Value == 0)
                    {
                        if (RUV.I.DeclaracionActual.DocumentoDigitalNombre == null || !File.Exists(RUV.I.DeclaracionActual.DocumentoDigitalNombre))
                        {
                            MessageBox.Show("Debe cargar la hoja 4 con la huella del declarante");
                            return false;
                        }
                    }

                    return true;
                }
            }
            else return true;
        }

        private bool IsWacomActive()
        {
            w::Info info = new w::Info();
            bool bActive = info.IsActive;

            return bActive;
        }

        enum eAccion
        {
            GrabarBorrador,
            CargarBorrador,
            Pruebas,
            Finalizar,
            DocumentoEscaneado,
            VerDocumento,
            Imprimir,
            Glosas,
            Devolver,
            TomarFirma,
        }

        #endregion

        #region SELECCION DE UN ANEXO

        /// <summary>
        /// Abrir la ventana de selección de anexos.
        /// </summary>
        void InvocarAnexo()
        {
            var Decla = RUV.I.DeclaracionActual;

            // No permitir continuar si no se ha ingresado al menos una persona afectada.
            if (!Decla.PersonasAfectadas.ListaPersonas.Any())
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                  @"Antes de continuar a cualquier anexo
debe registrar al menos una persona afectada
en la 'HOJA 2 DE 4'.");
                return;
            }
            GC.Collect();

            var VentanaSeleccion = new Ruv.WPF.Captura.Registro.Secciones.Controles.SeleccionAnexo();
            VentanaSeleccion.ShowDialog();
            var Anexo = VentanaSeleccion.AnexoSeleccionado;
            eSeccionRegistro Seccion = eSeccionRegistro.A01;

            if (Anexo == null) return;

            // Si el anexo no existe, crearlo.
            UserControl ControlAnexo = null;

            if (Anexo.Anexo == null)
            {
                IEnumerable<clsEntidadBase> ListaAnexos = null;

                // Nunca se ha invicado el anexo, luego debe crearse.
                switch (Anexo.NumeroAnexo)
                {
                    case 1: Anexo.Anexo = new clsAnexo01(); ListaAnexos = Decla.A01.Cast<clsEntidadBase>(); Decla.A01.Add(Anexo.Anexo as clsAnexo01); Seccion = eSeccionRegistro.A01; break;
                    case 2: Anexo.Anexo = new clsAnexo02(); ListaAnexos = Decla.A02.Cast<clsEntidadBase>(); Decla.A02.Add(Anexo.Anexo as clsAnexo02); Seccion = eSeccionRegistro.A02; break;
                    case 3: Anexo.Anexo = new clsAnexo03(); ListaAnexos = Decla.A03.Cast<clsEntidadBase>(); Decla.A03.Add(Anexo.Anexo as clsAnexo03); Seccion = eSeccionRegistro.A03; break;
                    case 4: Anexo.Anexo = new clsAnexo04(); ListaAnexos = Decla.A04.Cast<clsEntidadBase>(); Decla.A04.Add(Anexo.Anexo as clsAnexo04); Seccion = eSeccionRegistro.A04; break;
                    case 5: Anexo.Anexo = new clsAnexo05(); ListaAnexos = Decla.A05.Cast<clsEntidadBase>(); Decla.A05.Add(Anexo.Anexo as clsAnexo05); Seccion = eSeccionRegistro.A05; break;
                    case 6: Anexo.Anexo = new clsAnexo06(); ListaAnexos = Decla.A06.Cast<clsEntidadBase>(); Decla.A06.Add(Anexo.Anexo as clsAnexo06); Seccion = eSeccionRegistro.A06; break;
                    case 7: Anexo.Anexo = new clsAnexo07(); ListaAnexos = Decla.A07.Cast<clsEntidadBase>(); Decla.A07.Add(Anexo.Anexo as clsAnexo07); Seccion = eSeccionRegistro.A07; break;
                    case 8: Anexo.Anexo = new clsAnexo08(); ListaAnexos = Decla.A08.Cast<clsEntidadBase>(); Decla.A08.Add(Anexo.Anexo as clsAnexo08); Seccion = eSeccionRegistro.A08; break;
                    case 9: Anexo.Anexo = new clsAnexo09(); ListaAnexos = Decla.A09.Cast<clsEntidadBase>(); Decla.A09.Add(Anexo.Anexo as clsAnexo09); Seccion = eSeccionRegistro.A09; break;
                    case 10: Anexo.Anexo = new clsAnexo10(); ListaAnexos = Decla.A10.Cast<clsEntidadBase>(); Decla.A10.Add(Anexo.Anexo as clsAnexo10); Seccion = eSeccionRegistro.A10; break;
                    case 11: Anexo.Anexo = new clsAnexo11(); ListaAnexos = Decla.A11.Cast<clsEntidadBase>(); Decla.A11.Add(Anexo.Anexo as clsAnexo11); Seccion = eSeccionRegistro.A11; break;
                    case 13: Anexo.Anexo = new clsAnexo13(); ListaAnexos = Decla.A13.Cast<clsEntidadBase>(); Decla.A13.Add(Anexo.Anexo as clsAnexo13); Seccion = eSeccionRegistro.A13; break;
                }

                if (Anexo.Anexo == null) return;

                // Asignarle una nueva ID.
                var Entidad = Anexo.Anexo as clsEntidadBase;
                Entidad.EstadoRegistro = Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro.Insertar;
                RUV.I.Util.EntidadEstablecerSiguienteId(
                  ListaAnexos,
                  Anexo.Anexo as clsEntidadBase);
            }

            // Una vez creado, instanciarlo en pantalla.
            switch (Anexo.NumeroAnexo)
            {
                case 1: ControlAnexo = new A01(); break;
                case 2: ControlAnexo = new A02(); break;
                case 3: ControlAnexo = new A03(); break;
                case 4: ControlAnexo = new A04(); break;
                case 5: ControlAnexo = new A05(); break;
                case 6: ControlAnexo = new A06(); break;
                case 7: ControlAnexo = new A07(); break;
                case 8: ControlAnexo = new A08(); break;
                case 9: ControlAnexo = new A09(); break;
                case 10: ControlAnexo = new A10(); break;
                case 11: ControlAnexo = new A11(); break;
                case 13: ControlAnexo = new A13(); break;
            }

            if (ControlAnexo == null)
            {
                RUV.I.InfoGeneral.NumeroAnexoActual = null;
                return;
            }
            RUV.I.InfoGeneral.NumeroAnexoActual = Anexo.NumeroAnexo;

            // Adjuntarle los datos.
            ControlAnexo.DataContext = Anexo.Anexo;

            // Crearle su validador.
            ResumenValidacion RV = new ResumenValidacion();
            Extensiones.BindingEstablecer(
              ControlAnexo, null, RV,
              ResumenValidacion.ContenedorProperty, BindingMode.OneWay);

            // Vincular el ResumenValidacion con el focus.
            RV.AdornoFoco = RUV.I.UIPrincipal.AdornoFocoValidacion;
            RV.CambioEnReporteError += RV_CambioEnReporteError;

            svMain.Content = ControlAnexo;

            (ControlAnexo as ISeccionRegistro).MostrarEnInterfase();
            spValidadores.Children.Clear();
            spValidadores.Children.Add(RV);

            // Quitar o poner las barras de desplazamiento.
            ISeccionRegistro SR = ControlAnexo as ISeccionRegistro;
            if (SR.RequireScrollBars)
            {
                svMain.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                svMain.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            else
            {
                svMain.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                svMain.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }

            // El tamaño mínimo de uno de estos controles es 800.
            ControlAnexo.MinWidth = 800d;

            // Agregarlo a la lista de secciones.
            ListaSecciones.Add(Seccion,
              new Tuple<UserControl, ResumenValidacion, BotonMenu>(ControlAnexo, RV, null));

            // Lanzar la validación.
            if (RUV.I.DeclaracionActual.SoloLectura)
                HabilitarControlesEdicion();
            else
                RUV.I.MultiTarea.PosponerEjecucion(1000,
                  new Action(() =>
                        RV.Validar()
                    ));
        }

        #endregion

        #region LIMPIAR LISTA DE SECCIONES

        /// <summary>
        /// Quita de la memoria los anexos que se encuentren en la lista de sección.
        /// Los controles de los anexos se re-crean siempre que se invoquen.
        /// </summary>
        void LimpiarListaDeSecciones()
        {
            var AnexoActual = ListaSecciones
              .Where(x =>
                x.Value.Item1.DataContext is Ruv.Infrastructure.Crosscutting.Common.IAnexo
                );

            while (AnexoActual.Any())
                ListaSecciones.Remove(AnexoActual.ElementAt(0).Key);
        }

        #endregion

        #region INVOCAR LA IMPRESION

        /// <summary>
        /// Imprimir la declaración actual.
        /// </summary>
        void ImprimiDeclaraciónActual()
        {
            RUV.I.Configuraciones.Impresion.ImprimirDeclaracion(RUV.I.DeclaracionActual);

            //var thread = new Thread(new ThreadStart(SubImprimiDeclaraciónActual));
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
        }

        #endregion

        #region TRABAJAR SOBRE EL DOCUMENTO ESCANEADO

        /// <summary>
        /// Permite trabajar sobre un documento escaneado.
        /// </summary>
        void TrabajarDocumentoEscaneado()
        {
            var Decla = RUV.I.DeclaracionActual;
            //Pedir confirmación en caso de ya existir un archivo anterior.
            if (!string.IsNullOrWhiteSpace(Decla.DocumentoDigitalNombre)
              && !RUV.I.UIPrincipal.UsuarioConfirmar("Esta declaración ya cuenta con un archivo.\n¿Desea reemplazarla el archivo actual?"))
            {
                return;
            }
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = RUV.I.Configuraciones.Ubicaciones.OrigenDeclaraciones;
            dlg.Filter = resxGeneral::General.FiltrosCargasDescargas;
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                try
                {
                    string filename = dlg.FileName;
                    if (!File.Exists(filename))
                    {
                        RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                        "El archivo seleccionado ya no existe,\ndebe seleccionar otro.");
                        return;
                    }

                    // Verificar el tamaño máximo del archivo.
                    var TamañoMaximo = Ruv.WPF.Captura.Properties.Settings.Default.TamañoMaximoTomaLinea;
                    var ArchivoInfo = new FileInfo(filename);
                    if (ArchivoInfo.Length > (TamañoMaximo * 1000000))
                    {
                        RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                        $"El archivo seleccionado no puede exceder los {TamañoMaximo} megas en tamaño.");
                        return;
                    }

                    // Generar el nombre del archivo.
                    var Extension = System.IO.Path.GetExtension(filename);
                    var NombreArchivo = string.Format("{0}{1}", Guid.NewGuid().ToString(), Extension);

                    // Copiar el archivo a la ruta de la aplicación.
                    File.Copy(
                      System.IO.Path.Combine(filename),
                      System.IO.Path.Combine(RUV.I.Util.RutaArchivosLocales, NombreArchivo));

                    // Borrar archivo anterior, si existe.
                    if (!string.IsNullOrWhiteSpace(Decla.DocumentoDigitalNombre))
                    {
                        var ArchivoAnterior =
                          System.IO.Path.Combine(RUV.I.Util.RutaArchivosLocales, Decla.DocumentoDigitalNombre);
                        if (File.Exists(ArchivoAnterior)) File.Delete(ArchivoAnterior);
                    }

                    // Si la copia fué correcta, actualizar la propiedad correspondiente.
                    Decla.DocumentoDigitalNombre = NombreArchivo;
                }
                catch (Exception ex)
                {
                    RUV.I.UIPrincipal.ReportarErrorDeUsuario("No se pudo acceder al archivo:\n" + ex.Message);
                    return;
                }

            }

        }
        #endregion

        #region CREAR GLOSAS E INTENCIONES DE GLOSAS
        void TrabajarGlosas()
        {
            manejoGlosas DG = new manejoGlosas(RUV.I.DeclaracionActual);

            if (DG.ShowDialog() ?? false)
            {
                RUV.I.DeclaracionActual.Glosas = DG.ListaGlosas;
                RUV.I.DeclaracionActual.IGlosas = DG.ListaIntecionesGlosas;
            }
        }
        #endregion

        #region MODO DE SOLO LECTURA

        bool _ModoSoloLectura = false;
        /// <summary>
        /// ¿Se permite modificar la declaración actual?
        /// </summary>
        bool ModoSoloLectura
        {
            get { return _ModoSoloLectura; }
            set { _ModoSoloLectura = value; }
        }

        /// <summary>
        /// Habilitar/deshabilitar los botones que permite persistir 
        /// </summary>
        void HabilitarBotonesOperaciones()
        {
            eAccion EstadoBoton;
            wpBotonesOperaciones.Children.OfType<BotonMenu>()
              .Where(x =>
              {
                  if (x.Tag == null) return false;
                  Enum.TryParse<eAccion>(x.Tag.ToString(), out EstadoBoton);
                  return EstadoBoton != eAccion.Imprimir;
              })
              .Select(x => x).ToList()
              .ForEach(x => x.IsEnabled = !RUV.I.DeclaracionActual.SoloLectura);
        }

        /// <summary>
        /// Bloquear/Desbloquear los controles de edición para evitar la modificación
        /// de la declaración.
        /// </summary>
        void HabilitarControlesEdicion()
        {
            // Deshabilitar los controles para que el usuario no pueda cambiarlos.
            clsUIHelper UIH = new clsUIHelper();
            var Controles = UIH.GetChildren(
              svMain.Content as DependencyObject, CriterioBusquedaControlesSoloLectura, usarVisualTree: true)
              .Select(x => x.SourceControl);

            foreach (var item in Controles)
            {
                if (item.GetType() == typeof(TextBox))
                    (item as TextBox).IsReadOnly = RUV.I.DeclaracionActual.SoloLectura;
                else
                {
                    // Todos los demás, que heredan de UIElement implementan IsEnabled
                    //if (item.GetType() == typeof(ComboBox)
                    //|| item.GetType() == typeof(CheckBox)
                    //|| item.GetType() == typeof(RadioButton)
                    //|| item.GetType() == typeof(ListaOpciones))
                    (item as UIElement).IsEnabled = !RUV.I.DeclaracionActual.SoloLectura;
                }
            }

        }

        /// <summary>
        /// Lista de los tipos de controles que se utilizan para edición.
        /// </summary>
        Type[] ControlesParaEdicion;

        /// <summary>
        /// Permite buscar los controles que deben bloquerse contra edición.
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        FrameworkElementItem CriterioBusquedaControlesSoloLectura(DependencyObject child)
        {
            FrameworkElementItem Resultado = null;
            if (ControlesParaEdicion == null)
                ControlesParaEdicion = new Type[] {
          typeof(TextBox), typeof(ComboBox),
          typeof(RadioButton),typeof(CheckBox),
          typeof(ListaOpciones), typeof(CajaIngresoFecha),
          typeof(Button)};

            if (child != null && ControlesParaEdicion.Contains(child.GetType()))
            {
                Resultado = new FrameworkElementItem
                {
                    Description = null,
                    SourceControl = child as FrameworkElement
                };
            }

            return Resultado;
        }

        /// <summary>
        /// Altera la visibilidad del menú/botones de acuerdo a los permisos del usuario.
        /// </summary>
        public void MostrarOcultarMenu()
        {
            EstablecerVisibilidadMenuItem(eAccion.Glosas.ToString(),
                RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.Glosas));
        }

        /// <summary>
        /// Establece la visibilidad de los Menu/Botones de acuerdo a los permisos del usuario.
        /// </summary>
        /// <param name="itemTag"></param>
        /// <param name="esVisibile"></param>
        void EstablecerVisibilidadMenuItem(string itemTag, Boolean esVisibile)
        {
            var Item = wpRegistro.Children.OfType<BotonMenu>().Where(x =>
                x.Tag != null
                && x.Tag.ToString() == itemTag)
                .FirstOrDefault();

            if (Item != null)
                Item.Visibility = esVisibile ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }



        #endregion



        private void bmDescargarDeclaEscaneada_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RUV.I.DeclaracionActual.VersionFUD = RUV.I.DeclaracionActual.Versiones.FirstOrDefault(x => x.Seleccionado).Id;
            RUV.I.UIPrincipal.BloquearInterfase = "Cargando";
            ListaSecciones.Clear();
            spValidadores.Children.Clear();
            svMain.Content = null;
            ListaSecciones.Clear();
            RUV.I.MultiTarea.EjecutarEnBackground(
              (() =>
              {
                  GC.Collect();
                  // Corregir algunos vínculos.
                  CrearDeclarantePrimeraVez();

                  RUV.I.DeclaracionActual.CrearEnlacesPostCargue();

                  // Actualizar la lista de hechos.
                  RUV.I.DeclaracionActual.ActualizarConteoHechos();

                  this.Dispatcher.Invoke(
                    new Action(() =>
                    {
                        CargueInicialTomaDeclaracion(false);
                        RUV.I.UIPrincipal.BloquearInterfase = null;

                    }
                    ), System.Windows.Threading.DispatcherPriority.Normal, null);
              }));



        }
    }
}

