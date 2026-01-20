using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Utilidades_Controles_ruvDropDownList : System.Web.UI.UserControl
{

    public event SelectIndexChanged SelectIndexChange;

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataSourceID
    {
        get { return ddl.DataSourceID; }
        set { ddl.DataSourceID = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public override string ClientID
    {
        get { return ddl.ClientID; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string OnChangeScript
    {
        set { ddl.Attributes.Add("onChange", value); }
    }

    [Bindable(true)]
    [DefaultValue("ddl")]
    [Localizable(true)]
    public string IdCombo
    {
        get { return ddl.ID; }
        set
        {
            ddl.ID = value;
            ddl.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            cv_ddl.ControlToValidate = value;
        }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string ValidationGroup
    {
        get { return ddl.ValidationGroup; }
        set { ddl.ValidationGroup = value; }
    }

    [DefaultValue(true)]
    public bool Enabled
    {
        get { return ddl.Enabled; }
        set { ddl.Enabled = value; }
    }


    [DefaultValue(false)]
    public bool CauseValidation
    {
        get { return ddl.CausesValidation; }
        set { ddl.CausesValidation = value; }
    }



    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataValueField
    {
        get { return ddl.DataValueField; }
        set { ddl.DataValueField = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataTextFormatString
    {
        get { return ddl.DataTextFormatString; }
        set { ddl.DataTextFormatString = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataTextField
    {
        get { return ddl.DataTextField; }
        set { ddl.DataTextField = value; }
    }

    [DefaultValue("")]
    public object DataSource
    {
        get { return ddl.DataSource; }
        set
        {
            Varios.AgregarSeleccioneUno(ref ddl);
            ddl.DataSource = value;
        }
    }

    [Bindable(true)]
    [DefaultValue("0")]
    [Localizable(true)]
    public string SelectedValue
    {
        get { return ddl.SelectedValue; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                ddl.SelectedValue = "0";
            }
            else
            {
                ddl.SelectedValue = value;
            }

        }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public ListItem SelectedItem
    {
        get { return ddl.SelectedItem; }
    }


    [Bindable(true)]
    [DefaultValue(false)]
    [Localizable(true)]
    public bool TienenValor
    {
        get { return (ddl.SelectedIndex > 0); }
    }


    [Bindable(true)]
    [DefaultValue(false)]
    [Localizable(true)]
    public bool AutoPostBack
    {
        get { return ddl.AutoPostBack; }
        set { ddl.AutoPostBack = value; }
    }

    [Bindable(true)]
    [DefaultValue(100)]
    [Localizable(true)]
    public Unit Width
    {
        get { return ddl.Width; }
        set { ddl.Width = value; }
    }

    [Bindable(true)]
    [DefaultValue(0)]
    [Localizable(true)]
    public int SelectedIndex
    {
        get { return ddl.SelectedIndex; }
        set { ddl.SelectedIndex = value; }
    }

    private bool esRequerido;
    [Bindable(true)]
    [DefaultValue(false)]
    [Localizable(true)]
    public bool EsRequerido
    {
        get { return esRequerido; }
        set { esRequerido = value; }
    }

    private string mensajeError;
    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string MensajeError
    {
        get { return mensajeError; }
        set { mensajeError = value; }
    }


    [Bindable(true)]
    [Localizable(true)]
    public ListItemCollection Items
    {
        get { return ddl.Items; }
    }

    [DefaultValue("")]
    [Bindable(true)]
    [Localizable(true)]
    public string Valor { get; set; }


    [Bindable(true)]
    [Localizable(true)]
    public Poblar Source
    {
        set
        {
            InsertarDatos(value, Valor);
        }
    }

    [Bindable(true)]
    [Localizable(true)]
    public DropDownList DropDownList
    {
        get { return ddl; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        if (ddl.Items.Count == 0)
        {
            Varios.AgregarSeleccioneUno(ref ddl);
        }

        vce_cv_ddl.Enabled = esRequerido;
        cv_ddl.Enabled = esRequerido;
        if (esRequerido)
        {
            cv_ddl.ErrorMessage = mensajeError;
        }
    }


    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SelectIndexChange != null)
        {
            SelectIndexChange(sender, e);
        }
    }

    public void AgregarSeleccione()
    {
        Varios.AgregarSeleccioneUno(ref ddl);
    }


    public void AgregarOtroValor()
    {
        Varios.AgregarOtroValor(ref ddl);
    }

    public void InsertarDatos(Poblar tipo, string valor)
    {
        if (ddl.Items.Count == 0)
        {
            Varios.AgregarSeleccioneUno(ref ddl);
        }
        object obj = (object)ddl;
        DataSourceGeneral.PoblarControl(ref obj, tipo, valor);
        ddl = (DropDownList)obj;
    }

}
