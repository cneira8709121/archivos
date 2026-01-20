using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Reflection;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Utilidades_Controles_dpsFiltros : System.Web.UI.UserControl
{
    public event FiltroHandler Filtro;

    private Proceso procesos;
    public Proceso Procesos
    {
        get { return procesos; }
        set { procesos = value; }
    }

    public string StrOrderCriteria
    {
        get 
        {
            if (ddlOrder.SelectedItem == null || ddlOrder.SelectedItem.Value == "0")
                return string.Empty;
            else
                return ddlOrder.SelectedItem.Text; 
        }
    }

    public bool blnOrderByVisible
    {
        set
        {
            lblOrder.Visible = value;
            ddlOrder.Visible = value;
        }
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        OnFiltro(sender, new FiltroEventArgs(GenerarFiltro()));
    }

    void OnFiltro(object sender, FiltroEventArgs e)
    {
        if (Filtro != null)
        {
            Filtro(sender, e);
        }
    }

    protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        LimpiarCampos();
        int tipoFiltro = Convert.ToInt32(ddlFiltro.SelectedValue);
        clsTipoFiltro filtro = DataSourceGeneral.ObtenerFiltroPorId(tipoFiltro, Procesos);
        if (filtro.TipoDato == TypeCode.DateTime)
        {
            tbValoresFecha.Visible = true;
            tbValoresTexto.Visible = false;
        }
        else
        {
            tbValoresFecha.Visible = false;
            tbValoresTexto.Visible = true;
            if (filtro.TipoDato == TypeCode.String)
            {
                lblValor2.Visible = false;
                txtValor2.Visible = false;
            }
            else
            {
                lblValor2.Visible = true;
                txtValor2.Visible = true;
            }
        }
        btnFiltrar.Visible = (tipoFiltro > 0) ? true : false;
    }

    protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void LimpiarCampos()
    {
        txtFecha1.Text = txtFecha2.Text = txtValor1.Text = txtValor2.Text = string.Empty;
    }

    private clsFiltro GenerarFiltro()
    {
        clsFiltro filtro = new clsFiltro();
        filtro.FiltroPor = Convert.ToInt32(ddlFiltro.SelectedValue);
        filtro.Texto1 = txtValor1.Text;
        filtro.Texto2 = txtValor2.Text;
        filtro.Fecha1 = txtFecha1.Fecha;
        filtro.Fecha2 = txtFecha2.Fecha;
        return filtro;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Varios.AgregarSeleccioneUno(ref ddlFiltro);
            DataSourceGeneral.PoblarFiltroPorProceso(Procesos, ref ddlFiltro);

            Varios.AgregarSeleccioneUno(ref ddlOrder);
            DataSourceGeneral.PoblarFiltroPorProceso(Procesos, ref ddlOrder);
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsolutePath);
    }
}
