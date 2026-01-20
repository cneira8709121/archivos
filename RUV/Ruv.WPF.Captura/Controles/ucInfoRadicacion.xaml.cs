using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using System.IO;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using resx = Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.WPF.Captura.Controles;
using resxGeneral = Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.WPF.Captura.Infrastructure;
using Ruv.WPF.Captura.Utils;

namespace Ruv.WPF.Captura
{
	/// <summary>
	/// Interaction logic for ucInfoRadicacion.xaml
	/// </summary>
	public partial class ucInfoRadicacion : UserControl
    {
        #region DependencyProperties

        public static readonly DependencyProperty EditableProperty = DependencyProperty.Register("Editable", typeof(bool), typeof(ucInfoRadicacion), new UIPropertyMetadata(true, EditableCallback));

        #endregion
        #region Properties

        public bool BEditable
        {
            get { return (bool)GetValue(EditableProperty); }
            set { SetValue(EditableProperty, value); }
        }

        #endregion
        #region Constructor

        public ucInfoRadicacion()
		{
			this.InitializeComponent();
        }

        #endregion
        #region Private methods

        private static void EditableCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ucInfoRadicacion infRadicacion = (ucInfoRadicacion)d;
            if ((bool)e.NewValue)
            {
                infRadicacion.txbNumeroFormulario.IsEnabled = true;
                infRadicacion.btnGenerarNumeroFormulario.Visibility = Visibility.Visible;
                infRadicacion.spGenerar.Visibility = Visibility.Visible;
                infRadicacion.cbxPais.IsEnabled = true;
                infRadicacion.cbxDepartamento.IsEnabled = true;
                infRadicacion.cbxMunicipio.IsEnabled = true;
                infRadicacion.cbxEntidad.IsEnabled = true;
                infRadicacion.gbxCambiarImagen.Visibility = Visibility.Visible;
            }
            else
            {
                infRadicacion.txbNumeroFormulario.IsEnabled = false;
                infRadicacion.btnGenerarNumeroFormulario.Visibility = Visibility.Collapsed;
                infRadicacion.spGenerar.Visibility = Visibility.Collapsed;
                infRadicacion.cbxPais.IsEnabled = false;
                infRadicacion.cbxDepartamento.IsEnabled = false;
                infRadicacion.cbxMunicipio.IsEnabled = false;
                infRadicacion.cbxEntidad.IsEnabled = false;
                infRadicacion.gbxCambiarImagen.Visibility = Visibility.Collapsed;
            }
        }

        #region Events

