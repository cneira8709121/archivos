using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Devolucion;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.LiderRadicacion;
using Ruv.Infrastructure.Crosscutting.Common.General;
using resx = Ruv.Infrastructure.Crosscutting.Resources.Globalization;

namespace Ruv.WPF.Captura.Radicacion
{
    /// <summary>
    /// Interaction logic for LiderRadicacion.xaml
    /// </summary>
    public partial class LiderRadicacion : Page
    {
        #region Properties

        public int? NIdDeclaracion { get; set; }

        #endregion
        public LiderRadicacion()
        {
            InitializeComponent();
        }

        #region Private methods

        private void SetGeografia(clsRadicacion rad)
        {
            if (rad == null || !rad.ID_ENTIDADMUNICIPIO.HasValue) return;

            try
            {
                clsEntidadMunicipio em = RUV.I.InfoGeneral.ListaEntidadesMunicipiosTodos.Where(x => x.NId.HasValue && x.NId.Value == rad.ID_ENTIDADMUNICIPIO.Value).FirstOrDefault();
                clsParametroMunicipio m = RUV.I.InfoGeneral.ListaMunicipiosTodos.Where(x => x.Id.HasValue && x.Id.Value == em.NIdMunicipio.Value).FirstOrDefault();
                clsParametroDepartamento d = RUV.I.InfoGeneral.ListaDepartamentosTodos.Where(x => x.Id.HasValue && x.Id.Value == m.DepartamentoId).FirstOrDefault();
                clsParametroPais p = RUV.I.InfoGeneral.ListaPaises.Where(x => x.Id.HasValue && x.Id.Value == d.PaisId).FirstOrDefault();

                rad.ID_PAIS = p.Id;
                rad.ID_DEPARTAMENTO = d.Id;
                rad.ID_MUNICIPIO = m.Id;
                rad.ID_ENTIDADMUNICIPIO = rad.ID_ENTIDADMUNICIPIO;
            }
            catch
            {
                rad.ID_PAIS = null;
                rad.ID_DEPARTAMENTO = null;
                rad.ID_MUNICIPIO = null;
                rad.ID_ENTIDADMUNICIPIO = null;
            }
        }

        #region Events

        private void BtnGuardar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BindingExpression be = cObservaciones.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            if (be.HasError)
            {
                MessageBox.Show(resx::Advertencia.DiligenciarObservacion, resx.Controles.Advertencia);
                return;
            }

            string cError = string.Empty;
            clsLiderRadicacion lr = (clsLiderRadicacion)DataContext;

            if (!(lr.RadActual.ID_PAIS.HasValue && lr.RadActual.ID_DEPARTAMENTO.HasValue && lr.RadActual.ID_MUNICIPIO.HasValue && lr.RadActual.ID_ENTIDADMUNICIPIO.HasValue))
            {
                MessageBox.Show(resx::Advertencia.CamposGeografiaVacios, resx.Controles.Advertencia);
                return;
            }

            //Valida el nuevo numero de formulario
            ControlDocumentosService.IControlDocumentosService cd = RUV.I.Red.ServicioGestionDocumentos;
            eResultadoValidacionRadicacion ResultadoEstado = cd.ValidarNumeroFormulario(lr.RadActual);

            //if (ResultadoEstado != eResultadoValidacionRadicacion.validacionCorrecta)
            //{
            //    if (!(ResultadoEstado == eResultadoValidacionRadicacion.NumeroFormularioRadicado && lr.RadExistente != null && lr.RadExistente.ID == null))
            //    {
            //        MessageBox.Show(Advertencia.FormularioInvalido, Advertencia.AdvertenciaTitulo);
            //        return;
            //    }
            //}

