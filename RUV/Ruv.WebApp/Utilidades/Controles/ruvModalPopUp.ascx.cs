using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Utilidades_Controles_dpsModalPopUp : System.Web.UI.UserControl
{
    public event OnBtnClick Ok;
    public event OnBtnClick Cancel;

    [DefaultValue("")]
    public string UrlRedireccion
    {
        get { return UrlRedireccionHidden.Value; }
        set { UrlRedireccionHidden.Value = value; }
    }

    [DefaultValue("")]
    public string Mensaje
    {
        get { return lblMensajeModalpoup.Text; }
        set { lblMensajeModalpoup.Text = value; }
    }

    [DefaultValue("TituloLabel")]
    public string IDTitulo
    {
        get { return this.lblMensajeModalpoup.ID; }
        set { this.lblMensajeModalpoup.ID = value; this.lblMensajeModalpoup.ClientIDMode = System.Web.UI.ClientIDMode.Static; }
    }

    [DefaultValue("")]
    public string MensajeTextBox
    {
        get { return txtMensaje.Text; }
        set { txtMensaje.Text = value; }
    }

    [DefaultValue("programmaticModalPopupBehavior")]
    public string BehaviorID
    {
        get { return programmaticModalPopup.BehaviorID; }
        set { programmaticModalPopup.BehaviorID = value; }
    }

    [DefaultValue("LinkButton1_Modalpopup")]
    public string TargetControlID
    {
        get { return programmaticModalPopup.TargetControlID; }
        set { programmaticModalPopup.TargetControlID = value; }
    }


    [DefaultValue(false)]
    public bool DropShadow
    {
        get { return programmaticModalPopup.DropShadow; }
        set { programmaticModalPopup.DropShadow = value; }
    }

    [DefaultValue(true)]
    public bool MostrarBotones
    {
        get { return dvBotones.Visible; }
        set { dvBotones.Visible = value; }
    }

    [DefaultValue("RUV VALORACIÓN")]
    public string Titulo
    {
        get { return lblTitulo.Text; }
        set { lblTitulo.Text = value.Trim(); }
    }

    [DefaultValue(true)]
    public bool MostrarImagen
    {
        get { return imgCargando.Visible; }
        set
        {
            imgCargando.Visible = value;
            pnlTituloPrograma.Visible = !value;
        }
    }

    public void Ocultar()
    {
        programmaticModalPopup.Hide();
    }

    public void Mostrar()
    {
        UrlRedireccion = string.Empty;
        programmaticModalPopup.Show();
    }

    public void Mostrar(string urlRedireccion)
    {
        UrlRedireccion = urlRedireccion;
        programmaticModalPopup.Show();
    }

    [DefaultValue(false)]
    public bool filatextBox
    {
        get { return FilaTextBox.Visible; }
        set { FilaTextBox.Visible = value; }
    }

    [DefaultValue(false)]
    public bool filalabel
    {
        get { return FilaLabel.Visible; }
        set { FilaLabel.Visible = value; }
    }

    [DefaultValue("Ok")]
    public string TextoOk
    {
        get
        {
            return OkButton.Text;
        }
        set
        {
            OkButton.Text = value;
        }
    }

    [DefaultValue("Cancelar")]
    public string TextoCancelar
    {
        get
        {
            return CancelButton.Text;
        }
        set
        {
            CancelButton.Text = value;
        }
    }

    public string OnOkScript
    {
        get { return programmaticModalPopup.OnOkScript; }
        set
        {
            OkButton.OnClientClick = value;
        }
    }


    public bool VisibleBotonCancelar
    {
        get
        {
            return CancelButton.Visible;
        }
        set
        {
            if (value)
            {
                programmaticModalPopup.CancelControlID = CancelButton.ID;
            }
            else
            {
                programmaticModalPopup.CancelControlID = string.Empty;
            }
            CancelButton.Visible = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //int programaId = Convert.ToInt32(Session[ConstantesSesion.PROGRAMA]);
        //ProgramaBusiness objPrograma = new ProgramaBusiness();
        //lblTitulo.Text = objPrograma.TraerProgramaPorId(programaId).nombre;
        //if (VisibleBotonCancelar && MostrarBotones)
        //{
        //    programmaticModalPopup.CancelControlID = CancelButton.ID;
        //}
    }

    protected void OkButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(UrlRedireccion))
        {
            Response.Redirect(UrlRedireccion);
        }
        else
        {
            if (Ok != null)
            {
                Ok(sender, e);
            }
        }
    }
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        if (Cancel != null)
        {
            Cancel(sender, e);
        }
    }
}
