using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Utilidades_Controles_dpsTextBox : System.Web.UI.UserControl
{

    public event CambiaValor TextChanged;


    [Bindable(true)]
    [DefaultValue(true)]
    [Localizable(true)]
    public bool EsRequerido
    {
        get { return rv_txt.Enabled; }
        set { rv_txt.Enabled = value; }
    }
    private string mensajeRequerido;

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string MensajeRequerido
    {
        get { return rv_txt.ErrorMessage; }
        set { rv_txt.ErrorMessage = value; }
    }

    [DefaultValue("")]
    public string Text
    {
        get { return txt.Text; }
        set { txt.Text = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string ExpresionRegular
    {
        get { return rev_txt.ValidationExpression; }
        set { rev_txt.ValidationExpression = value; }
    }

    private bool requiereExpresion;
    [Bindable(true)]
    [DefaultValue(false)]
    [Localizable(true)]
    public bool RequiereExpresion
    {
        get { return requiereExpresion; }
        set { requiereExpresion = value; }
    }

    [DefaultValue(100)]
    public Unit Width
    {
        get { return txt.Width; }
        set { txt.Width = value; }
    }
    [DefaultValue(100)]
    public Unit Height
    {
        get { return txt.Height; }
        set { txt.Height = value; }
    }

    [Bindable(true)]
    [DefaultValue(false)]
    [Localizable(true)]
    public bool AutoPostBack
    {
        get { return txt.AutoPostBack; }
        set { txt.AutoPostBack = value; }
    }


    [DefaultValue(TextBoxMode.SingleLine)]
    public TextBoxMode TextMode
    {
        set { txt.TextMode = value; }
    }

    [Bindable(true)]
    [DefaultValue(true)]
    [Localizable(true)]
    public bool Enabled
    {
        get { return txt.Enabled; }
        set
        {
            txt.Enabled = value;
        }
    }


    [DefaultValue(false)]
    public bool Numerico
    {
        get { return ft_txt.Enabled; }
        set
        {
            ft_txt.Enabled = value;
        }
    }


    private string _script;


    [DefaultValue("")]
    public string Script
    {
        set { _script = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (requiereExpresion)
        {
            rev_txt.Enabled = requiereExpresion;
        }

        txt.Attributes.Add("onchange", _script);
    }


    public void txt_TextChanged(object sender, EventArgs e)
    {
        if (TextChanged != null)
        {
            TextChanged(sender, e);
        }
    }
}
