using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Business.DTO.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;

/// <summary>
/// Summary description for DatasourceResumenValoracion
/// </summary>
public class DatasourceResumenValoracion2
{
    public DatasourceResumenValoracion2()
    {
        cError = string.Empty;
        ServicioResumenValoracion = new ResumenValoracoinService();
    }


    ResumenValoracoinService ServicioResumenValoracion;
    string cError;
    public event Error ErrorConsulta;
    public int NIdDeclaracion { get; set; }
 


    void OnError(object sender, ErrorEventArgs e)
    {
        if (ErrorConsulta != null)
        {
            ErrorConsulta(sender, e);
        }
    }



    public IList<object> GetData()
    {

        List<clsResumenValoracion> lstResumenValoracion = ServicioResumenValoracion.ObtenerResumenValoracion(NIdDeclaracion, ref cError);
        if (!string.IsNullOrWhiteSpace(cError))
        {
            OnError(null, new ErrorEventArgs(cError));
        }

        IList<object> result = new List<object>();
        if (lstResumenValoracion == null)
            return result;


        return lstResumenValoracion.Cast<object>().ToList();
    }

    public clsResumenValoracion GetDatadw
    {
        get
        {
            List<clsResumenValoracion> lstResumenValoracion = ServicioResumenValoracion.ObtenerResumenValoracion(NIdDeclaracion, ref cError);
            if (!string.IsNullOrWhiteSpace(cError))
            {
                OnError(null, new ErrorEventArgs(cError));
            }

            IList<object> result = new List<object>();
            if (lstResumenValoracion == null)
                return null;

            return  lstResumenValoracion.First();
        }
    }



}