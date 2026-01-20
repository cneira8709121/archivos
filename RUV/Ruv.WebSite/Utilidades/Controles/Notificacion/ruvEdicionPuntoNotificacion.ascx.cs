using System;
using System.Web.UI;

public partial class Utilidades_Controles_Notificacion_ruvEdicionPuntoNotificacion : System.Web.UI.UserControl {

    protected void Page_Load(object sender, EventArgs e) {
        if (!Page.IsPostBack) {
            var service = new GeneralService();
            string errorMessage = string.Empty;
            this.puntoNotificacionPais.DataSource = service.ObtenerPaises(ref errorMessage);
            this.puntoNotificacionPais.DataValueField = "Id";
            this.puntoNotificacionPais.DataTextField = "Nombre";
            this.puntoNotificacionPais.DataBind();
        }
    }

}