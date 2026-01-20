using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using Ruv.Infrastructure.Crosscutting.Utilities;
using System.Globalization;

public partial class Utilidades_Controles_dpsHechoVictimizante : System.Web.UI.UserControl
{

    public event OnBtnClick NuevoHecho;
    public event Error Errores;
    
    public clsHecho HechoVictimizante
    {
        get
        {
            if (Session[ConstantesItems.HECHO] == null)
                Session[ConstantesItems.HECHO] = new clsHecho();

            return (clsHecho)Session[ConstantesItems.HECHO];
        }
        set
        {
            Session[ConstantesItems.HECHO] = value;
            
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void Show()
    {
        this.mpopUpNHecho.Show();
    }

    public void Hide()
    {
        this.mpopUpNHecho.Hide();
    }

    protected void btnCancelarClick(object sender, EventArgs e)
    {
        pnlNuevoHecho.Enabled = false;
        mpopUpNHecho.Hide();
    }

    public void LimpiarHechoVictimizante()
    {
        // Reiniciar el objeto hecho victimizante
        HechoVictimizante = null;
        // Limpiar los controles
        ddlHechosVictimizantes.SelectedIndex = 0;
        txtFecha.Text = string.Empty;
        LugarHecho.DepartamentoId = 0;
        LugarHecho.MunicipioId = 0;
        LugarHecho.TipoEntornoId = 0;
        LugarHecho.LocCorreId = 0;
        LugarHecho.BarrioVerId = 0;
        LugarHecho.OtroLocCorr = string.Empty;
        LugarHecho.OtroBarrioVer = string.Empty;
        // Recargar las persons que se pueden seleccionar

    }

    public void InicializarHechoVictimizante()
    {
        // Recargar las persons que se pueden seleccionar
        ddlPersonas.Items.Clear();
        ddlPersonas.DataSource = HechoVictimizante.Valoracion.PersonasDeclaracion;
        ddlPersonas.DataBind();
    }

    protected void tbnAgregar_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlPersonas.SelectedIndex > 0)
        {

            int PersonaId = Convert.ToInt32(ddlPersonas.SelectedValue);

            if (HechoVictimizante.Personas == null)
            {
                HechoVictimizante.Personas = new List<clsPersonaNuevoHecho>();
            }
            if (!HechoVictimizante.Personas.Exists(x => x.PersonaId == PersonaId))
            {
                AgregarPersona(PersonaId);
            }
            else
            {
                HechoVictimizante.Personas.Remove(HechoVictimizante.Personas.First(x => x.PersonaId == PersonaId));
                lbPersonasAnexo.Items.Remove(lbPersonasAnexo.Items.FindByValue(PersonaId.ToString()));
                AgregarPersona(PersonaId);
            }
        }
        Show();
    }

    private void AgregarPersona(int PersonaId)
    {
        if (chkVictima1.Checked && HechoVictimizante.Personas.Exists(x => x.Victima1))
        {
            lblMensajeValidación.Text = "Ya hay una persona marcada como victima 1";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>Mensaje(true, 'dvMensajeValidacionHecho')</script>", false);
            return;
        }
        else
        {
            if (ddlPersonas.SelectedValue == null)
            {
                lblMensajeValidación.Text = "Seleccione una persona";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>Mensaje(true, 'dvMensajeValidacionHecho')</script>", false);
                return;
            }
            if (ddlHechosVictimizantes.SelectedValue == null)
            {
                lblMensajeValidación.Text = "Seleccione un hecho victimizante";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>Mensaje(true, 'dvMensajeValidacionHecho')</script>", false);
                return;
            }
            clsPersonaNuevoHecho _persona = new clsPersonaNuevoHecho();
            _persona.PersonaId = Convert.ToInt32(ddlPersonas.SelectedValue);
            _persona.Victima1 = chkVictima1.Checked;
            if (Convert.ToInt32(ddlHechosVictimizantes.SelectedValue) != (int)eHechosVictimizantes.MinasAntipersonal_7)
            {
                if (chkEstadoHecho.Items.Count > 0)
                {
                    _persona.EstadoEnHecho = (chkEstadoHecho.Items[0].Selected) ? 1 : 0;
                }
                else
                {
                    _persona.EstadoEnHecho = 0;
                }
            }

            HechoVictimizante.Personas.Add(_persona);
            if (chkVictima1.Checked)
            {
                ListItem li = new ListItem();
                li.Value = PersonaId.ToString();
                li.Text = ddlPersonas.SelectedItem.Text + " (Victima 1)";
                lbPersonasAnexo.Items.Add(li);
            }
            else
            {
                ListItem li = new ListItem();
                li.Value = PersonaId.ToString();
                li.Text = ddlPersonas.SelectedItem.Text;
                lbPersonasAnexo.Items.Add(li);
            }
        }
        Show();
    }

