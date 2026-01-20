using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.WPF.Captura.Registro.Secciones.Controles;
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

namespace Ruv.WPF.Captura.Registro.Secciones
{
    /// <summary>
    /// Lógica de interacción para S01_TomaDeclaracion.xaml
    /// </summary>
    public partial class H01_TomaDeclaracion : UserControl, ISeccionRegistro
    {
        public H01_TomaDeclaracion()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(S01_TomaDeclaracion_Loaded);

            var tomaDeclaracion = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsTomaDeclaracion;
            
        }

        private bool isFirstLoad = true;
        public static bool EnableDeclarantePN { get; set; }
        public static bool EnableDeclaranteSN { get; set; }
        public static bool EnableDeclarantePA { get; set; }
        public static bool EnableDeclaranteSA { get; set; }
        public static bool EnableDeclaranteTD { get; set; }
        public static bool EnableDeclaranteND { get; set; }

        void S01_TomaDeclaracion_Loaded(object sender, RoutedEventArgs e)
        {
            var tomaDeclaracion = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsTomaDeclaracion;
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (RUV.I.Usuario.RolesUsuario.Contains(eRolesUsuario.TomaEnLinea))
                    tomaDeclaracion.ModificarTipoDocumentoDeclarante = true;
                else
                    tomaDeclaracion.ModificarTipoDocumentoDeclarante = false;
            }), System.Windows.Threading.DispatcherPriority.ApplicationIdle);

            if (isFirstLoad)
            {
                //Reset values
                H01_TomaDeclaracion.EnableDeclarantePN = true;
                H01_TomaDeclaracion.EnableDeclaranteSN = true;
                H01_TomaDeclaracion.EnableDeclarantePA = true;
                H01_TomaDeclaracion.EnableDeclaranteSA = true;
                //H01_TomaDeclaracion.EnableDeclaranteTD = true;
                //H01_TomaDeclaracion.EnableDeclaranteND = true;
                
                if (tomaDeclaracion != null)
                {
                    Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDeclaracion DeclaracionActual = tomaDeclaracion.Declaracion;

                    
                    
                    if (DeclaracionActual.AutoGeneradoPorRadicacion)
                    {
                        if (tomaDeclaracion.LugarDeclaracionPais.HasValue)
                            this.cb01Pais.IsEnabled = false;
                        if (tomaDeclaracion.LugarDeclaracionDepartamento.HasValue)
                            this.cb01Departamento.IsEnabled = false;
                        if (tomaDeclaracion.LugarDeclaracionMunicipio.HasValue)
                            this.cb01Municipio.IsEnabled = false;
                        //ENTIDADA QUE ATIENDE.
                        if (tomaDeclaracion.LugarDeclaracionEntidadMunicipio.HasValue)
                            this.cb01EntidadMunicipio.IsEnabled = false;
                        //FIN
                        if (!string.IsNullOrWhiteSpace(tomaDeclaracion.DeclarantePrimerNombre))
                            this.txbPrimerNombreDecl.IsEnabled = false;
                        if (!string.IsNullOrWhiteSpace(tomaDeclaracion.DeclaranteSegundoNombre))
                            this.txbSegundoNombreDecl.IsEnabled = false;
                        if (!string.IsNullOrWhiteSpace(tomaDeclaracion.DeclarantePrimerApellido))
                            this.txbPrimerApellidoDecl.IsEnabled = false;
                        if (!string.IsNullOrWhiteSpace(tomaDeclaracion.DeclaranteSegundoApellido))
                            this.txbSegundoApellidoDecl.IsEnabled = false;
                        //if (tomaDeclaracion.DeclaranteTipoDocumento.HasValue)
                        //    this.cb01TipoDocDecl.IsEnabled = false;
                        //if (!string.IsNullOrWhiteSpace(tomaDeclaracion.DeclaranteNumeroDocumento))
                        //    this.tbx01NumeroDocumentoIdentidadDecl.IsEnabled = false;
                    }
                    
                    // Diego Alvarez - 01/10/2013 - Deshabilitar combos de país, departamente y municipio para el rol digitador
                    if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.RuvDigitador))
                    {
                        if (tomaDeclaracion.LugarDeclaracionPais.HasValue)
                        {
                            pnlCb01Pais.IsEnabled = false;
                        }
                        if (tomaDeclaracion.LugarDeclaracionDepartamento.HasValue)
                        {
                            pnlCb01Departamento.IsEnabled = false;
                        }
                        if (tomaDeclaracion.LugarDeclaracionMunicipio.HasValue)
                        {
                            pnlCb01Municipio.IsEnabled = false;
                        }
                    }
                }

                //Definir si se bloquean
                H01_TomaDeclaracion.EnableDeclarantePN = this.txbPrimerNombreDecl.IsEnabled;
                H01_TomaDeclaracion.EnableDeclaranteSN = this.txbSegundoNombreDecl.IsEnabled;
                H01_TomaDeclaracion.EnableDeclarantePA = this.txbPrimerApellidoDecl.IsEnabled;
                H01_TomaDeclaracion.EnableDeclaranteSA = this.txbSegundoApellidoDecl.IsEnabled;
                //H01_TomaDeclaracion.EnableDeclaranteTD = this.cb01TipoDocDecl.IsEnabled;
                //H01_TomaDeclaracion.EnableDeclaranteND = this.tbx01NumeroDocumentoIdentidadDecl.IsEnabled;

                //Se oculta el combo País
                //txt01Pais.Visibility = System.Windows.Visibility.Collapsed;
                //cb01Pais.Visibility = System.Windows.Visibility.Collapsed;
                // La Segunda columna ya no tiene ancho.
                //grdLugarDeclaracion.ColumnDefinitions[0].Width = new GridLength(0.2d, GridUnitType.Pixel);


                isFirstLoad = false;
            }
        }

        #region ISeccionRegistro

        public eSeccionRegistro Seccion
        { get { return eSeccionRegistro.H01_TomaDeclaracion; } }

        public bool RequireScrollBars { get { return true; } }

        public void MostrarEnInterfase()
        {

        }



        #endregion

        private void tbx01NumeroDocumentoIdentidadDecl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RUV.I.Usuario.RolesUsuario.Contains(eRolesUsuario.TomaEnLinea))
            {
                var tomaDeclaracion = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsTomaDeclaracion;

                RUV.I.Red.VerificarEstadoRed();
                if (tomaDeclaracion != null && tomaDeclaracion.DeclaranteTipoDocumento != null)
                {
                    var tipoDocumento = tomaDeclaracion.DeclaranteTipoDocumento;
                    if (tipoDocumento.HasValue && RUV.I.Red.EstadoRed == eEstadoRed.Disponible)
                    {
                        BuscarDeclarante();
                    }
                    else
                    {
                        txbPrimerNombreDecl.IsEnabled = txbSegundoNombreDecl.IsEnabled = txbPrimerApellidoDecl.IsEnabled = txbSegundoApellidoDecl.IsEnabled = cifFechaNacimiento.IsEnabled = true;
                    }
                }
            }
        }


        private void tbx01NumeroDocumentoTutor_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RUV.I.Usuario.RolesUsuario.Contains(eRolesUsuario.TomaEnLinea))
            {
                var tomaDeclaracion = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsTomaDeclaracion;

                RUV.I.Red.VerificarEstadoRed();
                if (tomaDeclaracion != null && tomaDeclaracion.Encargado.RepresentanteTipoDocumento != null)
                {
                    var tipoDocumento = tomaDeclaracion.Encargado.RepresentanteTipoDocumento;
                    if (tipoDocumento.HasValue && RUV.I.Red.EstadoRed == eEstadoRed.Disponible)
                    {
                        BuscarTutor();
                    }
                    else
                    {
                        txbRepresentantePrimerNombre.IsEnabled = txbRepresentanteSegundoNombre.IsEnabled = txbRepresentantePrimerApellido.IsEnabled = txbRepresentanteSegundoApellido.IsEnabled = true;
                    }
                }
            }
        }

        private void cb01TipoDocTutor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RUV.I.Usuario.RolesUsuario.Contains(eRolesUsuario.TomaEnLinea))
            {
                var seleccionado = sender as ComboBox;
                if (seleccionado.SelectedValue != null)
                {
                    var tomaDeclaracion = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsTomaDeclaracion;

                    RUV.I.Red.VerificarEstadoRed();
                    if (tomaDeclaracion != null && tomaDeclaracion.Encargado.RepresentanteTipoDocumento != null && Convert.ToInt32(seleccionado.SelectedValue) != tomaDeclaracion.Encargado.RepresentanteTipoDocumento)
                    {
                        var tipoDocumento = tomaDeclaracion.Encargado.RepresentanteTipoDocumento;
                        if (tipoDocumento.HasValue && RUV.I.Red.EstadoRed == eEstadoRed.Disponible && !string.IsNullOrEmpty(tomaDeclaracion.Encargado.RepresentanteNumeroDocumento))
                        {
                            BuscarTutor();
                        }
                        else
                        {
                            txbRepresentantePrimerNombre.IsEnabled = txbRepresentanteSegundoNombre.IsEnabled = txbRepresentantePrimerApellido.IsEnabled = txbRepresentanteSegundoApellido.IsEnabled = true;
                        }
                    }
                }
            }
        }

        private void cb01TipoDocDecl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RUV.I.Usuario.RolesUsuario.Contains(eRolesUsuario.TomaEnLinea))
            {
                var seleccionado = sender as ComboBox;
                if (seleccionado.SelectedValue != null)
                {
                    var tomaDeclaracion = DataContext as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsTomaDeclaracion;

                    RUV.I.Red.VerificarEstadoRed();
                    if (tomaDeclaracion != null && tomaDeclaracion.DeclaranteTipoDocumento != null && Convert.ToInt32(seleccionado.SelectedValue) != tomaDeclaracion.DeclaranteTipoDocumento)
                    {
                        var tipoDocumento = tomaDeclaracion.DeclaranteTipoDocumento;
                        if (tipoDocumento.HasValue && RUV.I.Red.EstadoRed == eEstadoRed.Disponible && !string.IsNullOrEmpty(tomaDeclaracion.DeclaranteNumeroDocumento))
                        {
                            BuscarDeclarante();
                        }
                        else
                        {
                            txbPrimerNombreDecl.IsEnabled = txbSegundoNombreDecl.IsEnabled = txbPrimerApellidoDecl.IsEnabled = txbSegundoApellidoDecl.IsEnabled = cifFechaNacimiento.IsEnabled = true;
                        }
                    }
                }
            }
        }

        public void BuscarDeclarante()
        {
            bool encontrado = false;
            RUV.I.UIPrincipal.BloquearInterfase = "Buscando Persona en Registraduria";
            RUV.I.MultiTarea.EjecutarEnBackground(
            () =>
            {
                try
                {
                    var tomaDeclaracion = RUV.I.DeclaracionActual.TomaDeclaracion;
                    var personas = RUV.I.Red.ServicioGeneral.BuscarPersonaRNEC(tomaDeclaracion.DeclaranteNumeroDocumento, tomaDeclaracion.DeclaranteTipoDocumento.Value);
                    var lstPersonas = personas.ToList();
                    if (personas.Count() > 0 && lstPersonas.First().estado_cedula != "SIN INFORMACION" && !string.IsNullOrEmpty(lstPersonas.First().nom1))
                    {
                        this.Dispatcher.Invoke(
                       new Action(() =>
                       {
                           ConsultaRNEC consultaRNEC = new ConsultaRNEC(lstPersonas) { Owner = RUV.I.UIPrincipal };
                           consultaRNEC.ShowDialog();
                           if (consultaRNEC.DialogResult.HasValue && consultaRNEC.DialogResult.Value)
                           {
                               var resultado = consultaRNEC.PersonaSeleccionada;
                               if (resultado.estado_cedula != "SIN INFORMACION" && resultado.estado_cedula != null)
                               {
                                   tomaDeclaracion.DeclarantePrimerNombre = resultado.nom1.Trim();
                                   tomaDeclaracion.DeclaranteSegundoNombre = resultado.nom2;
                                   tomaDeclaracion.DeclarantePrimerApellido = resultado.ape1;
                                   tomaDeclaracion.DeclaranteSegundoApellido = resultado.ape2;
                                   tomaDeclaracion.DeclaranteFechaNacimiento = !string.IsNullOrEmpty(resultado.fechaNacimiento) ? Convert.ToDateTime(resultado.fechaNacimiento) : DateTime.MinValue;
                                   encontrado = true;
                               }
                               else
                               {
                                   tomaDeclaracion.DeclarantePrimerNombre = string.Empty;
                                   tomaDeclaracion.DeclaranteSegundoNombre = string.Empty;
                                   tomaDeclaracion.DeclarantePrimerApellido = string.Empty;
                                   tomaDeclaracion.DeclaranteSegundoApellido = string.Empty;
                                   tomaDeclaracion.DeclaranteFechaNacimiento = null;
                                   encontrado = false;
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
                           if (RUV.I.Usuario.RolesUsuario.Contains(eRolesUsuario.TomaEnLinea))
                               txbPrimerNombreDecl.IsEnabled = txbSegundoNombreDecl.IsEnabled = txbPrimerApellidoDecl.IsEnabled = txbSegundoApellidoDecl.IsEnabled = cifFechaNacimiento.IsEnabled = true;
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
                               txbPrimerNombreDecl.IsEnabled = txbSegundoNombreDecl.IsEnabled = txbPrimerApellidoDecl.IsEnabled = txbSegundoApellidoDecl.IsEnabled = cifFechaNacimiento.IsEnabled = false;
                           }
                           else
                           {
                               if (RUV.I.Usuario.RolesUsuario.Contains(eRolesUsuario.TomaEnLinea))
                                   txbPrimerNombreDecl.IsEnabled = txbSegundoNombreDecl.IsEnabled = txbPrimerApellidoDecl.IsEnabled = txbSegundoApellidoDecl.IsEnabled = cifFechaNacimiento.IsEnabled = true;
                           }
                       }
                       ), System.Windows.Threading.DispatcherPriority.Normal, null);
                }
            }
            );
        }

        public void BuscarTutor()
        {
            bool encontrado = false;
            RUV.I.UIPrincipal.BloquearInterfase = "Buscando Persona en Registraduria";
            RUV.I.MultiTarea.EjecutarEnBackground(
            () =>
            {
                try
                {
                    var tomaDeclaracion = RUV.I.DeclaracionActual.TomaDeclaracion;
                    var personas = RUV.I.Red.ServicioGeneral.BuscarPersonaRNEC(tomaDeclaracion.Encargado.RepresentanteNumeroDocumento, tomaDeclaracion.Encargado.RepresentanteTipoDocumento.Value);
                    var lstPersonas = personas.ToList();
                    if (lstPersonas.Count > 0 && lstPersonas.First().estado_cedula != "SIN INFORMACION" && !string.IsNullOrEmpty(lstPersonas.First().nom1))
                    {
                        this.Dispatcher.Invoke(
                       new Action(() =>
                       {
                           ConsultaRNEC consultaRNEC = new ConsultaRNEC(lstPersonas) { Owner = RUV.I.UIPrincipal };
                           consultaRNEC.ShowDialog();
                           if (consultaRNEC.DialogResult.HasValue && consultaRNEC.DialogResult.Value)
                           {
                               var resultado = consultaRNEC.PersonaSeleccionada;
                               if (resultado.estado_cedula != "SIN INFORMACION" && resultado.estado_cedula != null)
                               {
                                   tomaDeclaracion.Encargado.RepresentantePrimerNombre = resultado.nom1.Trim();
                                   tomaDeclaracion.Encargado.RepresentanteSegundoNombre = resultado.nom2;
                                   tomaDeclaracion.Encargado.RepresentantePrimerApellido = resultado.ape1;
                                   tomaDeclaracion.Encargado.RepresentanteSegundoApellido = resultado.ape2;
                                   encontrado = true;
                               }
                               else
                               {
                                   tomaDeclaracion.Encargado.RepresentantePrimerNombre = string.Empty;
                                   tomaDeclaracion.Encargado.RepresentanteSegundoNombre = string.Empty;
                                   tomaDeclaracion.Encargado.RepresentantePrimerApellido = string.Empty;
                                   tomaDeclaracion.Encargado.RepresentanteSegundoApellido = string.Empty;
                                   encontrado = false;
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
                           txbPrimerNombreDecl.IsEnabled = txbSegundoNombreDecl.IsEnabled = txbPrimerApellidoDecl.IsEnabled = txbSegundoApellidoDecl.IsEnabled = cifFechaNacimiento.IsEnabled = true;
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
                            txbRepresentantePrimerNombre.IsEnabled = txbRepresentanteSegundoNombre.IsEnabled = txbRepresentantePrimerApellido.IsEnabled = txbRepresentanteSegundoApellido.IsEnabled = false;
                        }
                        else
                        {
                            txbRepresentantePrimerNombre.IsEnabled = txbRepresentanteSegundoNombre.IsEnabled = txbRepresentantePrimerApellido.IsEnabled = txbRepresentanteSegundoApellido.IsEnabled = true;
                        }
                    }
                    ), System.Windows.Threading.DispatcherPriority.Normal, null);
                }
            }
            );
        }

        private void txtOtroCual_GotFocus(object sender, RoutedEventArgs e)
        {
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
                MessageBox.Show("Diligencie este campo cuando el hecho victimizante sea diferente a los relacionados en el cuadro anterior, deberá en la hoja 3 (narración de los hechos) registrar las circunstancias de tiempo, modo y lugar");
        }
    }
}
