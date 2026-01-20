using System;
using System.Web.UI;

public partial class Utilidades_Controles_Notificacion_ruvEdicionDireccionCorrespondencia : System.Web.UI.UserControl {

    protected void Page_Load(object sender, EventArgs e) {
        if (!Page.IsPostBack) {
            var service = new GeneralService();
            string errorMessage = string.Empty;
            this.direccionCorrespondenciaPais.DataSource = service.ObtenerPaises(ref errorMessage);
            this.direccionCorrespondenciaPais.DataValueField = "Id";
            this.direccionCorrespondenciaPais.DataTextField = "Nombre";
            this.direccionCorrespondenciaPais.DataBind();
        }
    }

}