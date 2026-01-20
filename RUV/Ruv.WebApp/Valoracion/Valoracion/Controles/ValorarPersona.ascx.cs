using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;
using System.ComponentModel;

public partial class Valoracion_Valoracion_Controles_ValorarPersona : System.Web.UI.UserControl
{

	public event OnGuardarOkPersona GuardarOk;


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

    [DefaultValue("personaDetalleBehaviorID")]
    public string NombreInterno
    {
        get { return DetailMPopUp.BehaviorID; }
        set { DetailMPopUp.BehaviorID = value; }
    }
    

	public List<clsPersonaAnexo> Persona
	{
		set
		{
			if (value == null) return;

            if (value[0].DecretoLey == "SI")
                value[0].DecretoLey = Convert.ToString(318);

            if (value[0].DecretoLey == "NO")
                value[0].DecretoLey = Convert.ToString(319);

            dvPersonaDetalle.DataSource = value;
			dvPersonaDetalle.DataBind();

			clsPersonaAnexo per = value.First();
			Utilidades_Controles_dpsCheckBoxList chklPrincipios = (Utilidades_Controles_dpsCheckBoxList)dvPersonaDetalle.FindControl("chkLPrincipios");
			Utilidades_Controles_ruvDropDownList ddlEstado = (dvPersonaDetalle.FindControl("ddlEstado") as Utilidades_Controles_ruvDropDownList);
            //CheckBox chkObservacionEstado = (dvPersonaDetalle.FindControl("chkObservacionEstado") as CheckBox);
            Utilidades_Controles_ruvDropDownList ddlObservacionEstado = (dvPersonaDetalle.FindControl("ddlObservacionEst") as Utilidades_Controles_ruvDropDownList);
            Utilidades_Controles_ruvDropDownList ddlHechoEnmarcado = (dvPersonaDetalle.FindControl("ddlHechoEnmarcado") as Utilidades_Controles_ruvDropDownList);
            Utilidades_Controles_ruvDropDownList ddlDecretoLey = (dvPersonaDetalle.FindControl("ddlDecretoLey") as Utilidades_Controles_ruvDropDownList);
            if (per.EstadoId.HasValue)
			{
                if (per.ObservacionId.HasValue)
                {
                    ddlObservacionEstado.SelectedValue = per.ObservacionId.Value.ToString();
                }
				if (per.Principios != null)
				{
					chklPrincipios.Seleccionados = per.Principios;
				}
                if (per.HechoEnmarcadoId.HasValue)
                {
                    ddlHechoEnmarcado.SelectedValue = per.HechoEnmarcadoId.Value.ToString();
                }
			}

            if (!per.Victima && !per.Afectado) {
                ddlEstado.SelectedValue = eEstadosValoracionPersona.NoValoradoNoAfectado.GetHashCode().ToString();
                ddlEstado.Enabled = false;
            }
			else if (per.Afectado && !per.Victima)
			{
				ddlEstado.SelectedValue = eEstadosValoracionPersona.NoValoradoAfectado.GetHashCode().ToString();
				//ddlEstado_SelectIndexChange(ddlEstado.DropDownList, EventArgs.Empty);
				ddlEstado.Enabled = false;
			}

			Session[ConstantesItems.VALORACION_PERSONA_GUARDADA] = false;
			Session[ConstantesItems.VALORACION_PERSONA_ULTIMA] = per.Id;
			
			if (Session[ConstantesItems.VALORACION_REPLICA] == null)
			{
				int CantidadA13 = 0;
				CantidadA13 = Valoracion.Hechos.Count(x => x.TipoHechoId == (int)eTiposAnexos.CensoMasivo_13);

				if (CantidadA13 > 0)
				{
					mpopReplicarTodosMasivo.Mensaje = "Esta declaración es un masivo. ¿Desea que la información de la valoración de esta persona aplique para todas las personas de la declaración?";
					mpopReplicarTodosMasivo.Mostrar();
				}
				else
				{
					mpopReplicarTodosMasivo.Mensaje = "¿Desea que la información de la valoración de esta persona aplique para todas las personas del hecho victimizante?";
					mpopReplicarTodosMasivo.Mostrar();
				}
			}
			Show();
		}
	}


	public void Show()
	{
		this.DetailMPopUp.Show();
		ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>CambioEstado()</script>", false);
	}

