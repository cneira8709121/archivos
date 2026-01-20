using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Correcciones;
using dto = Ruv.Business.DTO.Correcciones;
using resx = Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using System.Globalization;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using System.IO;
using System.Configuration;

public partial class Correcciones_SolicitudCorreccion : PaginaBase
{
    #region Propiedades

    private clsCargaDatosCorreccion DatoSinCorreccion
    {
        get
        {
            CorreccionesService serv = new CorreccionesService();
            string cError = string.Empty;
            clsCargaDatosCorreccion datoSinCorreccion = new clsCargaDatosCorreccion();

            List<clsCargaDatosCorreccion> lstDatosCorreccion = serv.CargaDatosCorreccion(int.Parse(Request.QueryString["id"]), ref cError);

            if (!string.IsNullOrEmpty(cError))
            {
                ModalPopUp.MostrarMensaje("Error", cError);
            }
            else
            {
                if (lstDatosCorreccion != null && lstDatosCorreccion.Count > 0)
                {
                    datoSinCorreccion = lstDatosCorreccion.FirstOrDefault();
                }
            }

            return datoSinCorreccion;
        }
    }

    #endregion Propiedades

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
 
        Master.UrlCurrenPage = Request.Url.AbsolutePath;
        Master.ValidarPermisoPagina();
        //Master.QuitarMenus(new string[] { "Valorar", "Resumen" });

        if (Request.QueryString["id"] == null) Response.Redirect("../Default.aspx");

