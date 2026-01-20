using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Consultas_WebUserControl_WebUserControlConsulta : System.Web.UI.UserControl
{
    #region Events declaration

    public event OnBtnClick ButtonClick;

    #endregion
    #region Properties

    public string CNumeroCedula { get { return TxtNumeroCedula.Text.Trim(); } }
    public string CNumeroFormulario { get { return TxtNumeroFormulario.Text.Trim(); } }
    public string CPrimerApellido { get { return TxtPrimerApellido.Text.Trim(); } }
    public string CPrimerNombre { get { return TxtPrimerNombre.Text.Trim(); } }

    #endregion
    #region Protected methods

    #region Events

    /// <summary>
    /// Handles the page load event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    
    /// <summary>
    /// Handler for "Consulta" action. Displays the registries from the filtered query
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnConsulta_Click(object sender, EventArgs e)
    {
        if (ButtonClick != null)
        {
            ButtonClick(sender, e);
        }
    }

    #endregion

    #endregion
}