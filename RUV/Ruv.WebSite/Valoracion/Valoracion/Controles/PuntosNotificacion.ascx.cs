using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;
using System.Web.Services;


public partial class Valoracion_Valoracion_Controles_PuntosNotificacion : System.Web.UI.UserControl
{
    #region Propiedades

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

    #endregion Propiedades

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {        

        }

        if (this.Page is IFormularioGuardar)
        {
            //if (Valoracion != null && Valoracion.Id_EntidadMunicipio > 0) {
            //    //ruvDdlEntidadMunicipio.SelectedValue = Valoracion.Id_EntidadMunicipio.ToString();
            //}
        }

    }

    //protected void btnGuardar_Click(object sender, EventArgs e)
    //{
    //    Valoracion.DireccionTerritorialId = int.Parse(ruvDdlDireccionesTerritoriales.SelectedValue);
    //    Valoracion.PuntoNotificacionID = int.Parse(ruvDdlPuntosNotificacion.SelectedValue);
    //    //Valoracion.PuntoNotificacionID = int.Parse(ddlPuntosNotificacion.SelectedValue);
    //}

    //protected void ddlDireccionesTerritoriales_SelectIndexChange(object sender, EventArgs e)
    //{
    //    ruvDdlPuntosNotificacion.Items.Add("nuevo");
    //}

    #endregion Eventos

    #region Funciones

    //[WebMethod]
    //public static List<clsPuntoNotificacion> ObtenerPuntosNotificacionPorIdDirTerritorial(string strIdDirTerritorial)
    //{
    //    NotificacionService serv = new NotificacionService();
    //    string cError = string.Empty;

    //    IList<clsPuntoNotificacion> lstPuntos = serv.ObtenerPuntosNotificacionPorIdDirTerritorial(int.Parse(strIdDirTerritorial), ref cError);

    //    if (lstPuntos != null && lstPuntos.Count > 0)
    //        return lstPuntos.ToList();
    //    else
    //        return null;
    //}

    //[WebMethod]
    //public static List<string> ObtenerPuntosNotificacionPorIdDir(string strIdDirTerritorial)
    //{
    //    List<string> lista = new List<string>();
    //    lista.Add("primero");
    //    lista.Add("segundo");

    //    return lista;
    //}

    //[WebMethod]
    //public static void Guardar(string strIdDirTerritorial, string strIdPuntosNotificacion)
    //{
    //    //Valoracion.DireccionTerritorialId = int.Parse(strIdDirTerritorial);
    //    //Valoracion.PuntoNotificacionID = int.Parse(strIdPuntosNotificacion);
    //}

    //[WebMethod]
    //public static string Test()
    //{
    //    return DateTime.Now.ToShortDateString();
    //}

    #endregion
}