using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.WPF.Captura.Controles;
using Ruv.WPF.Captura.Utils.DataSources;

namespace Ruv.WPF.Captura
{
    /// <summary>
    /// Interaction logic for GeneracionFormulario.xaml
    /// </summary>
    public partial class GeneracionFormulario : Page
    {
        public GeneracionFormulario()
        {
            this.InitializeComponent();
            // Insert code required on object creation below this point.
            AddCopyHandle();
        }

        #region Private methods

        void CopySelected()
        {
            if (dgFormulario.SelectedItems.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (clsFormulario s in dgFormulario.SelectedItems)
                {
                    sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                        new object[] { s.CNumeroFormulario, s.DGenerado, s.CEstado,
                            string.IsNullOrEmpty(s.CPais) ? s.NIdPais = 48 : s.NIdPais = s.NIdPais,
                            string.IsNullOrEmpty(s.CPais) ? "COLOMBIA" : s.CPais,
                            string.IsNullOrEmpty(s.CDepartamento) ? "NINGUNO" : s.CDepartamento,
                            string.IsNullOrEmpty(s.CMunicipio) ? "NINGUNO" : s.CMunicipio,
                            string.IsNullOrEmpty(s.CEntidad) ? "NINGUNO" : s.CEntidad}));
                }

                try
                {
                    System.Windows.Clipboard.SetData(DataFormats.Text, sb.ToString());
                }
                catch (COMException)
                {
                    MessageBox.Show("Sorry, unable to copy surveys to the clipboard. Try again.");
                }
            }
        }

        private void AddCopyHandle()
        {
            ExecutedRoutedEventHandler handler = (sender_, arg_) => { CopySelected(); };
            var command = new RoutedCommand("Copy", typeof(GridView));
            command.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control, "Copy"));
            dgFormulario.CommandBindings.Add(new CommandBinding(command, handler));
            try
            {
                System.Windows.Clipboard.SetData(DataFormats.Text, "");
            }
            catch (COMException)
            { }
        }

        private void SepararImprenta(IEnumerable<clsSeparacionFormularioSolicitud> eFrmSolicitud, ref string cError)
        {
            GeneraExcel(eFrmSolicitud,ref cError);
            ControlDocumentosService.ControlDocumentosServiceClient ctrDoc = RUV.I.Red.ServicioGestionDocumentos;
            ctrDoc.SepararFormularioImprenta(eFrmSolicitud.ToArray(), ref cError);            
            clsControlFormulario objFormulario = (clsControlFormulario)DataContext;
            objFormulario.EFiltro = eEstadoFormulario.IMPRENTA;
            if (pageControl.PageContract == null) return;
            var formulario = pageControl.PageContract as FormulariosUsuarioDataSource;
            formulario.IdEstado = objFormulario.EFiltro;

            pageControl.Navigate(PageChanges.First);
        }

        private void GeneraExcel(IEnumerable<clsSeparacionFormularioSolicitud> eFrmSolicitud, ref string cError)
        {
            string nombre = Guid.NewGuid().ToString() + ".txt";
            string archivo = System.IO.Path.Combine(RUV.I.Configuraciones.Ubicaciones.DestinoDescargas, nombre);
            
            using (StreamWriter sw = new StreamWriter(File.Create(archivo)))
            {
                foreach(clsSeparacionFormularioSolicitud frm in eFrmSolicitud)
                {
                    sw.WriteLine(string.Format("{0},",frm.CNumeroFormulario));
                }
            }
            Notificaciones notifica = new Notificaciones(archivo, Informacion.GeneradoCorrectamente);
            RUV.I.UIPrincipal.Notificar(notifica, string.Empty);
        }

        private void AsignarFormulario(List<clsFormulario> frmSolicitud, ref string cError)
        {
            ControlDocumentosService.ControlDocumentosServiceClient ctrDoc = RUV.I.Red.ServicioGestionDocumentos;
            ctrDoc.AsignarFormulario(frmSolicitud.ToArray(), ref cError);

            clsControlFormulario objFormulario = (clsControlFormulario)DataContext;
            objFormulario.EFiltro = eEstadoFormulario.ASIGNADO;
            if (pageControl.PageContract == null) return;
            var formulario = pageControl.PageContract as FormulariosUsuarioDataSource;
            formulario.IdEstado = objFormulario.EFiltro;

            pageControl.Navigate(PageChanges.First);
        }
        private void pageControl_PreviewPageChange(object sender, PageChangedEventArgs args)
        {
            List<Object> items = pageControl.ItemsSource.ToList();
            int count = items.Count;
        }

        private void pageControl_PageChanged(object sender, PageChangedEventArgs args)
        {
            List<Object> items = pageControl.ItemsSource.ToList();
            int count = items.Count;
            clsControlFormulario objFormulario = (clsControlFormulario)DataContext;
            objFormulario.LstFormularios = pageControl.ItemsSource.Cast<clsFormulario>().ToList();
        }

        [Obsolete("Remplazado por Control de Geografias")]
        private void AsignarPathCbxGeografia(ComboBox cbxSender)
        {
            cbxSender.SelectedValuePath = "Id";
            cbxSender.DisplayMemberPath = "Nombre";
        }

        #region Events


        private void cbxEstados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clsControlFormulario objFormulario = (clsControlFormulario)DataContext;
            if (pageControl.PageContract == null) return;
            var formulario = pageControl.PageContract as FormulariosUsuarioDataSource;
            formulario.IdEstado = objFormulario.EFiltro;

            // Diego Alvarez - 15/11/2013 - Se debe deshabilitar la columna geografía cuando el estado es ASIGNADO            
            dgFormulario.Columns[4].IsReadOnly = objFormulario.BGeografiaSoloLectura;

            pageControl.Navigate(PageChanges.Current);
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

            gbxGestionMasiva.Visibility = System.Windows.Visibility.Collapsed;
            clsControlFormulario objFormulario = new clsControlFormulario();
            DataContext = objFormulario;

            if (pageControl.PageContract == null) return;
            var formulario = pageControl.PageContract as FormulariosUsuarioDataSource;
            formulario.IdEstado = objFormulario.EFiltro;

            if (RUV.I.Red.EstadoRed == eEstadoRed.Disponible)
            {
                string cError = string.Empty;
                objFormulario.LstFormularios = new List<clsFormulario>();

                if (cError == string.Empty)
                {
                    if (pageControl.ItemsSource != null)
                    {
                        objFormulario.LstFormularios = pageControl.ItemsSource.Cast<clsFormulario>().ToList();
                        objFormulario.EFiltro = eEstadoFormulario.GENERADO;
                    }
                }

            }
            else
            {
                btnGuardar.IsEnabled = false;
                btnGenerar.IsEnabled = false;
                MessageBox.Show(Advertencia.RedNoDisponible, Advertencia.AdvertenciaTitulo);
            }
        }

        [Obsolete("Se cambio por el control de geografias")]
        private void cbxGeneral_Initialized(object sender, System.EventArgs e)
        {
            ComboBox cbxSender = (ComboBox)sender;
            clsFormulario frm = null;
            if (cbxSender.Tag == null) frm = (clsFormulario)dgFormulario.SelectedItem;

            switch (cbxSender.Name)
            {
                case "cbxPais":
                    AsignarPathCbxGeografia(cbxSender);
                    cbxSender.ItemsSource =null;
                    cbxSender.ItemsSource = RUV.I.InfoGeneral.ListaPaises.Select(x=> x.Id = 48);//RUV.I.InfoGeneral.ListaPaises;
                    break;
                case "cbxDepartamento":
                    AsignarPathCbxGeografia(cbxSender);
                    cbxSender.ItemsSource = null;
                    if (frm != null && frm.NIdPais != null) cbxSender.ItemsSource = RUV.I.InfoGeneral.ListaDepartamentos((long)frm.NIdPais).Where(x => x.TieneRepresentacion == true);
                    break;
                case "cbxMunicipio":
                    AsignarPathCbxGeografia(cbxSender);
                    cbxSender.ItemsSource = null;
                    if (frm != null && frm.NIdDepartamento != null) cbxSender.ItemsSource = RUV.I.InfoGeneral.ListaMunicipios((long)frm.NIdDepartamento).Where(x => x.TieneRepresentacion == true);
                    break;
                case "cbxEntidad":
                    cbxSender.SelectedValuePath = "NIdEntidad";
                    cbxSender.DisplayMemberPath = "CNombreEntidad";
                    cbxSender.ItemsSource = null;
                    if (frm != null && frm.NIdMunicipio != null) cbxSender.ItemsSource = RUV.I.InfoGeneral.ListaEntidadesMunicipios(frm.NIdMunicipio);
                    break;
            }
        }

        private void cbxSerie_Initialized(object sender, EventArgs e)
        {
            ComboBox cbxSender = (ComboBox)sender;
            cbxSender.ItemsSource = Recursos.Controles.NumerosSerie.Split(new char[] { ',' });
            cbxSender.SelectedIndex = 0;
        }

        protected void cbx_CheckedUncheckedAll(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            bool Checked = true;
            if (!chk.IsChecked.Value)
            {
                Checked = false;
            }

            clsControlFormulario frm = (clsControlFormulario)DataContext;
            foreach (var item in frm.LstFormularios)
            {
                item.BSelected = Checked;
            }

            if (frm.EFiltro == eEstadoFormulario.ASIGNADO) return;
            frm.VisibilidadMasivos = Checked;
        }

        private void cbx_CheckedUnchecked(object sender, RoutedEventArgs e)
        {
            if (dgFormulario.SelectedIndex >= 0)
            {
                clsControlFormulario objFormulario = DataContext as clsControlFormulario;

                if (objFormulario.EFiltro == eEstadoFormulario.ASIGNADO) return;

                int nTotalChecked = objFormulario.LstFormularios.Count(x => x.BSelected);
                objFormulario.VisibilidadMasivos = nTotalChecked > 0 ? true : false;

            }
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            
            clsControlFormulario objFormulario = (clsControlFormulario)DataContext;
            string cError = string.Empty;
            if (string.IsNullOrEmpty(objFormulario.CSerie)) { MessageBox.Show("Seleccione la serie"); return; }
            ControlDocumentosService.ControlDocumentosServiceClient ctrDoc = RUV.I.Red.ServicioGestionDocumentos;

            RUV.I.UIPrincipal.BloquearInterfase = "Generando...";

            RUV.I.MultiTarea.EjecutarEnBackground((() =>
            {
                ctrDoc.GenerarFormularios(uint.Parse(objFormulario.NCantidad.ToString()), objFormulario.CSerie,
                RUV.I.Usuario.Id, (int)eEstadoFormulario.GENERADO, null, null, null, null, ref cError);
            }),
                (() =>
                {
                    if (cError != string.Empty)
                    {
                        MessageBox.Show(string.Format(Errores.General, cError), Errores.ErrorTitulo);
                    }
                    else
                    {
                        objFormulario.EFiltro = eEstadoFormulario.GENERADO;

                        if (pageControl.PageContract == null) return;
                        var formulario = pageControl.PageContract as FormulariosUsuarioDataSource;
                        formulario.IdEstado = objFormulario.EFiltro;

                        pageControl.Navigate(PageChanges.First);
                    }

                    RUV.I.UIPrincipal.BloquearInterfase = null;
                })
            );
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            clsControlFormulario objControlFormulario = (clsControlFormulario)DataContext;
            MessageBoxResult result = MessageBox.Show(Informacion.ConfirmarDistribución, Informacion.Titulo, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;
            if (objControlFormulario.LstFormularios != null)
            {
                string cError = string.Empty;

                List<clsFormulario> FormulariosEnviar =
                    objControlFormulario.LstFormularios.
                    Where(x => x.EfId != eEstadoFormulario.INACTIVO && x.NIdPais.HasValue && x.NIdDepartamento.HasValue && x.NIdMunicipio.HasValue && x.NIdEntidad.HasValue).ToList();

                FormulariosEnviar.ForEach(x => { x.NIdUsuario = (uint)RUV.I.Usuario.Id; });

                AsignarFormulario(FormulariosEnviar, ref cError);
                objControlFormulario.VisibilidadMasivos = false;
                if (cError != string.Empty) MessageBox.Show(string.Format(Errores.General, cError), Errores.ErrorTitulo);
                else MessageBox.Show(Informacion.CambiosGuardados, Informacion.Titulo);
            }
        }

        private void btnSeparar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            clsControlFormulario objFormulario = (clsControlFormulario)DataContext;
            MessageBoxResult result = MessageBox.Show(Informacion.ConfirmarSepararImprenta, Informacion.Titulo, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;
            if (objFormulario.LstFormularios != null)
            {
                string cError = string.Empty;
                IEnumerable<clsSeparacionFormularioSolicitud> FormulariosEnviar = objFormulario.LstFormularios
                                    .Where(x => x.BSelected && x.EfId != eEstadoFormulario.INACTIVO)
                                    .Select(x => new clsSeparacionFormularioSolicitud
                                    {
                                        CNumeroFormulario = x.CNumeroFormulario,
                                        NIdUsuario = (uint)RUV.I.Usuario.Id
                                    });
                SepararImprenta(FormulariosEnviar, ref cError);

                if (cError != string.Empty) MessageBox.Show(string.Format(Errores.General, cError), Errores.ErrorTitulo);
                else MessageBox.Show(Informacion.CambiosGuardados, Informacion.Titulo);
            }
            objFormulario.EFiltro = eEstadoFormulario.GENERADO;
        }

        private void btnSepararFiltro_Click(object sender, RoutedEventArgs e)
        {
            clsControlFormulario objFormulario = (clsControlFormulario)DataContext;
            MessageBoxResult result = MessageBox.Show(Informacion.ConfirmarSepararImprenta, Informacion.Titulo, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;
            
            string cError = string.Empty;
            ControlDocumentosService.ControlDocumentosServiceClient ctrDoc = RUV.I.Red.ServicioGestionDocumentos;

            clsSeparacionFormularioSolicitud[] arrSeparados = null;
            RUV.I.UIPrincipal.BloquearInterfase = "Separando...";
            RUV.I.MultiTarea.EjecutarEnBackground((() =>
            {
                arrSeparados = ctrDoc.SepararFormularioImprentaFiltro(new clsSolicitudFormularioEstado { CNumeroFormulario = objFormulario.CSerieBuscar, NDesde = objFormulario.NDesde, NHasta = objFormulario.NHasta, DGenerado = objFormulario.DGenerado, NIdUsuario = RUV.I.Usuario.Id }, ref cError);
            }),
                (() =>
                {
                    if (!string.IsNullOrEmpty(cError)) MessageBox.Show(string.Format(Errores.General, cError), Errores.ErrorTitulo);
                    else if (arrSeparados != null)
                    {
                        if (arrSeparados.Length > 0)
                        {
                            GeneraExcel(arrSeparados.ToList(), ref cError);

                            if (!string.IsNullOrEmpty(cError)) MessageBox.Show(string.Format(Errores.General, cError), Errores.ErrorTitulo);
                            else MessageBox.Show(Informacion.CambiosGuardados, Informacion.Titulo);
                        }
                    }

                    objFormulario.EFiltro = eEstadoFormulario.IMPRENTA;
                    if (pageControl.PageContract == null) return;
                    var formulario = pageControl.PageContract as FormulariosUsuarioDataSource;
                    formulario.CNumeroFormulario = objFormulario.CSerieBuscar;
                    formulario.NDesde = objFormulario.NDesde;
                    formulario.NHasta = objFormulario.NHasta;
                    formulario.DGenerado = objFormulario.DGenerado;
                    formulario.IdEstado = objFormulario.EFiltro;

                    pageControl.Navigate(PageChanges.First);

                    RUV.I.UIPrincipal.BloquearInterfase = null;
                })
            );
        }

        private void ImageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string cError = string.Empty;
            PdfHelperServiceReference.PdfHelperServiceClient pdfHelp = RUV.I.Red.ServicioPdfHelper;
            clsFormulario frm = (clsFormulario)dgFormulario.SelectedItem;
            byte[] pdf = null;

            RUV.I.MultiTarea.EjecutarEnBackground(() =>
                {
                    RUV.I.UIPrincipal.MensajeNotificacion(string.Format("{0} {1}", "Descargando archivo en", RUV.I.Configuraciones.Ubicaciones.DestinoDescargas));

                    if (frm.CPais != "Colombia") pdf = pdfHelp.GenerateOnePdfFileConNacional(frm.CNumeroFormulario, ref cError);
                    else pdf = pdfHelp.GenerateOnePdfFile(frm.CNumeroFormulario, ref cError);
                }, () =>
                {
                    RUV.I.UIPrincipal.MensajeNotificacion();

                    if (!string.IsNullOrEmpty(cError) || pdf == null) MessageBox.Show(string.Format(Errores.General, cError), Errores.ErrorTitulo);
                    else
                    {
                        string fileName = string.Format(@"{0}{1}.pdf", RUV.I.Configuraciones.Ubicaciones.DestinoDescargas, frm.CNumeroFormulario);
                        try
                        {
                            using (FileStream fs = File.Create(fileName))
                            {
                                foreach (byte bPart in pdf)
                                {
                                    fs.WriteByte(bPart);
                                }

                                Notificaciones notifica = new Notificaciones(fileName, Informacion.GeneradoCorrectamente);
                                RUV.I.UIPrincipal.Notificar(notifica, string.Empty);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(string.Format(Errores.General, ex.Message), Advertencia.AdvertenciaTitulo);
                        }
                    }
                });
        }

        private void btnDistribuirFiltro_Click(object sender, RoutedEventArgs e)
        {
            clsControlFormulario objFormulario = (clsControlFormulario)DataContext;
            objFormulario.NPaisIdFiltro = 48;
            objFormulario.NDepartamentoIdFiltro = null;
            pGeografia.IsOpen = true;
        }

        private void btnDistribuirFiltroGeo_Click(object sender, RoutedEventArgs e)
        {
            pGeografia.IsOpen = false;
            clsControlFormulario objFormulario = (clsControlFormulario)DataContext;
            MessageBoxResult result = MessageBox.Show(Informacion.ConfirmarDistribución, Informacion.Titulo, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            string cError = string.Empty;
            ControlDocumentosService.ControlDocumentosServiceClient ctrDoc = RUV.I.Red.ServicioGestionDocumentos;

            bool bExito = false;
            RUV.I.UIPrincipal.BloquearInterfase = "Asignando...";
            RUV.I.MultiTarea.EjecutarEnBackground((() =>
            {
                bExito = ctrDoc.AsignarFormularioFiltro(new clsSolicitudFormularioEstado { CNumeroFormulario = objFormulario.CSerieBuscar, NDesde = objFormulario.NDesde, NHasta = objFormulario.NHasta, DGenerado = objFormulario.DGenerado, NIdUsuario = RUV.I.Usuario.Id, NIdPais = objFormulario.NPaisIdFiltro, NIdDepartamento = objFormulario.NDepartamentoIdFiltro, NIdMunicipio = objFormulario.NMunicipioIdFiltro, NIdEntidad = objFormulario.NEntidadMunicipioIdFiltro }, ref cError);
            }),
                (() =>
                {
                    if (!bExito || !string.IsNullOrEmpty(cError)) MessageBox.Show(string.Format(Errores.General, cError), Errores.ErrorTitulo);
                    else
                    {
                        MessageBox.Show(Informacion.CambiosGuardados, Informacion.Titulo);

                        objFormulario.EFiltro = eEstadoFormulario.ASIGNADO;
                        if (pageControl.PageContract == null) return;
                        var formulario = pageControl.PageContract as FormulariosUsuarioDataSource;
                        formulario.CNumeroFormulario = objFormulario.CSerieBuscar;
                        formulario.NDesde = objFormulario.NDesde;
                        formulario.NHasta = objFormulario.NHasta;
                        formulario.DGenerado = objFormulario.DGenerado;
                        formulario.IdEstado = objFormulario.EFiltro;

                        pageControl.Navigate(PageChanges.First);
                    }

                    RUV.I.UIPrincipal.BloquearInterfase = null;
                })
            );
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            clsControlFormulario objFormulario = (clsControlFormulario)DataContext;
            if (pageControl.PageContract == null) return;
            FormulariosUsuarioDataSource formulario = (FormulariosUsuarioDataSource)pageControl.PageContract;
            formulario.CNumeroFormulario = objFormulario.CSerieBuscar;
            formulario.NDesde = objFormulario.NDesde;
            formulario.NHasta = objFormulario.NHasta;
            formulario.DGenerado = objFormulario.DGenerado;
            formulario.IdEstado = objFormulario.EFiltro;

            pageControl.Navigate(PageChanges.First);
        }

        #region DescargarZip

        private void btnGenerarPdf_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            clsControlFormulario objFormulario = (clsControlFormulario)DataContext;
            if (objFormulario.LstFormularios != null && objFormulario.LstFormularios.Count(x => x.BSelected) > 0)
            {
                string fileName = string.Format(@"{0}formularios.zip", RUV.I.Configuraciones.Ubicaciones.DestinoDescargas);
                RUV.I.MultiTarea.EjecutarEnBackground((() => GenerarPdf(objFormulario, fileName)), (() => FinalizarDescarga(fileName)));
            }
        }

        private void FinalizarDescarga(string FileName)
        {
            RUV.I.UIPrincipal.MensajeNotificacion();

            Notificaciones notifica = new Notificaciones(FileName, Informacion.GeneradoCorrectamente);
            RUV.I.UIPrincipal.Notificar(notifica, string.Empty);
        }

        public void GenerarPdf(clsControlFormulario objFormulario, string FileName)
        {
            RUV.I.UIPrincipal.MensajeNotificacion(string.Format("{0} {1}", "Descargando archivo en", RUV.I.Configuraciones.Ubicaciones.DestinoDescargas));

            string cError = string.Empty;
            Dictionary<string,bool> dic = new Dictionary<string,bool>();
            PdfHelperServiceReference.PdfHelperServiceClient pdfHelp = RUV.I.Red.ServicioPdfHelper;
            objFormulario.LstFormularios
                        .Where(x => x.BSelected)
                        .ToList()
                        .ForEach(x => dic.Add(x.CNumeroFormulario, x.NIdPais == (long?)ePaises.Colombia));
            byte[] pdf = pdfHelp.GenerateManyPdfFilesAsZip(dic, ref cError);
            if (cError != string.Empty) MessageBox.Show(string.Format(Errores.General, cError), Errores.ErrorTitulo);
            else
            {
                try
                {
                    using (FileStream fs = File.Create(FileName))
                    {
                        foreach (byte bPart in pdf)
                        {
                            fs.WriteByte(bPart);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Errores.General, ex.Message), Advertencia.AdvertenciaTitulo);
                }
            }
        }

        #endregion

        #endregion

        #endregion
    }
}