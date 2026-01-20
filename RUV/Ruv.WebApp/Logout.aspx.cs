using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        Varios.CerrarCession(context);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static void CerrarSesion()
    {
        if (HttpContext.Current != null)
        {
            HttpContext context = HttpContext.Current;
            Varios.CerrarCession(context);
        }
    }

}