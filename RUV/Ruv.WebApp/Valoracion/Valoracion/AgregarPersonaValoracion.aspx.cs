using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.WebApp.Common;
using System.Configuration;
using System.IO;
using msg = Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;

public partial class Valoracion_Valoracion_AgregarPersonaValoracion : PaginaBase
{

    #region Public Properties
    
    public int? IdDeclaracion {
        get {
            return Request.QSIntegerField("IdDeclaracion");
        }
    }

    #endregion

    #region Event Handlers
    
    protected void Page_Load(object sender, EventArgs e) {

    }

    #endregion

    protected void btnAceptar_Click(object sender, EventArgs e) {

        if (!IdDeclaracion.HasValue) {
            ModalPopUp.MostrarMensaje("Aviso", "No existe información de declaración");
            return;
        }

        // Validaciones
        var requiredValidationError = string.Empty;
        if (string.IsNullOrWhiteSpace(txbPrimerNombre.Text))
            requiredValidationError += "Debe ingresar el primer nombre";
        if (string.IsNullOrWhiteSpace(txbPrimerApellido.Text))
            requiredValidationError += "Debe ingresar el primer apellido";
        if (string.IsNullOrWhiteSpace(ComentariosPersonaAgrgada.Text))
            requiredValidationError += "Debe ingresar los comentarios de adición de la persona";

        if (requiredValidationError == string.Empty) {
            
            var persona = new clsAgregarPersonaValoracion {
                cPrimerNombre = txbPrimerNombre.Text.Trim(),
                cSegundoNombre = txbSegundoNombre.Text.Trim(),
                cPrimerApellido = txbPrimerApellido.Text.Trim(),
                cSegundoApellido = txbSegundoApellido.Text.Trim(),
                cNumeroDocumento = txbNumeroDocumento.Text.Trim(),
                cDireccion = txbDireccion.Text.Trim(),
                cCorreoelectronico = txbCorreoElectronico.Text.Trim(),
                cTelefono = txbTelefono.Text.Trim(),
                cComunidad = txbComunidad.Text,
                lnDiscapacidad = new List<int>()
            };

            int tipoDocumentoValue = 0;
            if (int.TryParse(ddlTipoDocumento.SelectedValue.Trim(), out tipoDocumentoValue) && tipoDocumentoValue > 0)
                persona.nTipoDocumento = tipoDocumentoValue;

            DateTime fechaNacimientoValue = DateTime.MinValue;
            if (DateTime.TryParseExact(txbFechaNacimiento.Text.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fechaNacimientoValue))
                persona.cFechanacimiento = fechaNacimientoValue;

            int relacionValue = 0;
            if (int.TryParse(ddlRelacionFamiliar.SelectedValue.Trim(), out relacionValue) && relacionValue > 0)
                persona.nRelacion = relacionValue;

            int generoValue = 0;
            if (int.TryParse(ddlGenero.SelectedValue.Trim(), out generoValue) && generoValue > 0)
                persona.nGenero = generoValue;

            int etniaValue = 0;
            if (int.TryParse(ddlEtnia.SelectedValue.Trim(), out etniaValue) && etniaValue > 0)
                persona.nEtnia = etniaValue;

            int estadoCivilValue = 0;
            if (int.TryParse(ddlEstadoCivil.SelectedValue.Trim(), out estadoCivilValue) && estadoCivilValue > 0)
                persona.nEstadoCivil = estadoCivilValue;

            if (cblDiscapacidades.Seleccionados != null) {
                persona.lnDiscapacidad = cblDiscapacidades.Seleccionados;
            }

            if (persona.nGenero == 126) {
                
                int gestanteLactanteValue = 0;
                if (int.TryParse(rblGestanteLactante.SelectedValue, out gestanteLactanteValue))
                    persona.nGestante = gestanteLactanteValue;

                int mujerCabezaHogarValue = 0;
                if (int.TryParse(rblMujerCabezaHogar.SelectedValue, out mujerCabezaHogarValue))
                    persona.nCabezaHogar = mujerCabezaHogarValue;
            }

            persona.cComentarios = ComentariosPersonaAgrgada.Text.Trim();
            persona.nIdDeclaracion = IdDeclaracion.Value;
            //agregaper.cFechaAgregado = txbFechaAgragado.Text;

            var service = new ValoracionService();
            string cError = string.Empty;
            var result = service.AgregarPersonaService(persona, ref cError);

            if (result && cError == string.Empty) {
                int idValoracion = service.ObtenerIdValoracionporIdDeclaracionServ(IdDeclaracion.Value, ref cError);
                ModalPopUp.MostrarMensajeYRedirigir("Mensaje", "La persona fué agregada exitosamente", "Nueva.aspx?id=" + idValoracion);
            }
            else {
                ModalPopUp.MostrarMensaje("Error", "La persona no pudo ser adicionada a la declaración: " + cError);
            }
        }

        if (!string.IsNullOrEmpty(requiredValidationError))
            ModalPopUp.MostrarMensaje("Error", requiredValidationError);

    }

