using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.Infrastructure.Crosscutting.Resources.Presentation;
using Ruv.WebSite.Presentation.Adapters;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WebSite.Presentation.Correcciones
{

    public partial class ConsultaPersona : PaginaBase
    {

        #region Private Fields / Properties

        private bool aplicarFiltros;

        /// <summary>
        /// Filtro por número de cédula
        /// </summary>
        private string FiltroNumeroCedula 
        {
            get 
            {
                if(Session["filtroNumeroCedula"] != null)
                {
                    return Session["filtroNumeroCedula"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set 
            {
                Session["filtroNumeroCedula"] = value;
            }
        }

        /// <summary>
        /// Filtro por número de formulario
        /// </summary>
        private string FiltroNumeroFormulario
        {
            get
            {
                if (Session["filtroNumeroFormulario"] != null)
                {
                    return Session["filtroNumeroFormulario"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                Session["filtroNumeroFormulario"] = value;
            }
        }

        /// <summary>
        /// Filtro por primer apellido
        /// </summary>
        private string FiltroPrimerApellido
        {
            get
            {
                if (Session["filtroPrimerApellido"] != null)
                {
                    return Session["filtroPrimerApellido"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                Session["filtroPrimerApellido"] = value;
            }
        }

        /// <summary>
        /// Filtro por primer nombre
        /// </summary>
        private string FiltroPrimerNombre
        {
            get
            {
                if (Session["filtroPrimerNombre"] != null)
                {
                    return Session["filtroPrimerNombre"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                Session["filtroPrimerNombre"] = value;
            }
        }

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
                ShowMessage(Advertencia.DiligenciarAlMenosUnCampo);
                return false;
            }
            return true;
        }

        private void ShowMessage(string sMessage)
        {
            ModalPopUp.MostrarMensaje("Mensaje", sMessage);
        }

        protected void OdsConsulta_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
        {
            // Diego Alvarez - 10/10/2013 - Mantener los filtros seleccionados
            if (!aplicarFiltros)
            {
                this.FiltroNumeroCedula = wuConsulta.CNumeroCedula;
                this.FiltroPrimerNombre = wuConsulta.CPrimerNombre;
                this.FiltroPrimerApellido = wuConsulta.CPrimerApellido;
                this.FiltroNumeroFormulario = wuConsulta.CNumeroFormulario;
            }
            var parametrosConsulta = new clsConsultarEstadoDeclaracionSolicitud
                {
                    CNumeroDocumento = this.FiltroNumeroCedula,
                    CPrimerNombre = this.FiltroPrimerNombre,
                    CPrimerApellido = this.FiltroPrimerApellido,
                    CNumeroFormulario = this.FiltroNumeroFormulario
                };
            var datasource = e.ObjectInstance as DataSourceCorrecciones;
            datasource.RequestInfo = parametrosConsulta;
            datasource.ErrorConsulta +=new Infrastructure.Crosscutting.Common.Error(datasource_ErrorConsulta);
        }

        void datasource_ErrorConsulta(object sender, ErrorEventArgs e)
        {
            ShowMessage(e.ErrorMensaje);
        }

        #endregion

        #region Protected Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["AplicarFiltros"] != null && Convert.ToBoolean(Request.QueryString["AplicarFiltros"].ToString()))
                {
                    this.aplicarFiltros = true;
                    this.wuConsulta_OnButtonClick(null, null);
                }
                else
                {
                    this.aplicarFiltros = false;
                }
            }
            else
            {
                this.aplicarFiltros = false;
            }
            //Master.UrlCurrenPage = Request.Url.AbsolutePath;
            //Master.ValidarPermisoPagina();
        }

        protected void wuConsulta_OnButtonClick(object sender, EventArgs e)
        {
            if (!aplicarFiltros)
            {
                if (!ValidateControls()) return;
            }

            GridConsulta.DataBind();
            this.PanelConsulta.Visible = true;
        }

        protected void GridConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("SolicitudCorreccion.aspx?id={0}&urlEvio={1}", this.GridConsulta.SelectedValue, this.Request.Url.AbsolutePath));
        }


        #endregion

        #endregion

    }
}