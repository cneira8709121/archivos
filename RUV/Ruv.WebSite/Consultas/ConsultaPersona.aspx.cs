using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.Infrastructure.Crosscutting.Resources.Presentation;
using Ruv.WebSite.Presentation.Adapters;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WebSite.Presentation.Consultas
{

    public partial class ConsultaPersona : PaginaBase
    {

        #region Private Fields / Properties


        #endregion

        #region Private Methods

        /// <summary>
        /// Validation for the perform search functionality
        /// </summary>
        /// <returns></returns>
        private bool ValidateControls()
        {
            if (wuConsulta.CPrimerNombre == string.Empty && wuConsulta.CPrimerApellido == string.Empty && wuConsulta.CNumeroCedula == string.Empty && wuConsulta.CNumeroFormulario == string.Empty)
            {
                ShowMessage(Controles.Advertencia, Advertencia.DiligenciarAlMenosUnCampo);
                return false;
            }
            if ((string.IsNullOrWhiteSpace(wuConsulta.CNumeroCedula) & (string.IsNullOrWhiteSpace(wuConsulta.CNumeroFormulario))) && (string.IsNullOrWhiteSpace(wuConsulta.CPrimerApellido) | string.IsNullOrWhiteSpace(wuConsulta.CPrimerNombre)))
            {
                ShowMessage(Controles.Advertencia, Advertencia.NombreOApellido);
                return false;

            }
            return true;
        }

        private void ShowMessage(string sTitle, string sMessage)
        {
            Master.PopUpGeneral.Titulo = sTitle;
            Master.PopUpGeneral.MostrarBotones = true;
            Master.PopUpGeneral.MostrarImagen = false;
            Master.PopUpGeneral.VisibleBotonCancelar = false;
            Master.PopUpGeneral.Mensaje = sMessage;
            Master.PopUpGeneral.Mostrar();
        }

        protected void OdsConsulta_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
        {
            var parametrosConsulta = new clsConsultarEstadoDeclaracionSolicitud
            {
                CNumeroDocumento = wuConsulta.CNumeroCedula,
                CPrimerNombre = wuConsulta.CPrimerNombre,
                CPrimerApellido = wuConsulta.CPrimerApellido,
                CNumeroFormulario = wuConsulta.CNumeroFormulario
            };
            var datasource = e.ObjectInstance as DataSourceConsulta;
            datasource.RequestInfo = parametrosConsulta;
            datasource.ErrorConsulta +=new Infrastructure.Crosscutting.Common.Error(datasource_ErrorConsulta);
        }

        void datasource_ErrorConsulta(object sender, ErrorEventArgs e)
        {
            ShowMessage("Error", e.ErrorMensaje);
        }

        #endregion

        #region Protected Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.UrlCurrenPage = Request.Url.AbsolutePath;
            Master.ValidarPermisoPagina();
        }

        protected void wuConsulta_OnButtonClick(object sender, EventArgs e)
        {
            if (!ValidateControls()) return;

            GridConsulta.DataBind();
            this.PanelConsulta.Visible = true;
            updPnlAceptar.Update();
        }

        protected void GridConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("DetalleFormulario.aspx?id={0}&urlEvio={1}", this.GridConsulta.SelectedValue, this.Request.Url.AbsolutePath));
        }


        #endregion

        #endregion

    }
}