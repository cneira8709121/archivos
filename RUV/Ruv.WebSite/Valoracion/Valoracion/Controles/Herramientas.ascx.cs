using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;

public partial class Utilidades_Controles_dpsHerramientas : System.Web.UI.UserControl
{


    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public int Persona
    {
        get { return Convert.ToInt32(ViewState["Persona"]); }
        set { ViewState["Persona"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Cargar();
        }
    }
    private void Cargar()
    {
        lbHerramientas.Items.Clear();
        List<clsHerramietasOrganizar> herramientas = new List<clsHerramietasOrganizar>();
        if (Session[ConstantesItems.HERRAMIENTAS] != null)
        {
            herramientas = ((List<clsHerramietasOrganizar>)Session[ConstantesItems.HERRAMIENTAS]);
            if (Persona > 0 && herramientas.Count > 0)
            {
                foreach (clsHerramientaAnexoPer her in herramientas.First(x => x.PersonaId == Persona).Herramientas)
                {
                    ListItem li = new ListItem();
                    li.Text = string.Format("{0}-({1}){2}", her.Herramienta.Tipo.Nombre.Substring(0, 1), her.Fecha.ToShortDateString(), her.Herramienta.Nombre);
                    li.Value = her.HerramientaId.ToString();
                    lbHerramientas.Items.Add(li);
                }
            }
        }
    }

    protected void ddlTipoHerramienta_SelectIndexChange(object sender, EventArgs e)
    {
        if (ddlTipoHerramienta.TienenValor)
        {
            ddlFuentes.Items.Clear();
            ddlFuentes.Valor = ddlTipoHerramienta.SelectedValue;
            ddlFuentes.Source = Poblar.Herramientas;
        }
    }

    protected void btnAccion_Click(object sender, EventArgs e)
    {
        //Nuevo
        List<clsHerramietasOrganizar> herramientas = new List<clsHerramietasOrganizar>();
        if (Session[ConstantesItems.HERRAMIENTAS] != null)
        {
            herramientas = ((List<clsHerramietasOrganizar>)Session[ConstantesItems.HERRAMIENTAS]);
        }
        else
        {
            herramientas = new List<clsHerramietasOrganizar>();
        }
        bool editar = false;
        if (ViewState["Editar"] != null)
        {
            editar = true;
        }
        ValoracionService objService = new ValoracionService();
        if (!editar)
        {
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
                herra.Tipo = objService.TipoHerramientaPorId(herra.TipoId);
                her.Herramienta = herra;
            }
            else
            {
                if (ddlFuentes.SelectedIndex > 0)
                {
                    herra.Id = Convert.ToInt32(ddlFuentes.SelectedValue);
                    herra.Nombre = ddlFuentes.SelectedItem.Text;
                    herra.TipoId = Convert.ToInt32(ddlTipoHerramienta.SelectedValue);
                    herra.Tipo = objService.TipoHerramientaPorId(herra.TipoId);
                    her.Herramienta = herra;
                }
                
            }

            if (herramientas.Exists(x => x.PersonaId == Persona))
            {
                List<clsHerramientaAnexoPer> herAnexoPer = herramientas.First(x => x.PersonaId == Persona).Herramientas;
                herAnexoPer.Add(her);

                clsHerramietasOrganizar herorga = herramientas.First(x => x.PersonaId == Persona);
                herorga.Herramientas = herAnexoPer;
            }
            else
            {
                List<clsHerramientaAnexoPer> herAnexoPer = new List<clsHerramientaAnexoPer>();
                herAnexoPer.Add(her);

                clsHerramietasOrganizar herorga = new clsHerramietasOrganizar();
                herorga.Herramientas = herAnexoPer;
                herorga.PersonaId = Persona;

                herramientas.Add(herorga);
            }
        }
        else
        {
            int HerramientaId = Convert.ToInt32(lbHerramientas.SelectedValue);
            List<clsHerramientaAnexoPer> herAnexoPer = herramientas.First(x => x.PersonaId == Persona).Herramientas;
            clsHerramientaAnexoPer her = herAnexoPer.First(x => x.HerramientaId == HerramientaId);
            her.Fecha = txtFecha.Fecha;
            her.Descripcion = txtDescripcion.Text;
            her.HerramientaId = Convert.ToInt32(ddlFuentes.SelectedValue);
            her.UsadoParaDesicion = chkUsadoParaDesicio.Checked;
            clsHerramientas herra = new clsHerramientas();
            if (ddlFuentes.SelectedValue == ValoresDropDownList.OtroValor.GetHashCode().ToString())
            {
                herra.Id = (int)(ValoresDropDownList.NoSeleccion);
                herra.Nombre = txtFuente.Text.ToUpper();
                herra.TipoId = Convert.ToInt32(ddlTipoHerramienta.SelectedValue);
                herra.Tipo = objService.TipoHerramientaPorId(herra.TipoId);
                her.Herramienta = herra;
            }
            else
            {
                herra.Id = Convert.ToInt32(ddlFuentes.SelectedValue);
                herra.Nombre = ddlFuentes.SelectedItem.Text;
                herra.TipoId = Convert.ToInt32(ddlTipoHerramienta.SelectedValue);
                herra.Tipo = objService.TipoHerramientaPorId(herra.TipoId);
                her.Herramienta = herra;
            }
        }

        ViewState.Remove("Editar");

        Session[ConstantesItems.HERRAMIENTAS] = herramientas;

        Limpiar();
        Panel1.Visible = false;
        Panel2.Visible = true;
        ddlTipoHerramienta_SelectIndexChange(sender, EventArgs.Empty);
        Cargar();
    }
    protected void btnEditar_Click(object sender, EventArgs e)
    {
        //Modificar
        if (lbHerramientas.SelectedValue != null)
        {
            List<clsHerramietasOrganizar> herramientas = new List<clsHerramietasOrganizar>();
            if (Session[ConstantesItems.HERRAMIENTAS] != null)
            {
                herramientas = ((List<clsHerramietasOrganizar>)Session[ConstantesItems.HERRAMIENTAS]);
            }
            else
            {
                herramientas = new List<clsHerramietasOrganizar>();
            }
            int HerramientaId = Convert.ToInt32(lbHerramientas.SelectedValue);
            List<clsHerramientaAnexoPer> herAnexoPer = herramientas.First(x => x.PersonaId == Persona).Herramientas;
            clsHerramientaAnexoPer her = herAnexoPer.First(x => x.HerramientaId == HerramientaId);

            ddlTipoHerramienta.SelectedValue = her.Herramienta.TipoId.ToString();
            ddlTipoHerramienta_SelectIndexChange(sender, e);

            ddlFuentes.SelectedValue = her.HerramientaId.ToString();
            if (ddlFuentes.SelectedValue.Equals(ValoresDropDownList.OtroValor.GetHashCode().ToString()))
            {
                dvNuevaFuente.Visible = true;
                txtFuente.Text = her.Herramienta.Nombre;
            }

            chkUsadoParaDesicio.Checked = her.UsadoParaDesicion;

            txtDescripcion.Text = her.Descripcion;
            txtFecha.Fecha = her.Fecha;

            Panel1.Visible = true;
            Panel2.Visible = false;

            ViewState["Editar"] = true;
        }
    }

