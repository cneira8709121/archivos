using System;
using System.ComponentModel;
using System.Web.UI;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WebSite.Utilidades.Controles {

    public partial class ModalPopUp : UserControl, IModalPopUp
    {

        #region Managed Events

        protected event OnBtnClick Ok;

        protected event OnBtnClick Cancelar;

        #endregion

        #region Control Managed Properties

        [DefaultValue("")]
        protected string UrlRedireccion {
            get { return this.UrlRedireccionHiddenField.Value; }
            set { this.UrlRedireccionHiddenField.Value = value; }
        }

        [DefaultValue("Título")]
        protected string Titulo {
            get { return this.TituloLabel.Text; }
            set { this.TituloLabel.Text = value; }
        }

        [DefaultValue("Mensaje")]
        protected string Mensaje {
            get { return this.MensajeLabel.Text; }
            set { this.MensajeLabel.Text = value; }
        }

        [DefaultValue("Ok")]
        protected string TextoOk {
            get { return this.OkButton.Text; }
            set { this.OkButton.Text = value; }
        }

        [DefaultValue("Cancelar")]
        protected string TextoCancelar {
            get { return this.CancelarButton.Text; }
            set { this.CancelarButton.Text = value; }
        }

        #endregion

        #region Functions

        public void Ocultar() {
            this.programmaticModalPopup.Hide();
        }

        public void MostrarMensaje(string titulo, string mensaje) {
            this.Titulo = titulo;
            this.Mensaje = mensaje;
            this.OkButton.Visible = true;
            this.Ok = null;
            this.CancelarButton.Visible = false;
            this.Cancelar = null;
            this.UrlRedireccion = string.Empty;
            this.programmaticModalPopup.Show();
        }

        public void MostrarMensajeYRedirigir(string titulo, string mensaje, string urlRedireccion) {
            this.Titulo = titulo;
            this.Mensaje = mensaje;
            this.OkButton.Visible = true;
            this.Ok = null;
            this.CancelarButton.Visible = false;
            this.Cancelar = null;
            this.UrlRedireccion = urlRedireccion;
            this.programmaticModalPopup.Show();
        }

        public void MostrarMensajeConfirmacion(string titulo, string mensaje, OnBtnClick eventoAceptar) {
            this.Titulo = titulo;
            this.Mensaje = mensaje;
            this.OkButton.Visible = true;
            this.Ok = eventoAceptar;
            this.CancelarButton.Visible = true;
            this.Cancelar = null;
            this.UrlRedireccion = string.Empty;
            this.programmaticModalPopup.Show();
        }

        #endregion

        #region Control Events

        protected void OkButton_Click(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(UrlRedireccion)) {
                Response.Redirect(UrlRedireccion);
            }
            else {
                if (Ok != null) {
                    Ok(sender, e);
                }
            }
        }

        protected void CancelarButton_Click(object sender, EventArgs e) {
            if (Cancelar != null) {
                Cancelar(sender, e);
            }
        }

        #endregion
    
    }
}