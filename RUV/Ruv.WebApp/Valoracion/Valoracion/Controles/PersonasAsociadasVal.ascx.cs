using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.WebApp.Common;
using Ruv.WebApp.DataSources;

public partial class Valoracion_Valoracion_Controles_PersonasAsociadasVal : System.Web.UI.UserControl
{

    public int IdDeclaracion
    {
        get {
            int idDeclaracion = 0;
            if (int.TryParse(this.IdDeclaracionHidden.Value, out idDeclaracion)) 
                return idDeclaracion;

            var valoracion = HttpContext.Current.Session[ConstantesItems.VALORACION] as Ruv.Infrastructure.Crosscutting.Common.Valoracion.clsValoracion;
            if (valoracion != null)
                return valoracion.DeclaracionId;

            return 0;
        }
        set { this.IdDeclaracionHidden.Value = value.ToString(); }
    }

  
 
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void odsPersonasAsociadas_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        var datasource = e.ObjectInstance as DataSourcePersonasAsociadasDeclaracion;
        if (datasource != null) {
            datasource.nIdDeclaracion = this.IdDeclaracion;
            
        }
    }

    protected void btnAgregarPersona_Click(object sender, EventArgs e)
    {

        Response.Redirect(string.Format("AgregarPersonaValoracion.aspx?IdDeclaracion={0}", IdDeclaracion));
    }
   
}