    protected void tbnQuitar_Click(object sender, EventArgs e)
    {
        //Quitar
        if (lbHerramientas.SelectedValue != null)
        {
            List<clsHerramietasOrganizar> herramientas = new List<clsHerramietasOrganizar>();
            if (Session[ConstantesItems.HERRAMIENTAS] != null)
            {
                herramientas = ((List<clsHerramietasOrganizar>)Session[ConstantesItems.HERRAMIENTAS]);
            }
            else
            {
                herramientas = new List<clsHerramietasOrganizar>();
            }
            int HerramientaId = Convert.ToInt32(lbHerramientas.SelectedValue);
            List<clsHerramientaAnexoPer> herAnexoPer = herramientas.First(x => x.PersonaId == Persona).Herramientas;
            clsHerramientaAnexoPer her = herAnexoPer.First(x => x.HerramientaId == HerramientaId);

            herramientas.First(x => x.PersonaId == Persona).Herramientas.Remove(her);

            Cargar();
        }
    }


    protected void ddlFuentes_SelectIndexChange(object sender, EventArgs e)
    {
        
        dvNuevaFuente.Visible = (ddlFuentes.SelectedValue == ValoresDropDownList.OtroValor.GetHashCode().ToString()) ? true : false;
        if (ddlFuentes.SelectedValue == HerramientasNoAplica.NoAplicaContexto.GetHashCode().ToString()
            || ddlFuentes.SelectedValue == HerramientasNoAplica.NoAplicaJuridica.GetHashCode().ToString()
            || ddlFuentes.SelectedValue == HerramientasNoAplica.NoAplicaTecnica.GetHashCode().ToString())
        {
            txtDescripcion.EsRequerido = true;
            txtDescripcion.MensajeRequerido = "Ingrese por que no aplica una fuente de este tipo";
        }
        else
        {
            if (txtDescripcion.EsRequerido)
            {
                txtDescripcion.EsRequerido = false;
            }
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        //Espacio para nuevo
        Panel1.Visible = true;
        Panel2.Visible = false;
        Limpiar();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Regresar
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
        txtFecha.Text = txtFuente.Text = txtDescripcion.Text = string.Empty;
    }
}