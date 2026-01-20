using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.ActosAdmin;
using Ruv.Infrastructure.Crosscutting.Common;
using System.Configuration;

namespace Ruv.WebApp.ActosAdmin
{
    public partial class Default : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.UrlCurrenPage = Request.Url.AbsolutePath;
            Master.ValidarPermisoPagina();
            Master.CargarOpcionesporUrl();
            Master.OnOptionClick += new OptionHandler(Master_OnOptionClick);

            if (!Page.IsPostBack)
            {

            }
        }

        void Master_OnOptionClick(object sender, OptionEventArgs e)
        {
            switch (e.ControlName)
            {
                case "Nuevo":
                    Response.Redirect("Nuevo.aspx");
                    break;
                case "Editar":
                    Editar();
                    break;
                case "Atras":
                    Response.Redirect("~/Default.aspx");
                    break;
                case "Anular":
                    ViewState["Accion"] = "Anular";
                    Anular();
                    break;
                case "Firmar":
                    ViewState["Accion"] = "Firmar";
                    Firmar();
                    break;
                default:
                    break;
            }
        }

        private void Editar()
        {
            if (grdActosAdministrativos.SelectedValue != null)
            {
                int id = Convert.ToInt32(grdActosAdministrativos.SelectedValue);
                Session[ConstantesItems.ACTOS_ADMIN] = id;
                ActosAdminService objActoAdmin = new ActosAdminService();
                clsActosAdminstrativos actoAdmin = new clsActosAdminstrativos();
                actoAdmin = objActoAdmin.GetActoAdminPorId(id);
                Response.Redirect("Editar.aspx");

            }
            else
            {
                lblError.Text = "Seleccione un Acto administrativo";
            }
        }

        private void Firmar()
        {
            if (grdActosAdministrativos.SelectedValue != null)
            {
                int id = Convert.ToInt32(grdActosAdministrativos.SelectedValue);
                ActosAdminService objActoAdmin = new ActosAdminService();
                clsActosAdminstrativos actoAdmin = new clsActosAdminstrativos();
                actoAdmin = objActoAdmin.GetActoAdminPorId(id);

                if (actoAdmin.EstadoId == (int)eEstadoActoAdmin.Generado)
                {

                    mpopGuardar.Mensaje = "¿Esta seguro de firmar este documento?";
                    mpopGuardar.Mostrar();
                }
                else
                {
                    if (actoAdmin.EstadoId == (int)eEstadoActoAdmin.Firmado)
                    {
                        lblError.Text = "No puede firmar de nuevo este documento";
                    }
                    if (actoAdmin.EstadoId == (int)eEstadoActoAdmin.Anulado)
                    {
                        lblError.Text = "No puede firmar un documento anulado";
                    }
                }
            }
            else
            {
                lblError.Text = "Seleccione un Acto administrativo";
            }
        }

        private void Anular()
        {
            if (grdActosAdministrativos.SelectedValue != null)
            {
                int id = Convert.ToInt32(grdActosAdministrativos.SelectedValue);
                ActosAdminService objActoAdmin = new ActosAdminService();
                clsActosAdminstrativos actoAdmin = new clsActosAdminstrativos();
                actoAdmin = objActoAdmin.GetActoAdminPorId(id);

                if (actoAdmin.EstadoId == (int)eEstadoActoAdmin.Generado)
                {
                    mpopGuardar.Mensaje = "¿Esta seguro de anular este documento?";
                    mpopGuardar.Mostrar();
                }
                else
                {
                    if (actoAdmin.EstadoId == (int)eEstadoActoAdmin.Firmado)
                    {
                        lblError.Text = "No puede anular un documento ya firmado";
                    }
                    if (actoAdmin.EstadoId == (int)eEstadoActoAdmin.Anulado)
                    {
                        lblError.Text = "No puede anular de nuevo este documento";
                    }
                }
            }
            else
            {
                lblError.Text = "Seleccione un Acto administrativo";
            }
        }


        protected void dataEmpInfo_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
        {
            DataSourceActosAdmin info = e.ObjectInstance as DataSourceActosAdmin;
            Session["TotalRegistros"] = info.Cantidad();
            if (info != null)
                info.SortColumns = "ID";
        }

        protected void mpopGuardar_Ok(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(grdActosAdministrativos.SelectedValue);
            ActosAdminService objActoAdmin = new ActosAdminService();
            clsActosAdminstrativos actoAdmin = new clsActosAdminstrativos();
            actoAdmin = objActoAdmin.GetActoAdminPorId(id);

            actoAdmin.UsuarioId = Varios.UsuarioId();
            actoAdmin.EstadoRegistro = eEstadoRegistro.Modificado;

            string resultado = string.Empty;

            switch (ViewState["Accion"].ToString())
            {
                case "Anular":
                    actoAdmin.EstadoId = (int)eEstadoActoAdmin.Anulado;
                    break;
                case "Firmar":
                    actoAdmin.EstadoId = (int)eEstadoActoAdmin.Firmado;
                    break;
                default:
                    break;
            }
            resultado = objActoAdmin.Guardar(actoAdmin);
            if (resultado.Contains("char(13)"))
            {
                mpupError.MensajeTextBox = resultado;
                mpupError.Mostrar();
                return;
            }
            Response.Redirect("Default.aspx");
        }

        private List<clsActosAdminstrativos> ActosAdminstrativos
        {
            get
            {
                if (Session[ConstantesItems.ACTOS_ADMIN] == null)
                    Session[ConstantesItems.ACTOS_ADMIN] = new List<clsActosAdminstrativos>();

                return (List<clsActosAdminstrativos>)Session[ConstantesItems.ACTOS_ADMIN];
            }
            set
            {
                Session[ConstantesItems.ACTOS_ADMIN] = value;
            }
        }

        protected void Adfiltro_Filtro(object sender, FiltroEventArgs e)
        {
            DataSourceActosAdmin ObjActosAd = new DataSourceActosAdmin();
            clsTipoFiltro filtropor = DataSourceGeneral.ObtenerFiltroPorId(e.Filtro.FiltroPor, Proceso.ActoAdmin);
            Filtros filtro = (Filtros)Enum.ToObject(typeof(Filtros), filtropor.Id);
            if (filtropor.TipoDato != TypeCode.DateTime)
            {
                ActosAdminstrativos = ObjActosAd.ObtenerActosAdministrativosFiltro(filtropor.Nombre, e.Filtro.NombreDeclarante);
            }
            else
            {
                ActosAdminstrativos = ObjActosAd.ObtenerActosAdministrativosFiltro(filtropor.Nombre, "'" + e.Filtro.Fecha1 + "' AND '" + e.Filtro.Fecha2 + "'");
            }
            grdActosAdministrativos.DataSourceID = null;
            grdActosAdministrativos.DataSource = ActosAdminstrativos;
            grdActosAdministrativos.DataBind();
        }
    }
}