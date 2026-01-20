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
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.WPF.Captura.Utils.DataSources;

namespace Ruv.WPF.Captura
{
    public partial class InactivacionFormulario
    {
        #region Public methods

        public InactivacionFormulario()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.
        }

        #endregion
        #region Private methods

        private void InactivarFormulario(IEnumerable<clsFormulario> enuFrm, string observacion, ref string cError)
        {
            foreach (clsFormulario frm in enuFrm)
            {
                InactivarFormulario(frm.NId, observacion, ref cError);
                if (cError != string.Empty) break;
            }
        }

        private void InactivarFormulario(uint nIdFormulario, string observacion, ref string cError)
        {
            ControlDocumentosService.ControlDocumentosServiceClient ctrDoc = RUV.I.Red.ServicioGestionDocumentos;
            uint? nIdFormularioOut = ctrDoc.InactivarFormulario(nIdFormulario, observacion, ref cError);
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

        #region Events

        protected void cbx_CheckedUncheckedAll(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            bool Checked = true;
            if (!chk.IsChecked.Value)
            {
                Checked = false;
            }
            clsControlFormulario frm = (clsControlFormulario)DataContext;
            frm.VisibilidadMasivos = Checked;
            foreach (var item in frm.LstFormularios)
            {
                item.BSelected = Checked;
            }
        }


        private void btnBuscar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BuscarFormularios();
        }

        private void BuscarFormularios()
        {
            string cError = string.Empty;
            clsControlFormulario objControlFormu = DataContext as clsControlFormulario;
            ControlDocumentosService.ControlDocumentosServiceClient ctrDoc = RUV.I.Red.ServicioGestionDocumentos;
            clsFormularioSolicitudNoRadicados frmsBusqueda = new clsFormularioSolicitudNoRadicados
            {
                CNumeroFormulario = txbNumeroFormulario.Text.Trim().ToUpper(),
                NIdDepartamento = cbxDepartamento.SelectedValue == null ? null : (long?)(long)cbxDepartamento.SelectedValue,
                NIdEntidad = cbxEntidad.SelectedValue == null ? null : (short?)(short)cbxEntidad.SelectedValue,
                NIdMunicipio = cbxMunicipio.SelectedValue == null ? null : (long?)(int)cbxMunicipio.SelectedValue,
                NIdPais = cbxPais.SelectedValue == null ? null : (long?)(long)cbxPais.SelectedValue,
                EAccion = objControlFormu.Accion
            };

            FormulariosActivarDataSource formularios = pageControl.PageContract as FormulariosActivarDataSource;
            formularios.Filtro = frmsBusqueda;

            if (pageControl.ItemsSource != null)
            {
                objControlFormu.LstFormularios = pageControl.ItemsSource.Cast<clsFormulario>().ToList();
            }

            pageControl.Navigate(PageChanges.First);
        }

        private void btnGuardar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            clsControlFormulario objControlFormu = DataContext as clsControlFormulario;
            if (string.IsNullOrWhiteSpace(objControlFormu.CObservacion))
            {
                MessageBox.Show(Informacion.ObservacionRequerida, Informacion.Titulo);
                return;
            }
            if (objControlFormu.LstFormularios != null)
            {
                List<clsFormulario> lstToSend = objControlFormu.LstFormularios.Where(x => x.BSelected).ToList();
                if (lstToSend.Count == 0)
                {
                    MessageBox.Show(Informacion.NoDatosParaAccion, Informacion.Titulo);
                }
                string mensaje = string.Format(Informacion.ConfirmarActivaciónInactivación, objControlFormu.Accion.ToString());
                MessageBoxResult result = MessageBox.Show(mensaje, Informacion.Titulo, MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK) { 
                string cError = string.Empty;
                RUV.I.MultiTarea.EjecutarEnBackground(() => InactivarFormulario(lstToSend, objControlFormu.CObservacion, ref cError),
                    () =>
                    {
                        if (cError != string.Empty) MessageBox.Show(string.Format(Errores.General, cError), Errores.ErrorTitulo);
                        else {
                            BuscarFormularios();
                            objControlFormu.CObservacion = string.Empty;
                            objControlFormu.Accion = eAccionEnFormulario.Inactivar;
                            MessageBox.Show(Informacion.CambiosGuardados, Informacion.Titulo); 
                        }
                    }
                    );
                }
            }
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            if (RUV.I.Red.EstadoRed != eEstadoRed.Disponible) {
                btnBuscar.IsEnabled = false;
                btnGuardar.IsEnabled = false;
                MessageBox.Show(Advertencia.RedNoDisponible, Advertencia.AdvertenciaTitulo);
            }
        }

        #endregion

        #endregion
    }
}