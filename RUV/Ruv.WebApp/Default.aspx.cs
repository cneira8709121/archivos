using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Ruv.Infrastructure.Crosscutting.Common;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.Xml;
using SIRAV.Entidades.Administracion;

public partial class _Default : PaginaBase
{
    //protected ASP.UCTarea UCTarea;

    //private int IntCantidad;

    //private static string strFilro
    //{
    //    get
    //    {
    //        return UCListaTareas1.IntCantidad.ToString();
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            List<MENU> menus = Master.ItemMenu;
            //if (menus.Exists(x => x.ID.Contains("120301"))) Response.Redirect("~/Valoracion/Asignacion/AsignarValoraciones.aspx");
            //if (menus.Exists(x => x.ID.Contains("120302"))) Response.Redirect("~/Valoracion/Asignacion/ReasignarValoraciones.aspx");
            if (menus.Exists(x => x.ID.Contains("120303"))) Response.Redirect("~/Valoracion/Valoracion/Default.aspx");
            //if (menus.Exists(x => x.ID.Contains("120401"))) Response.Redirect("~/Consultas/ConsultaPersona.aspx");
            //if (menus.Exists(x => x.ID.Contains("120501"))) Response.Redirect("~/Correcciones/ConsultaPersona.aspx");

            /*

            if (RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.Ingresa_Valoracion)
                || RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.Reasignar_Valoracion)
                || RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.Valoracion)
                || RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.Valorar_Declaracion)
                || RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.Asignar)
                || RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.Solicitar_Correcion))
            {
                UCListaTareas1.Visible = false;
                if (RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.Valoracion))
                    Response.Redirect("~/Valoracion/Valoracion/Default.aspx");
            }
            if (RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.Notificaciones)) Response.Redirect("~/Notificaciones/ConsultarNotificaciones.aspx");
            if (RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.Consultar_Persona)) Response.Redirect("~/Consultas/ConsultaPersona.aspx");
            if (RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.Solicitar_Correcion)) Response.Redirect("~/Correcciones/ConsultaPersona.aspx");
            if (RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.NotificaiconesEntregadas)) Response.Redirect("~/Notificaciones/NotificacionesEntregadas.aspx");
            if (RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.NotificacionesEntregadas)) Response.Redirect("~/Notificaciones/NotificacionesEntregadas.aspx");
            if (RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.PreparadorNotificaciones)) Response.Redirect("~/Notificaciones/PrepararNotificaciones.aspx");*/
        }

        //IntCantidad = UCListaTareas1.IntCantidad;
    }

    [WebMethod]
    public static string Adicionar(string controlName, int count, string strFilter, string strOrder)
    {
        return RenderControl(controlName, count, strFilter, strOrder);
    }

    public static string RenderControl(string controlName, int count, string strFilter, string strOrder)
    {
        try
        {
            //StringWriter myWriter = new StringWriter();
            //HttpUtility.HtmlDecode(strFilter, myWriter);
            //String test = myWriter.ToString();

            //XmlDocument xmlDoc = new XmlDocument();
            //XmlText xmlStrFilter;
            //xmlStrFilter = xmlDoc.CreateTextNode(strFilter);

            Page page = new Page();
            HtmlForm form = new HtmlForm();

            //Ajuste no muy elegante, para poner las comillas simples en su lugar
            strFilter = strFilter.Replace("char(39)", "'");

            DataSourceListaTareas DSListaTareas = new DataSourceListaTareas(strFilter);
            //int intCantidad = DSListaTareas.CantidadTareas();
            int intStarRow = count;
            int intPageSize = 28;
            List<clsListaTareas> listaTareas = DSListaTareas.ObtenerListaTareas(intStarRow, intPageSize, strOrder);
            //ASP.UCTarea UCTarea;
            foreach (clsListaTareas tarea in listaTareas)
            {
                //UCTarea = (ASP.UCTarea)page.LoadControl(controlName);
                var UCTarea = page.LoadControl(controlName) as Ruv.WebApp.Utilidades.Controles.IUCTarea;
                UCTarea.ID = "UC" + tarea.Declaracion.ToString(); //Garantiza que cada uc de tarea tenga un ID único
                UCTarea.Formulario = tarea.Formulario;
                UCTarea.Estado = tarea.Accion;
                UCTarea.Fecha = tarea.FechaLlegada;
                UCTarea.IdDeclaracion = tarea.Declaracion;
                UCTarea.IdCorreccion = (tarea.Correccion == null) ? null : (int?)tarea.Correccion;
                //((Control)UCTarea).EnableViewState = false;

                form.Controls.Add(UCTarea as Control);
                form.EnableViewState = false;
                page.Controls.Add(form);
            }

            StringWriter textWriter = new StringWriter();
            HttpContext.Current.Server.Execute(page, textWriter, false);
            return textWriter.ToString();
        }
        catch (Exception ex)
        {
            RegistroTraza.I.Registrar(ex);
            return ex.ToString();
        }
    }

    //private static string CleanHTML(string html)
    //{
    //    StringWriter textWriter = new StringWriter();
    //    System.Net.WebUtility.HtmlDecode(html, textWriter);
    //    //HtmlTextWriter doc = new HtmlTextWriter(textWriter);
    //    HtmlDocument doc = new HtmlDocument();
    //    doc.LoadHtml(textWriter.ToString());
    //    var viewstate = doc.DocumentNode.SelectSingleNode("//input[@id='__VIEWSTATE']");
    //    viewstate.Remove();
    //    var eventvalidation = doc.DocumentNode.SelectSingleNode("//input[@id='__EVENTVALIDATION']");
    //    eventvalidation.Remove();
    //    var form = doc.DocumentNode.SelectSingleNode("form");
    //    form.Attributes.RemoveAll();
    //    form.Name = "div";

    //    string s = doc.DocumentNode.OuterHtml;
    //    return s;
    //}
}