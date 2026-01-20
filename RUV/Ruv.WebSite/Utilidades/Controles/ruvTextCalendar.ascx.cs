using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Globalization;
using System.Configuration;

public partial class Utilidades_Controles_dpsTextCalendar : System.Web.UI.UserControl
{
    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public DateTime Fecha
    {
        get 
        {
            DateTime Fecha = new DateTime();
            if (!string.IsNullOrEmpty(txt.Text))
            {
                Fecha = Convert.ToDateTime(txt.Text);
            }
            return Fecha;
        }
        set { txt.Text = value.ToString(); }
    }


    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    [Obsolete("Usado Para Combinada")]
    public string ClientScript
    {
        set
        {
            //Envia evento de clien en cliente
            //imgCalendar.OnClientClick = value;
        }
    }

    [DefaultValue(false)]
    public bool EsValidaLaFecha
    {
        get { return cv_txt.IsValid; }
        set { cv_txt.IsValid = value; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string Text
    {
        get { return txt.Text; }
        set { txt.Text = value; }
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

    [DefaultValue(100)]
    public Unit Width
    {
        get { return txt.Width; }
        set { txt.Width = value; }
    }

    public string FechaString
    {
        get
        {
            string fec = string.Empty;
            DateTime fecha = new DateTime();
            if (!string.IsNullOrEmpty(txt.Text))
            {
                fecha = Convert.ToDateTime(txt.Text);
                fec = string.Format("{0} de {1} de {2}", fecha.Day, fecha.ToString("MMMM"), fecha.Year);
            }
            return fec;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ce_txtFechaNacimiento.EndDate = DateTime.Now;
        ce_txtFechaNacimiento.StartDate = new DateTime(1900, 1, 1);
        if (!esRequerido)
        {
            rv_txt.Enabled = esRequerido;
            vc_rv_txt.Enabled = esRequerido;
        }
    }

    public bool Enabled
    {
        get
        {
            return txt.Enabled;
        }
        set
        {
            rv_txt.Enabled =
            rv_txt.Enabled =
            txt.Enabled =
            imgCalendar.Enabled =
            vc_rv_txt.Enabled = value;
            //vc_re_txt.Enabled= value;
        }
    }

    protected void cv_txt_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DateTime fecha = DateTime.Now;
        if (DateTime.TryParse(args.Value, out fecha))
        {
            DateTime maxima = DateTime.Now;
            DateTime minima = new DateTime(1900, 1, 1);
            if (fecha <= maxima && fecha >= minima)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
        else
        {
            args.IsValid = false;
        }
        if (!args.IsValid)
        {
            return;
        }
    }


}
