using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[ConstantesItems.ERROR] != null)
        {
            string error = Session[ConstantesItems.ERROR].ToString();
            txtStackTrack.Text = error;
            Varios.CerrarCession(HttpContext.Current);
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}