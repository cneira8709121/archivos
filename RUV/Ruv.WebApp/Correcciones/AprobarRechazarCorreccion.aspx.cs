using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using entidad = Ruv.Infrastructure.Crosscutting.Common.Entidades.Correcciones;
using Ruv.Infrastructure.Crosscutting.Common;
using System.IO;
using Ruv.Infrastructure.Crosscutting.Common.General;

public partial class Correcciones_AprobarRechazarCorreccion : PaginaBase
{
    #region Propiedades

    private int IdCorreccion
    {
        get
        {
            if (Session[ConstantesCorrecciones.IdCorreccion] == null)
                Session[ConstantesCorrecciones.IdCorreccion] = 0;

            return (int)Session[ConstantesCorrecciones.IdCorreccion];
        }
        set
        {
            Session[ConstantesCorrecciones.IdCorreccion] = value;
        }
    }

    private int IdRegPersona
    {
        get
        {
            if (Session[ConstantesCorrecciones.IdRegPersona] == null)
                Session[ConstantesCorrecciones.IdRegPersona] = 0;

            return (int)Session[ConstantesCorrecciones.IdRegPersona];
        }
        set
        {
            Session[ConstantesCorrecciones.IdRegPersona] = value;
        }
    }

    entidad::clsCargaDatosCorreccion DatosCorreccionActuales 
    {
        get
        {
            if (Session[ConstantesCorrecciones.DatosCorreccionActuales] == null)
                Session[ConstantesCorrecciones.DatosCorreccionActuales] = new entidad::clsCargaDatosCorreccion();

            return (entidad::clsCargaDatosCorreccion)Session[ConstantesCorrecciones.DatosCorreccionActuales];
        }
        set
        {
            Session[ConstantesCorrecciones.DatosCorreccionActuales] = value;
        }
    }

    entidad::clsCargaDatosCorreccion DatosCorreccionNuevos
    {
        get
        {
            if (Session[ConstantesCorrecciones.DatosCorreccionNuevos] == null)
                Session[ConstantesCorrecciones.DatosCorreccionNuevos] = new entidad::clsCargaDatosCorreccion();

            return (entidad::clsCargaDatosCorreccion)Session[ConstantesCorrecciones.DatosCorreccionNuevos];
        }
        set
        {
            Session[ConstantesCorrecciones.DatosCorreccionNuevos] = value;
        }
    }

    IList<entidad::clsCorreccion> CamposCorreccionNuevos
    {
        get
        {
            if (Session[ConstantesCorrecciones.CamposCorreccionNuevos] == null)
                Session[ConstantesCorrecciones.CamposCorreccionNuevos] = new List<entidad::clsCorreccion>();

            return (List<entidad::clsCorreccion>)Session[ConstantesCorrecciones.CamposCorreccionNuevos];
        }
        set
        {
            Session[ConstantesCorrecciones.CamposCorreccionNuevos] = value;
        }
    }

    private enum Resultado
    {
        exitoso = 1,
        fallido = 0
    }

    #endregion Propiedades

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IdCorreccion = int.Parse(Request.QueryString["idCorreccion"]);
            IdRegPersona = int.Parse(Request.QueryString["idRegPersona"]);

            string cErrorActuales = string.Empty;
            string cErrorNuevos = string.Empty;

            CorreccionesService service = new CorreccionesService();

            DatosCorreccionActuales = service.CargaDatosCorreccion(IdRegPersona, ref cErrorActuales).FirstOrDefault();
            CargarDatosActuales();

            DatosCorreccionNuevos = service.ConsultarCorreccion(IdCorreccion, ref cErrorNuevos);
            CargarDatosNuevos();

            CamposCorreccionNuevos = service.ConsultarCamposCorreccion(IdCorreccion, ref cErrorNuevos);

            EstablecerNumeroCorrecciones(CamposCorreccionNuevos);

