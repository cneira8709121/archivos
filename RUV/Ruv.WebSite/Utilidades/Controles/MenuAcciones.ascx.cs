using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;

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
    
    public void CargarMenus(List<Permisos> itemsMenu)
    {
        foreach (Permisos m in itemsMenu)
        {
            TableCell tblCell = new TableCell();        
            ImageButton btnItem = new ImageButton();
            btnItem.ID = m.Nombre;
            btnItem.ToolTip = m.Nombre;
            btnItem.ImageUrl = m.Imagen;
            btnItem.CssClass = "imgPequeñaMenu";
            btnItem.OnClientClick = m.ClientScript;
            btnItem.CausesValidation = m.CausaValidacion;
            if (m.ServerCode.Value)
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
