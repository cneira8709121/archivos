using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Business.DTO.Notificacion;

public partial class Mensajeria_WebUserControl : System.Web.UI.UserControl
{
    public event OnNotificacionClick MensajeClick;

    protected void Page_Load(object sender, EventArgs e) {
        if (!Page.IsPostBack) {
            string error = string.Empty;
            SIRAV.Entidades.Administracion.USUARIO _us = RUV.Current.Usuario;
            int currentUser = _us.ID;
            if (currentUser != 0)
            {
                var notificaciones = (new NotificacionInternaService()).ObtenerNotificacionInterna(currentUser, ref error);
                this.NotificacionesList.DataSource = notificaciones;
                this.NotificacionesList.DataBind();
                this.PanelNotificaciones.Visible = notificaciones != null && notificaciones.Count > 0;
            }
            else
                this.PanelNotificaciones.Visible = false;
        }
    }

    protected void IgnoreNotificationButton_Click(object sender, EventArgs args) {
        var button = sender as ImageButton;
        SIRAV.Entidades.Administracion.USUARIO _us = RUV.Current.Usuario;
        int currentUser = _us.ID;
        if (button != null && currentUser != 0) {
            int notificationId;
            if (int.TryParse(button.CommandArgument, out notificationId)) {
                string error = string.Empty;
                var service = new NotificacionInternaService();
                var result = service.MarcarLeido(notificationId, ref error);
                if (result && string.IsNullOrEmpty(error)) {
                    var notificaciones = service.ObtenerNotificacionInterna(currentUser, ref error);
                    this.NotificacionesList.DataSource = notificaciones;
                    this.NotificacionesList.DataBind();
                }
            }
        }
    }

    protected void ViewNotificationButton_click(object sender, EventArgs args)
    {
        if (sender != null && sender.GetType() == typeof(LinkButton))
        {
            LinkButton btnNotificacion = (LinkButton)sender;
            string msg = btnNotificacion.CommandArgument;

            if (MensajeClick != null)
            {
                MensajeClick(sender, new NotificacionEventArgs { CMensaje = msg });
            }
        }
    }
}