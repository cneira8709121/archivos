using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;
using System.ComponentModel;

public partial class Utilidades_Controles_dpsGeografia : System.Web.UI.UserControl
{

    [Bindable(true)]
    [Localizable(true)]
    public string ClientIDPais
    {
        get { return ddlPais.ClientID; }
        set
        {
            ddlPais.ID = value;
            ddlPais.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            hfPais.ID = "hfPais" + value;
            hfPais.ClientIDMode = System.Web.UI.ClientIDMode.Static;
        }
    }

    [Bindable(true)]
    [Localizable(true)]
    public string ClientIDDepto
    {
        get { return ddlDepartamento.ClientID; }
        set
        {
            ddlDepartamento.ID = value;
            ddlDepartamento.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            hfDpto.ID = "hfDpto" + value;
            hfDpto.ClientIDMode = System.Web.UI.ClientIDMode.Static;
        }
    }

    [Bindable(true)]
    [Localizable(true)]
    public string ClientIDMunicipio
    {
        get { return ddlMunicipio.ClientID; }
        set
        {
            ddlMunicipio.ID = value;
            ddlMunicipio.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            hfMun.ID = "hfMun" + value;
            hfMun.ClientIDMode = System.Web.UI.ClientIDMode.Static;
        }
    }

    [Bindable(true)]
    [DefaultValue(NivelesGeografia.Pais)]
    [Localizable(true)]
    public NivelesGeografia Nivel
    {
        get { return Nivel; }
        set
        {
            switch (value)
            {
                case NivelesGeografia.Pais:
                    cPais.Disabled = false;
                    cDpto.Disabled = false;
                    cMun.Disabled = false;
                    break;
                case NivelesGeografia.Departamento:
                    ddlPais.Disabled = true;
                    cDpto.Disabled = false;
                    cMun.Disabled = false;
                    break;
                case NivelesGeografia.Municipio:
                    cPais.Disabled = true;
                    cDpto.Disabled = true;
                    cMun.Disabled = false;
                    break;
            }
        }
    }

    private int _pais;

    [Bindable(true)]
    [DefaultValue(48)]
    [Localizable(true)]
    public int Pais
    {
        get
        {
            if (!string.IsNullOrEmpty(hfPais.Value))
                _pais = Convert.ToInt32(hfPais.Value);
            return _pais;
        }
        set
        {
            _pais = value;
            hfPais.Value = _pais.ToString();
        }
    }

    private int _departamento;
    [Bindable(true)]
    [DefaultValue(6377)]
    [Localizable(true)]
    public int Departamento
    {
        get
        {
            if (!string.IsNullOrEmpty(hfDpto.Value))
                _departamento = Convert.ToInt32(hfDpto.Value);
            return _departamento;
        }
        set
        {
            _departamento = value;
            hfDpto.Value = _departamento.ToString();
        }
    }

    private bool esRequerido;

    [Bindable(true)]
    [DefaultValue(true)]
    [Localizable(true)]
    public bool EsRequerido
    {
        get { return esRequerido; }
        set { esRequerido = value; }
    }
    private string mensajeRequerido;

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string MensajeRequerido
    {
        get { return mensajeRequerido; }
        set { mensajeRequerido = value; }
    }

    public bool Bloquear
    {
        set
        {
            ddlPais.Disabled = value;
            ddlDepartamento.Disabled = value;
            ddlMunicipio.Disabled = value;
        }
    }


    private int _municipio;
    [Bindable(true)]
    [DefaultValue(6516)]
    [Localizable(true)]
    public int Municipio
    {
        get
        {
            if (!string.IsNullOrEmpty(hfMun.Value))
                _municipio = Convert.ToInt32(hfMun.Value);
            return _municipio;
        }
        set
        {
            hfMun.Value = value.ToString();
            if (_pais == 0)
                _pais = 48;
            if (_pais > 0 && _departamento > 0 && value > 0)
            {
                string script = "<script>setGeografia('" + value + "', '" + this._departamento + "', '" + this._pais + "','" + this.ddlMunicipio.ClientID + "', '" + this.ddlDepartamento.ClientID + "','" + this.ddlPais.ClientID + "');</script>";
                ScriptManager.RegisterStartupScript
                    (this, this.GetType(),
                    Guid.NewGuid().ToString(),
                    script,
                    false);
            }
        }
    }

    public int TipoEntornoId
    {
        get { return Convert.ToInt32(Entorno.SelectedValue); }
        set { Entorno.SelectedValue = value.ToString(); }
    }

    public string OtroLocCorr
    {
        get
        {
            return LocCorr.Text;            
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
            return BarrioVereda.Text;            
        }
        set
        {
            if (value != null && !string.IsNullOrEmpty(value))
                BarrioVereda.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadInitData();
        if (!Page.IsPostBack)
        {
            
            List<clsGeografia> lista = RUV.Current.ListadosGeneralesValoracion.Geografias.Where(x => x.Tipo == 4 && x.Padre == 0).ToList();
            Entorno.DataSource = lista;
            Entorno.DataBind();
        }
    }
    private void LoadInitData()
    {
        if (this.hfMun.Value == "0" || string.IsNullOrEmpty(this.hfMun.Value))
        {
            string script = "<script>loadGeografia('" + this.ddlPais.ClientID + "', '" + this.ddlDepartamento.ClientID + "');</script>";
            ScriptManager.RegisterStartupScript
                (this, this.GetType(),
                Guid.NewGuid().ToString(),
                script,
                false);
        }
        ddlPais.Attributes.Add("onchange", "CambioPais('" + this.ddlPais.ClientID + "', '" + this.ddlDepartamento.ClientID + "','" + this.hfPais.ClientID + "');");
        ddlDepartamento.Attributes.Add("onchange", "CambioDpto('" + this.ddlDepartamento.ClientID + "', '" + this.ddlMunicipio.ClientID + "','" + this.hfDpto.ClientID + "');");
        ddlMunicipio.Attributes.Add("onchange", "CambioMun('" + this.ddlMunicipio.ClientID + "','" + this.hfMun.ClientID + "')");

    }

    public void ReiniciarGeografia()
    {
        string script = "<script>loadGeografia('" + this.ddlPais.ClientID + "', '" + this.ddlDepartamento.ClientID + "');</script>";
        ScriptManager.RegisterStartupScript
            (this, this.GetType(),
            Guid.NewGuid().ToString(),
            script,
            false);

        ddlPais.Attributes.Add("onchange", "CambioPais('" + this.ddlPais.ClientID + "', '" + this.ddlDepartamento.ClientID + "','" + this.hfPais.ClientID + "');");
        ddlDepartamento.Attributes.Add("onchange", "CambioDpto('" + this.ddlDepartamento.ClientID + "', '" + this.ddlMunicipio.ClientID + "','" + this.hfDpto.ClientID + "');");
        ddlMunicipio.Attributes.Add("onchange", "CambioMun('" + this.ddlMunicipio.ClientID + "','" + this.hfMun.ClientID + "')");
    }

}