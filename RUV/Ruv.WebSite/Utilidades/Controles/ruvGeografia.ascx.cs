using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Utilidades_Controles_dpsGeografia : System.Web.UI.UserControl
{

    public event CambiaValor CambioGeografia;

    public int DepartamentoId
    {
        get { return Convert.ToInt32(Departamento.SelectedValue); }
        set { Departamento.SelectedValue = value.ToString(); }
    }

    public int MunicipioId
    {
        get { return Convert.ToInt32(Municipio.SelectedValue); }
        set { Municipio.SelectedValue = value.ToString(); }
    }

    public int TipoEntornoId
    {
        get { return Convert.ToInt32(Entorno.SelectedValue); }
        set { Entorno.SelectedValue = value.ToString(); }
    }

    public int? LocCorreId
    {
        get
        {
            if (LocCorr.SelectedIndex > 0 && LocCorr.SelectedValue != null)
            {
                int valor = 0;
                Int32.TryParse(LocCorr.SelectedValue, out valor);
                if (valor > 0)
                {
                    return valor;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        set
        {
            if (LocCorr.SelectedIndex > 0)
                LocCorr.SelectedValue = value.Value.ToString();
        }
    }

    public int? BarrioVerId
    {
        get
        {
            if (BarrioVereda.SelectedIndex > 0 && BarrioVereda.SelectedValue != null)
            {
                int valor = 0;
                Int32.TryParse(BarrioVereda.SelectedValue, out valor);
                if (valor > 0)
                {
                    return valor;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        set
        {
            if (BarrioVereda.SelectedIndex > 0)
            {
                BarrioVereda.SelectedValue = value.Value.ToString();
            }
        }
    }


    public string OtroLocCorr
    {
        get
        {
            int valor = 0;
            Int32.TryParse(LocCorr.SelectedValue, out valor);
            if (valor == 0)
            {
                return LocCorr.Text;
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            if(value != null && !string.IsNullOrEmpty(value))
                LocCorr.Text = value;
        }
    }

    public string OtroBarrioVer
    {
        get
        {
            int valor = 0;
            Int32.TryParse(BarrioVereda.SelectedValue, out valor);
            if (valor == 0)
            {
                return BarrioVereda.Text;
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            if (value != null && !string.IsNullOrEmpty(value))
                BarrioVereda.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Cargar(0, Departamento.ID);
        }
    }

    public void Cargar(int valor, string id)
    {
        int nivel = (int)Enum.Parse(typeof(TipoGeografia), id);

        string actual = Enum.ToObject(typeof(TipoGeografia), nivel).ToString();
        string siguiente = Enum.ToObject(typeof(TipoGeografia), nivel + 1).ToString();
        
        
        if (valor > 0)
        {
            if (nivel == (int)TipoGeografia.Municipio)
            {
                valor = 0;
            }
            if (nivel >= (int)TipoGeografia.Entorno)
            {
                valor = Convert.ToInt32(Municipio.SelectedValue);
            }
            List<clsGeografia> lista = RUV.Current.ListadosGeneralesValoracion.Geografias.Where(x=> x.Tipo == nivel+1 && x.Padre == valor).ToList(); 

            if (this.FindControl(siguiente).GetType().ToString().Equals("ASP.utilidades_controles_ruvdropdownlist_ascx"))
            {
                ((Utilidades_Controles_ruvDropDownList)this.FindControl(siguiente)).Items.Clear();
                ((Utilidades_Controles_ruvDropDownList)this.FindControl(siguiente)).DataSource = lista;
                ((Utilidades_Controles_ruvDropDownList)this.FindControl(siguiente)).DataBind();
            }
            else
            {
                var ddl = ((AjaxControlToolkit.ComboBox)this.FindControl(siguiente));
                ddl.Items.Clear();
                ListItem liPrimero = new ListItem();
                liPrimero.Text = "[Seleccione Uno]";
                liPrimero.Value = ValoresDropDownList.NoSeleccion.GetHashCode().ToString();
                ddl.Items.Add(liPrimero);
                ddl.DataSource = lista;
                ddl.DataBind();
            }
        }
        else
        {
            List<clsGeografia> lista = RUV.Current.ListadosGeneralesValoracion.Geografias.Where(x => x.Tipo == nivel && x.Padre == valor).ToList();//objValoracion.ListarGeografia(nivel, padre, valores);

            if (this.FindControl(actual).GetType().ToString().Equals("ASP.utilidades_controles_ruvdropdownlist_ascx"))
            {
                ((Utilidades_Controles_ruvDropDownList)this.FindControl(actual)).DataSource = lista;
                ((Utilidades_Controles_ruvDropDownList)this.FindControl(actual)).DataBind();
            }
            else
            {
                var ddl = ((AjaxControlToolkit.ComboBox)this.FindControl(actual));
                ddl.Items.Clear();
                ListItem liPrimero = new ListItem();
                liPrimero.Text = "[Seleccione Uno]";
                liPrimero.Value = ValoresDropDownList.NoSeleccion.GetHashCode().ToString();
                ddl.Items.Add(liPrimero);
                ddl.DataSource = lista;
                ddl.DataBind();
            }
        }
    }


    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (sender as DropDownList);
        Cargar(Convert.ToInt32(ddl.SelectedValue), ddl.Parent.ID);
        if (CambioGeografia != null)
        {
            CambioGeografia(sender, e);
        }
    }
}