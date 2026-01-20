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
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.WPF.Captura.Infrastructure;
using Ruv.WPF.Captura.Controles;
using System.IO;
using Ruv.Infrastructure.Crosscutting.Common;
using resxGeneral = Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.WPF.Captura.Utils;


namespace Ruv.WPF.Captura.Registro.Secciones
{
    /// <summary>
    /// Lógica de interacción para Radicacion.xaml
    /// </summary>
    public partial class Radicacion : Page
    {
        ResumenValidacion RV;
        //GestorDocESigmaCliente objeSigmaCliente = new GestorDocESigmaCliente();
        // Thread workerThread = new Thread();

        public Radicacion()
        {
            InitializeComponent();
            DatoRadicacion = new clsRadicacion();
            DataContext = DatoRadicacion;

            RV = new ResumenValidacion();
            Extensiones.BindingEstablecer(this, null, RV, ResumenValidacion.ContenedorProperty, BindingMode.OneWay, null, true);

            // Vincular el ResumenValidacion con el focus.
            RV.AdornoFoco = RUV.I.UIPrincipal.AdornoFocoValidacion;
            RV.CambioEnReporteError += RV_CambioEnReporteError;

            spValidadores.Children.Clear();
            spValidadores.Children.Add(RV);

            this.Loaded += new RoutedEventHandler(Radicacion_Loaded);
        }

        void Radicacion_Loaded(object sender, RoutedEventArgs e)
        {
            RV.Validar();
            //if (listaOpciones1.ListaTBs != null)
            //    foreach (var item in listaOpciones1.ListaTBs)
            //    {
            //        //item.TabIndex = 8;
            //        item.TabIndex = listaOpciones1.TabIndex;
            //    }
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
        }

        clsRadicacion DatoRadicacion;

        private void Radicar_Click(object sender, RoutedEventArgs e)
        {
            string msgException = String.Empty;
            if (!RUV.I.Configuraciones.ConfiguracionGeneral.OmitirValidacionesAlEnviar)
            {
                RV.Validar();
                if (RV.HasErrors) return;
            }

            RUV.I.UIPrincipal.BloquearInterfase = "Radicando";


            decimal consec = 0;
            if (DatoRadicacion != null)
            {

                DatoRadicacion.ID_USUARIO_RADICA = RUV.I.Usuario.Id;
                DatoRadicacion.ID_UTERRITORIALRADICA = (short)RUV.I.Usuario.UnidadTerritorialId;

                //Verifica que el archivo aun exista
                if (!File.Exists(txtRutaImagen.Text))
                {
                    RUV.I.UIPrincipal.BloquearInterfase = null;
                    MessageBox.Show("El archivo seleccionado ya no existe,\ndebe seleccionar otro.");
                    return;
                }


                if ((int)cb01TipoRad.SelectedValue == (int)eTipoRadicacion.RadicacionDeclaracion)
                {
                    string mensaje = string.Empty;
                    string msgEsigna = string.Empty;
                    RUV.I.MultiTarea.EjecutarEnBackground((() =>
                    {
                        consec = RUV.I.Red.ServicioGeneral.GuardarRadicacion(DatoRadicacion);
                        FinBusqueda(consec, DatoRadicacion, ref mensaje);
                    }),
                        (() =>
                        {
                            RUV.I.UIPrincipal.BloquearInterfase = null;
                            tbNroFormulario.Focus();
                            MessageBox.Show(mensaje);
                            tbConsecutivo.Text = mensaje;
                            DatoRadicacion = null;
                            DatoRadicacion = new clsRadicacion();
                            DataContext = DatoRadicacion;
                        }));
                }
                else
                {
                    string mensaje = string.Empty;
                    string cError = string.Empty;
                    RUV.I.MultiTarea.EjecutarEnBackground((() =>
                    {
                        consec = RUV.I.Red.ServicioRadicacion.RadicarDevolucion(DatoRadicacion, ref cError);
                        FinBusqueda(consec, DatoRadicacion, ref mensaje);
                    }),
                        (() =>
                        {
                            RUV.I.UIPrincipal.BloquearInterfase = null;
                            tbNroFormulario.Focus();
                            MessageBox.Show(mensaje);
                            tbConsecutivo.Text = mensaje;
                            DatoRadicacion = null;
                            DatoRadicacion = new clsRadicacion();
                            DataContext = DatoRadicacion;
                        }));


                }
            }
        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = resxGeneral::General.FiltrosCargasDescargas;
            dlg.InitialDirectory = RUV.I.Configuraciones.Ubicaciones.OrigenDeclaraciones;
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                txtRutaImagen.Text = filename;
                //Jhon LIDR2 21/02/2014                
                if (!clsTipoImagen.validaExtensionImagen(filename))
                {
                    MessageBox.Show(" Tipo archivo seleccionado no es válido.\n Debe seleccionar archivos PDF o Imágenes.");
                    txtRutaImagen.Text = "";
                }
            }
        }

