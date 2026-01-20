using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.WebApp.Common;
using System.Configuration;
using SIRAV.Entidades.Administracion;
using Ruv.WebApp.New_Join_SIRAV.Services;

public partial class Valoracion_Valoracion_Nueva : PaginaBase, IFormularioGuardar
{

    #region QueryString Parameters

    #endregion

    #region Control de Valores de Sesión

    public clsValoracion ObtenerValoracionActual()
    {
        var valoracionActual = Session[ConstantesItems.VALORACION] as clsValoracion;

        if (valoracionActual == null)
        {
            CargarInfoDeclaracion();
            valoracionActual = Session[ConstantesItems.VALORACION] as clsValoracion;
            RegistroTraza.I.Registrar(this.GetType().Name + ":::ObtenerValoracionActual::: Valoración" + valoracionActual.ToString());
            ClientScript.RegisterStartupScript(typeof(Page), "ValoracionProperty", "window.alert('Se ha perdido la información de sesión. Por favor revise de nuevo los cambios realizados.');");
        }

        return valoracionActual;
    }

    public void EstablecerValoracionActual(clsValoracion value)
    {
        Session[ConstantesItems.VALORACION] = value;
    }

    #endregion

    #region Eventos

    /// <summary>
    /// Al cargar la pagina valida permisos y carga datos basicos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.UrlCurrenPage = Request.Url.AbsolutePath;
        Master.ValidarPermisoPagina();
        Master.CargarOpcionesporUrl();
        Master.OnOptionClick += new OptionHandler(Master_OnOptionClick);

