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
using System.Windows.Shapes;
using System.IO;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
    /// <summary>
    /// Lógica de interacción para DocumentoDigital.xaml
    /// </summary>
    public partial class DocumentoDigital : Window
    {
        public DocumentoDigital()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(DocumentoDigital_Loaded);
        }

        void DocumentoDigital_Loaded(object sender, RoutedEventArgs e)
        {
            // Activar el Watcher.
            FS = new FileSystemWatcher();
            FS.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;

            // Si la ruta no existe, tomar la carpeta de las imágenes.
            if (!Directory.Exists(RUV.I.Configuraciones.ConfiguracionGeneral.RutaArchivosEscaneados))
            {
                RUV.I.Configuraciones.ConfiguracionGeneral.RutaArchivosEscaneados =
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                RUV.I.Configuraciones.ConfiguracionGeneral.Grabar();
            }

            ActivarFS(RUV.I.Configuraciones.ConfiguracionGeneral.RutaArchivosEscaneados);

            ArmarListaArchivos();

            if (string.IsNullOrWhiteSpace(RUV.I.DeclaracionActual.DocumentoDigitalNombre))
            {
                btnArchivoActual.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                btnArchivoActual.Visibility = System.Windows.Visibility.Visible;
                btnArchivoActual.Content = "Archivo actual";
            }
        }

        #region EVENTOS EN EL FILE SYSTEM

        void ArchivoRenombrado(object sender, RenamedEventArgs e)
        {
            ArmarListaArchivos();
        }

        void CambioEnCarpeta(object sender, FileSystemEventArgs e)
        {
            // Re-armar la lista de los archivos.
            ArmarListaArchivos();
        }

        void ActivarFS(string ruta)
        {

            FS.Changed -= CambioEnCarpeta;
            FS.Created -= CambioEnCarpeta;
            FS.Deleted -= CambioEnCarpeta;
            FS.Renamed -= ArchivoRenombrado;
            FS.Path = ruta;

            FS.EnableRaisingEvents = true;

            FS.Changed += CambioEnCarpeta;
            FS.Created += CambioEnCarpeta;
            FS.Deleted += CambioEnCarpeta;
            FS.Renamed += ArchivoRenombrado;
        }

        #endregion

        /// <summary>
        /// Arma y despliega la lista de archivos.
        /// </summary>
        void ArmarListaArchivos()
        {
            string[] TiposDeArchivos = new string[] { "*.pdf", "*.tif", "*.tiff" };
            List<FileInfo> ArchivosInfo = new List<FileInfo>();
            var DI = new DirectoryInfo(RUV.I.Configuraciones.ConfiguracionGeneral.RutaArchivosEscaneados);

            foreach (string UnFiltro in TiposDeArchivos)
                ArchivosInfo.AddRange(DI.GetFiles(UnFiltro));

            List<clsArchivo> Archivos = new List<clsArchivo>();
            ArchivosInfo.ToList().ForEach(x =>
              Archivos.Add(new clsArchivo
              {
                  Nombre = x.Name,
                  Fecha = x.CreationTime
              }));


            this.Dispatcher.Invoke(
            new Action(() =>
              {
                  gbContenidoCarpeta.Header = "Contenido en: " + RUV.I.Configuraciones.ConfiguracionGeneral.RutaArchivosEscaneados;
                  lbxListaArchivos.ItemsSource = Archivos.OrderByDescending(x => x.Fecha);
              }
              ), System.Windows.Threading.DispatcherPriority.Normal, null);
        }

        #region VARIABLES

        FileSystemWatcher FS;

        class clsArchivo
        {
            public DateTime Fecha { get; set; }
            public string Nombre { get; set; }

        }

        #endregion

        #region CAMBIAR LA CARPETA ACTUAL

        private void SeleccionarOtraCarpeta(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog FBD = new System.Windows.Forms.FolderBrowserDialog();
            FBD.SelectedPath = RUV.I.Configuraciones.ConfiguracionGeneral.RutaArchivosEscaneados;

            var DR = FBD.ShowDialog();
            if (DR == System.Windows.Forms.DialogResult.OK)
            {
                RUV.I.Configuraciones.ConfiguracionGeneral.RutaArchivosEscaneados = FBD.SelectedPath;
                ArmarListaArchivos();
                RUV.I.Configuraciones.Grabar();
                ActivarFS(FBD.SelectedPath);
                //FS.Path = FBD.SelectedPath;
            }

        }

        #endregion

        #region SELECCIONAR ARCHIVO

        private void BotonSeleccionarArchivo(object sender, RoutedEventArgs e)
        {
            SeleccionArchivo();
        }

        private void SeleccionarArchivoDobleClick(object sender, MouseButtonEventArgs e)
        {
            SeleccionArchivo();
        }

        /// <summary>
        /// El usuario selecciona un archivo para adjuntar.
        /// </summary>
        private void SeleccionArchivo()
        {
            var Decla = RUV.I.DeclaracionActual;
            var Item = lbxListaArchivos.SelectedItem as clsArchivo;

            if (Item == null)
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario("Seleccione el archivo que quiera adjuntar");
                return;
            }

            //Pedir confirmación en caso de ya existir un archivo anterior.
            if (!string.IsNullOrWhiteSpace(Decla.DocumentoDigitalNombre)
              && !RUV.I.UIPrincipal.UsuarioConfirmar(
              "Esta declaración ya cuenta con un archivo.\n¿Desea reemplazarla el archivo actual?"))
            {
                return;
            }

            // ¿Aún existe el archivo seleccionado?
            var ArchivoSeleccionado = System.IO.Path.Combine(RUV.I.Configuraciones.ConfiguracionGeneral.RutaArchivosEscaneados, Item.Nombre);
            if (!File.Exists(ArchivoSeleccionado))
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                "El archivo seleccionado ya no existe,\ndebe seleccionar otro.");
                return;
            }

            // Verificar el tamaño máximo del archivo.
            var TamañoMaximo = Ruv.WPF.Captura.Properties.Settings.Default.TamañoMaximoTomaLinea;
            var ArchivoInfo = new FileInfo(ArchivoSeleccionado);
            if (ArchivoInfo.Length > (TamañoMaximo * 1000000))
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                $"El archivo seleccionado no puede exceder los {TamañoMaximo} megas en tamaño.");
                return;
            }

            // Generar el nombre del archivo.
            var Extension = System.IO.Path.GetExtension(Item.Nombre);
            var NombreArchivo = string.Format("{0}{1}", Guid.NewGuid().ToString(), Extension);

            try
            {
                // Copiar el archivo a la ruta de la aplicación.
                File.Copy(
                  System.IO.Path.Combine(RUV.I.Configuraciones.ConfiguracionGeneral.RutaArchivosEscaneados, Item.Nombre),
                  System.IO.Path.Combine(RUV.I.Util.RutaArchivosLocales, NombreArchivo));

                // Borrar archivo anterior, si existe.
                if (!string.IsNullOrWhiteSpace(Decla.DocumentoDigitalNombre))
                {
                    var ArchivoAnterior =
                      System.IO.Path.Combine(RUV.I.Util.RutaArchivosLocales, Decla.DocumentoDigitalNombre);
                    if (File.Exists(ArchivoAnterior)) File.Delete(ArchivoAnterior);
                }
            }
            catch (Exception ex)
            {
                RUV.I.UIPrincipal.ReportarErrorDeUsuario("No se pudo acceder al archivo:\n" + ex.Message);
                return;
            }

            // Si la copia fué correcta, actualizar la propiedad correspondiente.
            Decla.DocumentoDigitalNombre = NombreArchivo;

            // TODO: Al enviar subir el archivo al array de bytes.
            this.Close();
        }

        #endregion

        #region VER EL ARCHIVO ACTUAL

        /// <summary>
        /// Lanzar el visor por defecto para el archivo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerArchivoActual(object sender, RoutedEventArgs e)
        {
            var ArchivoLocal = System.IO.Path.Combine(
              RUV.I.Util.RutaArchivosLocales, RUV.I.DeclaracionActual.DocumentoDigitalNombre);
            if (!File.Exists(ArchivoLocal))
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario("El archivo no ha sido encontrado.");
                return;
            }

            System.Diagnostics.Process.Start(ArchivoLocal);
        }

        #endregion

        #region CERRAR ESTA VENTANA

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
