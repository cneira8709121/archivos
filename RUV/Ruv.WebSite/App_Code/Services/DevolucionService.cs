using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using Ruv.Business.Devolucion.Contratos;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Devolucion;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using util = Ruv.Infrastructure.Crosscutting.Utilities;
using dto = Ruv.Business.DTO.Devolucion;
using Ruv.Infrastructure.Crosscutting.Common.General;

[AspNetCompatibilityRequirements(RequirementsMode
    = AspNetCompatibilityRequirementsMode.Required)]

public class DevolucionService : IDevolucionService
{
    #region Public methods

    #region Services implementation

    public bool SolicitarDevolucion(clsDevolucion dev, ref string cError)
    {
        IDevolucion iDevolucion = (IDevolucion)util::Spring.GetService(Objetos.DevolucionBusiness);
        return iDevolucion.SolicitarDevolucion(new dto::clsDevolucion
            {
                NIdDeclaracion = dev.NIdDeclaracion.Value,
                //NIdRadicacion = dev.NIdRadicacion.Value,
                NIdUsuario = dev.NIdUsuario.Value,
                NIdEntidadMunicipio = dev.NIdEntidadMunicipio,
                IdsCausales = dev.LstCausalesDevolucion,
                CObservaciones = dev.CObservaciones
            }, ref cError);
    }

    public clsDevolucion ObtenerDevolucion(int nIdDeclaracion, ref string cError)
    {
        IDevolucion iDevolucion = (IDevolucion)util::Spring.GetService(Objetos.DevolucionBusiness);
        dto::clsDevolucion dev = iDevolucion.ObtenerDevolucion(nIdDeclaracion, ref cError);
        if (dev == null || !string.IsNullOrEmpty(cError)) return null;
        return new clsDevolucion
        {
            NId = dev.NId,
            NIdEntidadMunicipio = dev.NIdEntidadMunicipio,
            CDeclarante = string.Format("{0} {1} {2} {3}", new string[] { dev.CPrimerNombreDeclarante, dev.CSegundoNombreDeclarante, dev.CPrimerApellidoDeclarante, dev.CSegundoApellidoDeclarante }),
            CDireccion = dev.CDireccion,
            NTelefono = dev.NTelefono,
            CFuncionario = dev.CFuncionario,
            CNumeroGuia = dev.CNumeroGuia,
            CNumeroFud = dev.CNumeroFormulario,
            CPais = dev.CPais,
            CDepartamento = dev.CDepartamento,
            CMunicipio = dev.CMunicipio,
            CEntidad = dev.CEntidad,
            LstCausalesDevolucion = (List<int>)dev.IdsCausales,
            DRadicacion = dev.DFechaRadicacion,
            DSolicitudDevolucion = dev.DFechaSolicitud
        };
    }

    public bool ActualizarDevolucion(clsDevolucion dev, ref string cError)
    {
        IDevolucion iDevolucion = (IDevolucion)util::Spring.GetService(Objetos.DevolucionBusiness);
        return iDevolucion.ActualizarDevolucion(new dto::clsDevolucion
            {
                NId = dev.NId.Value,
                NIdUsuario = dev.NIdUsuario.Value,
                CNumeroGuia = dev.CNumeroGuia,
                CDireccion = dev.CDireccion,
                NTelefono = dev.NTelefono,
                CFuncionario = dev.CFuncionario,
                CParteEmotiva = dev.CParteEmotivaModificada
            }, ref cError);
    }

    public byte[] GenerarDocumentoDevolucion(int nIdDevolucion, ref string cError)
    { 
        IDevolucion iDevolucion = (IDevolucion)util::Spring.GetService(Objetos.DevolucionBusiness);
        dto::clsDatosparaDevolucion DatosDevolucion = iDevolucion.CargaDatosparaDevolucion(nIdDevolucion, ref cError);
        if(!string.IsNullOrEmpty(cError)) return null;

        PdfHelperService serv = new PdfHelperService();
        byte[] pdf = serv.GeneratePdfDevolucion(DatosDevolucion, ref cError);
        if (pdf == null || !string.IsNullOrEmpty(cError)) return null;
        return pdf;
    }

    public List<clsCausal> ObtenerCausalesDevolucion(ref string cError)
    {
        IDevolucion iDevolucion = (IDevolucion)util::Spring.GetService(Objetos.DevolucionBusiness);
        List<clsCausal> Causales = iDevolucion.ObtenerCausalesDevolucion(ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
        }
        return Causales;
    }

    #endregion

    #endregion
}