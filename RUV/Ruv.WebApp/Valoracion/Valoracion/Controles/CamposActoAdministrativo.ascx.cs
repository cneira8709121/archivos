using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;

public partial class Valoracion_Valoracion_Controles_CamposActoAdministrativo : System.Web.UI.UserControl
{


    /// <summary>
    /// Propiedad que guarda y obtiene de la session la valoracion actual
    /// </summary>
    public clsValoracion Valoracion
    {
        get
        {
            if (Session[ConstantesItems.VALORACION] == null)
                Session[ConstantesItems.VALORACION] = new clsValoracion();

            return (clsValoracion)Session[ConstantesItems.VALORACION];
        }
        set
        {
            Session[ConstantesItems.VALORACION] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>SeleccionTipoAA()</script>", false);
        if (!Page.IsPostBack)
        {            
            txtMotivacionInclusion.Text = Valoracion.Motivacion_Inclusion;
            txtMotivacionNoInclusion.Text = Valoracion.Motivacion_NoInclusion;
            txtResuelveArticulo1.Text = Valoracion.ResuelveArticulo1;
            txtResuelveArticulo2.Text = Valoracion.ResuelveArticulo2;
            rbtLTipoActo.SelectedValue = Valoracion.cIdTipoMotivo;
        }
            
    }



    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Valoracion.Motivacion_Inclusion = txtMotivacionInclusion.Text;
        Valoracion.Motivacion_NoInclusion = txtMotivacionNoInclusion.Text;
        Valoracion.ResuelveArticulo1 = txtResuelveArticulo1.Text;
        Valoracion.ResuelveArticulo2 = txtResuelveArticulo2.Text;
        Valoracion.cIdTipoMotivo = rbtLTipoActo.SelectedValue;
    }
}