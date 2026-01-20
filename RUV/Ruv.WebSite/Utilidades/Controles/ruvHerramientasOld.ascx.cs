using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;


public partial class Utilidades_Controles_dpsHerramientasOld : System.Web.UI.UserControl
{


    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string Descripcion
    {
        get { return txtDescripcion.Text; }
        set { txtDescripcion.Text = value; }
    }


    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string OtraFuente
    {
        get { return txtFuente.Text; }
        set { txtFuente.Text = value; }
    }



    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public int Fuente
    {
        get
        {
            return Convert.ToInt32(ddlFuentes.SelectedValue);
        }
        set
        {
            ddlFuentes.SelectedValue = value.ToString();
        }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public int TipoHerramienta
    {
        get { return Convert.ToInt32(ddlTipoHerramienta.SelectedValue); }
        set { ddlTipoHerramienta.SelectedValue = value.ToString(); }
    }



    [Bindable(true)]
    [DefaultValue(null)]
    [Localizable(true)]
    public List<clsHerramientaAnexoPer> Herramientas
    {
        set
        {
            foreach (clsHerramientaAnexoPer her in value)
            {
                ListItem li = new ListItem();
                li.Text = string.Format("{0}-({1}){2}", her.Herramienta.Tipo.Nombre.Substring(0,1), her.Fecha.ToShortDateString(), her.Herramienta.Nombre);
                li.Value = her.Herramienta.Nombre;
                lbHerramientas.Items.Add(li);
            }
        }
    }

    public event OnBtnHerramienta Agregar;
    public event OnBtnHerramienta Quitar;

    protected void ddlTipoHerramienta_SelectIndexChange(object sender, EventArgs e)
    {
        if (ddlTipoHerramienta.TienenValor)
        {
            ddlFuentes.Items.Clear();
            ddlFuentes.Valor = ddlTipoHerramienta.SelectedValue;
            ddlFuentes.Source = Poblar.Herramientas;
            ddlFuentes.AgregarOtroValor();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAccion_Click(object sender, EventArgs e)
    {
        ValoracionService objService = new ValoracionService();

        clsHerramientaAnexoPer her = new clsHerramientaAnexoPer();
        her.Id = 0;
        her.Fecha = txtFecha.Fecha;
        her.Descripcion = txtDescripcion.Text;
        her.HerramientaId = Convert.ToInt32(ddlFuentes.SelectedValue);
        her.UsadoParaDesicion = chkUsadoParaDesicio.Checked;
        clsHerramientas herra = new clsHerramientas();
        if (ddlFuentes.SelectedValue == ValoresDropDownList.OtroValor.GetHashCode().ToString())
        {
            herra.Id = 0;
            herra.Nombre = txtFuente.Text.ToUpper();
            herra.TipoId = Convert.ToInt32(ddlTipoHerramienta.SelectedValue);
            her.Herramienta = herra;
        }
        else
        {
            herra.Id = Convert.ToInt32(ddlFuentes.SelectedValue);
            herra.Nombre = ddlFuentes.SelectedItem.Text;
            herra.TipoId = Convert.ToInt32(ddlTipoHerramienta.SelectedValue);
            her.Herramienta = herra;
        }

        ListItem li = new ListItem();
        li.Text = string.Format("{0}-({1}){2}", ddlTipoHerramienta.SelectedItem.Text.Substring(0,1), her.Fecha.ToShortDateString(), her.Herramienta.Nombre);
        li.Value = her.AnexoPerId.ToString();
        li.Attributes.Add(her.AnexoPerId.ToString(), her.HerramientaId.ToString());
        lbHerramientas.Items.Add(li);

        //Limpiar
        Limpiar();

        Panel1.Visible = false;
        Panel2.Visible = true;

        ddlTipoHerramienta_SelectIndexChange(sender, EventArgs.Empty);

        OnAgregarClick(sender, new HerramientasEventArgs(her, lbHerramientas.Items.Count + 1));

    }
    void OnAgregarClick(object sender, HerramientasEventArgs e)
    {
        if (Agregar != null)
        {
            Agregar(sender, e);
        }
    }



    protected void ddlFuentes_SelectIndexChange(object sender, EventArgs e)
    {
        dvNuevaFuente.Visible = (ddlFuentes.SelectedValue == ValoresDropDownList.OtroValor.GetHashCode().ToString()) ? true : false;
    }
    protected void tbnQuitar_Click(object sender, EventArgs e)
    {
        OnQuitarClick(sender, new HerramientasEventArgs(null, lbHerramientas.SelectedIndex));
        lbHerramientas.Items.RemoveAt(lbHerramientas.SelectedIndex);
    }

    void OnQuitarClick(object sender, HerramientasEventArgs e)
    {
        if (Quitar != null)
        {
            Quitar(sender, e);
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Panel2.Visible = false;
        Limpiar();
    }
    protected void btnEditar_Click(object sender, EventArgs e)
    {
        
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        Limpiar();
    }

    private void Limpiar()
    {
        ddlTipoHerramienta.SelectedIndex = 0;
        ddlFuentes.Items.Clear();
        dvNuevaFuente.Visible = false;
        chkUsadoParaDesicio.Checked = false;
        ddlFuentes.AgregarSeleccione();
        txtFecha.Text = txtFuente.Text = txtDescripcion.Text= string.Empty;
    }
}