        [Obsolete("Antigua forma de guardar")]
        void OldRadicar()
        {
            decimal consec = 0;
            if (DatoRadicacion != null)
            {
                clsRed servicio = new clsRed();
                DatoRadicacion.ID_USUARIO_RADICA = RUV.I.Usuario.Id;
                consec = servicio.ServicioGeneral.RadicacionAlmacenar(DatoRadicacion);
                try
                {
                    consec = servicio.ServicioGeneral.RadicacionAlmacenar(DatoRadicacion);
                }
                catch (System.TimeoutException ex)
                {
                    Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada DE = new Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada(ex, "Radicación", "Problemas almacenando radicación", "Agotado tiempo de espera del servicio, por favor intente nuevamente.");
                    DE.ShowDialog();
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("RADICACION_REPETIDA") > 0)
                    {
                        Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada DE = new Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada(ex, "Radicación", "Problemas almacenando radicación", string.Format("Número de formulario repedito. {0}\"{1}\" ya existe registrado una radicación con . Verifique por favor.", Environment.NewLine, DatoRadicacion.NRO_FORMULARIO ?? string.Empty));
                        DE.ShowDialog();
                    }
                    else
                    {
                        Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada DE = new Ruv.WPF.Captura.Seguridad.DesplegarExceptionControlada(ex, "Radicación", "Problemas almacenando radicación", "Se presentó un problema al invocar el servicio, por favor intente nuevamente.");
                        DE.ShowDialog();
                    }
                }
            }
            if (consec != 0)
            {
                textBlock2.Visibility = System.Windows.Visibility.Visible;
                string strMensaje = consec.ToString() + " para el Formulario " + DatoRadicacion.NRO_FORMULARIO;
                tbConsecutivo.Text = strMensaje;
                MessageBox.Show("Se creó la radicación # " + strMensaje);
            }
            DatoRadicacion = new clsRadicacion();
            DataContext = DatoRadicacion;
            tbNroFormulario.Focus();
        }

        void FinBusqueda(decimal consec, clsRadicacion DatoRadicacion, ref string mensaje)
        {
            if (consec > 0)
            {

                string extension = System.IO.Path.GetExtension(DatoRadicacion.RUTAIMAGEN);

                if (extension.ToUpper() == ".PDF")
                {
                    //Cargar pdf al servidor
                    CargarPdf(consec, DatoRadicacion.RUTAIMAGEN);
                }
                else
                {
                    //Cargar imagen al servidor
                    CargarImagen(consec, DatoRadicacion.RUTAIMAGEN);
                }

                DatoRadicacion.ID = (int)consec;
                DatoRadicacion.RUTAIMAGEN = consec.ToString() + extension;

                if (DatoRadicacion.NRO_FORMULARIO != null)
                {
                    mensaje = "Se creó la radicación # " + consec.ToString() + " para el Formulario " + DatoRadicacion.NRO_FORMULARIO;
                }
                else
                {
                    mensaje = "Se creó la radicación # " + consec.ToString();
                }
            }
            else
            {
                if (consec == -2)
                {
                    mensaje = "El numero de Formulario " + DatoRadicacion.NRO_FORMULARIO + " yá existe, no se puede registar nuevamente";
                    RUV.I.Log.Registrar("Excepción en la Radicación '{0}'", DatoRadicacion.NRO_FORMULARIO);
                    RUV.I.Log.Registrar("Detalle excepción: ", mensaje);
                }
                else
                {
                    mensaje = "Se presentó un error al grabar la radicación";
                    RUV.I.Log.Registrar("Excepción en la Radicación '{0}'", DatoRadicacion.NRO_FORMULARIO);
                    RUV.I.Log.Registrar("Detalle excepción: ", mensaje);
                }
            }
        }

        /// <summary>
        /// Envía una imagen al servicio, para ser almacenada en el servidor
        /// </summary>
        /// <param name="consec">Es el nombre del archivo de imagen que se almacenará</param>
        /// <param name="imagePath">Ruta donde se almacenará la imagen en el cliente</param>
        private void CargarImagen(decimal consec, string imagePath)
        {
            //Crea el archivo de imagen 
            BitmapImage bitmapImage;
            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            if (!File.Exists(imagePath))
            {
                RUV.I.UIPrincipal.BloquearInterfase = null;
                MessageBox.Show("El archivo seleccionado ya no existe,\ndebe seleccionar otro.");
                return;
            }
            bitmapImage.StreamSource = System.IO.File.OpenRead(imagePath);
            bitmapImage.EndInit();
            byte[] imageData = new byte[bitmapImage.StreamSource.Length];
            bitmapImage.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);
            bitmapImage.StreamSource.Read(imageData, 0, imageData.Length);

            string fileName = consec.ToString() + System.IO.Path.GetExtension(imagePath);

            //Subir la imagen al servidor
            clsRed servicio = new clsRed();
            servicio.ServicioGeneral.CargarImagen(imageData, fileName);
        }

        /// <summary>
        /// Envía el archivo PDF al servicio para ser almacenado en el servidor
        /// </summary>
        /// <param name="consec">Es el nombre del archivo PDF que se almacenará</param>
        /// <param name="imagePath">Ruta donde se almacenará el PDF en el cliente</param>
        private void CargarPdf(decimal consec, string pdfPath)
        {
            if (!File.Exists(pdfPath))
            {
                RUV.I.UIPrincipal.BloquearInterfase = null;
                MessageBox.Show("El archivo seleccionado ya no existe,\ndebe seleccionar otro.");
                return;
            }

            byte[] _Buffer = null;

            System.IO.FileStream _FileStream = new System.IO.FileStream(pdfPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
            long _TotalBytes = new System.IO.FileInfo(pdfPath).Length;
            _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
            _FileStream.Close();
            _FileStream.Dispose();
            _BinaryReader.Close();

            string fileName = consec.ToString() + System.IO.Path.GetExtension(pdfPath);

            //Subir el PDF al servidor
            clsRed servicio = new clsRed();
            servicio.ServicioGeneral.CargarPdf(_Buffer, fileName);
        }


        //private void cb01TipoRad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if ((sender as ComboBox).SelectedValue != null)
        //    {
        //        if ((int)(sender as ComboBox).SelectedValue == (int)TipoRadicacion.RadicacionDevolución)
        //            EstablecerModo(ModoPagina.devolucion);
        //        else
        //            EstablecerModo(ModoPagina.formulario);
        //    }
        //}

        //private void EstablecerModo(ModoPagina modo)
        //{
        //    if (modo == ModoPagina.devolucion)
        //    {
        //        grdGeografia.Visibility = Visibility.Collapsed;
        //        grbDeclarante.Visibility = Visibility.Collapsed;

        //        DatoRadicacion.ModoFormulario = clsRadicacion.Modo.devolucion;

        //        //cb01TipoDocDecl.BindingGroup.ValidationRules. = false;
        //        //cb01TipoDocDecl.BindingGroup.NotifyOnValidationError = false;


        //    }
        //    else
        //    {
        //        grdGeografia.Visibility = Visibility.Visible;
        //        grbDeclarante.Visibility = Visibility.Visible;

        //        DatoRadicacion.ModoFormulario = clsRadicacion.Modo.formulario;
        //    }
        //}
    }
}