    protected void btnCancelar_Click(object sender, EventArgs e) {
        if (!IdDeclaracion.HasValue) {
            ModalPopUp.MostrarMensaje("Aviso", "No existe información de declaración");
            return;
        }

        var service = new ValoracionService();
        string cError = string.Empty;
        int idValoracion = service.ObtenerIdValoracionporIdDeclaracionServ(IdDeclaracion.Value, ref cError);
        Response.Redirect("Nueva.aspx?id=" + idValoracion);
    }

    protected void btnSubirImagen_Click(object sender, EventArgs e)
    {
        
        var path = Request.QSIntegerField("IdDeclaracion");
        var truthpath = ConfigurationManager.AppSettings["PathArchivosPersonasAgregadasValoracion"] + path.ToString();
        var directory = new DirectoryInfo(truthpath);

        if (directory.Exists == false)
        {
            directory.Create();
            if (fuCargarImagen.HasFile)
            {
                try
                {

                    string fileName = NextAvailableFilename(truthpath + "/" + path.ToString() + Path.GetExtension(fuCargarImagen.FileName));
                    string file = Path.Combine(truthpath, fileName);
                    fuCargarImagen.SaveAs(file);
                    ModalPopUp.MostrarMensaje(msg::Controles.Exito, "La imagen fue cargada Exitosamente");
                }
                catch (System.Web.HttpException ex)
                {
                    RegistroTraza.I.Registrar(ex);
                    ModalPopUp.MostrarMensaje(msg::Controles.Error,
                        string.Format(msg::Errores.General, ex.Message));
                    return;
                }
            }
        }
        else
        {
            if (fuCargarImagen.HasFile)
            {
                try
                {

                    string fileName = NextAvailableFilename(truthpath + "/" + path.ToString() + Path.GetExtension(fuCargarImagen.FileName)); 
                    string file = Path.Combine(truthpath, fileName);
                    fuCargarImagen.SaveAs(file);
                    ModalPopUp.MostrarMensaje(msg::Controles.Exito, "La imagen fue cargada Exitosamente");
                }
                catch (System.Web.HttpException ex)
                {
                    RegistroTraza.I.Registrar(ex);
                    ModalPopUp.MostrarMensaje(msg::Controles.Error,
                        string.Format(msg::Errores.General, ex.Message));
                    return;
                }
            }
        }
    }

    private static string numberPattern = " ({0})";

    public static string NextAvailableFilename(string path)
    {
        // Short-cut if already available
        if (!File.Exists(path))
            return path;

        // If path has extension then insert the number pattern just before the extension and return next filename
        if (Path.HasExtension(path))
            return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), numberPattern));

        // Otherwise just append the pattern to the path and return next filename
        return GetNextFilename(path + numberPattern);
    }

    private static string GetNextFilename(string pattern)
    {
        string tmp = string.Format(pattern, 1);
        if (tmp == pattern)
        {
            ArgumentException argumentException = new ArgumentException("The pattern must include an index place-holder", "pattern");
            RegistroTraza.I.Registrar(argumentException);
            throw argumentException;
        }

        if (!File.Exists(tmp))
            return tmp; // short-circuit if no matches

        int min = 1, max = 2; // min is inclusive, max is exclusive/untested

        while (File.Exists(string.Format(pattern, max)))
        {
            min = max;
            max *= 2;
        }

        while (max != min + 1)
        {
            int pivot = (max + min) / 2;
            if (File.Exists(string.Format(pattern, pivot)))
                min = pivot;
            else
                max = pivot;
        }

        return string.Format(pattern, max);
    }

}