        if (!IsPostBack)
        {
            CorreccionesService serv = new CorreccionesService();
            string cError = string.Empty;
            List<clsCargaDatosCorreccion> lstDatosCorreccion = serv.CargaDatosCorreccion(int.Parse(Request.QueryString["id"]), ref cError);

            if (!string.IsNullOrEmpty(cError)) ModalPopUp.MostrarMensaje("Error", cError);
            else
            {
                if (lstDatosCorreccion != null && lstDatosCorreccion.Count > 0)
                {
                    clsCargaDatosCorreccion datosCorreccion = lstDatosCorreccion.FirstOrDefault();
                    txbPrimerNombre.Text = datosCorreccion.CPrimerNombre;
                    txbSegundoNombre.Text = datosCorreccion.CSegundoNombre;
                    txbPrimerApellido.Text = datosCorreccion.CPrimerApellido;
                    txbSegundoApellido.Text = datosCorreccion.CSegundoApellido;
                    ddlTipoDocumento.SelectedValue = datosCorreccion.NTipoDocumento.ToString();
                    txbNumeroDocumento.Text = datosCorreccion.CNumeroDocumento == null ? null : datosCorreccion.CNumeroDocumento.ToString();
                    txbFechaNacimiento.Text = datosCorreccion.DNacimiento.ToShortDateString();
                    txbDireccion.Text = datosCorreccion.CDireccion;
                    txbTelefono.Text = datosCorreccion.CTelefono;
                    txbCorreoElectronico.Text = datosCorreccion.CCorreo;
                    ddlGenero.SelectedValue = datosCorreccion.NGenero.ToString();
                    ddlEtnia.SelectedValue = datosCorreccion.NEtnia.ToString();
                    cblDiscapacidades.Seleccionados = datosCorreccion.LstDiscapacidad == null ? new List<int>() : datosCorreccion.LstDiscapacidad;
                }
            }
        }
    }

    protected void chkChackedChanged(object sender, EventArgs e)
    {
        if (sender.GetType() != typeof(CheckBox)) return;
        CheckBox chkSender = (CheckBox)sender;
        if (chkSender == chkPrimerNombre) txbPrimerNombre.Enabled                = !txbPrimerNombre.Enabled;
        else if (chkSender == chkSegundoNombre) txbSegundoNombre.Enabled         = !txbSegundoNombre.Enabled;
        else if (chkSender == chkPrimerApellido) txbPrimerApellido.Enabled       = !txbPrimerApellido.Enabled;
        else if (chkSender == chkSegundoApellido) txbSegundoApellido.Enabled     = !txbSegundoApellido.Enabled;
        else if (chkSender == chkTipoDocumento) ddlTipoDocumento.Enabled         = !ddlTipoDocumento.Enabled;
        else if (chkSender == chkNumeroDocumento) txbNumeroDocumento.Enabled     = !txbNumeroDocumento.Enabled;
        else if (chkSender == chkFechaNacimento) txbFechaNacimiento.Enabled      = !txbFechaNacimiento.Enabled;
        else if (chkSender == chkDireccion) txbDireccion.Enabled                 = !txbDireccion.Enabled;
        else if (chkSender == chkTelefono) txbTelefono.Enabled                   = !txbTelefono.Enabled;
        else if (chkSender == chkCorreoElectronico) txbCorreoElectronico.Enabled = !txbCorreoElectronico.Enabled;
        else if (chkSender == chkGenero) ddlGenero.Enabled                       = !ddlGenero.Enabled;
        else if (chkSender == chkEtnia) ddlEtnia.Enabled                         = !ddlEtnia.Enabled;
        else if (chkSender == chkDiscapacidades) cblDiscapacidades.Enabled       = (!cblDiscapacidades.Enabled);
        else if (chkSender == chkFallecido)
        {
            chkEsFallecido.Enabled = !chkEsFallecido.Enabled;
            txtNroRegDefuncion.Enabled = !txtNroRegDefuncion.Enabled;
            if (chkEsFallecido.Checked)
            {
                if (chkFallecido.Checked)
                    txtNroRegDefuncion.Enabled = true;
            }
            else
            {
                txtNroRegDefuncion.Text = string.Empty;
                txtNroRegDefuncion.Enabled = false;
            }
        }
        //{
        //    if (cblDiscapacidades.SelectedItem.Value == "Ninguna") cblDiscapacidades.Enabled = (!cblDiscapacidades.Enabled);
        //}
    }

    protected void btnClick(object sender, EventArgs e)
    {
        if (sender.GetType() != typeof(Button)) return;
        Button btnSender = (Button)sender;
        if (btnSender == btnAceptar)
        {
            clsCargaDatosCorreccion datoSinCorreccion = DatoSinCorreccion;

            List<dto::clsCorreccion> lstCorreccion = new List<dto::clsCorreccion>();
            if (chkPrimerNombre.Checked && HayCambio(eCamposCorreccion.PrimerNombre, txbPrimerNombre.Text)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.PrimerNombre.GetHashCode(), Valor = txbPrimerNombre.Text });
            if (chkSegundoNombre.Checked && HayCambio(eCamposCorreccion.SegundoNombre, txbSegundoNombre.Text)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.SegundoNombre.GetHashCode(), Valor = txbSegundoNombre.Text });
            if (chkPrimerApellido.Checked && HayCambio(eCamposCorreccion.PrimerApellido, txbPrimerApellido.Text)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.PrimerApellido.GetHashCode(), Valor = txbPrimerApellido.Text });
            if (chkSegundoApellido.Checked && HayCambio(eCamposCorreccion.SegundoApellido, txbSegundoApellido.Text)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.SegundoApellido.GetHashCode(), Valor = txbSegundoApellido.Text });
            if (chkTipoDocumento.Checked && HayCambio(eCamposCorreccion.TipoDocumento, ddlTipoDocumento.SelectedValue)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.TipoDocumento.GetHashCode(), Valor = ddlTipoDocumento.SelectedValue });
            if (chkNumeroDocumento.Checked && HayCambio(eCamposCorreccion.Documento, txbNumeroDocumento.Text)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.Documento.GetHashCode(), Valor = txbNumeroDocumento.Text });

            // Diego Alvarez - 03/10/2013 - Validación para que no permita guardar si el formato de fecha no es correcto
            DateTime validDate;
            if (!DateTime.TryParseExact(txbFechaNacimiento.Text, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-us"), DateTimeStyles.None, out validDate))
            {
                txbFechaNacimiento.EsValidaLaFecha = false;
                return;
            }
            else
            {
                //COR 3 se modifica el formato de la fecha año de 4
                //if (chkFechaNacimento.Checked && HayCambio(eCamposCorreccion.FechaNacimiento, txbFechaNacimiento.Text)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.FechaNacimiento.GetHashCode(), Valor = DateTime.Parse(txbFechaNacimiento.Text).ToString("dd-MMM-yy", CultureInfo.CreateSpecificCulture("en-us")) });
                if (chkFechaNacimento.Checked && HayCambio(eCamposCorreccion.FechaNacimiento, txbFechaNacimiento.Text)) 
                    lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.FechaNacimiento.GetHashCode(), Valor = DateTime.Parse(txbFechaNacimiento.Text).ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-us")) });
            }
            if (chkDireccion.Checked && HayCambio(eCamposCorreccion.Direccion, txbDireccion.Text)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.Direccion.GetHashCode(), Valor = txbDireccion.Text });
            if (chkTelefono.Checked && HayCambio(eCamposCorreccion.Telefono, txbTelefono.Text)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.Telefono.GetHashCode(), Valor = txbTelefono.Text });
            if (chkCorreoElectronico.Checked && HayCambio(eCamposCorreccion.CorreoElectronico, txbTelefono.Text)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.CorreoElectronico.GetHashCode(), Valor = txbCorreoElectronico.Text });
            if (chkGenero.Checked && HayCambio(eCamposCorreccion.Genero, ddlGenero.SelectedValue)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.Genero.GetHashCode(), Valor = ddlGenero.SelectedValue });
            if (chkEtnia.Checked && HayCambio(eCamposCorreccion.Etnia, ddlEtnia.SelectedValue)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.Etnia.GetHashCode(), Valor = ddlEtnia.SelectedValue });
            if (chkEtnia.Checked && HayCambio(eCamposCorreccion.Etnia, ddlEtnia.SelectedValue) && HayCambio(eCamposCorreccion.SubEtnia, ddlSubEtnia.SelectedValue)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.SubEtnia.GetHashCode(), Valor = ddlSubEtnia.SelectedValue });
            if (chkDiscapacidades.Checked && HayCambio(eCamposCorreccion.Discapacidades, cblDiscapacidades.Seleccionados)) lstCorreccion.Add(new dto::clsCorreccion { Campo = eCamposCorreccion.Discapacidades.GetHashCode(), Valor = cblDiscapacidades.Seleccionados == null ? null : string.Join("|", cblDiscapacidades.Seleccionados.Select(x => x)) });


            CorreccionesService serv = new CorreccionesService();
            string cError = string.Empty;
            
            if (lstCorreccion.Count == 0)
            {
                ModalPopUp.MostrarMensaje("Advertencia", string.Format("{0} ó {1}", Informacion.NoDatosParaAccion, Informacion.NoCambios));
                return;
            }

            int idCorreccion = serv.SolicitarCorreccionOut(int.Parse(Request.QueryString["id"]), RUV.Current.Usuario.Id, lstCorreccion, ref cError);
            if (idCorreccion == 0 || !string.IsNullOrEmpty(cError))
                ModalPopUp.MostrarMensaje("Error", cError);
            else
            {
                if (fuAdjunto.HasFile)
                {
                    try
                    {
                        string ruta = ConfigurationManager.AppSettings["PathArchivosCorrecciones"];
                        string extensionArchivo = Path.GetExtension(fuAdjunto.FileName);
                        fuAdjunto.SaveAs(ruta + idCorreccion.ToString() + extensionArchivo);
                    }
                    catch (Exception ex)
                    {
                        RegistroTraza.I.Registrar(ex);
                        ModalPopUp.MostrarMensaje("Error", Errores.ErrorAdjuntandoArchivo + " - " + ex.Message);
                    }
                }

                // Diego Alvarez - 10/10/2013 - se agrega parámetro para aplicar los últimos filtros seleccionados
                ModalPopUp.MostrarMensajeYRedirigir("Éxito", resx::Informacion.CambiosGuardados, Request.QueryString["urlEvio"] + "?AplicarFiltros=true");
            }
        }
        else if (btnSender == btnCancelar)
        {
            // Diego Alvarez - 10/10/2013 - se agrega parámetro para aplicar los últimos filtros seleccionados
            Response.Redirect(Request.QueryString["urlEvio"] + "?AplicarFiltros=true");
        }
    }

    protected void ddlEtnia_SelectIndexChange(object sender, EventArgs e) 
    {
        int etniaSelectedId = 0;
        if (int.TryParse(ddlEtnia.SelectedValue, out etniaSelectedId)) 
        {
            if (etniaSelectedId > 0) 
            {
                // Obtener el número de la etnia
                var dropDownObject = (object)ddlSubEtnia.DropDownList;
                DataSourceGeneral.PoblarControl(ref dropDownObject, Poblar.SubEtnias, etniaSelectedId.ToString());
                ddlSubEtnia.Enabled = true;
                return;
            }
        }
        ddlSubEtnia.SelectedValue = "0";
        ddlSubEtnia.Enabled = false;
    }

    protected void cblSelectIndexChange(object sender, EventArgs e)
    {
        if (sender.GetType() != typeof(CheckBoxList)) return;

        if (cblDiscapacidades.Seleccionados.Count <= 0)
        {
            foreach (ListItem ctrl in cblDiscapacidades.Items)
            {
                ctrl.Enabled = true;
            }
        }
        else
        {
            if (cblDiscapacidades.Seleccionados.Contains(eDiscapacidades.Ninguna.GetHashCode()))
            {
                foreach (ListItem ctrl in cblDiscapacidades.Items)
                {
                    if (int.Parse(ctrl.Value) == eDiscapacidades.Ninguna.GetHashCode()) continue;
                    ctrl.Enabled = false;
                }
            }
            else
            {
                cblDiscapacidades.Items.FindByValue(eDiscapacidades.Ninguna.GetHashCode().ToString()).Enabled = false;
            }
        }
    }

    #endregion Eventos

    #region Funciones

    private bool HayCambio(eCamposCorreccion campo, object valor)
    {
        clsCargaDatosCorreccion datoSinCorreccion = DatoSinCorreccion;
        bool cambio = false;

        switch (campo)
        {
            case eCamposCorreccion.PrimerNombre:
                if (txbPrimerNombre.Text != (datoSinCorreccion.CPrimerNombre == null ? string.Empty : datoSinCorreccion.CPrimerNombre))
                    cambio = true;
                break;
            case eCamposCorreccion.SegundoNombre:
                if (txbSegundoNombre.Text != (datoSinCorreccion.CSegundoNombre == null ? string.Empty : datoSinCorreccion.CSegundoNombre))
                    cambio = true;
                break;
            case eCamposCorreccion.PrimerApellido:
                if (txbPrimerApellido.Text != (datoSinCorreccion.CPrimerApellido == null ? string.Empty : datoSinCorreccion.CPrimerApellido))
                    cambio = true;
                break;
            case eCamposCorreccion.SegundoApellido:
                if (txbSegundoApellido.Text != (datoSinCorreccion.CSegundoApellido == null ? string.Empty : datoSinCorreccion.CSegundoApellido))
                    cambio = true;
                break;
            case eCamposCorreccion.TipoDocumento:
                if (ddlTipoDocumento.SelectedValue != datoSinCorreccion.NTipoDocumento.ToString())
                    cambio = true;
                break;
            case eCamposCorreccion.Documento:
                if (txbNumeroDocumento.Text != datoSinCorreccion.CNumeroDocumento)
                    cambio = true;
                break;
            case eCamposCorreccion.FechaNacimiento:
                if (txbFechaNacimiento.Text != datoSinCorreccion.DNacimiento.ToShortDateString())
                    cambio = true;
                break;
            case eCamposCorreccion.Genero:
                if (ddlGenero.SelectedValue != datoSinCorreccion.NGenero.ToString())
                    cambio = true;
                break;
            case eCamposCorreccion.Etnia:
                if (ddlEtnia.SelectedValue != datoSinCorreccion.NEtnia.ToString())
                    cambio = true;
                break;
            case eCamposCorreccion.SubEtnia:
                if (ddlSubEtnia.SelectedValue != datoSinCorreccion.NSubetnia.ToString())
                    cambio = true;
                break;
            case eCamposCorreccion.Discapacidades:
                if (!cblDiscapacidades.Seleccionados.SequenceEqual(datoSinCorreccion.LstDiscapacidad))
                    cambio = true;
                break;
            case eCamposCorreccion.Direccion:
                if (txbDireccion.Text != (datoSinCorreccion.CDireccion == null ? string.Empty : datoSinCorreccion.CDireccion))
                    cambio = true;
                break;
            case eCamposCorreccion.Telefono:
                if (txbTelefono.Text != (datoSinCorreccion.CTelefono == null ? string.Empty : datoSinCorreccion.CTelefono))
                    cambio = true;
                break;
            case eCamposCorreccion.CorreoElectronico:
                if (txbCorreoElectronico.Text != datoSinCorreccion.CCorreo)
                    cambio = true;
                break;
        /*    case eCamposCorreccion.Fallecido:
                if (chkEsFallecido.Checked != datoSinCorreccion)
                    cambio = true;
                break;*/
            default:
                break;
        }

        return cambio;
    }

    #endregion funciones

    protected void chkEsFallecido_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEsFallecido.Checked)
            txtNroRegDefuncion.Enabled = true;
        else
        {
            txtNroRegDefuncion.Text = string.Empty;
            txtNroRegDefuncion.Enabled = false;
        }
    }
}