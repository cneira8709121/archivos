using Microsoft.Win32;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.FirmaDeclaracion;
using Ruv.WPF.Captura.Controles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using resx = Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using resxGeneral = Ruv.Infrastructure.Crosscutting.Resources;
using w = Ruv.Infrastructure.Crosscutting.Utilities.Wacom;

namespace Ruv.WPF.Captura.Registro.Secciones
{
    /// <summary>
    /// Lógica de interacción para H04_VerificacionProcedimiento.xaml
    /// </summary>
    public partial class H04_VerificacionProcedimiento : UserControl, ISeccionRegistro
    {
        public H04_VerificacionProcedimiento()
        {
            InitializeComponent();

            Extensiones.BindingEstablecer(DataContext,
              "DeclaranteSabeFirmar",
              this, ValorSeleccionUnica_DeclaranteSabeFirmarProperty);

            this.Loaded += new RoutedEventHandler(H04_VerificacionProcedimiento_Loaded);
        }

        void H04_VerificacionProcedimiento_Loaded(object sender, RoutedEventArgs e)
        {
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
            {
                var Decla = RUV.I.DeclaracionActual;

                if (!string.IsNullOrWhiteSpace(Decla.DocumentosSoporteNombre) && Decla.VerificacionProcedimiento.NumeroTotalSoportes > 0)
                {
                    lnkVerSoportes.Visibility = Visibility.Visible;
                    btnSoportes.Visibility = Visibility.Visible;
                }
                else
                {
                    lnkVerSoportes.Visibility = Visibility.Collapsed;
                    btnSoportes.Visibility = Visibility.Collapsed;
                }
                if (!string.IsNullOrWhiteSpace(Decla.DocumentoDigitalNombre))
                {
                    Decla.VerificacionProcedimiento.DebeCargarDeclaracionEscaneada = true;
                    btnDeclaracion.Visibility = Visibility.Visible;
                    lnkVerDeclaracion.Visibility = Visibility.Visible;
                }
                else
                {
                    btnDeclaracion.Visibility = Visibility.Collapsed;
                    lnkVerDeclaracion.Visibility = Visibility.Collapsed;
                }

                if (Decla.DocumentoAnexo != null &&
                    Decla.DocumentoDigitalNombre != null &&
                    Decla.RadicacionId.HasValue &&
                    Decla.DocumentoDigitalNombre.StartsWith(Decla.RadicacionId.HasValue ?
                    Decla.RadicacionId.Value.ToString() :
                    string.Empty))
                {
                    lnkVerDeclaracion.Visibility = Visibility.Collapsed;
                    Decla.VerificacionProcedimiento.DebeCargarDeclaracionEscaneada = false;
                    lnkVerSoportes.Visibility = Visibility.Collapsed;
                    spMain.IsEnabled = false;
                }
            }
        }

        #region ISeccionRegistro

        public eSeccionRegistro Seccion
        { get { return eSeccionRegistro.H04_VerificacionProcedimiento; } }

        public bool RequireScrollBars { get { return true; } }

        public void MostrarEnInterfase()
        { }

        #endregion

        #region MOSTRAR MENSAJE DE ADVERTENCIA SI NO SE SABE FIRMAR

        public int? ValorSeleccionUnica_DeclaranteSabeFirmar
        {
            get { return (int?)GetValue(ValorSeleccionUnica_DeclaranteSabeFirmarProperty); }
            set { SetValue(ValorSeleccionUnica_DeclaranteSabeFirmarProperty, value); }
        }

        public static readonly DependencyProperty ValorSeleccionUnica_DeclaranteSabeFirmarProperty =
            DependencyProperty.Register("ValorSeleccionUnica_DeclaranteSabeFirmar", typeof(int?),
            typeof(H04_VerificacionProcedimiento), new UIPropertyMetadata(null, ValorSeleccionUnica_DeclaranteSabeFirmar_Changed));

