using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;
using SIRAV.Entidades.Administracion;

public partial class Utilidades_Controles_MenuAcciones : System.Web.UI.UserControl
{
    public event OptionHandler OpcionClick;

    private string url;

    public string Url
    {
        get { return url; }
        set { url = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //CargarMenus(itemsMenu);
        }
    }
    
    public void CargarMenus(List<MENU> itemsMenu)
    {
        foreach (MENU m in itemsMenu)
        {
            TableCell tblCell = new TableCell();        
            ImageButton btnItem = new ImageButton();
            btnItem.ID = m.NOMBRE;
            btnItem.ToolTip = m.NOMBRE;
            btnItem.ImageUrl = m.IMAGEN;
            btnItem.CssClass = "imgPequeñaMenu";
            btnItem.OnClientClick = m.CLIENT_SCRIPT;
            btnItem.CausesValidation = m.CAUSA_VALIDACION.Value;
            //if (m.ServerCode.Value)
            btnItem.Click += new ImageClickEventHandler(btnItem_Click);
            tblCell.Controls.Add(btnItem);
            tblRow.Cells.Add(tblCell);
        }
    }


    public void QuitarMenu(string[] menus)
    {
        foreach (string item in menus)
        {
            foreach (TableCell cell in tblRow.Cells)
            {
                cell.Controls.Remove(cell.FindControl(item));
            }
        }
    }

    void btnItem_Click(object sender, ImageClickEventArgs e)
    {
        OnOptionClick(this, new OptionEventArgs(((ImageButton)sender).ID));
    }

    void OnOptionClick(object sender, OptionEventArgs e)
    {
        if (OpcionClick != null)
        {
            OpcionClick(sender, e);
        }
    }


}
