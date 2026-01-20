using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class _Descargar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Arch = string.Empty;
        if (Request.QueryString["Arch"] != null)
        {
            Arch = Request.QueryString["Arch"].ToString();
        }
        byte[] archivo = null;
        if (Session["Arch"] != null) {
            archivo = (byte[])Session["Arch"];
        }
        else if (Session["Arch2"] != null) {
            archivo = (byte[])Session["Arch2"];
        }
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Type", "application/force-download");
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Arch));
        //Response.Headers.Add("Content-disposition", string.Format("attachment; filename=\"{0}\"", Request.Browser.Browser == "IE" ? HttpUtility.UrlPathEncode(Arch) : Arch));
        if (archivo != null)
        {
            Response.BinaryWrite(archivo);
            archivo = null;
            Session["Arch"] = null;
        }
        else
        {
            Response.WriteFile(Arch);
        }

        Response.Flush();
        Response.ClearContent();
        Response.Clear();
        Response.End();
    }
}
