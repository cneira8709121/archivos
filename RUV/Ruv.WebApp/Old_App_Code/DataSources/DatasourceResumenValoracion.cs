using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Business.DTO.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;

/// <summary>
/// Summary description for DatasourceResumenValoracion
/// </summary>
public class DatasourceResumenValoracion {

    private ResumenValoracoinService ServicioResumenValoracion;
    private string cError;

    public int NIdDeclaracion { get; set; }

    public DatasourceResumenValoracion() {
        cError = string.Empty;
        ServicioResumenValoracion = new ResumenValoracoinService();
    }

    public IList<object> GetData() {

        List<clsResumenValoracion> lstResumenValoracion = ServicioResumenValoracion.ObtenerResumenValoracion(NIdDeclaracion, ref cError);
        if (!string.IsNullOrWhiteSpace(cError)) {
            OnError(null, new ErrorEventArgs(cError));
            return null;
        }
        
        if (lstResumenValoracion == null)
            return new List<object>();

        return lstResumenValoracion.Cast<object>().ToList();
    }

    #region Error Event

    public event Ruv.Infrastructure.Crosscutting.Common.Error ErrorConsulta;

    void OnError(object sender, ErrorEventArgs e)
    {
        if (ErrorConsulta != null)
        {
            ErrorConsulta(sender, e);
        }
    }

    #endregion

}