            switch (ResultadoEstado)
            {
                case eResultadoValidacionRadicacion.validacionCorrecta:
                    lr.RadActual.PARAM_RESULTADO_VALIDACION = eResultadoValidacionRadicacion.validacionCorrecta.GetHashCode();
                    RadicacionServiceReference.IRadicacionService rc = RUV.I.Red.ServicioRadicacion;
                    if (!(rc.ActualizarRadicacion(lr.RadActual, lr.CObservacion, ref cError) || string.IsNullOrEmpty(cError))) MessageBox.Show(string.Format(resx::Errores.General, cError), resx::Controles.Error);
                    else
                    {
                        if (!(cd.MarcarRadicado(lr.RadActual.NRO_FORMULARIO, ref cError) && string.IsNullOrEmpty(cError))) MessageBox.Show(string.Format(resx::Errores.General, cError), resx::Controles.Error);
                        else
                        {
                            MessageBox.Show(resx::Informacion.CambiosGuardados, resx::Controles.Informacion);
                            RUV.I.UIPrincipal.NavegarAListaDeTareas();
                        }
                    }
                    break;
                case eResultadoValidacionRadicacion.faltaNumeroFormulario:
                    MessageBox.Show(resx::Advertencia.FormularioVacio, resx::Advertencia.AdvertenciaTitulo);
                    break;
                case eResultadoValidacionRadicacion.NumeroFormularioInvalido:
                    MessageBox.Show(resx::Advertencia.FormularioInvalido, resx::Advertencia.AdvertenciaTitulo);
                    break;
                case eResultadoValidacionRadicacion.NumeroFormularioRadicado:
                    MessageBox.Show(resx::Advertencia.FormularioYaRadicado, resx::Advertencia.AdvertenciaTitulo);
                    break;
                case eResultadoValidacionRadicacion.NumeroFormularioInactivo:
                    MessageBox.Show(resx::Advertencia.FormularioInactivo, resx::Advertencia.AdvertenciaTitulo);
                    break;
                case eResultadoValidacionRadicacion.NumeroFormularioNoAsignado:
                case eResultadoValidacionRadicacion.ProcedenciaErronea:
                    // Se pregunta si se desea reasignar el formulario
                    MessageBoxResult result = MessageBox.Show(resx::Informacion.RedistribuirFormulario, resx::Informacion.Titulo, MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        clsFormulario frm = cd.ObtenerFormulario(lr.RadActual.NRO_FORMULARIO, ref cError);
                        if (!string.IsNullOrEmpty(cError)) MessageBox.Show(string.Format(resx::Errores.General, cError), resx::Controles.Error);
                        else
                        {
                            // Se modifican los identificadores de la distribución del formulario
                            frm.NIdPais = lr.RadActual.ID_PAIS;
                            frm.NIdDepartamento = lr.RadActual.ID_DEPARTAMENTO;
                            frm.NIdMunicipio = lr.RadActual.ID_MUNICIPIO;
                            frm.NIdEntidad = lr.RadActual.ID_ENTIDADMUNICIPIO;
                            cd.AsignarFormulario(new clsFormulario[] { frm }, ref cError);
                            if (!string.IsNullOrEmpty(cError)) MessageBox.Show(string.Format(resx::Errores.General, cError), resx::Controles.Error);
                            else
                            {
                                BtnGuardar_Click(null, null);
                            }
                        }
                    }
                    break;
            }
        }

        private void BtnDevolver_Click(object sender, RoutedEventArgs e)
        {
            clsLiderRadicacion lRad = (clsLiderRadicacion)DataContext;

            if (!(lRad.RadActual.ID_PAIS.HasValue && lRad.RadActual.ID_DEPARTAMENTO.HasValue && lRad.RadActual.ID_MUNICIPIO.HasValue && lRad.RadActual.ID_ENTIDADMUNICIPIO.HasValue))
            {
                MessageBox.Show(resx::Advertencia.CamposGeografiaVacios, resx.Controles.Advertencia);
                return;
            }

            SolicitudDevolucion sDev = new SolicitudDevolucion();
            clsDevolucion dev = new clsDevolucion()
            {
                NIdDeclaracion = NIdDeclaracion,
                NIdUsuario = RUV.I.Usuario.Id,
                NIdEntidadMunicipio = lRad.RadActual.ID_ENTIDADMUNICIPIO
            };
            sDev.ucCausales.DataContext = dev;
            sDev.ucCausales.EParametroTipoCausal = eTipoParametros.CausalesLiderRadicacion;
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
        }

        private void BtnCancelar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RUV.I.UIPrincipal.NavegarAListaDeTareas();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            string cError = string.Empty;
            RadicacionServiceReference.IRadicacionService rc = RUV.I.Red.ServicioRadicacion;
            Ruv.Infrastructure.Crosscutting.Common.Entidades.LiderRadicacion.clsLiderRadicacion lr;
            try
            {
                lr = rc.CargarDatos(NIdDeclaracion.Value, ref cError);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            lr.RadActual.DocumentoDigital = RUV.I.DeclaracionActual.DocumentoDigital;

            SetGeografia(lr.RadActual);
            SetGeografia(lr.RadExistente);

            DataContext = lr;

            if (lr.RadActual.PARAM_RESULTADO_VALIDACION.Value == eResultadoValidacionRadicacion.NumeroFormularioInactivo.GetHashCode() ||
                lr.RadActual.PARAM_RESULTADO_VALIDACION.Value == eResultadoValidacionRadicacion.NumeroFormularioInvalido.GetHashCode() ||
                lr.RadActual.PARAM_RESULTADO_VALIDACION.Value == eResultadoValidacionRadicacion.NumeroFormularioRadicado.GetHashCode() ||
                lr.RadActual.PARAM_RESULTADO_VALIDACION.Value == eResultadoValidacionRadicacion.faltaNumeroFormulario.GetHashCode())
                ucRadActual.spGenerar.Visibility = Visibility.Visible;
            else
                ucRadActual.spGenerar.Visibility = Visibility.Collapsed;

            if (lr.RadExistente == null) ucRadExistente.btnImagen.Visibility = Visibility.Collapsed;

            BindingExpression be = cObservaciones.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }




        #endregion

        #endregion
    }
}
