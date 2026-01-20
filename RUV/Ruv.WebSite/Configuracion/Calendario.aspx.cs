using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Business.DTO.Feriado;
using Ruv.WebSite.Common;
using Ruv.WebSite.DataSources.Feriados;

public partial class Configuracion_Calendario : PaginaBase
{

    #region Page Event Handlers
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            var years = new List<int>();
            for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year + 10; i++) {
                years.Add(i);
            }
            this.filterAno.DataSource = years;
            this.filterAno.DataBind();

            if (Request.QSIntegerField("year") != null)
                this.filterAno.SelectedValue = Request.QSIntegerField("year").Value.ToString();
            else
                this.filterAno.SelectedValue = DateTime.Now.Year.ToString();
            Validate();
        }
    }

    #endregion

    #region Databound Event Handlers

    protected void odsFeriados_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        var dataSourceController = e.ObjectInstance as DataSourceFeriados;
        if (dataSourceController != null)
        {
            dataSourceController.Ano = Request.QSIntegerField("year") ?? DateTime.Now.Year;
        }
    }

    #endregion

    #region Action Event Handlers

    protected void btnAdicionarFestivo_Click(object sender, EventArgs e)
    {
        string cError = string.Empty;
        if (clnAdicionar.SelectedDates.Count == 1) {
            var holiday = clnAdicionar.SelectedDate;
            var id = new FeriadosService().CreacionFestivo(holiday, txtNombre.Text, txtDescripcion.Text, chkRecurrente.Checked, ref cError);

            if (string.IsNullOrEmpty(cError))
                ModalPopUp.MostrarMensajeYRedirigir("Mensaje", "Festivo adicionado exitosamente", "Calendario.aspx");
            else
                ModalPopUp.MostrarMensaje("Error", "No se pudo adicionar festivo: " + cError);
        }
        else {
            ModalPopUp.MostrarMensaje("Error", "Debe seleccionar exactamente una fecha para agregar");
        }
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        string redirectionUrl = "Calendario.aspx";

        if (this.filterAno.SelectedValue != string.Empty)
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("year", this.filterAno.SelectedValue);
        
        Response.Redirect(redirectionUrl);
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        string cError = string.Empty;
        var service = new FeriadosService();
        foreach (GridViewRow row in grdFeriados.Rows)
        {
            CheckBox checkBox = (CheckBox)row.FindControl("CheckItem") as CheckBox;
            if (checkBox.Checked)
            {
                int id = int.Parse(grdFeriados.DataKeys[row.RowIndex].Value.ToString());
                service.BorrarFestivo(id, ref cError);
            }
        }
        if (string.IsNullOrEmpty(cError))
            ModalPopUp.MostrarMensajeYRedirigir("Mensaje", "Festivos eliminados exitosamente", "Calendario.aspx");
        else
            ModalPopUp.MostrarMensaje("Error", "No se pudo eliminar los festivos seleccionados: " + cError);
    }

    protected void CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowItem in grdFeriados.Rows)
        {
            chk = (CheckBox)(rowItem.Cells[0].FindControl("CheckItem"));
            chk.Checked = ((CheckBox)sender).Checked;
        }
    }

    #endregion

}