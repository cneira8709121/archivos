using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.WebApp.Utilidades.Controles;

public partial class ListaTareas_UCTarea : System.Web.UI.UserControl, IUCTarea {
    
    #region Propiedades
    
    public string Formulario
    {
        set 
        {
            lblFormulario.Text = value;
        }
    }

    public string Estado
    {
        set
        {
            lblEstado.Text = value;
        }
    }

    public DateTime Fecha
    {
        set
        {
            lblFecha.Text = value.ToString("dd/MM/yyyy");
        }
    }
    
    public int IdDeclaracion 
    {
        get
        {
            if (hfIdDeclaracion.Value != null)
                return int.Parse(hfIdDeclaracion.Value);
            else
                return 0;
        }
        set
        {
            hfIdDeclaracion.Value = value.ToString();
        }
    }

    public int? IdCorreccion
    {
        get
        {
            if (!string.IsNullOrEmpty(hfIdCorreccion.Value))
                return int.Parse(hfIdCorreccion.Value);
            else
                return null;
        }
        set
        {
            hfIdCorreccion.Value = value.ToString();
        }
    }

    public string strUrlTarea;

    #endregion Propiedades

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        DataSourceListaTareas info = new DataSourceListaTareas();

        //string[] arrParametros = e.CommandArgument.ToString().Split(new char[] { '|' });
        clsListaTareas clsListaTareas = info.ObtenerTarea(IdDeclaracion, IdCorreccion);

        if (clsListaTareas != null && (clsListaTareas.Tipo == "CORRECCION"))
        {
            HLinkTrabajar.NavigateUrl = string.Format("~/Correcciones/AprobarRechazarCorreccion.aspx?idCorreccion={0}&idRegpersona={1}&urlEvio={2}", clsListaTareas.Correccion, clsListaTareas.Regpersona, this.Request.Url.AbsolutePath);
        }

        if (clsListaTareas != null && (clsListaTareas.IdAccion == (int)eEstadoDeclaracion.ValoracionPendientePorRevision || clsListaTareas.IdAccion == (int)eEstadoDeclaracion.ValoracionPendientePorFirma))
        {
            HLinkTrabajar.NavigateUrl = string.Format("~/ActosAdmin/LiderValoracionJefe.aspx?id={0}&urlEvio={1}", IdDeclaracion, this.Request.Url.AbsolutePath);
        }
    }

    //protected void imgTrabajar_Click(object sender, ImageClickEventArgs e)
    //{
    //    DataSourceListaTareas info = new DataSourceListaTareas();

    //    //string[] arrParametros = e.CommandArgument.ToString().Split(new char[] { '|' });
    //    clsListaTareas clsListaTareas = info.ObtenerTarea(IdDeclaracion, IdCorreccion);

    //    if (clsListaTareas != null && (clsListaTareas.Tipo == "CORRECCION"))
    //    {
    //        Response.Redirect(string.Format("~/Correcciones/AprobarRechazarCorreccion.aspx?idCorreccion={0}&idRegpersona={1}&urlEvio={2}", clsListaTareas.Correccion, clsListaTareas.Regpersona, this.Request.Url.AbsolutePath));
    //    }

    //    if (clsListaTareas != null && (clsListaTareas.IdAccion == (int)eEstadoDeclaracion.ValoracionPendientePorRevision || clsListaTareas.IdAccion == (int)eEstadoDeclaracion.ValoracionPendientePorFirma))
    //    {
    //        Response.Redirect(string.Format("~/ActosAdmin/LiderValoracionJefe.aspx?id={0}&urlEvio={1}", IdDeclaracion, this.Request.Url.AbsolutePath));
    //    }
    //}

    #endregion Eventos
}