        if (!Page.IsPostBack)
        {
            LimpiarSessiones();
            CargarInfoDeclaracion();
            (dvBasicaInfor.FindControl("txtFechaValoracion") as Utilidades_Controles_dpsTextCalendar).FechaInicio = DateTime.Today;

        }

    }

    /// <summary>
    /// Evento al seleccionar una opción en la pagina
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Master_OnOptionClick(object sender, OptionEventArgs e)
    {
        switch (e.ControlName)
        {
            // Diego Alvarez - 15/10/2013 - Se debe guardar la declaración antes de ingresar una nueva persona
            case "Personas Asociadas a la Declaracion":
                //Thread guardar = new Thread(GuardarParaAgregarPersonas);
                //guardar.Start();
                //Se modifica el guardado para que lo haga antes de continuar el ingreso de la persona, ya no se hace en otro hilo
                if (Guardar(eEstadosValoracion.IniciaValoracion))
                {
                    ModalPopupExtender modal = (ModalPopupExtender)PersonasAsociadas.FindControl("mpopUpPersonasAsociadas");
                    modal.Show();
                }

                break;
            case "Guardar":
                if (Guardar(eEstadosValoracion.IniciaValoracion))
                {
                    Master.PopUpGeneral.Mensaje = "Se ha guardado correctamente la información";
                    Master.PopUpGeneral.MostrarImagen = false;
                    Master.PopUpGeneral.MostrarBotones = true;
                    Master.PopUpGeneral.VisibleBotonCancelar = false;
                    Master.PopUpGeneral.Mostrar();
                }
                break;
            case "Ver Declaración":
                DescargarDocumento();
                break;
            case "Nuevo Hecho Victimizante":
                hvNuevo.Show();
                break;
            case "Atras":
                IrAtras();
                break;
            default:
                break;
        }
    }




    public void BuscarPersona(object sender, EventArgs e)
    {
        Guardar(eEstadosValoracion.PendientePorValorar, false);
        CargarInfoDeclaracion();
        clsValoracion actual = ObtenerValoracionActual();

        string original = Newtonsoft.Json.JsonConvert.SerializeObject(actual);

        List<clsHechosValoracion> hechosEncontrados = new List<clsHechosValoracion>();
        if (!string.IsNullOrEmpty(txtBuscar.Value))
        {
            foreach (var hecho in actual.Hechos)
            {
                List<clsPersonaAnexo> personaHechoTotal = hecho.Personas;
                clsHechosValoracion hechoActual = new clsHechosValoracion();
                var personasEncontradas = hecho.Personas.Where(x => x.NumeroDocumento == txtBuscar.Value).ToList();
                List<clsPersonaAnexo> personaHecho = new List<clsPersonaAnexo>();
                if (personasEncontradas.Count > 0)
                {
                    foreach (var item in personasEncontradas)
                    {
                        personaHecho.Add(item);
                    }
                    hechoActual = hecho;
                    hechoActual.Personas = personaHecho;
                    hechosEncontrados.Add(hechoActual);
                }
            }
            acHechos.DataSource = hechosEncontrados;
            acHechos.DataBind();
            LogicaMostrarColumnas(hechosEncontrados);
        }
        else
        {
            acHechos.DataSource = actual.Hechos;
            acHechos.DataBind();
            LogicaMostrarColumnas(actual.Hechos);
        }

        actual = Newtonsoft.Json.JsonConvert.DeserializeObject<clsValoracion>(original);
        EstablecerValoracionActual(actual);
    }



    /// <summary>
    /// Evento cuando se selecciona una persona para valorar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPersonasAnexos_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int hechoactual = Convert.ToInt32(gvHechos.DataKeys[(((sender as GridView).Parent.Parent) as GridViewRow).DataItemIndex].Value);
        int hechoactual = Convert.ToInt32(((((sender as GridView).Parent.Parent) as AccordionPane).FindControl("hfHechoId") as HiddenField).Value);
        int ultimohecho = Convert.ToInt32(Session[ConstantesItems.VALORACION_ANEXO_ID]);
        int ultimapersona = Convert.ToInt32(Session[ConstantesItems.VALORACION_PERSONA_ULTIMA]);
        int actualpersona = Convert.ToInt32((sender as GridView).SelectedDataKey.Value);
        if (!Convert.ToBoolean(Session[ConstantesItems.VALORACION_PERSONA_GUARDADA]))
        {
            if ((ultimohecho == hechoactual && actualpersona != ultimapersona) || (ultimohecho != hechoactual))
            {
                Session[ConstantesItems.VALORACION_ANEXO_ID] = hechoactual;
                Session[ConstantesItems.VALORACION_PERSONA_GRILLA] = (sender as GridView);
                mpoup.Mensaje = "Esta seguro de continuar sin guardar la informacion de la persona";
                mpoup.Mostrar();
            }
            else
            {
                personasDetalle.Show();
            }
        }
        else
        {
            if ((ultimohecho == hechoactual && actualpersona != ultimapersona) || (ultimohecho != hechoactual))
            {
                Session[ConstantesItems.VALORACION_ANEXO_ID] = hechoactual;
                Session[ConstantesItems.VALORACION_PERSONA_GRILLA] = (sender as GridView);
            }
            CapturarPersona(sender);
        }

    }

    /// <summary>
    /// Cuando se selecciona aceptar en la pregunta antes de ingresar a valorar persona
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void mpoup_Ok(object sender, EventArgs e)
    {
        CapturarPersona(Session[ConstantesItems.VALORACION_PERSONA_GRILLA]);
    }

    /// <summary>
    /// Cuando se selecciona cancelar en la pregunta antes de ingresar a valorar persona
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void mpoup_Cancel(object sender, EventArgs e)
    {
        (Session[ConstantesItems.VALORACION_PERSONA_GRILLA] as GridView).SelectedIndex = Convert.ToInt32(Session[ConstantesItems.VALORACION_PERSONA_ULTIMA]);
    }

    /// <summary>
    /// Agregar autor a todas las personas del hecho victimizante
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnAgregarATodosAutor_Click(object sender, EventArgs e)
    {
        GridView gvPersona = ((sender as ImageButton).Parent.Parent.Parent.Parent as GridView);
        Utilidades_Controles_ruvDropDownList ddlautor = ((sender as ImageButton).Parent.FindControl("ddlAutores") as Utilidades_Controles_ruvDropDownList);
        var valoracionActual = ObtenerValoracionActual();

        foreach (GridViewRow row in gvPersona.Rows)
        {
            AccordionPane rowHecho = row.Parent.Parent.Parent.Parent as AccordionPane;
            int hecho = Convert.ToInt32((rowHecho.FindControl("hfHechoId") as HiddenField).Value);
            int persona = Convert.ToInt32(gvPersona.DataKeys[row.DataItemIndex].Value);

            if (ddlautor.SelectedValue != null && ddlautor.SelectedIndex > 0)
            {
                ListItem li = new ListItem();
                int valor = Convert.ToInt32(ddlautor.SelectedValue);
                li.Value = valor.ToString();
                li.Text = ddlautor.SelectedItem.Text;
                clsPersonaAnexo _persona = new clsPersonaAnexo();
                _persona = valoracionActual.Hechos.First(z => z.Id == hecho).Personas.First(x => x.Id == persona);

                clsAutores autor = new clsAutores();
                autor.Id = valor;
                if (_persona.Autores != null)
                {
                    if (!_persona.Autores.Exists(x => x.Id == valor))
                    {
                        (row.FindControl("lbxAutores") as Utilidades_Controles_dpsListBox).Items.Add(li);
                        _persona.Autores.Add(autor);
                    }
                }
                else
                {
                    (row.FindControl("lbxAutores") as Utilidades_Controles_dpsListBox).Items.Add(li);
                    List<clsAutores> autores = new List<clsAutores>();
                    autores.Add(autor);
                    _persona.Autores = autores;
                }
            }
        }
        EstablecerValoracionActual(valoracionActual);
    }

    /// <summary>
    /// Quitar un autor de la lista, individual
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnQuitarAutor_Click(object sender, EventArgs e)
    {
        //GridViewRow gvrHecho = ((sender as ImageButton).Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow);
        AccordionPane gvrHecho = ((sender as ImageButton).Parent.Parent.Parent.Parent.Parent.Parent as AccordionPane);
        GridView gvPersona = gvrHecho.FindControl("gvPersonasAnexos") as GridView;
        GridViewRow gvrPersona = ((sender as ImageButton).Parent.Parent as GridViewRow);
        Utilidades_Controles_dpsListBox lbxAutores = (gvrPersona.FindControl("lbxAutores") as Utilidades_Controles_dpsListBox);

        if (lbxAutores.SelectedIndex != -1)
        {
            var valoracionActual = ObtenerValoracionActual();

            int valor = Convert.ToInt32(lbxAutores.SelectedItem.Value);
            //int hecho = Convert.ToInt32(gvHechos.DataKeys[gvrHecho.DataItemIndex].Value);
            int hecho = Convert.ToInt32((gvrHecho.FindControl("hfHechoId") as HiddenField).Value);
            int persona = Convert.ToInt32(gvPersona.DataKeys[gvrPersona.DataItemIndex].Value);
            lbxAutores.Items.Remove(lbxAutores.SelectedItem);
            clsPersonaAnexo _persona = new clsPersonaAnexo();
            _persona = valoracionActual.Hechos.First(z => z.Id == hecho).Personas.First(x => x.Id == persona);
            clsAutores autor = new clsAutores();
            autor.Id = valor;
            _persona.Autores.Remove(_persona.Autores.First(x => x.Id == valor));

            EstablecerValoracionActual(valoracionActual);
        }
    }

    /// <summary>
    /// Agregar un autor en la list, individual
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnAgregarAutor_Click(object sender, EventArgs e)
    {
        //GridViewRow gvrHecho = ((sender as ImageButton).Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow);
        AccordionPane gvrHecho = ((sender as ImageButton).Parent.Parent.Parent.Parent.Parent.Parent as AccordionPane);
        GridViewRow gvrPersona = ((sender as ImageButton).Parent.Parent as GridViewRow);
        GridView gvPersona = ((sender as ImageButton).Parent.Parent.Parent.Parent as GridView);
        Utilidades_Controles_dpsListBox lbxAutores = gvrPersona.FindControl("lbxAutores") as Utilidades_Controles_dpsListBox;
        Utilidades_Controles_ruvDropDownList ddlAutor = gvrPersona.FindControl("ddlLAutores") as Utilidades_Controles_ruvDropDownList;

        if (ddlAutor.SelectedValue != null && ddlAutor.SelectedIndex > 0)
        {
            var valoracionActual = ObtenerValoracionActual();

            int valor = Convert.ToInt32(ddlAutor.SelectedValue);
            int persona = Convert.ToInt32(gvPersona.DataKeys[gvrPersona.DataItemIndex].Value);
            //int hecho = Convert.ToInt32(gvHechos.DataKeys[gvrHecho.DataItemIndex].Value);
            int hecho = Convert.ToInt32((gvrHecho.FindControl("hfHechoId") as HiddenField).Value);

            ListItem li = new ListItem();
            li.Value = valor.ToString();
            li.Text = ddlAutor.SelectedItem.Text;

            clsPersonaAnexo _persona = new clsPersonaAnexo();
            _persona = valoracionActual.Hechos.First(z => z.Id == hecho).Personas.First(x => x.Id == persona);

            clsAutores autor = new clsAutores();
            autor.Id = valor;
            if (_persona.Autores != null)
            {
                if (!_persona.Autores.Exists(x => x.Id == valor))
                {
                    lbxAutores.Items.Add(li);
                    _persona.Autores.Add(autor);
                }
            }
            else
            {
                lbxAutores.Items.Add(li);
                List<clsAutores> aut = new List<clsAutores>();
                aut.Add(autor);
                _persona.Autores = aut;
            }

            EstablecerValoracionActual(valoracionActual);
        }
    }

    /// <summary>
    /// Agregar a todas las personas del hecho victimizante Infracciones
    /// </summary> 
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnAgregarATodos_Click(object sender, EventArgs e)
    {
        GridView gvPersona = ((sender as ImageButton).Parent.Parent.Parent.Parent as GridView);
        var valoracionActual = ObtenerValoracionActual();

        foreach (GridViewRow row in gvPersona.Rows)
        {
            //GridViewRow rowHecho = row.Parent.Parent.Parent.Parent as GridViewRow;
            AccordionPane rowHecho = row.Parent.Parent.Parent.Parent as AccordionPane;
            //int hecho = Convert.ToInt32(gvHechos.DataKeys[rowHecho.DataItemIndex].Value);
            int hecho = Convert.ToInt32((rowHecho.FindControl("hfHechoId") as HiddenField).Value);
            int persona = Convert.ToInt32(gvPersona.DataKeys[row.DataItemIndex].Value);


            Utilidades_Controles_ruvDropDownList ddlautor = ((sender as ImageButton).Parent.FindControl("ddlInfraccionesAnexo") as Utilidades_Controles_ruvDropDownList);
            ListItem li = new ListItem();
            int valor = Convert.ToInt32(ddlautor.SelectedValue);
            li.Value = valor.ToString();

            if (ddlautor.SelectedValue != null && ddlautor.SelectedIndex > 0)
            {
                li.Text = ddlautor.SelectedItem.Text;

                clsPersonaAnexo _persona = new clsPersonaAnexo();
                _persona = valoracionActual.Hechos.First(z => z.Id == hecho).Personas.First(x => x.Id == persona);

                clsInfracciones infracc = new clsInfracciones();
                infracc.Id = valor;

                if (_persona.InfraccionesDHI != null)
                {
                    if (!_persona.InfraccionesDHI.Exists(x => x.Id == valor))
                    {
                        (row.FindControl("lbxInfracciones") as Utilidades_Controles_dpsListBox).Items.Add(li);
                        _persona.InfraccionesDHI.Add(infracc);
                    }
                }
                else
                {
                    (row.FindControl("lbxInfracciones") as Utilidades_Controles_dpsListBox).Items.Add(li);
                    List<clsInfracciones> aut = new List<clsInfracciones>();
                    aut.Add(infracc);
                    _persona.InfraccionesDHI = aut;
                }
            }

        }

        EstablecerValoracionActual(valoracionActual);
    }

    /// <summary>
    /// Quitar infraccion de la lista, individual
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnQuitar_Click(object sender, EventArgs e)
    {
        AccordionPane gvrHecho = ((sender as ImageButton).Parent.Parent.Parent.Parent.Parent.Parent as AccordionPane);
        GridView gvPersona = gvrHecho.FindControl("gvPersonasAnexos") as GridView;
        GridViewRow gvrPersona = ((sender as ImageButton).Parent.Parent as GridViewRow);
        Utilidades_Controles_dpsListBox lbxInfracciones = (gvrPersona.FindControl("lbxInfracciones") as Utilidades_Controles_dpsListBox);

        if (lbxInfracciones.SelectedIndex != -1)
        {
            var valoracionActual = ObtenerValoracionActual();

            int valor = Convert.ToInt32(lbxInfracciones.SelectedItem.Value);
            //int hecho = Convert.ToInt32(gvHechos.DataKeys[gvrHecho.DataItemIndex].Value);
            int hecho = Convert.ToInt32((gvrHecho.FindControl("hfHechoId") as HiddenField).Value);
            int persona = Convert.ToInt32(gvPersona.DataKeys[gvrPersona.DataItemIndex].Value);
            lbxInfracciones.Items.Remove(lbxInfracciones.SelectedItem);
            clsPersonaAnexo _persona = new clsPersonaAnexo();
            _persona = valoracionActual.Hechos.First(z => z.Id == hecho).Personas.First(x => x.Id == persona);
            clsInfracciones infra = new clsInfracciones();
            infra.Id = valor;
            _persona.InfraccionesDHI.Remove(_persona.InfraccionesDHI.First(x => x.Id == valor));

            EstablecerValoracionActual(valoracionActual);
        }
    }

    /// <summary>
    /// Agregar infraccion, Individual
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnAgregar_Click(object sender, EventArgs e)
    {
        AccordionPane gvrHecho = ((sender as ImageButton).Parent.Parent.Parent.Parent.Parent.Parent as AccordionPane);
        GridViewRow gvrPersona = ((sender as ImageButton).Parent.Parent as GridViewRow);
        GridView gvPersona = ((sender as ImageButton).Parent.Parent.Parent.Parent as GridView);
        Utilidades_Controles_dpsListBox lbxInfraccion = gvrPersona.FindControl("lbxInfracciones") as Utilidades_Controles_dpsListBox;
        Utilidades_Controles_ruvDropDownList ddlInfraccion = gvrPersona.FindControl("ddlLInfracciones") as Utilidades_Controles_ruvDropDownList;

        if (ddlInfraccion.SelectedValue != null && ddlInfraccion.SelectedIndex > 0)
        {

            var valoracionActual = ObtenerValoracionActual();

            int valor = Convert.ToInt32(ddlInfraccion.SelectedValue);
            int persona = Convert.ToInt32(gvPersona.DataKeys[gvrPersona.DataItemIndex].Value);
            //int hecho = Convert.ToInt32(gvHechos.DataKeys[gvrHecho.DataItemIndex].Value);
            int hecho = Convert.ToInt32((gvrHecho.FindControl("hfHechoId") as HiddenField).Value);
            ListItem li = new ListItem();
            li.Value = valor.ToString();
            li.Text = ddlInfraccion.SelectedItem.Text;

            clsPersonaAnexo _persona = new clsPersonaAnexo();
            _persona = valoracionActual.Hechos.First(z => z.Id == hecho).Personas.First(x => x.Id == persona);

            clsInfracciones infra = new clsInfracciones();
            infra.Id = valor;

            if (_persona.InfraccionesDHI != null)
            {
                if (!_persona.InfraccionesDHI.Exists(x => x.Id == valor))
                {
                    lbxInfraccion.Items.Add(li);
                    _persona.InfraccionesDHI.Add(infra);
                }
            }
            else
            {
                lbxInfraccion.Items.Add(li);
                List<clsInfracciones> aut = new List<clsInfracciones>();
                aut.Add(infra);
                _persona.InfraccionesDHI = aut;
            }

            EstablecerValoracionActual(valoracionActual);
        }
    }

    /// <summary>
    /// Entrar a capturar los registros anteriores
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRegAneteriores_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvRegAneteriores.SelectedValue != null)
        {
            var valoracionActual = ObtenerValoracionActual();

            int registro = Convert.ToInt32(gvRegAneteriores.SelectedValue);
            ViewState["RegistroId"] = registro;
            (gvRegAneteriores.SelectedRow.FindControl("chkMarcarRegistro") as CheckBox).Checked = true;

            lbxPersonas.Items.Clear();
            lbxPersonas.DataSource = valoracionActual.PersonasDeclaracion;
            lbxPersonas.DataBind();
            chkPreguntas.Seleccionados = new List<int>();
            chkTodas.Checked = false;

            if (valoracionActual.RegistrosAnteriores != null)
            {
                if (valoracionActual.RegistrosAnteriores.Exists(x => x.RegistroId == registro))
                {
                    clsRegistrosValoracion rv = valoracionActual.RegistrosAnteriores.First(x => x.RegistroId == registro);
                    lbxPersonas.Seleccionados = rv.RegPersonas;
                    chkPreguntas.Seleccionados = rv.Preguntas;
                }
                else
                {
                    lbxPersonas.Seleccionados = new List<int>();
                    chkPreguntas.Seleccionados = new List<int>();
                }
            }
            else
            {
                lbxPersonas.Seleccionados = new List<int>();
                chkPreguntas.Seleccionados = new List<int>();
            }

            mpExtRegAnt.Show();
            EstablecerValoracionActual(valoracionActual);
        }
    }

    /// <summary>
    /// Desmarcar o marcar si hay personas con registros anteriores
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkNoSeEncuentran_CheckedChanged(object sender, EventArgs e)
    {
        var valoracionActual = ObtenerValoracionActual();
        if (chkNoSeEncuentran.Checked)
        {
            valoracionActual.RegistrosAnteriores.Clear();
            foreach (GridViewRow gvr in gvRegAneteriores.Rows)
            {
                (gvr.FindControl("chkMarcarRegistro") as CheckBox).Checked = false;
            }
            tblRegistros.Visible = false;
        }
        else
        {
            tblRegistros.Visible = true;
        }
        EstablecerValoracionActual(valoracionActual);
    }

    /// <summary>
    /// Selecciona todas las personas que tienen registros anteriores
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkTodas_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTodas.Checked)
        {
            foreach (ListItem item in lbxPersonas.Items)
            {
                item.Selected = true;
            }
        }
        else
        {
            foreach (ListItem item in lbxPersonas.Items)
            {
                item.Selected = false;
            }
        }
        mpExtRegAnt.Show();
    }

    /// <summary>
    /// Guardar el registro anterior de las personas correspondientes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        int registroid = Convert.ToInt32(ViewState["RegistroId"]);
        var valoracionActual = ObtenerValoracionActual();
        if (valoracionActual.RegistrosAnteriores != null)
        {
            if (!valoracionActual.RegistrosAnteriores.Exists(x => x.RegistroId == registroid))
            {
                clsRegistrosValoracion rv = new clsRegistrosValoracion();
                rv.RegistroId = registroid;
                rv.ValoracionId = valoracionActual.Id;
                rv.RegPersonas = lbxPersonas.Seleccionados;
                rv.Preguntas = chkPreguntas.Seleccionados;
                valoracionActual.RegistrosAnteriores.Add(rv);
            }
            else
            {
                clsRegistrosValoracion rv = valoracionActual.RegistrosAnteriores.First(x => x.RegistroId == registroid);
                rv.RegistroId = registroid;
                rv.ValoracionId = valoracionActual.Id;
                rv.RegPersonas = lbxPersonas.Seleccionados;
                rv.Preguntas = chkPreguntas.Seleccionados;
            }
        }
        else
        {
            clsRegistrosValoracion rv = new clsRegistrosValoracion();
            rv.RegistroId = registroid;
            rv.ValoracionId = valoracionActual.Id;
            rv.RegPersonas = lbxPersonas.Seleccionados;
            rv.Preguntas = chkPreguntas.Seleccionados;
            List<clsRegistrosValoracion> lrv = new List<clsRegistrosValoracion>();
            lrv.Add(rv);
            valoracionActual.RegistrosAnteriores = lrv;
        }

        EstablecerValoracionActual(valoracionActual);
    }


    /// <summary>
    /// Validar si es considerada una declaración
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkValidacion_CheckedChanged(object sender, EventArgs e)
    {
        var valoracionActual = ObtenerValoracionActual();
        dvCaptura.Visible = ((dvBasicaInfo2.FindControl("chkValidacion") as CheckBox).Checked) ? true : false;
        dvBasicaInfo2.Rows[1].Visible = ((dvBasicaInfo2.FindControl("chkValidacion") as CheckBox).Checked) ? false : true;
        //dvBasicaInfo2.Rows[2].Visible = ((dvBasicaInfo2.FindControl("chkValidacion") as CheckBox).Checked) ? false : true;
        if ((dvBasicaInfo2.FindControl("chkValidacion") as CheckBox).Checked)
        {
            //(dvBasicaInfo2.FindControl("txtObservacionValidacion") as Utilidades_Controles_dpsTextBox).Text = string.Empty;
            (dvBasicaInfo2.FindControl("chkCausales") as Utilidades_Controles_dpsCheckBoxList).LimpiarSelecciones();
            foreach (clsHechosValoracion hecho in valoracionActual.Hechos)
            {
                foreach (clsPersonaAnexo persona in hecho.Personas)
                {
                    if (persona.EstadoId == (int)eEstadosValoracionPersona.NoValoradoDevuelto)
                    {
                        persona.EstadoId = null;
                        persona.Principios.Clear();
                    }
                }
            }
        }
        EstablecerValoracionActual(valoracionActual);
    }

    /// <summary>
    /// Ocurre cuando aseguran que se quiere finalizar la valoración o si ya se finalizo sale de la captura y muestra el resumen
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void mpopGuardar_Ok(object sender, EventArgs e)
    {
        if (!mpopGuardar.VisibleBotonCancelar)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            Finalizar();
        }
    }

    /// <summary>
    /// Ocurre cuando dan guardar al nuevo anexo se verifica que las fechas concuerden, que tenga personas.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardarAnexo(object sender, EventArgs e)
    {
        ShowMessage("Los Cambios se Realizaron Con exito");
        //Response.Redirect("Nueva.aspx");
    }

    protected void ErrorGuardarHecho(object sender, Ruv.Infrastructure.Crosscutting.Common.ErrorEventArgs e)
    {
        Master.MensajeDeError.MensajeTextBox = e.ErrorMensaje;
        Master.MensajeDeError.Mostrar();
    }

    protected void personaDetalle_OnGuardarOk(object sender, PersonaAnexoEventArgs e)
    {
        if (Session[ConstantesItems.VALORACION_REPLICA] != null)
        {
            if (Convert.ToBoolean(Session[ConstantesItems.VALORACION_REPLICA]))
            {
                AsignarTodosMasivo(e.Persona);
            }
        }
        MarcarEstados(e.Persona);
    }

    #endregion

    #region Propiedades

    /// <summary>
    /// Propiedad que guarda y obtiene el hecho victimizante que se va a guardar
    /// </summary>
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

    #endregion

    #region Metodos

    /// <summary>
    /// Descargar Documento Por Id Declaración
    /// </summary>
    private void DescargarDocumento()
    {
        CriticaNService objCriticaN = new CriticaNService();
        clsCryptoUtil CriptoUtil = new clsCryptoUtil();
        string fileName = string.Empty;
        clsValoracion valoracionActual = ObtenerValoracionActual();
        clsDeclaracion declaracion = valoracionActual.Declaracion;

        if (declaracion.DocumentoAnexo != null)
        {
            Session["Arch"] = declaracion.DocumentoAnexo;
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>Descargar('" + declaracion.RadicacionId.ToString() + "-XPS.zip" + "')</script>", false);

            if (!string.IsNullOrEmpty(declaracion.DocumentoDigitalNombre) && declaracion.DocumentoDigital != null)
            {
                Session["Arch2"] = declaracion.DocumentoDigital;
                Master.PopUpGeneral.Mensaje = "Desea guardar los anexos añadidos a la declaración?";
                Master.PopUpGeneral.MostrarImagen = false;
                Master.PopUpGeneral.MostrarBotones = true;
                Master.PopUpGeneral.VisibleBotonCancelar = true;
                Master.PopUpGeneral.OnOkScript = "Descargar('" + System.IO.Path.GetFileName(declaracion.DocumentoDigitalNombre) + "')";
                Master.PopUpGeneral.Mostrar();
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(declaracion.DocumentoDigitalNombre) && declaracion.DocumentoDigital != null)
            {
                Session["Arch"] = declaracion.DocumentoDigital;
                fileName = System.IO.Path.GetFileName(declaracion.DocumentoDigitalNombre);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>Descargar('" + fileName + "')</script>", false);
            }
            else
            {
                mpopMensajes.Mensaje = "No Existe el documento escaneado";
                mpopMensajes.Mostrar();
            }
        }
    }

    /// <summary>
    /// Funcion para guardar declaración antes de agregar nuevas personas asociadas
    /// </summary>
    private void GuardarParaAgregarPersonas()
    {
        Guardar(eEstadosValoracion.IniciaValoracion);
    }

    /// <summary>
    /// Validar Si es masivo
    /// </summary>
    /// <param name="persona"></param>
    private void AsignarTodosMasivo(clsPersonaAnexo persona)
    {
        var valoracionActual = ObtenerValoracionActual();
        if (valoracionActual.Hechos.Count(x => x.TipoHechoId == (int)eTiposAnexos.CensoMasivo_13) > 0)
        {
            foreach (clsHechosValoracion hecho in valoracionActual.Hechos)
            {
                foreach (clsPersonaAnexo Nper in hecho.Personas)
                {
                    Replicar(Nper, persona);
                    MarcarEstados(Nper);
                }
            }
        }
        else
        {
            clsHechosValoracion hecho = valoracionActual.Hechos.FirstOrDefault(x => x.Id == persona.ValAnexoId);
            foreach (clsPersonaAnexo Nper in hecho.Personas)
            {
                Replicar(Nper, persona);
                MarcarEstados(Nper);
            }
        }
        Session[ConstantesItems.VALORACION_REPLICA] = null;
    }

    /// <summary>
    /// Copiar propiedades
    /// </summary>
    /// <param name="Nper"></param>
    /// <param name="persona"></param>
    private void Replicar(clsPersonaAnexo Nper, clsPersonaAnexo persona)
    {
        foreach (var item in Nper.GetType().GetProperties())
        {
            if (item.Name.Equals("Afectado") || item.Name.Equals("Victima") || item.Name.Equals("AfectacionesDetectadas")
                || item.Name.Equals("EstadoId") || item.Name.Equals("ObservacionId") || item.Name.Equals("Principios") || item.Name.Equals("DecretoLey") || item.Name.Equals("HechoEnmarcadoId"))
            {
                foreach (var itSource in persona.GetType().GetProperties())
                {
                    if (item.Name == itSource.Name)
                    {
                        var valor1 = itSource.GetValue(persona, null);
                        if (valor1 != null)
                        {
                            item.SetValue(Nper, itSource.GetValue(persona, null), null);
                        }
                    }
                }
            }
        }

    }

    /// <summary>
    /// Retroceder a la lista de tareas, no verifica nada
    /// </summary>
    private void IrAtras()
    {
        Response.Redirect("Default.aspx");
    }

    /// <summary>
    /// Borra lo que tenga las sesiones usadas en la captura de la valoración
    /// </summary>
    private void LimpiarSessiones()
    {
        Session[ConstantesItems.VALORACION] = null;
        Session[ConstantesItems.VALORACION_ANEXO_ID] = null;
        Session[ConstantesItems.VALORACION_PERSONA_GRILLA] = null;
        Session[ConstantesItems.VALORACION_PERSONA_GUARDADA] = true;
        Session[ConstantesItems.VALORACION_PERSONA_ULTIMA] = -1;
        Session[ConstantesItems.HERRAMIENTAS] = 1;
        Session[ConstantesItems.VALORACION_REPLICA] = null;
    }

    /// <summary>
    /// Guarda la declaracion con el estado que se le envie, exepto si no es considerada una declaración que se deja NoValoradoDevuelto
    /// </summary>
    /// <param name="estado"></param>
    /// <returns></returns>
    public bool Guardar(eEstadosValoracion estado, bool finalizar = false)
    {
        ValoracionService objService = new ValoracionService();
        clsValoracion valoracionActual = ObtenerValoracionActual();

        valoracionActual.FechaValoracion = (dvBasicaInfor.FindControl("txtFechaValoracion") as Utilidades_Controles_dpsTextCalendar).Fecha;
        valoracionActual.FechaRealValoracion = DateTime.Now;

        valoracionActual.EsDeclaracion = (dvBasicaInfo2.FindControl("chkValidacion") as CheckBox).Checked;
        valoracionActual.Observacion = (dvBasicaInfo2.FindControl("ObservacionValoracion") as TextBox).Text;
        valoracionActual.EstadoId = (int)estado;
        if (valoracionActual.EsDeclaracion)
        {
            List<clsHerramietasOrganizar> herOrga = (List<clsHerramietasOrganizar>)Session[ConstantesItems.HERRAMIENTAS];
            List<clsHechosValoracion> tmpHechos = new List<clsHechosValoracion>();


            foreach (clsHechosValoracion item in valoracionActual.Hechos)
            {
                foreach (clsPersonaAnexo per in item.Personas)
                {
                    if (herOrga.Exists(x => x.PersonaId == per.Id))
                    {
                        per.Herramietas = herOrga.First(x => x.PersonaId == per.Id).Herramientas;
                    }
                }
                tmpHechos.Add(item);
            }
            valoracionActual.Hechos = tmpHechos;
        }
        else
        { // Se marcará como devolución
            if (finalizar)
            {
                valoracionActual.EstadoId = (int)eEstadosValoracion.NoValoradoDevuelto;
                valoracionActual.CausalDevolucion = (dvBasicaInfo2.FindControl("chkCausales") as Utilidades_Controles_dpsCheckBoxList).Seleccionados;
                List<clsHechosValoracion> tmpHechos = new List<clsHechosValoracion>();
                foreach (clsHechosValoracion item in valoracionActual.Hechos)
                {
                    foreach (clsPersonaAnexo per in item.Personas)
                    {
                        per.EstadoId = (int)eEstadosValoracionPersona.NoValoradoDevuelto;
                        per.Principios = valoracionActual.CausalDevolucion;
                    }
                    item.UltimaFechaEdicion = DateTime.Now;
                    tmpHechos.Add(item);
                }
                valoracionActual.Hechos = tmpHechos;
            }
        }

        // Registros anteriores (SIPOD y otras bases de datos)
        foreach (GridViewRow gvr in gvRegAneteriores.Rows)
        {
            int reg = Convert.ToInt32(gvRegAneteriores.DataKeys[gvr.DataItemIndex].Value);
            clsRegistrosValoracion regValoracion = new clsRegistrosValoracion();
            if (valoracionActual.RegistrosAnteriores.Exists(x => x.RegistroId == reg))
            {
                regValoracion = valoracionActual.RegistrosAnteriores.First(x => x.RegistroId == reg);
            }
            if (!(gvr.FindControl("chkMarcarRegistro") as CheckBox).Checked)
            {
                valoracionActual.RegistrosAnteriores.Remove(regValoracion);
            }
        }

        string guardar = objService.GuardarValoracion(valoracionActual, finalizar);

        if (valoracionActual.CausalDevolucion.Count == 0)
        {
            if (finalizar)
                CrearActoAdministrativo();
        }
        else
        {
            if (string.IsNullOrEmpty(guardar))
                Master.PopUpGeneral.Mensaje = "Se realizó la devolución correctamente";
            Master.PopUpGeneral.MostrarImagen = false;
            Master.PopUpGeneral.MostrarBotones = true;
            Master.PopUpGeneral.VisibleBotonCancelar = false;
            Master.PopUpGeneral.Mostrar("Default.aspx");
        }

        if (!string.IsNullOrEmpty(guardar))
        {
            lblError.Text = "Ocurrio un error guardando la información";
            Master.MensajeDeError.MensajeTextBox = guardar;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            Master.MensajeDeError.Mostrar();
            return false;
        }

        return true;
    }


    /// <summary>
    /// Ejecuta las validaciones y muestra un mensaje de confirmación para finalizar la valoración
    /// </summary>
    private void Finalizar()
    {
        ListItemCollection valida = EsValido();
        if (valida.Count == 0)
        {
            Guardar(eEstadosValoracion.PendientePorNotificar, true);
        }
        else
        {
            Validaciones1.Visible = true;
            Validaciones1.DataSource = valida;
            Validaciones1.DataBind();
        }
    }

    /// <summary>
    /// Ejecuta las validaciones
    /// V4. Solicitan quitar validaciones para las herramientas
    /// </summary>
    /// <returns></returns>
    private ListItemCollection EsValido()
    {
        clsValoracion valoracionActual = ObtenerValoracionActual();
        ListItemCollection items = new ListItemCollection();
        string Error = string.Empty;
        int i = 1;
        if ((dvBasicaInfo2.FindControl("chkValidacion") as CheckBox).Checked)
        {
            foreach (GridViewRow regant in gvRegAneteriores.Rows)
            {
                if ((regant.FindControl("chkMarcarRegistro") as CheckBox).Checked)
                {
                    int reg = Convert.ToInt32(gvRegAneteriores.DataKeys[regant.DataItemIndex].Value);
                    string registroNombre = regant.Cells[2].Text;
                    if (!valoracionActual.RegistrosAnteriores.Exists(x => x.RegistroId == reg))
                    {
                        Error = string.Format("Ingresar las personas que se encuentran en el Registro Anterior {0} e indique por que se encuentra allí", registroNombre);
                        ListItem li = new ListItem();
                        li.Value = i.ToString();
                        li.Text = Error;
                        items.Add(li);
                        i++;
                    }
                    else
                    {
                        clsRegistrosValoracion regVal = valoracionActual.RegistrosAnteriores.First(x => x.RegistroId == reg);
                        if (regVal.RegPersonas.Count == 0 || regVal.Preguntas.Count == 0)
                        {
                            Error = string.Format("Ingresar las personas que se encuentran en el Registro Anterior {0} e indique por que se encuentra allí", registroNombre);
                            ListItem li = new ListItem();
                            li.Value = i.ToString();
                            li.Text = Error;
                            items.Add(li);
                            i++;
                        }
                    }
                }
            }

            foreach (clsHechosValoracion it in valoracionActual.Hechos)
            {
                if (it.TipoHechoId != (int)eTiposAnexos.AbandonoDespojoForzadoTierras_11)
                {

                    if (it.Personas != null)
                    {
                        foreach (clsPersonaAnexo item in it.Personas)
                        {
                            if (item.Victima && item.EstadoId != (int)eEstadosValoracionPersona.EnValoración)
                            {
                                if (it.TipoHechoId != (int)eTiposAnexos.CensoMasivo_13)
                                {

                                    if (item.Autores.Count == 0 || item.Autores == null)
                                    {
                                        Error = string.Format("Indique un autor o mas a {0} del hecho victimizante {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                        ListItem li = new ListItem();
                                        li.Text = Error;
                                        li.Value = i.ToString();
                                        items.Add(li);
                                        i++;
                                    }
                                    else
                                    {
                                        foreach (clsAutores autor in item.Autores)
                                        {
                                            if (autor.FechaCreacion.HasValue)
                                            {
                                                if (it.Fecha < autor.FechaCreacion)
                                                {
                                                    Error = string.Format("El autor {0} no puede ser seleccionado para {1} del hecho victimizante {2} del {3} por que la fecha del hecho es menor a la fecha de creación del autor", autor.Nombre, item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                                    ListItem li = new ListItem();
                                                    li.Value = i.ToString();
                                                    li.Text = Error;
                                                    items.Add(li);
                                                    i++;
                                                }
                                            }
                                            if (autor.FechaDesmovilizacion.HasValue)
                                            {

                                                if (it.Fecha > autor.FechaDesmovilizacion)
                                                {
                                                    Error = string.Format("El autor {0} no puede ser seleccionado para {1} del hecho victimizante {2} del {3} por que en la fecha del hecho el autor ya se habia desmovilizado", autor.Nombre, item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                                    ListItem li = new ListItem();
                                                    li.Value = i.ToString();
                                                    li.Text = Error;
                                                    items.Add(li);
                                                    i++;
                                                }
                                            }
                                        }
                                    }

                                    if (item.InfraccionesDHI.Count == 0 || item.InfraccionesDHI == null)
                                    {
                                        Error = string.Format("Indique la infracción al DIH a {0} del hecho victimizante {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                        ListItem li = new ListItem();
                                        li.Value = i.ToString();
                                        li.Text = Error;
                                        items.Add(li);
                                        i++;
                                    }
                                }

                                #region HerramientasValidacion[Obsoleto]

                                /* if (item.Herramietas.Count == 0 || item.Herramietas == null)
                                 {
                                     Error = string.Format("Indique las herramientas de {0} del hecho victimizante {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                     ListItem li = new ListItem();
                                     li.Value = i.ToString();
                                     li.Text = Error;
                                     items.Add(li);
                                     i++;
                                 }
                                 else
                                 {
                                     int contexto = 0;
                                     int juridica = 0;
                                     int tecnica = 0;
                                     foreach (clsHerramientaAnexoPer herramienta in item.Herramietas)
                                     {
                                         if (herramienta.Herramienta.TipoId == (int)eTipoHerramientaValoracion.Contexto)
                                         {
                                             contexto++;
                                         }
                                         if (herramienta.Herramienta.TipoId == (int)eTipoHerramientaValoracion.Juridica)
                                         {
                                             juridica++;
                                         }
                                         if (herramienta.Herramienta.TipoId == (int)eTipoHerramientaValoracion.Tecnica)
                                         {
                                             tecnica++;
                                         }
                                     }
                                     if (contexto == 0)
                                     {
                                         Error = string.Format("Indique al menos una herramienta de contexto de {0} del hecho victimizante {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                         ListItem li = new ListItem();
                                         li.Value = i.ToString();
                                         li.Text = Error;
                                         items.Add(li);
                                         i++;
                                     }
                                     if (tecnica == 0)
                                     {
                                         Error = string.Format("Indique al menos una herramienta técnica de {0} del hecho victimizante {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                         ListItem li = new ListItem();
                                         li.Value = i.ToString();
                                         li.Text = Error;
                                         items.Add(li);
                                         i++;
                                     }
                                     if (juridica == 0)
                                     {
                                         Error = string.Format("Indique al menos una herramienta jurídica de {0} del hecho victimizante {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                         ListItem li = new ListItem();
                                         li.Value = i.ToString();
                                         li.Text = Error;
                                         items.Add(li);
                                         i++;
                                     }
                                 }*/

                                #endregion

                                if (item.Principios.Count == 0)
                                {
                                    Error = string.Format("Seleccione el principio de {0} del Hecho {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                    ListItem li = new ListItem();
                                    li.Value = i.ToString();
                                    li.Text = Error;
                                    items.Add(li);
                                    i++;
                                }
                            }
                            if (item.DecretoLey == null || item.DecretoLey == "")
                            {
                                Error = string.Format("Seleccione si se enmarca el decreto ley étnico a {0} del hecho {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                ListItem li = new ListItem();
                                li.Value = i.ToString();
                                li.Text = Error;
                                items.Add(li);
                                i++;
                            }
                            if (item.EstadoId == null)
                            {
                                Error = string.Format("Seleccione el estado de {0} del Hecho {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                ListItem li = new ListItem();
                                li.Value = i.ToString();
                                li.Text = Error;
                                items.Add(li);
                                i++;
                            }
                            else
                            {

                                //if (RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.Valorar))
                                //{
                                if (item.EstadoId == (int)eEstadosValoracionPersona.Excluido)
                                {
                                    Error = string.Format("El perfil valorador no tiene permitido marcar el estado excluido en {0} del hecho {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                    ListItem li = new ListItem();
                                    li.Value = i.ToString();
                                    li.Text = Error;
                                    items.Add(li);
                                    i++;
                                }
                                //}
                                if (item.EstadoId == (int)eEstadosValoracionPersona.Incluido || item.EstadoId == (int)eEstadosValoracionPersona.NoIncluido)
                                {
                                    if (item.ObservacionId == null)
                                    {
                                        Error = string.Format("Seleccione si {0} es Activo o Inactivo del Hecho {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                        ListItem li = new ListItem();
                                        li.Value = i.ToString();
                                        li.Text = Error;
                                        items.Add(li);
                                        i++;
                                    }
                                    if (item.EstadoId == (int)eEstadosValoracionPersona.Incluido && item.Principios.Count > 1)
                                    {
                                        Error = string.Format("Seleccione únicamente un principio de Inclusión para {0} del Hecho {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                        ListItem li = new ListItem();
                                        li.Value = i.ToString();
                                        li.Text = Error;
                                        items.Add(li);
                                        i++;
                                    }
                                }
                                if (item.EstadoId == (int)eEstadosValoracionPersona.Incluido)
                                {
                                    if (item.HechoEnmarcadoId == null)
                                    {
                                        Error = string.Format("Seleccione un hecho enmarcado a {0} del Hecho {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                        ListItem li = new ListItem();
                                        li.Value = i.ToString();
                                        li.Text = Error;
                                        items.Add(li);
                                        i++;
                                    }
                                }
                                if (item.EstadoId == (int)eEstadosValoracionPersona.EnValoración)
                                {
                                    Error = string.Format("No se puede finalizar por que el estado no puede ser en Valoración para {0} del hecho {1} del {2}", item.Persona, it.TipoHecho, it.Fecha.ToShortDateString());
                                    ListItem li = new ListItem();
                                    li.Value = i.ToString();
                                    li.Text = Error;
                                    items.Add(li);
                                    i++;
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            /*if (string.IsNullOrEmpty((dvBasicaInfo2.FindControl("txtObservacionValidacion") as Utilidades_Controles_dpsTextBox).Text))
            {
                Error = string.Format("Escriba alguna observación para proporcionar información por la cual no es considerada declaración");
                ListItem li = new ListItem();
                li.Value = i.ToString();
                li.Text = Error;
                items.Add(li);
                i++;
            }*/
            if ((dvBasicaInfo2.FindControl("chkCausales") as Utilidades_Controles_dpsCheckBoxList).Seleccionados.Count == 0)
            {
                Error = string.Format("Seleccione las causales por la que no es considerada una declaración");
                ListItem li = new ListItem();
                li.Value = i.ToString();
                li.Text = Error;
                items.Add(li);
                i++;
            }
        }
        return items;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static List<clsPrincipioEstado> ObtenerPrincipios()
    {
        return RUV.Current.ListadosGeneralesValoracion.Principios;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static List<clsObservacionEstado> ObtenerObservacionesEstado()
    {
        return RUV.Current.ListadosGeneralesValoracion.Observaciones;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static List<clsHechoEnmarcado> HechoEnmarcado()
    {
        return RUV.Current.ListadosGeneralesValoracion.HechoEnmarcado;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static void GuardarEditar(string observacion)
    {
        Administracion objAdmin = new Administracion();
        string Usuario = HttpContext.Current.Session[ConstantesSesion.USUARIO_ID_LOGIN].ToString();
        string tokenapp = HttpContext.Current.Session[ConstantesSesion.USUARIO_APP].ToString();
        SIRAV.Cliente.Auditoria.ClienteAuditoria objCliente = new SIRAV.Cliente.Auditoria.ClienteAuditoria();
        USUARIO _Usuario = objAdmin.UsuarioPorToken(Usuario);
        objCliente.InsertarRegistroEditarDeclaracion(2, observacion, _Usuario.ID, tokenapp);
    }



    /// <summary>
    /// LLena el formulario con la información de la declaración a valorar
    /// </summary>
    private void CargarInfoDeclaracion()
    {

        var idValoracion = Request.QSIntegerField("id");
        if (idValoracion != null)
        {
            ValoracionService objValService = new ValoracionService();
            //Obtener Informacion Basica de la valoracion incluidos hechos y personas
            var valoracionActual = objValService.ValoracionPorId(idValoracion.Value, true);

            if (valoracionActual == null)
            {
                Response.Redirect("Default.aspx?errorMessage=La valoración no existe");
            }
            else if (valoracionActual.ValoradorId != RUV.Current.Usuario.ID)
            {
                Response.Redirect("Default.aspx?errorMessage=No tiene permisos para ver la valoración");
            }
            else if (valoracionActual.EstadoId == (int)eEstadosValoracion.PendientePorNotificar)
            {
                Response.Redirect("Default.aspx?errorMessage=La valoración no se puede abrir cuando ya fue finalizada");
            }

            bool esFueraDeColombia = objValService.FueraDeColombia(valoracionActual.DeclaracionId);
            if (esFueraDeColombia)
            {
                var validacionFueraDeColombia = new ListItemCollection();
                validacionFueraDeColombia.Add(new ListItem() { Text = "Por favor valide si se configura un desplazamiento transnacional o transfronterizo e ingrese al módulo editar y confirme la marca correcta, de ser necesario, ajuste el tipo de desplazamiento forzado" });

                Validaciones2.Visible = true;
                Validaciones2.DataSource = validacionFueraDeColombia;
                Validaciones2.DataBind();
            }
            /*variables a guardar para pasar por URL al WPF*/
            hdnIdDeclaracion.Value = valoracionActual.DeclaracionId.ToString();
            hdnIdValoracion.Value = valoracionActual.Id.ToString();
            hdnUrl.Value = System.Configuration.ConfigurationManager.AppSettings["UrlRuv"];

            try
            {
                USUARIO usuarioaut = (USUARIO)Session[ConstantesSesion.USUARIO];
                string tokenApp = Session[ConstantesSesion.USUARIO_APP].ToString();
                string error = string.Empty;
                USUARIO_BASICO usuario = objValService.UsuarioPorId(usuarioaut.ID, ref error);
                hdnLogin.Value = usuario.USERNAME;
                Cryptography.Cryptography.Encrypt oEncrypt = new Cryptography.Cryptography.Encrypt();
                clsCryptoUtil cifrado = new clsCryptoUtil();
                hdnPassword.Value = cifrado.EncryptStringFixed(oEncrypt.DecryptData(usuario.CLAVE));
            }
            catch (Exception)
            {
            }

            //Obtener informacion Basica de la declaracion
            List<clsDeclaracionInfoValoracion> InfoDeclaracion = objValService.InformacionDeclaracionPorId(valoracionActual.Id);
            valoracionActual.BasicDeclaracion = InfoDeclaracion;
            SIRAV.Entidades.Administracion.USUARIO usr = Varios.Usuario(HttpContext.Current);
            string valorador = string.Format("{0} {1} {2} {3}", usr.PRIMER_NOMBRE, usr.SEGUNDO_NOMBRE, usr.PRIMER_APELLIDO, usr.SEGUNDO_APELLIDO);
            InfoDeclaracion.ForEach(z => z.Valorador = valorador);
            //ObtenerLaDeclaracion
            //#region Debe Buscar Una Solucion mejor a esto

            GeneralService objGeneral = new GeneralService();
            //string llave = RUV.Current.LlaveUsuario;
            clsDeclaracion declar = objGeneral.ObtenerImagenDeclaracion(valoracionActual.DeclaracionId);
            valoracionActual.Declaracion = declar;
            hvNuevo.FechaDeclaracion = declar.TomaDeclaracion.FechaDeclaracion.Value;

            //#endregion

            //Muestra la informacion en los controles detailsView
            List<clsValoracion> valoraciones = new List<clsValoracion>();
            valoraciones.Add(valoracionActual);
            dvBasicaInfor.DataSource = InfoDeclaracion;
            dvBasicaInfo2.DataSource = valoraciones;
            dvBasicaInfor.DataBind();
            dvBasicaInfo2.DataBind();
            (dvBasicaInfo2.FindControl("chkCausales") as Utilidades_Controles_dpsCheckBoxList).Seleccionados = valoracionActual.CausalDevolucion;

            if (valoracionActual.CausalDevolucion.Count < 1)
                (dvBasicaInfo2.FindControl("ObservacionValoracion") as TextBox).Text = valoracionActual.Observacion;

            //Lista Los registros anteriores
            gvRegAneteriores.DataSource = objValService.ListarRegistrosAnteriores();
            gvRegAneteriores.DataBind();

            //Muestra los hechos Victimizantes en el Accordion

            valoracionActual.Hechos.ForEach(
                x =>
                {
                    x.MuestraAbandono = false;
                    x.MuestraDespojo = false;
                    if (x.TipoHechoId == 11)
                    {
                        if (x.FechaAbandono != null)
                            x.MuestraAbandono = true;
                        if (x.FechaDespojo != null)
                            x.MuestraDespojo = true;
                    }
                }
            );
            acHechos.DataSource = valoracionActual.Hechos;
            acHechos.DataBind();

            //Carga información de Valoracion al NuevoHecho
            hvNuevo.HechoVictimizante.Valoracion = valoracionActual;
            hvNuevo.InicializarHechoVictimizante();

            //Marca que que registros anteriores tiene marcada la valoracion
            if (valoracionActual.RegistrosAnteriores.Count > 0)
            {
                foreach (GridViewRow gvr in gvRegAneteriores.Rows)
                {
                    foreach (clsRegistrosValoracion regValoracion in valoracionActual.RegistrosAnteriores)
                    {
                        if (Convert.ToInt32(gvRegAneteriores.DataKeys[gvr.DataItemIndex].Value) == regValoracion.RegistroId)
                        {
                            (gvr.FindControl("chkMarcarRegistro") as CheckBox).Checked = true;
                        }
                    }
                }
            }
            else
            {
                chkNoSeEncuentran.Checked = true;
                tblRegistros.Visible = false;
            }

            Session[ConstantesItems.HERRAMIENTAS] = LogicaMostrarColumnas(valoracionActual.Hechos);

            // Cargar Informacion Dependiente
            if (valoracionActual != null)
                this.PersonasAsociadas.IdDeclaracion = valoracionActual.DeclaracionId;

            EstablecerValoracionActual(valoracionActual);

            chkValidacion_CheckedChanged(null, EventArgs.Empty);
        }
    }


    public List<clsHerramietasOrganizar> LogicaMostrarColumnas(List<clsHechosValoracion> hechos)
    {
        //Se recorren los hechos victimizantes para dependiendo del tipo de hecho mostrar ciertas columas e información
        //Muestra las herramientas guardadas por cada persona en cada hecho
        int i = 0;
        List<clsHerramietasOrganizar> herramien = new List<clsHerramietasOrganizar>();
        foreach (clsHechosValoracion item in hechos)
        {
            GridView gvPersonas = (acHechos.Panes[i].FindControl("gvPersonasAnexos") as GridView);
            foreach (clsPersonaAnexo per in item.Personas)
            {
                clsHerramietasOrganizar her = new clsHerramietasOrganizar();
                her.PersonaId = per.Id;
                her.Herramientas = per.Herramietas;
                herramien.Add(her);

                MarcarEstados(per);
            }

            if (item.TipoHechoId == (int)eTiposAnexos.CensoMasivo_13)
            {
                acHechos.Panes[i].FindControl("tblInfHecho").Visible = false;
                OcultarMostrar(true, gvPersonas);
            }
            else
            {
                acHechos.Panes[i].FindControl("tblInfHecho").Visible = true;
                OcultarMostrar(false, gvPersonas);
            }

            foreach (GridViewRow gvRPer in gvPersonas.Rows)
            {
                if (item.TipoHechoId == (int)eTiposAnexos.HomicidioMasacre_6)
                {
                    gvRPer.FindControl("trFallecida").Visible = true;
                }
                if (item.TipoHechoId == (int)eTiposAnexos.DesaparicionForzada_4)
                {
                    gvRPer.FindControl("trDesaparecida").Visible = true;
                }
                if (item.TipoHechoId == (int)eTiposAnexos.Secuestro_8)
                {
                    gvRPer.FindControl("trSecuestrado").Visible = true;
                }
                if (item.TipoHechoId == (int)eTiposAnexos.MinasAntipersonal_7)
                {
                    gvRPer.FindControl("trEstadoPorMina").Visible = true;
                }
                if (item.TipoHechoId == (int)eTiposAnexos.DesplazamientoForzado_5)
                {
                    gvRPer.FindControl("trSeDesplazo").Visible = true;
                }
            }
            i++;
        }
        return herramien;
    }

    /// <summary>
    /// Muestra u oculta las columnas de informacion de la persona si es anexo 13 la vista de las personas quita autores, infracciones, herramientas 
    /// y muestra en forma de grilla los datos de la persona y no de detalle como ocurre para los demas hechos victimizantes.
    /// </summary>
    /// <param name="anexo13"></param>
    /// <param name="gvPersonas"></param>
    private void OcultarMostrar(bool anexo13, GridView gvPersonas)
    {
        for (int i = 0; i < gvPersonas.Columns.Count; i++)
        {
            if (anexo13)
            {
                if (i >= 10 && i <= 13)
                {
                    gvPersonas.Columns[i].Visible = !anexo13;
                }
            }
            else
            {
                if (i >= 1 && i <= 9)
                {
                    gvPersonas.Columns[i].Visible = anexo13;
                }
            }

        }
    }

    /// <summary>
    /// Cuando hay un cambio de estado de una persona se marca el nuevo estado en la entidad y se deja una marca en la casilla indicando el cambio,
    /// Muestra el tooltip del incono imgDetalle con el nuevo estado. Se ejectura al cargar la declaracion y al cambiar estado.
    /// </summary>
    /// <param name="per"></param>
    /// <param name="hecho"></param>
    private void MarcarEstados(clsPersonaAnexo per)
    {
        //foreach (GridViewRow gvhecho in gvHechos.Rows)
        foreach (AccordionPane gvhecho in acHechos.Panes)
        {
            int hechoActual = Convert.ToInt32((gvhecho.FindControl("hfHechoId") as HiddenField).Value);  //DataKeys[gvhecho.DataItemIndex].Value);
            //int hechoActual = Convert.ToInt32(gvHechos.DataKeys[gvhecho.DataItemIndex].Value);
            GridView gvPersonas = (GridView)gvhecho.FindControl("gvPersonasAnexos");

            if (hechoActual == per.ValAnexoId)
            {
                foreach (GridViewRow gvRPer in gvPersonas.Rows)
                {
                    int persona = Convert.ToInt32(gvPersonas.DataKeys[gvRPer.DataItemIndex].Value);
                    if (persona == per.Id)
                    {
                        ImageButton img = (ImageButton)gvRPer.FindControl("imgDetalle");
                        eEstadosValoracionPersona estado = eEstadosValoracionPersona.NoValoradoDevuelto;
                        if (per.EstadoId.HasValue)
                        {
                            // Diego Alvarez - 26/09/2013 - No debe dejar pasar si no se ha seleccionado Observacion
                            if ((per.EstadoId == (int)eEstadosValoracionPersona.NoIncluido || per.EstadoId == (int)eEstadosValoracionPersona.Incluido)
                                && per.ObservacionId.HasValue
                                || !(per.EstadoId == (int)eEstadosValoracionPersona.NoIncluido || per.EstadoId == (int)eEstadosValoracionPersona.Incluido)
                                && !per.ObservacionId.HasValue)
                            {
                                estado = (eEstadosValoracionPersona)Enum.ToObject(typeof(eEstadosValoracionPersona), per.EstadoId.Value);
                                img.ToolTip = "Estado: " + estado.ToString();
                                gvRPer.BorderColor = System.Drawing.ColorTranslator.FromHtml("#00FF15");
                                gvRPer.BorderWidth = 1;
                            }
                            else
                            {
                                img.ToolTip = "No se ha Valorado";
                                gvRPer.BorderColor = System.Drawing.ColorTranslator.FromHtml("#F64040");
                                gvRPer.BorderWidth = 1;
                            }
                        }
                        else
                        {
                            img.ToolTip = "No se ha Valorado";
                            gvRPer.BorderColor = System.Drawing.ColorTranslator.FromHtml("#F64040");
                            gvRPer.BorderWidth = 1;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Carga la información de la persona a la que se le va a capturar el estado de valoración.
    /// </summary>
    /// <param name="sender"></param>
    public void CapturarPersona(object sender)
    {
        var valoracionActual = ObtenerValoracionActual();
        GridView gvPersonasAnexos = (sender as GridView);
        if (gvPersonasAnexos.SelectedValue != null)
        {
            int personaId = Convert.ToInt32(gvPersonasAnexos.SelectedValue);
            int hechoId = Convert.ToInt32(Session[ConstantesItems.VALORACION_ANEXO_ID]);

            List<clsPersonaAnexo> personas = valoracionActual.Hechos.First(h => h.Id == hechoId).Personas.Where(x => x.Id == personaId).ToList();
            /*dvPersonaDetalle.DataSource = personas;
            dvPersonaDetalle.DataBind();
             * Cambiar por pasar a control
             */
            personasDetalle.Persona = personas;
        }

        EstablecerValoracionActual(valoracionActual);
    }

    public void ShowMessage(string sMessage)
    {
        var idValoracion = Request.QSIntegerField("id");
        Master.PopUpGeneral.MostrarBotones = true;
        Master.PopUpGeneral.VisibleBotonCancelar = false;
        Master.PopUpGeneral.MostrarImagen = false;
        Master.PopUpGeneral.Mensaje = sMessage;
        Master.PopUpGeneral.Mostrar("Nueva.aspx?id=" + idValoracion.Value);
        //Master.PopUpGeneral.Mostrar(Request.QueryString["~/Valoracion/Valoracion/Nueva"]);
    }

    [WebMethod]
    public static string Test()
    {
        return DateTime.Now.ToShortDateString();
    }

    /// <summary>
    /// Filtra los Departamentos por el pais
    /// </summary>
    /// <param name="idPais">identificador pais</param>
    /// <returns></returns>
    /// <remarks>s.gutierrez@globant.com    04/04/2013</remarks>
    [WebMethod]
    public static List<clsGeografiaCompleta> ObtenerDepartamentosPorPais(string idPais)
    {
        int id_Pais = Int32.Parse(idPais);

        string cError = string.Empty;
        GeneralService service = new GeneralService();
        List<clsGeografiaCompleta> departamentosFiltrados = service.ObtenerDepartamentosPorPais(id_Pais, ref cError);

        return departamentosFiltrados;
    }

    /// <summary>
    /// Filtra los Municipios por Departamento
    /// </summary>
    /// <param name="idDepar">identificador Departamento</param>
    /// <returns></returns>
    /// <remarks>s.gutierrez@globant.com    04/04/2013</remarks>
    [WebMethod]
    public static List<clsGeografiaCompleta> ObtenerMunicipiosPorDepar(string idDepar)
    {
        int id_Depar = Int32.Parse(idDepar);

        string cError = string.Empty;
        GeneralService service = new GeneralService();
        List<clsGeografiaCompleta> municipiosFiltrados = service.ObtenerMunicipiosPorDepartamento(id_Depar, ref cError);

        return municipiosFiltrados;
    }

    /// <summary>
    /// Filtra las Entidades Puntos de Notificacion por Municipio
    /// </summary>
    /// <param name="idMuni">identificador de Municipio</param>
    /// <returns></returns>
    /// <remarks>s.gutierrez@globant.com    04/04/2013</remarks>
    [WebMethod]
    public static List<clsEntidadMunicipio> ObtenerEntidadesPorMuni(string idMuni)
    {
        if (HttpContext.Current.Session[ConstantesItems.GENERALES_DATOS] != null)
        {
            long? id_Muni = Int32.Parse(idMuni);
            clsListasGeneralesValoracion listasGenerales = (clsListasGeneralesValoracion)HttpContext.Current.Session[ConstantesItems.GENERALES_DATOS];
            List<clsEntidadMunicipio> entidadesPorMuni = listasGenerales.EntidadesMunicipio.Where(x => x.NIdMunicipio == id_Muni).ToList();
            return entidadesPorMuni;
        }
        return null;
    }

    #endregion


    private void CrearActoAdministrativo()
    {

        clsValoracion valoracionActual = ObtenerValoracionActual();

        int ConceptoId = 0;

        ValoracionService objValService = new ValoracionService();
        clsConceptoDeclaracion conceptoDeclaracion = objValService.ObtenerConceptoDeclaracion(valoracionActual.DeclaracionId);
        if (conceptoDeclaracion != null && conceptoDeclaracion.Id_Concepto != null)
            ConceptoId = conceptoDeclaracion.Id_Concepto;

        if (ConceptoId == 0)
        {
            Generar();
        }
        else
        {
            pnlNuevoActoEx.Show();
        }
    }

    private void Generar()
    {
        ValoracionService objValService = new ValoracionService();
        SIRAV.Cliente.Administracion.ClienteUsuario objClienteAdmin = new SIRAV.Cliente.Administracion.ClienteUsuario();
        USUARIO usuarioSirav = objClienteAdmin.ObtenerUsuarioPorToken(Session[ConstantesSesion.USUARIO_ID_LOGIN].ToString());

        clsValoracion valoracionActual = ObtenerValoracionActual();

        Ruv.WebApp.New_Join_SIRAV.Services.ActosAdministrativos ActosAdmin = new Ruv.WebApp.New_Join_SIRAV.Services.ActosAdministrativos();
        SIRAV.Common.Resultado<KeyValuePair<int, string>> resultAA = ActosAdmin.CrearActoAdministrativo(new SIRAV.Entidades.ActosAdmin.DECLARACION() { CODIGO_DECLARACION = valoracionActual.BasicDeclaracion.FirstOrDefault().Formulario, CODIGO_iNTERNO = valoracionActual.DeclaracionId.ToString(), ORIGEN = 2, VALORADOR = usuarioSirav.ID.ToString() });
        if (resultAA.Error == null && resultAA.ClassResult.Key > 0)
        {
            clsConceptoDeclaracion conceptoDeclaracion = new clsConceptoDeclaracion();
            conceptoDeclaracion.Id_Declaracion = valoracionActual.DeclaracionId;
            conceptoDeclaracion.Id_Concepto = resultAA.ClassResult.Key;
            objValService.InsertaConceptoDeclaracion(conceptoDeclaracion);

            RedirigeASirav(resultAA.ClassResult.Key);

            Master.PopUpGeneral.Mensaje = "Se ha guardado correctamente la información";
            Master.PopUpGeneral.MostrarImagen = false;
            Master.PopUpGeneral.MostrarBotones = true;
            Master.PopUpGeneral.VisibleBotonCancelar = false;
            Master.PopUpGeneral.Mostrar("Default.aspx");

        }
        else
        {
            if (resultAA.Error.Message.StartsWith("1"))
            {
                /*
                int ConceptoId = 0;
                
                clsConceptoDeclaracion conceptoDeclaracion = objValService.ObtenerConceptoDeclaracion(valoracionActual.DeclaracionId);
                if (conceptoDeclaracion != null && conceptoDeclaracion.Id_Concepto != null)
                    ConceptoId = conceptoDeclaracion.Id_Concepto;

                if (ConceptoId > 0)
                    RedirigeASirav(ConceptoId);
                */
                Master.PopUpGeneral.Mensaje = "Se ha guardado correctamente la información";
                Master.PopUpGeneral.MostrarImagen = false;
                Master.PopUpGeneral.MostrarBotones = true;
                Master.PopUpGeneral.VisibleBotonCancelar = false;
                Master.PopUpGeneral.Mostrar("Default.aspx");
            }
        }
    }

    private void RedirigeASirav(int ConceptoId)
    {
        string urlSirav = ConfigurationManager.AppSettings["UrlSirav"].ToString();
        string urlInterna = string.Format("~/Valoracion/Modificar.aspx?idConcepto={0}", ConceptoId);
        string tokenApp = Varios.TokenApp();
        string tokenUsuario = Varios.Token();

        string _open = "<script>window.open('" + string.Format(@"{0}/Externo.aspx?TUF={1}&TUA={2}&URL={3}", urlSirav, tokenUsuario, tokenApp, urlInterna) + "', '_blank');</script>";
        //Response.Write(_open);
        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, false);
    }

    protected void mpopNuevoActo_Ok(object sender, EventArgs e)
    {
        Generar();
    }

    protected void mpopNuevoActo_Cancel(object sender, EventArgs e)
    {
        /*
        clsValoracion valoracionActual = ObtenerValoracionActual();
        int ConceptoId = 0;
        
        ValoracionService objValService = new ValoracionService();
        clsConceptoDeclaracion conceptoDeclaracion = objValService.ObtenerConceptoDeclaracion(valoracionActual.DeclaracionId);
         * */
        /* Buscar el ultimo ConceptoAsociado a la declaracion */
        /*
        if (conceptoDeclaracion != null && conceptoDeclaracion.Id_Concepto != null)
            ConceptoId = conceptoDeclaracion.Id_Concepto;
        if (ConceptoId > 0)
            RedirigeASirav(ConceptoId);
        */
        Master.PopUpGeneral.Mensaje = "Se ha guardado correctamente la información";
        Master.PopUpGeneral.MostrarImagen = false;
        Master.PopUpGeneral.MostrarBotones = true;
        Master.PopUpGeneral.VisibleBotonCancelar = false;
        Master.PopUpGeneral.Mostrar("Default.aspx");
    }
}