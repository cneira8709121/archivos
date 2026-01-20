using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Business.DTO.Valoracion;
using System.Web.Services;

public partial class Valoracion_Valoracion_Controles_ValoracionHistoricoPopUp : System.Web.UI.UserControl
{

    #region Atributos

    public int nIdValoracion { get; set; }

    public int cantidadRegistrosHistorial { get; set; }

    // Diego Alvarez - 15/11/2013 - Delegado y Evento para deshabilitar el botón de mostrar historial de la página padre
    public delegate void PageRenderEventHandler(object sender, EventArgs e);
    public event PageRenderEventHandler OnUserControlPageRendered;

    #endregion

    public Valoracion_Valoracion_Controles_ValoracionHistoricoPopUp()
    {
        // Diego Alvarez - 15/11/2013 - Se inicializa el evento
        this.PreRender += new EventHandler(OnPageRendered);
    }

    /// <summary>
    /// // Diego Alvarez - 15/11/2013 - Método que se ejecuta en el PreRender del control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnPageRendered(object sender, EventArgs e)
    {
        if (OnUserControlPageRendered != null)
            OnUserControlPageRendered(this, e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cantidadRegistrosHistorial = consultarHistorial(this.nIdValoracion);
            consultarMotivacion();
        }
    }

    public int consultarHistorial(int nIdValoracion)    
    {
        LiderValoracionService service = new LiderValoracionService();
        string cError = string.Empty;
        List<clsValoracionHistorico> historicosList = service.consultarValoracionHistorico(nIdValoracion, ref cError);
        grdHistorico.DataSource = historicosList;
        grdHistorico.DataBind();
        if (historicosList == null)
        {
            return 0;
        }
        else
        {
            return historicosList.Count;
        }
    }

    public void consultarMotivacion() {
        LiderValoracionService service = new LiderValoracionService();
        string cError = string.Empty;
        string motivacion = service.consultarMotivacionValoracionHistorico(this.nIdValoracion, ref cError);
        if (string.IsNullOrEmpty(cError))
            lblMotivacion.Text = motivacion;
    }

}