	protected void Page_Load(object sender, EventArgs e)
	{

	}
	/// <summary>
	/// Guardar Información de la Asignacion del estado de la valoracion de la persona
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public void brnGuardar_Click(object sender, EventArgs e)
	{
		int hechoId = Convert.ToInt32(Session[ConstantesItems.VALORACION_ANEXO_ID]);
		int idPersona = Convert.ToInt32(Session[ConstantesItems.VALORACION_PERSONA_ULTIMA]);

		clsPersonaAnexo persona = Valoracion.Hechos.First(h => h.Id == hechoId).Personas.First(x => x.Id == idPersona);
		persona.Victima = (dvPersonaDetalle.FindControl("chkVictima") as CheckBox).Checked;
		persona.Afectado = (dvPersonaDetalle.FindControl("chkAfectado") as CheckBox).Checked;
		persona.AfectacionesDetectadas = (dvPersonaDetalle.FindControl("chkLAfectaciones") as Utilidades_Controles_dpsCheckBoxList).Seleccionados;

        Utilidades_Controles_ruvDropDownList ddlDecretoLey = (dvPersonaDetalle.FindControl("ddlDecretoLey") as Utilidades_Controles_ruvDropDownList);
        if (ddlDecretoLey != null)
        {
            if (ddlDecretoLey.SelectedValue != null && ddlDecretoLey.SelectedIndex > 0)
            {
                persona.DecretoLey = ddlDecretoLey.SelectedItem.ToString();
            }
            else
            {
                persona.DecretoLey = null;
            }
        }

        Utilidades_Controles_ruvDropDownList ddlestado = (dvPersonaDetalle.FindControl("ddlEstado") as Utilidades_Controles_ruvDropDownList);
		if (ddlestado.TienenValor)
		{
			persona.EstadoId = Convert.ToInt32((dvPersonaDetalle.FindControl("ddlEstado") as Utilidades_Controles_ruvDropDownList).SelectedValue);
		}
		else
		{
			persona.EstadoId = null;
		}

        Utilidades_Controles_ruvDropDownList Observacion = (dvPersonaDetalle.FindControl("ddlObservacionEst") as Utilidades_Controles_ruvDropDownList);
		if (Observacion != null && persona.EstadoId != null)
		{
            if (Observacion.SelectedValue != null && Observacion.SelectedIndex > 0)
            {
                persona.ObservacionId = Convert.ToInt32(Observacion.SelectedValue);
            }
            else
            {
                // Diego Alvarez - 25/09/2013 - No debe dejar pasar si no se ha seleccionado Observacion
                persona.ObservacionId = null;
            }
		}
		else
		{
			persona.ObservacionId = null;
		}

        Utilidades_Controles_ruvDropDownList HechoEnmarcado = (dvPersonaDetalle.FindControl("ddlHechoEnmarcado") as Utilidades_Controles_ruvDropDownList);
        if (HechoEnmarcado != null && persona.EstadoId != null)
        {
            if (HechoEnmarcado.SelectedValue != null && HechoEnmarcado.SelectedIndex > 0)
            {
                persona.HechoEnmarcadoId = Convert.ToInt32(HechoEnmarcado.SelectedValue);
            }
            else
            {
                persona.HechoEnmarcadoId = null;
            }
        }

		persona.Principios = (dvPersonaDetalle.FindControl("chkLPrincipios") as Utilidades_Controles_dpsCheckBoxList).Seleccionados;
		persona.Observacion = (dvPersonaDetalle.FindControl("txtObservacionValidacion") as Utilidades_Controles_dpsTextBox).Text;

		Session[ConstantesItems.VALORACION_PERSONA_GUARDADA] = true;
		DetailMPopUp.Hide();
		if (GuardarOk != null)
		{
			GuardarOk(null, new PersonaAnexoEventArgs(persona));
		}
	}

	/// <summary>
	/// Replicar A todas las personas existentes en la declaracion sin importar tipo de anexo
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void mpopReplicarTodosMasivo_Ok(object sender, EventArgs e)
	{
		Session[ConstantesItems.VALORACION_REPLICA] = true;
		Show();
	}

    protected void imgCerrar_Click(object sender, ImageClickEventArgs e)
    {
        Session[ConstantesItems.VALORACION_PERSONA_GUARDADA] = true;
    }
}