        private static void ValorSeleccionUnica_DeclaranteSabeFirmar_Changed(
          DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
            {
                var Decla = RUV.I.DeclaracionActual;
                int? NuevoValor = (int?)e.NewValue;
                if (NuevoValor == 0 && Decla.DocumentoDigitalNombre == null && !File.Exists(Decla.DocumentoDigitalNombre))
                {
                    MessageBox.Show("Recuerde imprimir la hoja 4,\ntomar la huella dactilar del declarante y cargarlo escaneado nuevamente");
                    RUV.I.DeclaracionActual.VerificacionProcedimiento.DebeCargarDeclaracionEscaneada = true;
                    RUV.I.DeclaracionActual.VerificacionProcedimiento.LinkDocumentos = false;

                }
                else
                {
                    if (!NuevoValor.HasValue)
                    {
                        RUV.I.DeclaracionActual.VerificacionProcedimiento.DebeCargarDeclaracionEscaneada = false;
                        RUV.I.DeclaracionActual.VerificacionProcedimiento.LinkDocumentos = false;
                    }
                    else
                    {
                        int? valorAnterior = (int?)e.OldValue;
                        if (valorAnterior == 0 && Decla.DocumentoDigitalNombre != null && File.Exists(Decla.DocumentoDigitalNombre))
                        {
                            Decla.DocumentoDigital = null;
                            Decla.DocumentoDigitalNombre = null;
                            RUV.I.DeclaracionActual.VerificacionProcedimiento.DebeCargarDeclaracionEscaneada = true;
                            RUV.I.DeclaracionActual.VerificacionProcedimiento.LinkDocumentos = false;
                        }

                        if (NuevoValor == 0 && Decla.DocumentoDigitalNombre != null && File.Exists(Decla.DocumentoDigitalNombre))
                        {
                            Decla.DocumentoDigital = null;
                            Decla.DocumentoDigitalNombre = null;
                            RUV.I.DeclaracionActual.VerificacionProcedimiento.DebeCargarDeclaracionEscaneada = true;
                            RUV.I.DeclaracionActual.VerificacionProcedimiento.LinkDocumentos = false;
                        }

                        if ((Decla.DocumentoDigitalNombre == null || !File.Exists(Decla.DocumentoDigitalNombre)) && NuevoValor == 1)
                        {
                            if (Decla.DocumentoDigital == null && !Decla.RadicacionId.HasValue)
                            {
                                if (Decla.Firmas == null || (Decla.Firmas != null && Decla.Firmas.Count == 0))
                                {
                                    if (MessageBox.Show("¿Cuenta con pad digital?", "Pad", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                    {
                                        RUV.I.DeclaracionActual.VerificacionProcedimiento.DebeCargarDeclaracionEscaneada = false;
                                        RUV.I.DeclaracionActual.VerificacionProcedimiento.LinkDocumentos = false;
                                        w::Info info = new w::Info();
                                        if (info.IsActive)
                                        {
                                            clsDeclaracion decla = RUV.I.DeclaracionActual;
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
                                        }
                                        else
                                            MessageBox.Show("Por favor conecte la Pad digital y tome la firma mediante el botón Tomar Firma Digital");

                                    }
                                    else
                                    {
                                        if (Decla.DocumentoDigitalNombre == null || !File.Exists(Decla.DocumentoDigitalNombre))
                                        {
                                            MessageBox.Show("Recuerde imprimir la hoja 4,\ntomar la firma del declarante y cargarlo escaneado nuevamente");
                                            RUV.I.DeclaracionActual.VerificacionProcedimiento.DebeCargarDeclaracionEscaneada = true;
                                            RUV.I.DeclaracionActual.VerificacionProcedimiento.LinkDocumentos = false;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Decla.DocumentoDigital != null && Decla.RadicacionId.HasValue)
                            {
                                RUV.I.DeclaracionActual.VerificacionProcedimiento.DebeCargarDeclaracionEscaneada = false;
                                RUV.I.DeclaracionActual.VerificacionProcedimiento.LinkDocumentos = false;
                            }
                        }
                    }
                }
            }
        }


        #endregion

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var Decla = RUV.I.DeclaracionActual;
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
            {
                int cantidad = Convert.ToInt32((sender as TextBox).Text);
                if (cantidad > 0)
                {
                    if (string.IsNullOrWhiteSpace(RUV.I.DeclaracionActual.DocumentosSoporteNombre))
                    {
                        MessageBox.Show("Por favor cargar los soportes");
                        btnSoportes.Visibility = Visibility.Visible;
                    }
                }
                if (cantidad == 0)
                {
                    if (string.IsNullOrWhiteSpace(Decla.DocumentosSoporteNombre))
                    {
                        lnkVerSoportes.Visibility = Visibility.Collapsed;
                        btnSoportes.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        File.Delete(Decla.DocumentosSoporteNombre);
                        RUV.I.DeclaracionActual.DocumentoAnexo = null;
                        RUV.I.DeclaracionActual.DocumentosSoporteNombre = null;
                        lnkVerSoportes.Visibility = Visibility.Collapsed;
                        btnSoportes.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        void TrabajarDocumentoEscaneado(string tipoDoc)
        {
            var Decla = RUV.I.DeclaracionActual;
            //Pedir confirmación en caso de ya existir un archivo anterior.
            if (tipoDoc == "Declaracion")
            {
                if (!string.IsNullOrWhiteSpace(Decla.DocumentoDigitalNombre)
                  && !RUV.I.UIPrincipal.UsuarioConfirmar("Esta declaración ya cuenta con un archivo.\n¿Desea reemplazarla el archivo actual?"))
                {
                    return;
                }
            }
            if (tipoDoc == "Soportes")
            {
                if (!string.IsNullOrWhiteSpace(Decla.DocumentosSoporteNombre)
                  && !RUV.I.UIPrincipal.UsuarioConfirmar("Esta declaración ya cuenta con un archivo de soporte.\n¿Desea reemplazarla el archivo actual?"))
                {
                    return;
                }
            }
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = RUV.I.Configuraciones.Ubicaciones.OrigenDeclaraciones;
            dlg.Filter = resxGeneral::General.FiltrosCargasDescargas;
            dlg.FilterIndex = 4;
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

                    var rutaArchivo = System.IO.Path.Combine(RUV.I.Util.RutaArchivosLocales, NombreArchivo);

                    if (tipoDoc == "Declaracion")
                    {
                        //Validar tamaño total
                        if (File.Exists(Decla.DocumentosSoporteNombre))
                        {
                            if ((File.ReadAllBytes(Decla.DocumentosSoporteNombre).Length + ArchivoInfo.Length) > (TamañoMaximo * 1000000))
                            {
                                RUV.I.UIPrincipal.ReportarInformacionDeUsuario($"La sumatoria de los archivos cargados no puede exceder los {TamañoMaximo} megas en tamaño.");
                                return;
                            }
                        }

                        // Borrar archivo anterior, si existe.
                        if (!string.IsNullOrWhiteSpace(Decla.DocumentoDigitalNombre))
                        {
                            var ArchivoAnterior =
                              System.IO.Path.Combine(RUV.I.Util.RutaArchivosLocales, Decla.DocumentoDigitalNombre);
                            if (File.Exists(ArchivoAnterior)) File.Delete(ArchivoAnterior);
                        }

                        // Si la copia fué correcta, actualizar la propiedad correspondiente.
                        Decla.DocumentoDigitalNombre = rutaArchivo;
                    }
                    else if (tipoDoc == "Soportes")
                    {
                        //Validar tamaño total
                        if (File.Exists(Decla.DocumentoDigitalNombre))
                        {
                            if ((File.ReadAllBytes(Decla.DocumentoDigitalNombre).Length + ArchivoInfo.Length) > (TamañoMaximo * 1000000))
                            {
                                RUV.I.UIPrincipal.ReportarInformacionDeUsuario($"La sumatoria de los archivos cargados no puede exceder los {TamañoMaximo} megas en tamaño.");
                                return;
                            }
                        }

                        // Borrar archivo anterior, si existe.
                        if (!string.IsNullOrWhiteSpace(Decla.DocumentosSoporteNombre))
                        {
                            var ArchivoAnterior =
                              System.IO.Path.Combine(RUV.I.Util.RutaArchivosLocales, Decla.DocumentosSoporteNombre);
                            if (File.Exists(ArchivoAnterior)) File.Delete(ArchivoAnterior);
                        }

                        // Si la copia fué correcta, actualizar la propiedad correspondiente.
                        Decla.DocumentosSoporteNombre = rutaArchivo;
                    }

                }
                catch (Exception ex)
                {
                    RUV.I.UIPrincipal.ReportarErrorDeUsuario("No se pudo acceder al archivo:\n" + ex.Message);
                    return;
                }

            }

        }

        private void btnSoportes_Click(object sender, RoutedEventArgs e)
        {
            TrabajarDocumentoEscaneado("Soportes");
            var Decla = RUV.I.DeclaracionActual;
            if (!string.IsNullOrWhiteSpace(Decla.DocumentosSoporteNombre))
            {
                lnkVerSoportes.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TrabajarDocumentoEscaneado("Declaracion");
            var Decla = RUV.I.DeclaracionActual;
            if (!string.IsNullOrWhiteSpace(Decla.DocumentoDigitalNombre))
            {
                lnkVerDeclaracion.Visibility = Visibility.Visible;
            }
        }

        private void hlVerDeclaracion_Click(object sender, RoutedEventArgs e)
        {
            DescargarDocumento("Declaracion");
        }

        private void hlVerSoportes_Click(object sender, RoutedEventArgs e)
        {
            DescargarDocumento("Soportes");
        }

        private void DescargarDocumento(string tipoDoc)
        {
            if (tipoDoc == "Declaracion")
            {
                if (!string.IsNullOrWhiteSpace(RUV.I.DeclaracionActual.DocumentoDigitalNombre) && File.Exists(RUV.I.DeclaracionActual.DocumentoDigitalNombre))
                {
                    System.Diagnostics.Process.Start(RUV.I.DeclaracionActual.DocumentoDigitalNombre);
                }
                else
                {
                    MessageBox.Show(resx::Informacion.NoDocumentoEscaneado, resx::Controles.Advertencia);
                    return;
                }
            }
            else if (tipoDoc == "Soportes")
            {
                if (!string.IsNullOrWhiteSpace(RUV.I.DeclaracionActual.DocumentosSoporteNombre) && File.Exists(RUV.I.DeclaracionActual.DocumentosSoporteNombre))
                {
                    System.Diagnostics.Process.Start(RUV.I.DeclaracionActual.DocumentosSoporteNombre);
                }
                else
                {
                    if (RUV.I.DeclaracionActual.DocumentoAnexo != null && !string.IsNullOrEmpty(RUV.I.DeclaracionActual.DocumentoDigitalNombre))
                    {
                        var rutaArchivo =
                              System.IO.Path.Combine(RUV.I.Util.RutaArchivosLocales, RUV.I.DeclaracionActual.DocumentoDigitalNombre);
                        File.WriteAllBytes(rutaArchivo, RUV.I.DeclaracionActual.DocumentoAnexo);
                        System.Diagnostics.Process.Start(rutaArchivo);
                    }
                    else
                    {
                        MessageBox.Show(resx::Informacion.NoDocumentoEscaneado, resx::Controles.Advertencia);
                        return;
                    }
                }
            }

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Solo permite caracteres numéricos
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex que coincide con lo permitido
            return !regex.IsMatch(text);
        }

    }
}

