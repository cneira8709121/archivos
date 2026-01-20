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
using System.ComponentModel;

public partial class HechoVictimizanteC : System.Web.UI.UserControl
{

    public event OnBtnClick NuevoHecho;
    public event Ruv.Infrastructure.Crosscutting.Common.Error Errores;

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


    [Bindable(true)]
    [Localizable(true)]
    public DateTime FechaDeclaracion
    {
        set
        {
            hdFechaDeclaracion.Value = value.ToString();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        txtFecha.Visible = true;
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
        LugarHecho.Departamento = 0;
        LugarHecho.Municipio = 0;
        LugarHecho.TipoEntornoId = 0;
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

        clsPersonaNuevoHecho _persona = new clsPersonaNuevoHecho();
        _persona.PersonaId = Convert.ToInt32(ddlPersonas.SelectedValue);
        _persona.Victima1 = chkVictima1.Checked;
        if (Convert.ToInt32(ddlHechosVictimizantes.SelectedValue) != (int)eHechosVictimizantes.MinasAntipersonal_7)
        {
            if (hdEstadoHecho.Value == "0" || hdEstadoHecho.Value == "")
            {
                _persona.EstadoEnHecho = 0;
               
            }
            else
            {
                _persona.EstadoEnHecho = Convert.ToInt32(hdEstadoHecho.Value);//chkEstadodelHecho.Items[0].Selected) ? 1 : 0;
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

    protected void ddlPersonas_SelectIndexChange(object sender, EventArgs e)
    {

        //if (ddlPersonas.SelectedIndex > 0)
        //{
        //    trDatosVictima.Visible = true;
        //    if (ddlHechosVictimizantes.SelectedIndex > 0)
        //    {
        //        chkEstadoHecho.Items.Clear();
        //        switch (Convert.ToInt32(ddlHechosVictimizantes.SelectedValue))
        //        {

        //            case (int)eHechosVictimizantes.DesaparicionForzada_4:
        //                dvEstadoEnHecho.Visible = true;
        //                chkEstadoHecho.Items.Add(new ListItem() { Value = "0", Text = "Se encuentra desaparecido" });
        //                break;
        //            case (int)eHechosVictimizantes.DesplazamientoForzado_5:
        //                dvEstadoEnHecho.Visible = true;
        //                chkEstadoHecho.Items.Add(new ListItem() { Value = "0", Text = "Se desplazó" });
        //                break;
        //            case (int)eHechosVictimizantes.HomicidioMasacre_6:
        //                dvEstadoEnHecho.Visible = true;
        //                chkEstadoHecho.Items.Add(new ListItem() { Value = "0", Text = "Persona fallecida" });
        //                break;
        //            case (int)eHechosVictimizantes.MinasAntipersonal_7:
        //                dvEstadoEnHecho.Visible = true;
        //                chkEstadoHecho.Valor = eTipoParametros.SituacionActualVictimaSecuestro.GetHashCode().ToString();
        //                chkEstadoHecho.Source = Poblar.Parametros;
        //                break;
        //            case (int)eHechosVictimizantes.Secuestro_8:
        //                dvEstadoEnHecho.Visible = true;
        //                chkEstadoHecho.Items.Add(new ListItem() { Value = "0", Text = "Persona secuestrada" });
        //                break;
        //            default:
        //                dvEstadoEnHecho.Visible = false;
        //                break;
        //        }
        //    }
        //}
        //else
        //{

        //    trDatosVictima.Visible = false;
        //}
        //Show();
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

        //VALIDACIONES



    }

    protected int ValorEspecificoTipoHecho(int tipohecho)
    {
        switch (tipohecho)
        {
            case (int)eHechosVictimizantes.AbandonoDespojoForzadoTierras_11: /*diferencia si es inmueble, mueble o credito*/
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
        clsDeclaracionInfoValoracion declara = HechoVictimizante.Valoracion.BasicDeclaracion.First();

        HechoVictimizante.Id = 0;
        HechoVictimizante.TipoHecho = Convert.ToInt32(ddlHechosVictimizantes.SelectedValue);
        HechoVictimizante.Fecha = txtFecha.Fecha;
        HechoVictimizante.Departamento = LugarHecho.Departamento;
        HechoVictimizante.Municipio = LugarHecho.Municipio;
        HechoVictimizante.Tipoentorno = LugarHecho.TipoEntornoId;
        HechoVictimizante.CorrLocId = null;
        HechoVictimizante.BarrVerId = null;
        HechoVictimizante.OtraLocCorrId = LugarHecho.OtroLocCorr;
        HechoVictimizante.OtroBarVerId = LugarHecho.OtroBarrioVer;
        HechoVictimizante.ValorEspecifico = ValorEspecificoTipoHecho(HechoVictimizante.TipoHecho);
        HechoVictimizante.TipoHechoOtro = (ddlHechosOtros.SelectedIndex > 0) ? (int?)Convert.ToInt32(ddlHechosOtros.SelectedValue) : null;
        if (HechoVictimizante.ValorEspecifico == (int)eTipoAnexo11.Inmueble)
        {
            HechoVictimizante.ValInmuebleAbandono = Convert.ToInt32(chkAbandono.Checked);
            HechoVictimizante.ValInmuebleDespojo = Convert.ToInt32(chkDespojo.Checked);
            if (chkDespojo.Checked)
            {
                HechoVictimizante.Fecha = TxtFechadespojo.Fecha;   
                HechoVictimizante.FechaDespojo = TxtFechadespojo.Fecha;
                if (!chkAbandono.Checked)
                    HechoVictimizante.FechaAbandono = null;
            }
            if (chkAbandono.Checked)
            {
                HechoVictimizante.Fecha = txtfechaAbandono.Fecha;
                HechoVictimizante.FechaAbandono = txtfechaAbandono.Fecha;
                if(!chkDespojo.Checked)
                    HechoVictimizante.FechaDespojo = null;
            }
            
        }
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
        //Show();
    }

    protected void LugarHecho_Cambio(object sender, EventArgs e)
    {
        Show();
    }

    protected void ddlHechosVictimizantes_SelectIndexChange(object sender, EventArgs e)
    {

    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        
        if (!string.IsNullOrEmpty(lbPersonasAnexo.SelectedValue))
        {
            int PersonaId = Convert.ToInt32(lbPersonasAnexo.SelectedValue);
            clsPersonaNuevoHecho _persona = HechoVictimizante.Personas.First(x => x.PersonaId == PersonaId);
            ddlPersonas.SelectedValue = PersonaId.ToString();
            ddlPersonas_SelectIndexChange(sender, EventArgs.Empty);

            //trDatosVictima.Visible = true;
            chkVictima1.Checked = _persona.Victima1;
            if (HechoVictimizante.TipoHecho == (int)eHechosVictimizantes.MinasAntipersonal_7)
            {
                foreach (ListItem item in chkEstadodelHecho.Items)

                {
                    if (item.Value == _persona.EstadoEnHecho.ToString())
                    {
                        item.Selected = true;
                    }
                }
            }
            else
            {
                if (chkEstadodelHecho.Items.Count > 0)
                {
                    chkEstadodelHecho.Items[0].Selected = (_persona.EstadoEnHecho == 1) ? true : false;
                }
            }
        }
        Show();
    }


}