            if (ExisteArchivoAdjunto())
                btnDescargarAdjunto.Enabled = true;
            else
                btnDescargarAdjunto.Enabled = false;
        }
    }

    protected void btnAceptarCorreccion_Click(object sender, EventArgs e)
    {
        CorreccionesService service = new CorreccionesService();
        string cError = string.Empty;

        if (service.AprobarCorreccion(IdCorreccion, RUV.Current.Usuario.ID, txtObservaciones.Text, ref cError))
        {
            ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Exito, 
                        Ruv.Infrastructure.Crosscutting.Resources.Globalization.Informacion.CambiosGuardados, 
                        Resultado.exitoso);
        }
        else
        {
            ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error,
                        string.Format(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Errores.General, cError), 
                        Resultado.fallido);
        }
    }

    protected void btnRechazarCorreccion_Click(object sender, EventArgs e)
    {
        
        if (txtObservaciones.Text == string.Empty || txtObservaciones.Text == null)
        {
            ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Advertencia, Ruv.Infrastructure.Crosscutting.Resources.Globalization.Advertencia.DiligenciarObservacion, Resultado.fallido, false);
            return;
        }

        else
        {
            CorreccionesService service = new CorreccionesService();
            string cError = string.Empty;

            if (service.RechazarCorreccion(IdCorreccion, RUV.Current.Usuario.ID, txtObservaciones.Text, ref cError))
            {
                ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Exito,
                            Ruv.Infrastructure.Crosscutting.Resources.Globalization.Informacion.CambiosGuardados,
                            Resultado.exitoso);
            }
            else
            {
                ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error,
                            string.Format(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Errores.General, cError),
                            Resultado.exitoso);
            }
        }
    }

    protected void btnDescargarAdjunto_Click(object sender, EventArgs e)
    {
        string ruta = System.Configuration.ConfigurationManager.AppSettings["PathArchivosCorrecciones"];
        string nombreArchivo = IdCorreccion.ToString();
        string extension = string.Empty;

        string[] archivosEnDirectorio = Directory.GetFiles(ruta, nombreArchivo + ".*");

        if (archivosEnDirectorio.Count() > 0)
            extension = System.IO.Path.GetExtension(archivosEnDirectorio.First());
        else
            return;

        if (File.Exists(ruta + nombreArchivo + extension))
        {
            Response.Clear();
            Response.ContentType = "application/" +  extension.Replace(".", "");
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + extension);
            Response.WriteFile(ruta + nombreArchivo + extension);
            Response.Flush();
            Response.End();
        }
    }

    #endregion Eventos

    #region Funciones

    private void EstablecerNumeroCorrecciones(IList<entidad::clsCorreccion> camposNuevos)
    {
        foreach (Ruv.Infrastructure.Crosscutting.Common.eCamposCorreccion campoCorreccion in Enum.GetValues(typeof(Ruv.Infrastructure.Crosscutting.Common.eCamposCorreccion)))
        {
            if (!camposNuevos.Select(x => x.Campo).ToList().Contains((int)campoCorreccion))
            {
                OcultarCampo(campoCorreccion);
            }
        }
    }

    private void OcultarCampo(eCamposCorreccion eCampoCorreccion)
    {
        if (eCampoCorreccion == eCamposCorreccion.CorreoElectronico)
        {
            txbCorreoElectronico.Visible = false;
            txbCorreoElectronico0.Visible = false;
            lblCorreoElectronico.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.Direccion)
        {
            txbDireccion.Visible = false;
            txbDireccion0.Visible = false;
            lblDireccion.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.PrimerApellido)
        {
            txbPrimerApellido.Visible = false;
            txbPrimerApellido0.Visible = false;
            lblPrimerApellido.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.PrimerNombre)
        {
            txbPrimerNombre.Visible = false;
            txbPrimerNombre0.Visible = false;
            lblPrimerNombre.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.SegundoApellido)
        {
            txbSegundoApellido.Visible = false;
            txbSegundoApellido0.Visible = false;
            lblSegundoApellido.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.SegundoNombre)
        {
            txbSegundoNombre.Visible = false;
            txbSegundoNombre0.Visible = false;
            lblSegundoNombre.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.Telefono)
        {
            txbTelefono.Visible = false;
            txbTelefono0.Visible = false;
            lblTelefono.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.FechaNacimiento)
        {
            txtFechaNacimiento.Visible = false;
            txtFechaNacimiento0.Visible = false;
            lblFechaNacimiento.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.Discapacidades)
        {
            ChkBoxListDiscpacidades.Visible = false;
            ChkBoxListDiscpacidades0.Visible = false;
            lblDiscapacidades.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.Etnia)
        {
            txtEtnia.Visible = false;
            txtEtnia0.Visible = false;
            lblEtnia.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.SubEtnia)
        {
            txtEtnia.Visible = false;
            txtEtnia0.Visible = false;
            lblEtnia.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.Genero)
        {
            txtGenero.Visible = false;
            txtGenero0.Visible = false;
            lblGenero.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.Documento)
        {
            txbNumeroDocumento.Visible = false;
            txbNumeroDocumento0.Visible = false;
            lblNumeroDocumento.Visible = false;
        }
        if (eCampoCorreccion == eCamposCorreccion.TipoDocumento)
        {
            txtTipoDocumento.Visible = false;
            txtTipoDocumento0.Visible = false;
            lblTipoDocumento.Visible = false;
        }
    }

    private void CargarDatosActuales()
    {
        txbPrimerNombre.Text = DatosCorreccionActuales.CPrimerNombre;
        txbSegundoNombre.Text = DatosCorreccionActuales.CSegundoNombre;
        txbPrimerApellido.Text = DatosCorreccionActuales.CPrimerApellido;
        txbSegundoApellido.Text = DatosCorreccionActuales.CSegundoApellido;
        txtTipoDocumento.Text = NombreTipoDocumento(DatosCorreccionActuales.NTipoDocumento);
        txbNumeroDocumento.Text = DatosCorreccionActuales.CNumeroDocumento;
        txtFechaNacimiento.Text = DatosCorreccionActuales.DNacimiento.ToShortDateString();
        txbDireccion.Text = DatosCorreccionActuales.CDireccion;
        txbTelefono.Text = DatosCorreccionActuales.CTelefono;
        txbCorreoElectronico.Text = DatosCorreccionActuales.CCorreo;
        txtGenero.Text =  NombreGenero(DatosCorreccionActuales.NGenero);
        //txtEtnia.Text = DatosCorreccionActuales.NEtnia.ToString();
        txtEtnia.Text = NombreEtnia(DatosCorreccionActuales.NEtnia);
        if (DatosCorreccionActuales.NSubetnia == null)
        {
            int subet = 0;
            txtSubEtnia.Text = NombreSubEtnia(subet);
        }
        txtSubEtnia.Text = NombreEtnia(DatosCorreccionActuales.NSubetnia);
        ChkBoxListDiscpacidades.Seleccionados = DatosCorreccionActuales.LstDiscapacidad == null ? new List<int>() : DatosCorreccionActuales.LstDiscapacidad;
    }

    private void CargarDatosNuevos()
    {
        txbPrimerNombre0.Text = DatosCorreccionNuevos.CPrimerNombre;
        txbSegundoNombre0.Text = DatosCorreccionNuevos.CSegundoNombre;
        txbPrimerApellido0.Text = DatosCorreccionNuevos.CPrimerApellido;
        txbSegundoApellido0.Text = DatosCorreccionNuevos.CSegundoApellido;
        txtTipoDocumento0.Text = NombreTipoDocumento(DatosCorreccionNuevos.NTipoDocumento);
        txbNumeroDocumento0.Text = DatosCorreccionNuevos.CNumeroDocumento;
        txtFechaNacimiento0.Text = DatosCorreccionNuevos.DNacimiento.ToShortDateString();
        txbDireccion0.Text = DatosCorreccionNuevos.CDireccion;
        txbTelefono0.Text = DatosCorreccionNuevos.CTelefono;
        txbCorreoElectronico0.Text = DatosCorreccionNuevos.CCorreo;
        txtGenero0.Text = NombreGenero(DatosCorreccionNuevos.NGenero);
        //txtEtnia0.Text = DatosCorreccionNuevos.NEtnia.ToString();
        txtEtnia0.Text = NombreEtnia(DatosCorreccionNuevos.NEtnia);
        txtSubEtnia0.Text = NombreSubEtnia(DatosCorreccionNuevos.NSubetnia);
        ChkBoxListDiscpacidades0.Seleccionados = DatosCorreccionNuevos.LstDiscapacidad == null ? new List<int>() : DatosCorreccionNuevos.LstDiscapacidad; ;
    }

    private string NombreGenero(int idGenero)
    {
        string strNombreGenero = string.Empty;

        foreach (Ruv.Infrastructure.Crosscutting.Common.eGenero genero in Enum.GetValues(typeof(Ruv.Infrastructure.Crosscutting.Common.eGenero)))
        {
            if ((int)genero == idGenero)
                strNombreGenero = genero.ToString();
        }

        return strNombreGenero;
    }

    private string NombreEtnia(int idEtnia)
    {
        string strNombreEtnia = string.Empty;

        clsParametroGeneral parametroGeneral = RUV.Current.ListadosGeneralesValoracion.Parametros.FirstOrDefault(x => x.Id == idEtnia);

        if (parametroGeneral != null)
            strNombreEtnia = parametroGeneral.Nombre;

        return strNombreEtnia;
    }

    private string NombreSubEtnia(int idSubEtnia)
    {
        string cError = string.Empty;
        string strNombreSubEtnia = string.Empty;
        CorreccionesService CorrecServ = new CorreccionesService();
        strNombreSubEtnia = CorrecServ.ObtieneNombreSubEtnia(idSubEtnia, ref cError);
        return strNombreSubEtnia;
    }

    private string NombreTipoDocumento(int idTipoDocumento)
    {
        string strNombreTipoDocumento= string.Empty;

        foreach (Ruv.Infrastructure.Crosscutting.Common.eTipoDocumento tipoDocumento in Enum.GetValues(typeof(Ruv.Infrastructure.Crosscutting.Common.eTipoDocumento)))
        {
            if ((int)tipoDocumento == idTipoDocumento)
                strNombreTipoDocumento = tipoDocumento.ToString();
        }

        return strNombreTipoDocumento;
    }

    private void ShowMessage(string sTitle, string sMessage, Resultado resultado, bool bMostrarCancelar = true)
    {
        //Master.PopUpGeneral.Titulo = sTitle;
        //Master.PopUpGeneral.MostrarBotones = true;
        //Master.PopUpGeneral.VisibleBotonCancelar = bMostrarCancelar;
        //Master.PopUpGeneral.MostrarImagen = false;
        //Master.PopUpGeneral.Mensaje = sMessage;
        //if (resultado == Resultado.exitoso)
        //    Master.PopUpGeneral.Mostrar("~/Default.aspx");
        //else
        //    Master.PopUpGeneral.Mostrar();
        if (resultado == Resultado.exitoso)
            ModalPopUp.MostrarMensajeYRedirigir(sTitle, sMessage, "~/Default.aspx");
        else
            ModalPopUp.MostrarMensaje(sTitle, sMessage);
    }

    private bool ExisteArchivoAdjunto()
    {
        string ruta = System.Configuration.ConfigurationManager.AppSettings["PathArchivosCorrecciones"];
        string nombreArchivo = IdCorreccion.ToString();
        string extension = string.Empty;

        string[] archivosEnDirectorio = Directory.GetFiles(ruta, nombreArchivo + ".*");

        if (archivosEnDirectorio.Count() > 0)
            return true;
        else
            return false;
    }

    #endregion

}