    protected void ddlPersonas_SelectIndexChange(object sender, EventArgs e)
    {
        
        if (ddlPersonas.SelectedIndex > 0)
        {
            trDatosVictima.Visible = true;
            if (ddlHechosVictimizantes.SelectedIndex > 0)
            {
                chkEstadoHecho.Items.Clear();
                switch (Convert.ToInt32(ddlHechosVictimizantes.SelectedValue))
                {

                    case (int)eHechosVictimizantes.DesaparicionForzada_4:
                        dvEstadoEnHecho.Visible = true;
                        chkEstadoHecho.Items.Add(new ListItem() { Value = "0", Text = "Se encuentra desaparecido" });
                        break;
                    case (int)eHechosVictimizantes.DesplazamientoForzado_5:
                        dvEstadoEnHecho.Visible = true;
                        chkEstadoHecho.Items.Add(new ListItem() { Value = "0", Text = "Se desplazó" });
                        break;
                    case (int)eHechosVictimizantes.HomicidioMasacre_6:
                        dvEstadoEnHecho.Visible = true;
                        chkEstadoHecho.Items.Add(new ListItem() { Value = "0", Text = "Persona fallecida" });
                        break;
                    case (int)eHechosVictimizantes.MinasAntipersonal_7:
                        dvEstadoEnHecho.Visible = true;
                        chkEstadoHecho.Valor = eTipoParametros.SituacionActualVictimaSecuestro.GetHashCode().ToString();
                        chkEstadoHecho.Source = Poblar.Parametros;
                        break;
                    case (int)eHechosVictimizantes.Secuestro_8:
                        dvEstadoEnHecho.Visible = true;
                        chkEstadoHecho.Items.Add(new ListItem() { Value = "0", Text = "Persona secuestrada" });
                        break;
                    default:
                        dvEstadoEnHecho.Visible = false;
                        break;
                }
            }
        }
        else
        {

            trDatosVictima.Visible = false;
        }
        Show();
    }