        private void btnImagen_Click(object sender, RoutedEventArgs e)
        {
            clsRadicacion rad = (clsRadicacion)DataContext;
            if (rad == null || !rad.ID.HasValue) return;

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "TIFF files (*.tif)|*.tif|PDF files (*.pdf)|*.pdf|JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            //rad.RUTAIMAGEN = rad.ID.ToString();
            saveFile.FileName = rad.RUTAIMAGEN;
            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllBytes(saveFile.FileName, (byte[])rad.DocumentoDigital);

                    Notificaciones notifica = new Notificaciones(saveFile.FileName, resx::Informacion.GeneradoCorrectamente);
                    RUV.I.UIPrincipal.Notificar(notifica, string.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(resx::Errores.General, ex.Message), resx::Controles.Error);
                }
            }
        }

        private void btnGenerarNumeroFormulario_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            clsRadicacion rad = (clsRadicacion)DataContext;
            if (rad == null || !rad.ID.HasValue) return;

            if (rad.ID_PAIS == null || rad.ID_DEPARTAMENTO == null || rad.ID_MUNICIPIO == null || rad.ID_ENTIDADMUNICIPIO == null)
            {
                MessageBox.Show(resx::Advertencia.CamposGeografiaVacios, resx::Controles.Advertencia);
                return;
            }

            string cError = string.Empty;
            ControlDocumentosService.ControlDocumentosServiceClient ctrDoc = RUV.I.Red.ServicioGestionDocumentos;
            clsFormulario[] arrFormulario = ctrDoc.GenerarFormularios(1, (string)cbxSerie.SelectedValue,
                RUV.I.Usuario.Id, (int)eEstadoFormulario.ASIGNADO, (int?)rad.ID_PAIS, (int?)rad.ID_DEPARTAMENTO, (int?)rad.ID_MUNICIPIO, (int?)rad.ID_ENTIDADMUNICIPIO, ref cError);
            if (cError != string.Empty) {
                MessageBox.Show(string.Format(resx::Errores.General, cError), resx::Controles.Error);
            }
            else {
                if (arrFormulario == null || arrFormulario.Length < 1)
                    throw new InvalidOperationException(string.Format("No se pudo generar un formulario de serie {0} con el usuario {1}, estado {2}, pais {3}, depto {4}, municipio {5}, entidad {6}",
                        (string)cbxSerie.SelectedValue,
                        RUV.I.Usuario.Id,
                        (int)eEstadoFormulario.ASIGNADO,
                        (int?)rad.ID_PAIS,
                        (int?)rad.ID_DEPARTAMENTO,
                        (int?)rad.ID_MUNICIPIO,
                        (int?)rad.ID_ENTIDADMUNICIPIO));
                rad.NRO_FORMULARIO = arrFormulario[0].CNumeroFormulario;
            }
        }

        private void cbxSerie_Initialized(object sender, System.EventArgs e)
        {
            ComboBox cbxSender = (ComboBox)sender;
            cbxSender.ItemsSource = Recursos.Controles.NumerosSerie.Split(new char[] { ',' });
            cbxSender.SelectedIndex = 0;
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

        private byte[] CargarImagen(decimal consec, string imagePath, ref string cError)
        {
            //Crea el archivo de imagen 
            BitmapImage bitmapImage;
            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            if (!File.Exists(imagePath))
            {
                RUV.I.UIPrincipal.BloquearInterfase = null;
                MessageBox.Show("El archivo seleccionado ya no existe,\ndebe seleccionar otro.");
                return null;
            }
            bitmapImage.StreamSource = System.IO.File.OpenRead(imagePath);
            bitmapImage.EndInit();
            byte[] imageData = new byte[bitmapImage.StreamSource.Length];
            bitmapImage.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);
            bitmapImage.StreamSource.Read(imageData, 0, imageData.Length);

            string fileName = consec.ToString() + System.IO.Path.GetExtension(imagePath);

            //Subir la imagen al servidor
            clsRed servicio = new clsRed();
            bool cargoimagen = servicio.ServicioGeneral.CargarImagen(imageData, fileName);

            if (cargoimagen)
            {
                RUV.I.UIPrincipal.BloquearInterfase = null;
                MessageBox.Show("Los cambios se realizaron satisfactoriamente");
                return imageData;
            }
            else
            {
                RUV.I.UIPrincipal.BloquearInterfase = null;
                MessageBox.Show("Los cambios no se realizaron");
                cError = "Hubo error al cargar la imagen";
                return null;
            }
        }

        private byte[] CargarPdf(decimal consec, string pdfPath, ref string cError)
        {
            if (!File.Exists(pdfPath))
            {
                RUV.I.UIPrincipal.BloquearInterfase = null;
                MessageBox.Show("El archivo seleccionado ya no existe,\ndebe seleccionar otro.");
                return null;
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
            bool cargopdf = servicio.ServicioGeneral.CargarPdf(_Buffer, fileName);
            if (cargopdf)
            {
                RUV.I.UIPrincipal.BloquearInterfase = null;
                MessageBox.Show("Los cambios se realizaron satisfactoriamente");
                return _Buffer;
            }
            else
            {
                RUV.I.UIPrincipal.BloquearInterfase = null;
                MessageBox.Show("Los cambios no se realizaron satisfactoriamente");
                cError = "Hubo error al cargar la imagen";
                return null;
            }
        }

        private void BtnCambiaImagen_Click(object sender, RoutedEventArgs e)
        {
            string cError = string.Empty;
            clsRadicacion rad = (clsRadicacion)DataContext;
            
            //datacontext llega vacio
            //if(rad != null)
            decimal idradicacion = (decimal)rad.ID;            
               
           string extension = System.IO.Path.GetExtension(rad.RUTAIMAGEN);
           string[] ruta = txtRutaImagen.Text.Split(('.'));
           if (!string.IsNullOrEmpty(ruta[0]))
           {
               if (extension.ToUpper() == ".PDF" || ruta[1].ToUpper() == "PDF")
               {
                   //Cargar pdf al servidor
                   byte[] DocumentoNuevo = CargarPdf(idradicacion, txtRutaImagen.Text, ref cError);
                   if (string.IsNullOrEmpty(cError))
                       rad.DocumentoDigital = DocumentoNuevo;
                   else
                   {
                       RUV.I.UIPrincipal.BloquearInterfase = null;
                       MessageBox.Show("Los cambios  no se realizaron");
                   }
               }
               else
               {
                   //Cargar imagen al servidor
                   byte[] DocumentoNuevo = CargarImagen(idradicacion, txtRutaImagen.Text, ref cError);
                   if (string.IsNullOrEmpty(cError))
                       rad.DocumentoDigital = DocumentoNuevo;
               }
           }
           else 
           {
               MessageBox.Show("Debe seleccionar una imagen antes de generar el cambio");
           }
        }

        #endregion

        #endregion
    }
}