    protected void btnRemover_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(lbPersonasAnexo.SelectedValue))
        {
            int PersonaId = Convert.ToInt32(lbPersonasAnexo.SelectedValue);
            if (HechoVictimizante.Personas.Exists(x => x.PersonaId == PersonaId))
            {
                HechoVictimizante.Personas.Remove(HechoVictimizante.Personas.First(x => x.PersonaId == PersonaId));
                lbPersonasAnexo.Items.RemoveAt(lbPersonasAnexo.SelectedIndex);
            }
        }
        Show();
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        clsDeclaracionInfoValoracion declara = HechoVictimizante.Valoracion.BasicDeclaracion.First();

        HechoVictimizante.Id = 0;
        HechoVictimizante.TipoHecho = Convert.ToInt32(ddlHechosVictimizantes.SelectedValue);
        HechoVictimizante.Fecha = txtFecha.Fecha;
        HechoVictimizante.Departamento = LugarHecho.DepartamentoId;
        HechoVictimizante.Municipio = LugarHecho.MunicipioId;
        HechoVictimizante.Tipoentorno = LugarHecho.TipoEntornoId;
        HechoVictimizante.CorrLocId = LugarHecho.LocCorreId;
        HechoVictimizante.BarrVerId = LugarHecho.BarrioVerId;
        HechoVictimizante.OtraLocCorrId = LugarHecho.OtroLocCorr;
        HechoVictimizante.OtroBarVerId = LugarHecho.OtroBarrioVer;
        HechoVictimizante.ValorEspecifico = ValorEspecificoTipoHecho(HechoVictimizante.TipoHecho);
        if (HechoVictimizante.Fecha != DateTime.MinValue) 
        {
            if (HechoVictimizante.Fecha < declara.FechaRadicado)
            {
                if (HechoVictimizante.Personas != null && HechoVictimizante.Personas.Count > 0)
                {
                    if (HechoVictimizante.Municipio == 0)
                    {
                        lblMensajeValidación.Text = "Indique la información de lugar de ocurrencia del hecho";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>Mensaje(true, 'dvMensajeValidacionHecho')</script>", false);
                        Show();
                    }
                    else if (HechoVictimizante.Personas.Count(x => x.Victima1) > 0)
                    {
                        Show();
                        mpopUpNuevoHecho.Mostrar();
                    }
                    else
                    {
                        lblMensajeValidación.Text = "Indique la victima 1";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>Mensaje(true, 'dvMensajeValidacionHecho')</script>", false);
                        Show();
                    }
                }
                else
                {
                    lblMensajeValidación.Text = "Seleccione almenos una persona para el hecho victimizante";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>Mensaje(true, 'dvMensajeValidacionHecho')</script>", false);
                    Show();
                }
            }
            else
            {
                lblMensajeValidación.Text = "La fecha del nuevo hecho victimizante no puede ser mayor a la fecha de la declaración";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>Mensaje(true, 'dvMensajeValidacionHecho')</script>", false);
                Show();
            }
        }
        else
        {
            lblMensajeValidación.Text = "Debe haber fecha para el nuevo hecho victimizante";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>Mensaje(true, 'dvMensajeValidacionHecho')</script>", false);
            Show();
        }
    }

    protected int ValorEspecificoTipoHecho(int tipohecho)
    {
        switch (tipohecho)
        {
            case (int)eHechosVictimizantes.AbandonoDespojoForzadoTierras_11: /*difernecia si es inmueble, mueble o credito*/
                if (rbInmueble.Checked)
                {
                    return (int)eTipoAnexo11.Inmueble;
                }
                else
                {
                    if (rbMueble.Checked)
                    {
                        return (int)eTipoAnexo11.Mueble;    
                    }
                    else
                    {
                        return (int)eTipoAnexo11.Credito;
                    }
                }
            default:
                return 0;
        }
    }

    protected void mpopUpNuevoHecho_Ok(object sender, EventArgs e)
    {

        //string sMensaje = "Los Cambios se Realizaron Con exito";
        ValoracionService objValoracion = new ValoracionService();
        string resultado = objValoracion.NuevoHecho(HechoVictimizante);

        var formulario = this.Page as IFormularioGuardar;
        if (formulario != null)
        {
            //formulario.ShowMessage(sMensaje);
            formulario.Guardar(eEstadosValoracion.IniciaValoracion);
        }
        
        // Limpiar();
        if (!string.IsNullOrEmpty(resultado))
        {
            Hide();
            Errores(sender, new ErrorEventArgs(resultado));
        }
        else
        {
            Hide();
            LimpiarHechoVictimizante();
            NuevoHecho(sender, EventArgs.Empty);
            //formulario.ShowMessage(sMensaje);
        }
    }

    protected void mpopUpNuevoHecho_Cancel(object sender, EventArgs e)
    {
        Show();
    }

    protected void LugarHecho_Cambio(object sender, EventArgs e)
    {
        Show();
    }

    protected void ddlHechosVictimizantes_SelectIndexChange(object sender, EventArgs e)
    {
        ddlPersonas.SelectedIndex = 0;
        chkVictima1.Checked = false;
        chkEstadoHecho.Items.Clear();
        trDatosVictima.Visible = false;
        lblAn11.Visible = false;
        if (ddlHechosVictimizantes.SelectedValue == ((int)eHechosVictimizantes.AbandonoDespojoForzadoTierras_11).ToString())
        {
            lblAn11.Visible = true;
        }
        Show();
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(lbPersonasAnexo.SelectedValue))
        {
            int PersonaId = Convert.ToInt32(lbPersonasAnexo.SelectedValue);
            clsPersonaNuevoHecho _persona = HechoVictimizante.Personas.First(x => x.PersonaId == PersonaId);
            ddlPersonas.SelectedValue = PersonaId.ToString();
            ddlPersonas_SelectIndexChange(sender, EventArgs.Empty);

            trDatosVictima.Visible = true;
            chkVictima1.Checked = _persona.Victima1;
            if (HechoVictimizante.TipoHecho == (int)eHechosVictimizantes.MinasAntipersonal_7)
            {
                foreach (ListItem item in chkEstadoHecho.Items)
                {
                    if (item.Value == _persona.EstadoEnHecho.ToString())
                    {
                        item.Selected = true;
                    }
                }
            }
            else
            {
                if (chkEstadoHecho.Items.Count > 0)
                {
                    chkEstadoHecho.Items[0].Selected = (_persona.EstadoEnHecho == 1) ? true : false;
                }
            }
        }
        Show();
    }
  
}